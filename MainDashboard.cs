// ============================================================
// MainDashboard.cs - Form chính của ứng dụng
// Hiển thị tất cả máy tính, header doanh thu, timer live
//
// CÁCH TẠO TRONG VISUAL STUDIO:
//   Project → Add → New Item → Windows Form
//   Đặt tên: MainDashboard.cs
//   XÓA hết code trong .cs và .Designer.cs, paste code này vào
// ============================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public class MainDashboard : Form
    {
        // ========================
        // CONTROLS
        // ========================
        private Panel pnlHeader;
        private Label lblTitle, lblRevenue, lblOnlineInfo, lblDate;
        private FlowLayoutPanel flowPCs;       // Grid chứa các UCPC
        private Panel pnlFooter;
        private Label lblStatus;
        private Button btnManageServices, btnReport, btnRefresh;
        private System.Windows.Forms.Timer timerLive; // Timer cập nhật mỗi giây

        // Danh sách tất cả UCPC controls trên màn hình
        private List<UCPC> pcControls = new List<UCPC>();

        // DB helper (Singleton)
        private DatabaseHelper db = DatabaseHelper.Instance;

        // ========================
        // CONSTRUCTOR
        // ========================
        public MainDashboard()
        {
            this.Text = "Net Cafe";
            this.Size = new Size(1024, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(800, 600);
            this.Font = new Font("Segoe UI", 9);

            // Icon (nếu có file icon)
            // this.Icon = new Icon("Resources/icon.ico");

            BuildUI();       // Tạo giao diện
            LoadPCs();       // Load máy từ DB
            StartTimer();    // Bắt đầu đếm giây
        }

        // ============================================================
        // XÂY DỰNG GIAO DIỆN
        // ============================================================
        private void BuildUI()
        {
            // ---- HEADER ----
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(18, 40, 76), // Xanh đêm
                Padding = new Padding(15, 0, 15, 0)
            };

            lblTitle = new Label
            {
                Text = "☕ NET CAFE",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(15, 12),
                AutoSize = true
            };

            lblRevenue = new Label
            {
                Text = "Doanh thu hôm nay: 0 VNĐ",
                ForeColor = Color.FromArgb(255, 200, 0),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(15, 48),
                AutoSize = true
            };

            lblOnlineInfo = new Label
            {
                Text = "Máy đang dùng: 0/0",
                ForeColor = Color.FromArgb(150, 220, 150),
                Font = new Font("Segoe UI", 10),
                Location = new Point(350, 48),
                AutoSize = true
            };

            lblDate = new Label
            {
                Text = DateTime.Now.ToString("dddd, dd/MM/yyyy"),
                ForeColor = Color.FromArgb(180, 200, 230),
                Font = new Font("Segoe UI", 9),
                AutoSize = true
            };
            lblDate.Location = new Point(this.ClientSize.Width - lblDate.PreferredWidth - 120, 14);
            this.Resize += (s, e) => {
                lblDate.Location = new Point(this.ClientSize.Width - 260, 14);
            };

            // Nút bên phải header
            btnManageServices = CreateHeaderButton("Quản lý Dịch Vụ", 120, 8);
            btnManageServices.Location = new Point(820, 15);
            btnManageServices.Click += BtnManageServices_Click;

            btnReport = CreateHeaderButton("📊 Báo Cáo", 100, 8);
            btnReport.Location = new Point(820, 45);
            btnReport.Click += BtnReport_Click;

            pnlHeader.Controls.AddRange(new Control[]
            { lblTitle, lblRevenue, lblOnlineInfo, lblDate, btnManageServices, btnReport });

            // ---- MAIN AREA (FlowLayoutPanel chứa các UCPC) ----
            // Thêm nút chuyển màn hình khách, đặt ngay trước pnlHeader.Controls.AddRange
            var btnCustomer = new Button
            {
                Text = "👤",
                Size = new Size(46, 80),
                Dock = DockStyle.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 140, 180),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16),
                Cursor = Cursors.Hand
            };
            btnCustomer.FlatAppearance.BorderSize = 0;
            btnCustomer.Click += (s, e) =>
            {
                Computer activePc = null;
                foreach (var u in pcControls)
                    if (u.PC.Status == PCStatus.InUse) { activePc = u.PC; break; }

                if (activePc == null)
                {
                    MessageBox.Show("Chưa có máy nào đang hoạt động.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                new CustomerLoginForm(activePc).Show();
            };
            pnlHeader.Controls.Add(btnCustomer); // thêm trước AddRange

            var pnlMain = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };

            var lblSection = new Label
            {
                Text = "DANH SÁCH MÁY TÍNH",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 100),
                Dock = DockStyle.Top,
                Height = 28,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(5, 0, 0, 0)
            };

            flowPCs = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(5)
            };

            pnlMain.Controls.Add(flowPCs);
            pnlMain.Controls.Add(lblSection);

            // ---- FOOTER ----
            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 36,
                BackColor = Color.FromArgb(230, 232, 238)
            };

            lblStatus = new Label
            {
                Text = "✔ Sẵn sàng",
                ForeColor = Color.FromArgb(60, 100, 60),
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, 10),
                AutoSize = true
            };

            btnRefresh = new Button
            {
                Text = "🔄 Tải lại",
                Location = new Point(900, 6),
                Size = new Size(90, 26),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(200, 210, 230),
                Font = new Font("Segoe UI", 8),
                Cursor = Cursors.Hand
            };
            btnRefresh.Click += (s, e) =>
            {
                SimulatedClock.AdvanceDay();
                SetStatus($"⏭ Chuyển sang ngày {SimulatedClock.Now:dd/MM/yyyy}");
                LoadPCs();
            };

            pnlFooter.Controls.AddRange(new Control[] { lblStatus, btnRefresh });

            // Thêm vào Form
            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlFooter);
        }

        private Button CreateHeaderButton(string text, int width, int height)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(width, 26),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(50, 80, 130),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 8),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = Color.FromArgb(80, 110, 170);
            return btn;
        }

        // ============================================================
        // LOAD MACHINES FROM DATABASE
        // ============================================================
        private void LoadPCs()
        {
            try
            {
                flowPCs.Controls.Clear();
                pcControls.Clear();

                DataTable dt = db.GetAllComputers();

                foreach (DataRow row in dt.Rows)
                {
                    // Tạo đúng loại Computer dựa vào IsVip
                    bool isVip = Convert.ToBoolean(row["IsVip"]);
                    Computer pc = isVip ? new VipComputer() : new Computer();

                    pc.ID = Convert.ToInt32(row["PCID"]);
                    pc.Name = row["PCName"].ToString();
                    pc.HourlyRate = Convert.ToDecimal(row["HourlyRate"]);
                    pc.Notes = dt.Columns.Contains("Notes") && row["Notes"] != DBNull.Value ? row["Notes"].ToString() : "";

                    // Nếu đang dùng → lấy StartTime từ DB
                    int status = Convert.ToInt32(row["Status"]);
                    if (status == 1 && row["StartTime"] != DBNull.Value)
                    {
                        pc.Status = PCStatus.InUse;
                        pc.StartTime = Convert.ToDateTime(row["StartTime"]);

                        // Load orders chưa thanh toán của máy này
                        DataTable orders = db.GetUnpaidOrders(pc.ID);
                        foreach (DataRow oRow in orders.Rows)
                        {
                            pc.Orders.Add(new OrderItem
                            {
                                ItemID = Convert.ToInt32(oRow["ItemID"]),
                                ServiceID = Convert.ToInt32(oRow["ServiceID"]),
                                ServiceName = oRow["ServiceName"].ToString(),
                                UnitPrice = Convert.ToDecimal(oRow["UnitPrice"]),
                                Quantity = Convert.ToInt32(oRow["Quantity"])
                            });
                        }
                    }


                    // Tạo UserControl và đăng ký events
                    var ucpc = new UCPC(pc);
                    ucpc.OnStartSession += HandleStartSession;
                    ucpc.OnCheckout += HandleCheckout;
                    ucpc.OnAddService += HandleAddService;

                    pcControls.Add(ucpc);
                    flowPCs.Controls.Add(ucpc);
                }

                UpdateHeader();
                lblDate.Text = SimulatedClock.DisplayDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khi tải dữ liệu máy:\n" + ex.Message,
                    "Lỗi Database",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // TIMER: Cập nhật hiển thị mỗi giây
        // ============================================================
        private void StartTimer()
        {
            timerLive = new System.Windows.Forms.Timer();
            timerLive.Interval = 1000; // 1000ms = 1 giây
            timerLive.Tick += (s, e) =>
            {
                // Cập nhật từng UCPC (thời gian + tiền)
                foreach (var ucpc in pcControls)
                    ucpc.UpdateDisplay();

                // Cập nhật thông tin header
                UpdateHeader();
            };
            timerLive.Start();
        }

        // ============================================================
        // XỬ LÝ SỰ KIỆN: MỞ MÁY
        // ============================================================
        private void HandleStartSession(Computer pc)
        {
            var confirm = MessageBox.Show(
                $"Mở máy {pc.Name}?\nGiá: {pc.HourlyRate:N0} VNĐ/giờ",
                "Xác nhận mở máy",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                // Cập nhật DB: Status = 1, StartTime = bây giờ
                db.ExecuteNonQuery(
                    "UPDATE Computers SET Status = 1, StartTime = @now WHERE PCID = @id",
                    new[]
                    {
                        new SqlParameter("@now", DateTime.Now),
                        new SqlParameter("@id",  pc.ID)
                    });

                // Cập nhật object trong bộ nhớ
                pc.StartSession();

                // Tìm UCPC tương ứng và cập nhật hiển thị
                var ucpc = pcControls.Find(u => u.PC.ID == pc.ID);
                ucpc?.UpdateDisplay();

                UpdateHeader();
                SetStatus($"✔ Đã mở máy {pc.Name}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở máy: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // XỬ LÝ SỰ KIỆN: TÍNH TIỀN / CHECKOUT
        // ============================================================
        private void HandleCheckout(Computer pc)
        {
            // Mở form thanh toán
            var form = new CheckoutForm(pc);
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Sau khi checkout thành công → reset máy trong DB
                try
                {
                    // Đánh dấu tất cả orders là đã thanh toán
                    db.ExecuteNonQuery(
                        "UPDATE OrderItems SET IsPaid = 1 WHERE PCID = @id AND IsPaid = 0",
                        new[] { new SqlParameter("@id", pc.ID) });

                    // Reset máy về trạng thái trống
                    db.ExecuteNonQuery(
                        "UPDATE Computers SET Status = 0, StartTime = NULL WHERE PCID = @id",
                        new[] { new SqlParameter("@id", pc.ID) });

                    // Cập nhật object trong bộ nhớ
                    pc.EndSession();

                    // Cập nhật UCPC
                    var ucpc = pcControls.Find(u => u.PC.ID == pc.ID);
                    ucpc?.UpdateDisplay();

                    UpdateHeader();
                    SetStatus($"✔ Đã thanh toán máy {pc.Name}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi checkout: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ============================================================
        // XỬ LÝ SỰ KIỆN: GỌI DỊCH VỤ
        // ============================================================
        private void HandleAddService(Computer pc)
        {
            // Mở ServiceForm ở chế độ "thêm vào máy"
            var form = new ServiceForm(pc);
            form.ShowDialog();

            // Sau khi form đóng → reload orders cho máy đó
            DataTable orders = db.GetUnpaidOrders(pc.ID);
            pc.Orders.Clear();
            foreach (DataRow row in orders.Rows)
            {
                pc.Orders.Add(new OrderItem
                {
                    ItemID = Convert.ToInt32(row["ItemID"]),
                    ServiceID = Convert.ToInt32(row["ServiceID"]),
                    ServiceName = row["ServiceName"].ToString(),
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                    Quantity = Convert.ToInt32(row["Quantity"])
                });
            }

            // Cập nhật hiển thị UCPC
            var ucpc = pcControls.Find(u => u.PC.ID == pc.ID);
            ucpc?.UpdateDisplay();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainDashboard
            // 
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Name = "MainDashboard";
            this.Load += new System.EventHandler(this.MainDashboard_Load);
            this.ResumeLayout(false);

        }

        private void MainDashboard_Load(object sender, EventArgs e)
        {

        }

        // ============================================================
        // QUẢN LÝ DỊCH VỤ (không cần chọn máy)
        // ============================================================
        private void BtnManageServices_Click(object sender, EventArgs e)
        {
            var form = new ServiceForm(null); // null = mở để quản lý
            form.ShowDialog();
        }

        // ============================================================
        // BÁO CÁO ĐƠN GIẢN
        // ============================================================
        private void BtnReport_Click(object sender, EventArgs e)
        {
            new ReportForm().ShowDialog();
        }


        // ============================================================
        // CẬP NHẬT HEADER
        // ============================================================
        private void UpdateHeader()
        {
            int online = 0;
            foreach (var ucpc in pcControls)
                if (ucpc.PC.Status == PCStatus.InUse) online++;

            lblOnlineInfo.Text = $"Máy đang dùng: {online}/{pcControls.Count}";

            try
            {
                decimal revenue = db.GetTodayRevenue();
                lblRevenue.Text = $"Doanh thu hôm nay: {revenue:N0} VNĐ";
            }
            catch { /* Bỏ qua nếu lỗi khi update header */ }
        }

        private void SetStatus(string message)
        {
            lblStatus.Text = message;
        }

        // Dọn dẹp timer khi đóng form
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerLive?.Stop();
            timerLive?.Dispose();
            base.OnFormClosing(e);
        }
    }
}