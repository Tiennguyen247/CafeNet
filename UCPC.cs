// ============================================================
// UCPC.cs - UserControl đại diện cho 1 máy tính
// Hiển thị: tên máy, trạng thái, thời gian, tiền, các nút
//
// CÁCH TẠO TRONG VISUAL STUDIO:
//   Project → Add → New Item → User Control (Windows Forms)
//   Đặt tên: UCPC.cs
//   XÓA hết code trong file .cs và .Designer.cs
//   Paste code này vào UCPC.cs
// ============================================================

using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public class UCPC : UserControl
    {
        // ========================
        // THUỘC TÍNH
        // ========================
        public Computer PC { get; private set; }

        // Events: Khi user click các nút, MainDashboard sẽ xử lý
        public event Action<Computer> OnStartSession;
        public event Action<Computer> OnCheckout;
        public event Action<Computer> OnAddService;

        // Controls
        private Panel pnlHeader;
        private Label lblName, lblVipBadge;
        private Label lblStatus, lblTime, lblFee;
        private Button btnStart, btnCheckout, btnService;

        // Màu sắc
        private readonly Color COLOR_EMPTY = Color.FromArgb(232, 245, 233); // Xanh nhạt
        private readonly Color COLOR_IN_USE = Color.FromArgb(255, 235, 238); // Đỏ nhạt
        private readonly Color COLOR_HEADER = Color.FromArgb(30, 58, 95);    // Xanh đậm
        private readonly Color COLOR_VIP_HDR = Color.FromArgb(100, 70, 0);    // Vàng đậm
        private readonly Color COLOR_ORANGE = Color.FromArgb(230, 100, 0);

        // ========================
        // CONSTRUCTOR
        // ========================
        public UCPC(Computer pc)
        {
            PC = pc;
            InitializeComponents(); // Tạo giao diện
            UpdateDisplay();         // Cập nhật lần đầu
        }

        // ========================
        // TẠO GIAO DIỆN
        // ========================
        private void InitializeComponents()
        {
            // Kích thước tổng thể của mỗi PC card
            this.Size = new Size(190, 210);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(6);
            this.Cursor = Cursors.Hand;

            // ---- HEADER (Tên máy) ----
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 38,
                BackColor = PC.IsVip ? COLOR_VIP_HDR : COLOR_HEADER
            };

            lblName = new Label
            {
                Text = PC.Name,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            // Badge [VIP] nếu là máy VIP
            if (PC.IsVip)
            {
                lblVipBadge = new Label
                {
                    Text = " ★ VIP ",
                    ForeColor = Color.Gold,
                    BackColor = Color.Transparent,
                    Font = new Font("Segoe UI", 7, FontStyle.Bold),
                    Size = new Size(38, 16),
                    Location = new Point(148, 2),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                pnlHeader.Controls.Add(lblVipBadge);
            }
            pnlHeader.Controls.Add(lblName);

            // ---- TRẠNG THÁI ----
            lblStatus = new Label
            {
                Location = new Point(5, 44),
                Size = new Size(180, 22),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // ---- ĐỒNG HỒ ----
            lblTime = new Label
            {
                Location = new Point(5, 66),
                Size = new Size(180, 40),
                Font = new Font("Consolas", 18, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(50, 50, 50)
            };

            // ---- SỐ TIỀN ----
            lblFee = new Label
            {
                Location = new Point(5, 106),
                Size = new Size(180, 24),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = COLOR_ORANGE
            };

            // ---- NÚT MỞ MÁY ----
            btnStart = new Button
            {
                Text = "▶  Mở Máy",
                Location = new Point(5, 136),
                Size = new Size(88, 32),
                BackColor = Color.FromArgb(0, 120, 212),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Click += (s, e) => OnStartSession?.Invoke(PC);

            // ---- NÚT TÍNH TIỀN ----
            btnCheckout = new Button
            {
                Text = "💰 Tính Tiền",
                Location = new Point(97, 136),
                Size = new Size(88, 32),
                BackColor = Color.FromArgb(198, 40, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.Click += (s, e) => OnCheckout?.Invoke(PC);

            // ---- NÚT DỊCH VỤ ----
            btnService = new Button
            {
                Text = "🍜  Gọi Dịch Vụ",
                Location = new Point(5, 172),
                Size = new Size(180, 30),
                BackColor = Color.FromArgb(40, 150, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Enabled = false
            };
            btnService.FlatAppearance.BorderSize = 0;

            var toolTip = new ToolTip { ShowAlways = true };
            this.MouseClick += (s, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    string current = PC.Notes ?? "";
                    string input = Microsoft.VisualBasic.Interaction.InputBox(
                        "Nhập ghi chú cho máy " + PC.Name + ":",
                        "Ghi chú", current);

                    if (input == null) return; // Bấm Cancel
                    PC.Notes = input;

                    // Lưu xuống DB
                    DatabaseHelper.Instance.ExecuteNonQuery(
                        "UPDATE Computers SET Notes = @note WHERE PCID = @id",
                        new[]
                        {
                new System.Data.SqlClient.SqlParameter("@note", (object)input ?? DBNull.Value),
                new System.Data.SqlClient.SqlParameter("@id",   PC.ID)
                        });

                    toolTip.SetToolTip(this, string.IsNullOrEmpty(input) ? "" : "📝 " + input);
                }
            };

            btnService.Click += (s, e) => OnAddService?.Invoke(PC);

            // Thêm tất cả vào UserControl
            this.Controls.AddRange(new Control[]
            {
                pnlHeader, lblStatus, lblTime, lblFee,
                btnStart, btnCheckout, btnService
            });
        }

        // ============================================================
        // CẬP NHẬT HIỂN THỊ
        // Method này được gọi mỗi giây từ Timer ở MainDashboard
        // ============================================================
        public void UpdateDisplay()
        {
            // Dùng Invoke để update UI từ thread khác nếu cần (tránh lỗi cross-thread)
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateDisplay));
                return;
            }

            if (PC.Status == PCStatus.Empty)
            {
                // Máy trống
                this.BackColor = COLOR_EMPTY;
                lblStatus.Text = "● TRỐNG";
                lblStatus.ForeColor = Color.FromArgb(30, 130, 30);
                lblTime.Text = "--:--:--";
                lblTime.ForeColor = Color.Silver;
                lblFee.Text = "0 VNĐ";

                btnStart.Enabled = true;
                btnCheckout.Enabled = false;
                btnService.Enabled = false;
            }
            else
            {
                // Máy đang dùng
                this.BackColor = COLOR_IN_USE;
                lblStatus.Text = "● ĐANG DÙNG";
                lblStatus.ForeColor = Color.FromArgb(180, 20, 20);
                lblTime.Text = PC.GetElapsedTime();
                lblTime.ForeColor = Color.FromArgb(30, 30, 30);
                lblFee.Text = PC.CalculateTotalFee().ToString("N0") + " VNĐ";

                btnStart.Enabled = false;
                btnCheckout.Enabled = true;
                btnService.Enabled = true;
            }
        }
    }
}