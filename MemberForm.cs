using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public class MemberForm : Form
    {
        private DatabaseHelper db = DatabaseHelper.Instance;

        // Left panel
        private TextBox txtSearch;
        private DataGridView dgvMembers;

        // Right — profile card
        private Label lblName, lblPhone, lblBalance, lblPoints;

        // Right — content panels
        private Panel pnlContent, pnlInfo, pnlTopUp, pnlHistory;

        // Info panel
        private TextBox txtEditName, txtEditPhone;
        private bool _isAdding = false;

        // TopUp panel
        private Label lblCurrentBalance;
        private NumericUpDown nudAmount;

        // History panel
        private DataGridView dgvHistory;

        // State
        private Member _selected = null;

        public MemberForm()
        {
            InitializeComponents();
            LoadMembers();
        }

        // ============================================================
        // BUILD UI
        // ============================================================
        private void InitializeComponents()
        {
            this.Text = "Quản lý Hội Viên";
            this.Size = new Size(840, 580);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.MinimizeBox = false;

            // Header
            var pnlHeader = new Panel
            { Dock = DockStyle.Top, Height = 52, BackColor = Color.FromArgb(18, 40, 76) };
            new Label
            {
                Text = "👤  QUẢN LÝ HỘI VIÊN",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Parent = pnlHeader
            };

            var pnlLeft = new Panel
            {
                Width = 310,
                Dock = DockStyle.Left,
                BackColor = Color.White,
                Padding = new Padding(0, 0, 1, 0)
            };

            var pnlRight = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(248, 249, 252)
            };


            BuildLeftPanel(pnlLeft);
            BuildRightPanel(pnlRight);


            this.Controls.Add(pnlRight);
            this.Controls.Add(pnlLeft);
            this.Controls.Add(pnlHeader);
        }

        // ---- LEFT: danh sách hội viên ----
        private void BuildLeftPanel(Panel p)
        {
            new Label
            {
                Text = "🔍 Tìm kiếm:",
                Location = new Point(8, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                Parent = p
            };

            txtSearch = new TextBox
            {
                Location = new Point(8, 28),
                Size = new Size(278, 26),
                Font = new Font("Segoe UI", 9),
                BorderStyle = BorderStyle.FixedSingle
            };
            txtSearch.TextChanged += (s, e) => LoadMembers(txtSearch.Text);

            dgvMembers = new DataGridView
            {
                Location = new Point(8, 60),
                Size = new Size(278, 390),
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                MultiSelect = false,
                Font = new Font("Segoe UI", 9)
            };
            dgvMembers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(18, 40, 76);
            dgvMembers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMembers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvMembers.SelectionChanged += DgvMembers_SelectionChanged;

            var btnAdd = MakeBtn("➕  Thêm hội viên mới",
                new Point(8, 458), new Size(278, 36), Color.FromArgb(30, 150, 70));
            btnAdd.Click += BtnAdd_Click;

            p.Controls.AddRange(new Control[] { txtSearch, dgvMembers, btnAdd });
        }

        // ---- RIGHT: chi tiết + hành động ----
        private void BuildRightPanel(Panel p)
        {
            // Profile card
            var pnlProfile = new Panel
            {
                Location = new Point(12, 12),
                Size = new Size(490, 88),
                BackColor = Color.FromArgb(18, 40, 76)
            };

            // Avatar
            var pic = new PictureBox { Size = new Size(62, 62), Location = new Point(12, 13) };
            pic.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(new SolidBrush(Color.FromArgb(0, 150, 200)), 0, 0, 60, 60);
                e.Graphics.FillEllipse(Brushes.White, 18, 8, 24, 24);
                e.Graphics.FillEllipse(Brushes.White, 8, 34, 44, 28);
            };

            lblName = new Label
            {
                Text = "--- Chọn hội viên ---",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(84, 12),
                Size = new Size(395, 26)
            };
            lblPhone = new Label
            {
                Text = "",
                ForeColor = Color.FromArgb(180, 210, 255),
                Font = new Font("Segoe UI", 9),
                Location = new Point(84, 42),
                AutoSize = true
            };
            lblBalance = new Label
            {
                Text = "",
                ForeColor = Color.Gold,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(84, 62),
                AutoSize = true
            };
            pnlProfile.Controls.AddRange(new Control[] { pic, lblName, lblPhone, lblBalance });

            // Tab buttons
            var btnInfo = MakeTabBtn("📋 Thông tin", 12);
            var btnTopUp = MakeTabBtn("💰 Nạp tiền", 132);
            var btnHistory = MakeTabBtn("📜 Lịch sử", 252);

            btnInfo.Click += (s, e) => ShowPanel(pnlInfo);
            btnTopUp.Click += (s, e) => ShowPanel(pnlTopUp);
            btnHistory.Click += (s, e) => { ShowPanel(pnlHistory); LoadHistory(); };

            // Content area
            pnlContent = new Panel
            {
                Location = new Point(12, 158),
                Size = new Size(490, 360),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            pnlInfo = BuildInfoPanel();
            pnlTopUp = BuildTopUpPanel();
            pnlHistory = BuildHistoryPanel();

            pnlContent.Controls.AddRange(new Control[] { pnlInfo, pnlTopUp, pnlHistory });

            p.Controls.AddRange(new Control[]
                { pnlProfile, btnInfo, btnTopUp, btnHistory, pnlContent });

            ShowPanel(pnlInfo);
        }

        private Panel BuildInfoPanel()
        {
            var pnl = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            new Label
            {
                Text = "Họ và tên: *",
                Location = new Point(15, 18),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                Parent = pnl
            };
            txtEditName = new TextBox
            {
                Location = new Point(15, 36),
                Size = new Size(290, 28),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            new Label
            {
                Text = "Số điện thoại: *",
                Location = new Point(15, 74),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                Parent = pnl
            };
            txtEditPhone = new TextBox
            {
                Location = new Point(15, 92),
                Size = new Size(290, 28),
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblPoints = new Label
            {
                Text = "",
                Location = new Point(15, 132),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };

            var btnSave = MakeBtn("💾 Lưu",
                new Point(15, 168), new Size(105, 36), Color.FromArgb(0, 120, 212));
            btnSave.Click += BtnSave_Click;

            var btnCancel = MakeBtn("✕ Huỷ",
                new Point(130, 168), new Size(90, 36), Color.FromArgb(120, 120, 120));
            btnCancel.Click += (s, e) => { _isAdding = false; if (_selected != null) RefreshProfile(_selected); };

            var btnDel = MakeBtn("🗑 Xóa",
                new Point(230, 168), new Size(90, 36), Color.FromArgb(180, 30, 30));
            btnDel.Click += BtnDelete_Click;

            pnl.Controls.AddRange(new Control[]
                { txtEditName, txtEditPhone, lblPoints, btnSave, btnCancel, btnDel });
            return pnl;
        }

        private Panel BuildTopUpPanel()
        {
            var pnl = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };

            lblCurrentBalance = new Label
            {
                Text = "Số dư hiện tại: 0 VNĐ",
                Location = new Point(15, 18),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(18, 40, 76)
            };

            new Label
            {
                Text = "Số tiền nạp (VNĐ):",
                Location = new Point(15, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                Parent = pnl
            };

            nudAmount = new NumericUpDown
            {
                Location = new Point(15, 80),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 11),
                Minimum = 10000,
                Maximum = 10000000,
                Increment = 10000,
                Value = 100000,
                ThousandsSeparator = true
            };

            // Nút nhanh
            int[] amounts = { 50000, 100000, 200000, 500000 };
            string[] labels = { "+50k", "+100k", "+200k", "+500k" };
            for (int i = 0; i < 4; i++)
            {
                int amt = amounts[i];
                var q = new Button
                {
                    Text = labels[i],
                    Location = new Point(15 + i * 66, 120),
                    Size = new Size(58, 26),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(230, 240, 255),
                    Font = new Font("Segoe UI", 8),
                    Cursor = Cursors.Hand
                };
                q.FlatAppearance.BorderColor = Color.FromArgb(100, 150, 230);
                q.Click += (s, e) => nudAmount.Value = Math.Min(nudAmount.Maximum, amt);
                pnl.Controls.Add(q);
            }

            var btnNap = MakeBtn("💰  Xác nhận nạp tiền",
                new Point(15, 162), new Size(210, 42), Color.FromArgb(30, 150, 70));
            btnNap.Click += BtnTopUp_Click;

            pnl.Controls.AddRange(new Control[] { lblCurrentBalance, nudAmount, btnNap });
            return pnl;
        }

        private Panel BuildHistoryPanel()
        {
            var pnl = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            dgvHistory = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                Font = new Font("Segoe UI", 9),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 80, 120);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            pnl.Controls.Add(dgvHistory);
            return pnl;
        }

        // ============================================================
        // HELPERS
        // ============================================================
        private Button MakeBtn(string text, Point loc, Size sz, Color bg)
        {
            var btn = new Button
            {
                Text = text,
                Location = loc,
                Size = sz,
                FlatStyle = FlatStyle.Flat,
                BackColor = bg,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private Button MakeTabBtn(string text, int x)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, 112),
                Size = new Size(112, 34),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(18, 40, 76),
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(18, 40, 76);
            return btn;
        }

        private void ShowPanel(Panel target)
        {
            pnlInfo.Visible = target == pnlInfo;
            pnlTopUp.Visible = target == pnlTopUp;
            pnlHistory.Visible = target == pnlHistory;
        }

        // ============================================================
        // DATA
        // ============================================================
        private void LoadMembers(string search = "")
        {
            var dt = string.IsNullOrEmpty(search)
                ? db.GetAllMembers()
                : db.FindMemberByPhone(search);

            var display = new DataTable();
            display.Columns.Add("MemberID", typeof(int));
            display.Columns.Add("Họ tên", typeof(string));
            display.Columns.Add("SĐT", typeof(string));
            display.Columns.Add("Số dư (VNĐ)", typeof(string));

            foreach (DataRow row in dt.Rows)
                display.Rows.Add(
                    row["MemberID"],
                    row["FullName"],
                    row["Phone"],
                    Convert.ToDecimal(row["Balance"]).ToString("N0"));

            dgvMembers.DataSource = display;
            if (dgvMembers.Columns.Contains("MemberID"))
                dgvMembers.Columns["MemberID"].Visible = false;
        }

        private void DgvMembers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMembers.SelectedRows.Count == 0 || _isAdding) return;
            int id = Convert.ToInt32(dgvMembers.SelectedRows[0].Cells["MemberID"].Value);

            var dt = db.ExecuteQuery(
                "SELECT * FROM Members WHERE MemberID = @id",
                new[] { new SqlParameter("@id", id) });

            if (dt.Rows.Count == 0) return;
            var row = dt.Rows[0];

            _selected = new Member
            {
                MemberID = id,
                FullName = row["FullName"].ToString(),
                Phone = row["Phone"].ToString(),
                Balance = Convert.ToDecimal(row["Balance"]),
                Points = Convert.ToInt32(row["Points"])
            };

            RefreshProfile(_selected);
        }

        private void RefreshProfile(Member m)
        {
            lblName.Text = m.FullName;
            lblPhone.Text = "📞 " + m.Phone;
            lblBalance.Text = $"💰 Số dư: {m.Balance:N0} VNĐ  |  ⭐ Điểm: {m.Points}";

            txtEditName.Text = m.FullName;
            txtEditPhone.Text = m.Phone;
            lblPoints.Text = $"Điểm tích lũy: {m.Points}";
            lblCurrentBalance.Text = $"Số dư hiện tại: {m.Balance:N0} VNĐ";
        }

        // ============================================================
        // BUTTON HANDLERS
        // ============================================================
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            _isAdding = true; _selected = null;
            lblName.Text = "Hội viên mới"; lblPhone.Text = ""; lblBalance.Text = "";
            txtEditName.Text = ""; txtEditPhone.Text = ""; lblPoints.Text = "";
            ShowPanel(pnlInfo); txtEditName.Focus();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEditName.Text) ||
                string.IsNullOrWhiteSpace(txtEditPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ họ tên và số điện thoại.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_isAdding)
                {
                    db.ExecuteNonQuery(
                        "INSERT INTO Members (FullName, Phone) VALUES (@n, @p)",
                        new[]
                        {
                            new SqlParameter("@n", txtEditName.Text.Trim()),
                            new SqlParameter("@p", txtEditPhone.Text.Trim())
                        });
                    MessageBox.Show("Đã thêm hội viên mới!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_selected != null)
                {
                    db.ExecuteNonQuery(
                        "UPDATE Members SET FullName=@n, Phone=@p WHERE MemberID=@id",
                        new[]
                        {
                            new SqlParameter("@n",  txtEditName.Text.Trim()),
                            new SqlParameter("@p",  txtEditPhone.Text.Trim()),
                            new SqlParameter("@id", _selected.MemberID)
                        });
                    MessageBox.Show("Đã cập nhật!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                _isAdding = false;
                LoadMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (_selected == null) return;
            if (MessageBox.Show($"Xóa '{_selected.FullName}'?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            db.ExecuteNonQuery("UPDATE Members SET IsActive=0 WHERE MemberID=@id",
                new[] { new SqlParameter("@id", _selected.MemberID) });

            _selected = null;
            lblName.Text = "--- Chọn hội viên ---";
            lblPhone.Text = lblBalance.Text = "";
            LoadMembers();
        }

        private void BtnTopUp_Click(object sender, EventArgs e)
        {
            if (_selected == null)
            {
                MessageBox.Show("Chọn hội viên trước.", "Thông báo"); return;
            }

            decimal amount = nudAmount.Value;

            try
            {
                db.TopUpMember(_selected.MemberID, amount,
                    $"Nạp tiền {SimulatedClock.Now:HH:mm dd/MM/yyyy}");

                _selected.Balance += amount;
                lblCurrentBalance.Text = $"Số dư hiện tại: {_selected.Balance:N0} VNĐ";
                lblBalance.Text = $"💰 Số dư: {_selected.Balance:N0} VNĐ  |  ⭐ Điểm: {_selected.Points}";

                MessageBox.Show(
                    $"Nạp {amount:N0} VNĐ thành công!\nSố dư mới: {_selected.Balance:N0} VNĐ",
                    "Nạp tiền thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp tiền: " + ex.Message);
            }
        }

        private void LoadHistory()
        {
            if (_selected == null) return;

            var dt = db.ExecuteQuery(
                @"SELECT TxTime, TxType, Amount, Note
                  FROM MemberTransactions WHERE MemberID=@id
                  ORDER BY TxTime DESC",
                new[] { new SqlParameter("@id", _selected.MemberID) });

            var display = new DataTable();
            display.Columns.Add("Thời gian", typeof(string));
            display.Columns.Add("Loại", typeof(string));
            display.Columns.Add("Số tiền (VNĐ)", typeof(string));
            display.Columns.Add("Ghi chú", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                decimal amt = Convert.ToDecimal(row["Amount"]);
                display.Rows.Add(
                    Convert.ToDateTime(row["TxTime"]).ToString("HH:mm dd/MM/yyyy"),
                    row["TxType"].ToString(),
                    (amt >= 0 ? "+" : "") + amt.ToString("N0"),
                    row["Note"].ToString());
            }
            dgvHistory.DataSource = display;
        }
    }
}