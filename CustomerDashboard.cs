using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public class CustomerDashboard : Form
    {
        private Computer _pc;
        private System.Windows.Forms.Timer _timer;

        private Label lblPCName, lblTotal, lblTimeUsed,
                      lblAmountUsed, lblTimeLeft, lblBalance;
        private Member _member;

        public CustomerDashboard(Computer pc, Member member = null)
        {
            _pc = pc;
            _member = member;
            InitializeComponents();
            UpdateInfo();
            StartTimer();
        }

        private void InitializeComponents()
        {
            this.Text = "Khách hàng — " + (_pc?.Name ?? "---");
            this.Size = new Size(310, 590);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            int y = 20;

            // Avatar
            var pic = new PictureBox
            { Size = new Size(70, 70), Location = new Point(120, y) };
            pic.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillEllipse(
                    new SolidBrush(Color.FromArgb(0, 170, 210)), 0, 0, 68, 68);
                g.FillEllipse(Brushes.White, 22, 10, 24, 24);
                using var path = new GraphicsPath();
                path.AddEllipse(10, 36, 48, 32);
                g.FillPath(Brushes.White, path);
            };
            y += 82;

            // Tên máy / số điện thoại
            lblPCName = new Label
            {
                Text = _pc?.Name ?? "---",
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(20, y),
                Size = new Size(265, 32),
                TextAlign = ContentAlignment.MiddleCenter
            };
            y += 42;

            // Panel thông tin
            var pnl = new Panel
            {
                Location = new Point(15, y),
                Size = new Size(270, 100),
                BackColor = Color.FromArgb(245, 248, 255),
                BorderStyle = BorderStyle.FixedSingle
            };

            MakeInfoRow(pnl, "Tổng thanh toán", 5, out _, out lblTotal);
            MakeInfoRow(pnl, "Sử dụng:", 38, out lblTimeUsed, out lblAmountUsed);
            MakeInfoRow(pnl, "Còn lại:", 70, out lblTimeLeft, out lblBalance);

            y += 112;

            // Button grid (3 × 3)
            string[] names = { "💬 Giao tiếp","🍜 Gọi món","🚪 Đăng xuất",
                                  "👤 Khách hàng","🔒 Khóa máy","💰 Nạp tiền",
                                  "🎮 Game Menu","📢 Hoạt động","💡 Góp ý" };
            bool[] active = { false, true, true,
                                  false, false, false,
                                  false, false, false };

            for (int i = 0; i < 9; i++)
            {
                int col = i % 3, row = i / 3;
                var btn = new Button
                {
                    Text = names[i],
                    Location = new Point(15 + col * 88, y + row * 82),
                    Size = new Size(80, 68),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.White,
                    ForeColor = active[i]
                                    ? Color.FromArgb(0, 150, 190)
                                    : Color.LightGray,
                    Font = new Font("Segoe UI", 7.5f),
                    Enabled = active[i],
                    Cursor = active[i] ? Cursors.Hand : Cursors.Default
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);

                string captured = names[i];
                btn.Click += (s, e) => HandleButtonClick(captured);
                this.Controls.Add(btn);
            }
            y += 256;

            // Footer
            var lblVer = new Label
            {
                Text = "CafeNet v1.0.0 @ " + DateTime.Now.Year,
                Location = new Point(75, y + 10),
                AutoSize = true,
                ForeColor = Color.LightGray,
                Font = new Font("Segoe UI", 8)
            };

            this.Controls.AddRange(new Control[]
                { pic, lblPCName, pnl, lblVer });
        }

        private void MakeInfoRow(Panel parent, string label,
                                  int y, out Label left, out Label right)
        {
            left = new Label
            {
                Text = label,
                Location = new Point(6, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            right = new Label
            {
                Text = "0",
                Location = new Point(175, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            parent.Controls.Add(left);
            parent.Controls.Add(right);
        }

        private void HandleButtonClick(string name)
        {
            if (name.Contains("Gọi món"))
            {
                if (_pc == null || _pc.Status != PCStatus.InUse)
                {
                    MessageBox.Show("Máy chưa mở. Liên hệ nhân viên.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var sf = new ServiceForm(_pc);
                sf.ShowDialog();

                // Reload orders sau khi đóng ServiceForm
                var db = DatabaseHelper.Instance;
                DataTable orders = db.GetUnpaidOrders(_pc.ID);
                _pc.Orders.Clear();
                foreach (DataRow row in orders.Rows)
                    _pc.Orders.Add(new OrderItem
                    {
                        ItemID = Convert.ToInt32(row["ItemID"]),
                        ServiceID = Convert.ToInt32(row["ServiceID"]),
                        ServiceName = row["ServiceName"].ToString(),
                        UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                        Quantity = Convert.ToInt32(row["Quantity"])
                    });
                UpdateInfo();
            }
            else if (name.Contains("Đăng xuất"))
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Tính năng đang phát triển.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateInfo()
        {
            if (_pc == null) return;
            decimal fee = _pc.CalculateTimeFee();
            decimal services = _pc.GetServiceTotal();

            lblTotal.Text = (fee + services).ToString("N0") + " VNĐ";
            lblTimeUsed.Text = _pc.GetElapsedTime();
            lblAmountUsed.Text = fee.ToString("N0") + " VNĐ";
            lblTimeLeft.Text = "∞";
            lblBalance.Text = "0 VNĐ";

            if (_member != null)
            {
                lblPCName.Text = _member.FullName;
                lblBalance.Text = $"Số dư ví: {_member.Balance:N0} VNĐ";
            }
        }

        private void StartTimer()
        {
            _timer = new System.Windows.Forms.Timer { Interval = 1000 };
            _timer.Tick += (s, e) => UpdateInfo();
            _timer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _timer?.Stop();
            _timer?.Dispose();
            base.OnFormClosing(e);
        }
    }
}