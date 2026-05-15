// ============================================================
// MainDashboard.cs - Form chính của ứng dụng
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
        private Panel pnlHeader;
        private Label lblTitle, lblRevenue, lblOnlineInfo, lblDate;
        private FlowLayoutPanel flowPCs;
        private Panel pnlFooter;
        private Label lblStatus;
        private Button btnManageServices, btnReport, btnRefresh;
        private System.Windows.Forms.Timer timerLive;

        private List<UCPC> pcControls = new List<UCPC>();
        private DatabaseHelper db = DatabaseHelper.Instance;

        public MainDashboard()
        {
            this.Text = "Net Cafe";
            this.Size = new Size(1024, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.MinimumSize = new Size(800, 600);
            this.Font = new Font("Segoe UI", 9);

            BuildUI();
            LoadPCs();
            StartTimer();
        }

        private void BuildUI()
        {
            // ---- HEADER ----
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.FromArgb(18, 40, 76),
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
                Text = SimulatedClock.DisplayDate,
                ForeColor = Color.FromArgb(180, 200, 230),
                Font = new Font("Segoe UI", 9),
                AutoSize = true
            };
            lblDate.Location = new Point(this.ClientSize.Width - lblDate.PreferredWidth - 120, 14);
            this.Resize += (s, e) =>
            {
                lblDate.Location = new Point(this.ClientSize.Width - 260, 14);
            };

            btnManageServices = CreateHeaderButton("Quản lý Dịch Vụ", 120, 8);
            btnManageServices.Location = new Point(820, 15);
            btnManageServices.Click += BtnManageServices_Click;

            btnReport = CreateHeaderButton("📊 Báo Cáo", 100, 8);
            btnReport.Location = new Point(820, 45);
            btnReport.Click += BtnReport_Click;

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

            pnlHeader.Controls.AddRange(new Control[]
                { lblTitle, lblRevenue, lblOnlineInfo, lblDate,
                  btnManageServices, btnReport, btnCustomer });

            // ---- MAIN AREA ----
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
                // 1. Auto-checkout tất cả máy đang chạy trước khi chuyển ngày
                foreach (var ucpc in pcControls)
                {
                    if (ucpc.PC.Status == PCStatus.InUse && ucpc.PC.StartTime != null)
                    {
                        decimal timeFee = ucpc.PC.CalculateTimeFee();
                        decimal serviceFee = ucpc.PC.GetServiceTotal();
                        decimal total = timeFee + serviceFee;

                        db.ExecuteNonQuery(
                            @"INSERT INTO Transactions
                                (PCID, TimeFee, ServiceFee, TotalAmount, Note, PaymentMethod, CheckoutTime)
                              VALUES
                                (@pcId, @timeFee, @serviceFee, @total, @note, N'Tự động', @checkoutTime)",
                            new[]
                            {
                                new SqlParameter("@pcId",         ucpc.PC.ID),
                                new SqlParameter("@timeFee",      timeFee),
                                new SqlParameter("@serviceFee",   serviceFee),
                                new SqlParameter("@total",        total),
                                new SqlParameter("@checkoutTime", SimulatedClock.Now),
                                new SqlParameter("@note",
                                    $"Auto-checkout {ucpc.PC.Name} cuối ngày {SimulatedClock.Now:dd/MM/yyyy}")
                            });

                        db.ExecuteNonQuery(
                            "UPDATE OrderItems SET IsPaid = 1 WHERE PCID = @id AND IsPaid = 0",
                            new[] { new SqlParameter("@id", ucpc.PC.ID) });

                        db.ExecuteNonQuery(
                            "UPDATE Computers SET Status = 0, StartTime = NULL WHERE PCID = @id",
                            new[] { new SqlParameter("@id", ucpc.PC.ID) });

                        ucpc.PC.EndSession();
                    }
                }

                // 2. Chuyển sang ngày mới
                SimulatedClock.AdvanceDay();
                SetStatus($"⏭ Chuyển sang ngày {SimulatedClock.Now:dd/MM/yyyy} — đã lưu doanh thu hôm qua");
                LoadPCs();
            };  // ← đóng btnRefresh.Click

            pnlFooter.Controls.AddRange(new Control[] { lblStatus, btnRefresh });

            this.Controls.Add(pnlMain);
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlFooter);
        }  // ← đóng BuildUI()

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

        private void LoadPCs()
        {
            try
            {
                flowPCs.Controls.Clear();
                pcControls.Clear();

                DataTable dt = db.GetAllComputers();

                foreach (DataRow row in dt.Rows)
                {
                    bool isVip = Convert.ToBoolean(row["IsVip"]);
                    Computer pc = isVip ? new VipComputer() : new Computer();

                    pc.ID = Convert.ToInt32(row["PCID"]);
                    pc.Name = row["PCName"].ToString();
                    pc.HourlyRate = Convert.ToDecimal(row["HourlyRate"]);
                    pc.Notes = dt.Columns.Contains("Notes") && row["Notes"] != DBNull.Value
                                    ? row["Notes"].ToString() : "";

                    int status = Convert.ToInt32(row["Status"]);
                    if (status == 1 && row["StartTime"] != DBNull.Value)
                    {
                        pc.Status = PCStatus.InUse;
                        pc.StartTime = Convert.ToDateTime(row["StartTime"]);

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
                MessageBox.Show("Lỗi khi tải dữ liệu máy:\n" + ex.Message,
                    "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartTimer()
        {
            timerLive = new System.Windows.Forms.Timer { Interval = 1000 };
            timerLive.Tick += (s, e) =>
            {
                foreach (var ucpc in pcControls)
                    ucpc.UpdateDisplay();
                UpdateHeader();
            };
            timerLive.Start();
        }

        private void HandleStartSession(Computer pc)
        {
            var confirm = MessageBox.Show(
                $"Mở máy {pc.Name}?\nGiá: {pc.HourlyRate:N0} VNĐ/giờ",
                "Xác nhận mở máy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                db.ExecuteNonQuery(
                    "UPDATE Computers SET Status = 1, StartTime = @now WHERE PCID = @id",
                    new[]
                    {
                        new SqlParameter("@now", SimulatedClock.Now),
                        new SqlParameter("@id",  pc.ID)
                    });

                pc.StartSession();
                pcControls.Find(u => u.PC.ID == pc.ID)?.UpdateDisplay();
                UpdateHeader();
                SetStatus($"✔ Đã mở máy {pc.Name}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở máy: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleCheckout(Computer pc)
        {
            var form = new CheckoutForm(pc);
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    db.ExecuteNonQuery(
                        "UPDATE OrderItems SET IsPaid = 1 WHERE PCID = @id AND IsPaid = 0",
                        new[] { new SqlParameter("@id", pc.ID) });

                    db.ExecuteNonQuery(
                        "UPDATE Computers SET Status = 0, StartTime = NULL WHERE PCID = @id",
                        new[] { new SqlParameter("@id", pc.ID) });

                    pc.EndSession();
                    pcControls.Find(u => u.PC.ID == pc.ID)?.UpdateDisplay();
                    UpdateHeader();
                    SetStatus($"✔ Đã thanh toán máy {pc.Name}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi checkout: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void HandleAddService(Computer pc)
        {
            var form = new ServiceForm(pc);
            form.ShowDialog();

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
            pcControls.Find(u => u.PC.ID == pc.ID)?.UpdateDisplay();
        }

        private void BtnManageServices_Click(object sender, EventArgs e)
        {
            new ServiceForm(null).ShowDialog();
        }

        private void BtnReport_Click(object sender, EventArgs e)
        {
            new ReportForm().ShowDialog();
        }

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
            catch { }
        }

        private void SetStatus(string message) => lblStatus.Text = message;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerLive?.Stop();
            timerLive?.Dispose();
            base.OnFormClosing(e);
        }
    }
}