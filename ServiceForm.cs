// ============================================================
// ServiceForm.cs - Quản lý dịch vụ (CRUD) + Gọi đồ cho máy
//
// 2 chế độ dùng:
//   1. ServiceForm(null)   → Quản lý danh mục dịch vụ (CRUD)
//   2. ServiceForm(pc)     → Gọi đồ ăn cho máy cụ thể
// ============================================================

using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public class ServiceForm : Form
    {
        // null nếu mở để quản lý, khác null nếu mở để gọi đồ
        private Computer _currentPC;
        private bool IsOrderMode => _currentPC != null;

        private DatabaseHelper db = DatabaseHelper.Instance;

        // Controls
        private DataGridView dgvServices;
        private Panel pnlLeft, pnlRight;
        private TextBox txtName, txtPrice, txtStock, txtSearch;
        private NumericUpDown nudQuantity;
        private Button btnAdd, btnEdit, btnDelete, btnSave, btnCancel;
        private Button btnAddToPC, btnClose;
        private Label lblMode, lblError;

        public ServiceForm(Computer pc)
        {
            _currentPC = pc;
            InitializeComponents();
            LoadServices();
        }

        private void InitializeComponents()
        {
            string title = IsOrderMode
                ? $"Gọi dịch vụ cho máy: {_currentPC.Name}"
                : "Quản lý Dịch Vụ";

            this.Text = title;
            this.Size = new Size(820, 580);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.Font = new Font("Segoe UI", 9);

            // ---- HEADER ----
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = IsOrderMode ? Color.FromArgb(40, 120, 60) : Color.FromArgb(18, 40, 76)
            };

            lblMode = new Label
            {
                Text = IsOrderMode ? $"🍜  GỌI ĐỒ ĂN - {_currentPC.Name}" : "🛒  QUẢN LÝ DỊCH VỤ",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblMode);

            // ---- SEARCH BAR ----
            var pnlSearch = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.White,
                Padding = new Padding(10, 8, 10, 5)
            };

            var lblSearch = new Label
            {
                Text = "🔍 Tìm kiếm:",
                Location = new Point(10, 12),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };

            txtSearch = new TextBox
            {
                Location = new Point(85, 9),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 9)
            };
            txtSearch.TextChanged += (s, e) => FilterServices(txtSearch.Text);

            pnlSearch.Controls.AddRange(new Control[] { lblSearch, txtSearch });

            // ---- LEFT: DataGridView ----
            pnlLeft = new Panel
            {
                Dock = DockStyle.Left,
                Width = 500,
                Padding = new Padding(10)
            };

            dgvServices = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 9),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvServices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 70, 110);
            dgvServices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvServices.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dgvServices.SelectionChanged += DgvServices_SelectionChanged;

            pnlLeft.Controls.Add(dgvServices);

            // ---- RIGHT: Form nhập liệu hoặc form gọi đồ ----
            pnlRight = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.White
            };

            BuildRightPanel();

            // ---- Thêm vào Form ----
            this.Controls.Add(pnlRight);
            this.Controls.Add(pnlLeft);
            this.Controls.Add(pnlSearch);
            this.Controls.Add(pnlHeader);
        }

        // ============================================================
        // RIGHT PANEL: tuỳ chế độ
        // ============================================================
        private void BuildRightPanel()
        {
            pnlRight.Controls.Clear();

            if (IsOrderMode)
                BuildOrderPanel();  // Gọi đồ cho máy
            else
                BuildCRUDPanel();   // Quản lý danh mục
        }

        // Panel gọi đồ
        private void BuildOrderPanel()
        {
            int y = 10;

            MakeLabel("Tên dịch vụ:", y); y += 25;
            txtName = MakeTextbox(y, true); y += 45; // Readonly khi chọn từ grid

            MakeLabel("Đơn giá (VNĐ):", y); y += 25;
            txtPrice = MakeTextbox(y, true); y += 45;

            MakeLabel("Tồn kho:", y); y += 25;
            txtStock = MakeTextbox(y, true); y += 45;

            MakeLabel("Số lượng:", y); y += 25;
            nudQuantity = new NumericUpDown
            {
                Location = new Point(10, y),
                Size = new Size(120, 28),
                Minimum = 1,
                Maximum = 99,
                Value = 1,
                Font = new Font("Segoe UI", 10)
            };
            pnlRight.Controls.Add(nudQuantity);
            y += 50;

            lblError = new Label
            {
                Location = new Point(10, y),
                Size = new Size(270, 40),
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 8)
            };
            pnlRight.Controls.Add(lblError);
            y += 50;

            btnAddToPC = new Button
            {
                Text = "➕  Thêm vào máy " + _currentPC.Name,
                Location = new Point(10, y),
                Size = new Size(260, 40),
                BackColor = Color.FromArgb(40, 120, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAddToPC.FlatAppearance.BorderSize = 0;
            btnAddToPC.Click += BtnAddToPC_Click;
            pnlRight.Controls.Add(btnAddToPC);
            y += 55;

            btnClose = new Button
            {
                Text = "✕  Đóng",
                Location = new Point(10, y),
                Size = new Size(260, 35),
                BackColor = Color.FromArgb(150, 150, 160),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.OK
            };
            btnClose.FlatAppearance.BorderSize = 0;
            pnlRight.Controls.Add(btnClose);
        }

        // Panel CRUD
        private void BuildCRUDPanel()
        {
            int y = 10;

            MakeLabel("Tên dịch vụ: *", y); y += 25;
            txtName = MakeTextbox(y, false); y += 42;

            MakeLabel("Đơn giá (VNĐ): *", y); y += 25;
            txtPrice = MakeTextbox(y, false); y += 42;

            MakeLabel("Số lượng trong kho:", y); y += 25;
            txtStock = MakeTextbox(y, false); y += 42;

            lblError = new Label
            {
                Location = new Point(10, y),
                Size = new Size(270, 35),
                ForeColor = Color.Red,
                Font = new Font("Segoe UI", 8)
            };
            pnlRight.Controls.Add(lblError);
            y += 40;

            btnSave = new Button
            {
                Text = "💾  Lưu",
                Location = new Point(10, y),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(0, 120, 212),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "✕ Huỷ",
                Location = new Point(140, y),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(200, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => { ClearForm(); dgvServices.ClearSelection(); };

            pnlRight.Controls.AddRange(new Control[] { btnSave, btnCancel });
            y += 50;

            // CRUD buttons
            btnAdd = MakeActionButton("➕ Thêm mới", y, Color.FromArgb(50, 150, 50));
            btnAdd.Click += BtnAdd_Click;
            y += 45;

            btnEdit = MakeActionButton("✏️ Sửa", y, Color.FromArgb(180, 120, 0));
            btnEdit.Click += BtnEdit_Click;
            y += 45;

            btnDelete = MakeActionButton("🗑️ Xoá", y, Color.FromArgb(200, 50, 50));
            btnDelete.Click += BtnDelete_Click;

            pnlRight.Controls.AddRange(new Control[] { btnAdd, btnEdit, btnDelete });
        }

        // Helpers tạo control
        private Label MakeLabel(string text, int y)
        {
            var lbl = new Label { Text = text, Location = new Point(10, y), AutoSize = true, Font = new Font("Segoe UI", 9) };
            pnlRight.Controls.Add(lbl);
            return lbl;
        }

        private TextBox MakeTextbox(int y, bool readOnly)
        {
            var txt = new TextBox
            {
                Location = new Point(10, y),
                Size = new Size(260, 28),
                Font = new Font("Segoe UI", 9),
                ReadOnly = readOnly,
                BackColor = readOnly ? Color.FromArgb(240, 240, 240) : Color.White
            };
            pnlRight.Controls.Add(txt);
            return txt;
        }

        private Button MakeActionButton(string text, int y, Color color)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(10, y),
                Size = new Size(260, 35),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        // ============================================================
        // LOAD SERVICES VÀO DATAGRIDVIEW
        // ============================================================
        private void LoadServices(string search = "")
        {
            try
            {
                string sql = @"
                    SELECT ServiceID, ServiceName AS [Tên dịch vụ], 
                           Price AS [Đơn giá], Stock AS [Tồn kho]
                    FROM Services
                    WHERE IsActive = 1
                    AND (@search = '' OR ServiceName LIKE '%' + @search + '%')
                    ORDER BY ServiceName";

                DataTable dt = db.ExecuteQuery(sql, new[]
                {
                    new SqlParameter("@search", search)
                });

                dgvServices.DataSource = dt;

                // Định dạng cột tiền
                if (dgvServices.Columns.Contains("Đơn giá"))
                    dgvServices.Columns["Đơn giá"].DefaultCellStyle.Format = "N0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dịch vụ: " + ex.Message);
            }
        }

        private void FilterServices(string keyword)
        {
            LoadServices(keyword);
        }

        // ============================================================
        // CHỌN DÒNG TRONG GRID → Điền vào form
        // ============================================================
        private void DgvServices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count == 0) return;

            DataGridViewRow row = dgvServices.SelectedRows[0];
            if (row.IsNewRow) return;

            txtName.Text = row.Cells["Tên dịch vụ"].Value?.ToString() ?? "";
            txtPrice.Text = row.Cells["Đơn giá"].Value?.ToString() ?? "";
            txtStock.Text = row.Cells["Tồn kho"].Value?.ToString() ?? "";
            lblError.Text = "";
        }

        // ============================================================
        // ORDER MODE: THÊM VÀO MÁY
        // ============================================================
        private void BtnAddToPC_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count == 0)
            {
                lblError.Text = "Vui lòng chọn dịch vụ từ danh sách!";
                return;
            }

            DataGridViewRow row = dgvServices.SelectedRows[0];
            int serviceId = Convert.ToInt32(row.Cells["ServiceID"].Value);
            int stock = Convert.ToInt32(row.Cells["Tồn kho"].Value);
            int qty = (int)nudQuantity.Value;
            decimal unitPrice = Convert.ToDecimal(row.Cells["Đơn giá"].Value);

            if (stock < qty)
            {
                lblError.Text = $"Không đủ hàng! Tồn kho: {stock}";
                return;
            }

            try
            {
                // INSERT vào OrderItems
                db.ExecuteNonQuery(
                    "INSERT INTO OrderItems (PCID, ServiceID, Quantity, UnitPrice) VALUES (@pcId, @sid, @qty, @price)",
                    new[]
                    {
                        new SqlParameter("@pcId",  _currentPC.ID),
                        new SqlParameter("@sid",   serviceId),
                        new SqlParameter("@qty",   qty),
                        new SqlParameter("@price", unitPrice)
                    });

                // Giảm kho
                db.ExecuteNonQuery(
                    "UPDATE Services SET Stock = Stock - @qty WHERE ServiceID = @sid",
                    new[]
                    {
                        new SqlParameter("@qty", qty),
                        new SqlParameter("@sid", serviceId)
                    });

                string serviceName = txtName.Text;
                decimal total = unitPrice * qty;

                MessageBox.Show(
                    $"✔ Đã thêm: {serviceName} x{qty} = {total:N0} VNĐ\nvào máy {_currentPC.Name}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                nudQuantity.Value = 1;
                lblError.Text = "";
                LoadServices(); // Reload để thấy kho giảm
            }
            catch (Exception ex)
            {
                lblError.Text = "Lỗi: " + ex.Message;
            }
        }

        // ============================================================
        // CRUD MODE
        // ============================================================

        private int _editingId = -1; // -1 = đang thêm mới

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            _editingId = -1;
            txtName.Focus();
            lblError.Text = "Nhập thông tin dịch vụ mới rồi bấm Lưu";
            lblError.ForeColor = Color.FromArgb(0, 80, 180);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count == 0)
            {
                lblError.Text = "Chọn dịch vụ cần sửa!";
                return;
            }
            _editingId = Convert.ToInt32(dgvServices.SelectedRows[0].Cells["ServiceID"].Value);
            lblError.Text = "Sửa thông tin rồi bấm Lưu";
            lblError.ForeColor = Color.FromArgb(0, 80, 180);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtName.Text)) { lblError.Text = "Vui lòng nhập tên dịch vụ!"; return; }
            if (string.IsNullOrWhiteSpace(txtPrice.Text)) { lblError.Text = "Vui lòng nhập đơn giá!"; return; }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                lblError.Text = "Đơn giá không hợp lệ!";
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
                stock = 0;

            try
            {
                if (_editingId == -1)
                {
                    // INSERT
                    db.ExecuteNonQuery(
                        "INSERT INTO Services (ServiceName, Price, Stock) VALUES (@name, @price, @stock)",
                        new[]
                        {
                            new SqlParameter("@name",  txtName.Text.Trim()),
                            new SqlParameter("@price", price),
                            new SqlParameter("@stock", stock)
                        });
                    MessageBox.Show("✔ Đã thêm dịch vụ mới!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // UPDATE
                    db.ExecuteNonQuery(
                        "UPDATE Services SET ServiceName=@name, Price=@price, Stock=@stock WHERE ServiceID=@id",
                        new[]
                        {
                            new SqlParameter("@name",  txtName.Text.Trim()),
                            new SqlParameter("@price", price),
                            new SqlParameter("@stock", stock),
                            new SqlParameter("@id",    _editingId)
                        });
                    MessageBox.Show("✔ Đã cập nhật dịch vụ!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                ClearForm();
                LoadServices();
            }
            catch (Exception ex)
            {
                lblError.Text = "Lỗi: " + ex.Message;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count == 0) { lblError.Text = "Chọn dịch vụ cần xoá!"; return; }

            int id = Convert.ToInt32(dgvServices.SelectedRows[0].Cells["ServiceID"].Value);
            string nm = dgvServices.SelectedRows[0].Cells["Tên dịch vụ"].Value.ToString();

            var confirm = MessageBox.Show(
                $"Xoá dịch vụ '{nm}'?\n(Không thể hoàn tác)",
                "Xác nhận xoá",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                // Soft delete (ẩn thay vì xoá thật, bảo toàn lịch sử)
                db.ExecuteNonQuery(
                    "UPDATE Services SET IsActive = 0 WHERE ServiceID = @id",
                    new[] { new SqlParameter("@id", id) });

                MessageBox.Show("✔ Đã xoá dịch vụ!", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadServices();
            }
            catch (Exception ex)
            {
                lblError.Text = "Lỗi: " + ex.Message;
            }
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtPrice.Text = "";
            txtStock.Text = "";
            lblError.Text = "";
            lblError.ForeColor = Color.Red;
            _editingId = -1;
        }
    }
}