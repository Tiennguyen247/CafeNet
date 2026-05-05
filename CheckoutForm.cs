using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace CyberCafeManager
{
    public class CheckoutForm : Form
    {
        private Computer _pc;
        private DatabaseHelper db = DatabaseHelper.Instance;

        // Controls
        private Label lblTitle, lblPCName;
        private Label lblTimeLabel, lblTimeValue;
        private Label lblTimeFeeLabel, lblTimeFeeValue;
        private Label lblServiceFeeLabel, lblServiceFeeValue;
        private Label lblTotalLabel, lblTotalValue;
        private ListBox lstServices;
        private Button btnConfirm, btnCancel;
        private GroupBox grpPayment;
        private RadioButton rdoCash, rdoCard, rdoTransfer;
        private string _receiptText = "";
        private Panel pnlTotal;

        public CheckoutForm(Computer pc)
        {
            _pc = pc;
            InitializeComponents();
            LoadData();
        }

        private void InitializeComponents()
        {
            this.Text = "Thanh Toán";
            this.Size = new Size(420, 590);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // --- Header ---
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(18, 40, 76)
            };

            lblTitle = new Label
            {
                Text = "💰  THANH TOÁN",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblTitle);

            // --- Tên máy ---
            lblPCName = new Label
            {
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(18, 40, 76),
                Location = new Point(20, 75),
                AutoSize = true
            };

            // --- Thời gian ---
            MakeRow("⏱  Thời gian dùng:", 110, out lblTimeLabel, out lblTimeValue);
            MakeRow("🕐  Tiền giờ:", 140, out lblTimeFeeLabel, out lblTimeFeeValue);

            // --- Danh sách dịch vụ ---
            var lblServicesTitle = new Label
            {
                Text = "🍜  Dịch vụ đã gọi:",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(20, 168),
                AutoSize = true
            };

            lstServices = new ListBox
            {
                Location = new Point(20, 190),
                Size = new Size(370, 100),
                Font = new Font("Consolas", 9),
                BorderStyle = BorderStyle.FixedSingle
            };

            MakeRow("🛒  Tổng dịch vụ:", 300, out lblServiceFeeLabel, out lblServiceFeeValue);

            // --- Panel tổng cộng ---
            pnlTotal = new Panel
            {
                Location = new Point(20, 330),
                Size = new Size(370, 50),
                BackColor = Color.FromArgb(18, 40, 76)
            };

            lblTotalLabel = new Label
            {
                Text = "TỔNG CỘNG:",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Location = new Point(15, 14),
                AutoSize = true
            };

            lblTotalValue = new Label
            {
                ForeColor = Color.Gold,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(200, 10),
                TextAlign = ContentAlignment.MiddleRight
            };

            pnlTotal.Controls.AddRange(new Control[] { lblTotalLabel, lblTotalValue });

            // --- Nút bấm ---
            btnConfirm = new Button
            {
                Text = "✔  Xác nhận thanh toán",
                Location = new Point(20, 515),
                Size = new Size(200, 42),
                BackColor = Color.FromArgb(30, 150, 70),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Click += BtnConfirm_Click;

            btnCancel = new Button
            {
                Text = "✕  Huỷ",
                Location = new Point(230, 515),
                Size = new Size(160, 42),
                BackColor = Color.FromArgb(200, 50, 50),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            // ---- PHƯƠNG THỨC THANH TOÁN ----
            grpPayment = new GroupBox
            {
                Text = "💳  Phương thức thanh toán",
                Location = new Point(20, 450),
                Size = new Size(370, 55),
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            rdoCash = new RadioButton { Text = "💵 Tiền mặt", Location = new Point(10, 22), AutoSize = true, Checked = true };
            rdoCard = new RadioButton { Text = "💳 Thẻ", Location = new Point(110, 22), AutoSize = true };
            rdoTransfer = new RadioButton { Text = "📲 CK", Location = new Point(235, 22), AutoSize = true };
            grpPayment.Controls.AddRange(new Control[] { rdoCash, rdoCard, rdoTransfer });

            this.Controls.AddRange(new Control[]
            {
                pnlHeader, lblPCName, lblTimeLabel, lblTimeValue,
                lblTimeFeeLabel, lblTimeFeeValue,
                lblServicesTitle, lstServices,
                lblServiceFeeLabel, lblServiceFeeValue,
                pnlTotal, grpPayment, btnConfirm, btnCancel
            });
        }

        private void MakeRow(string labelText, int y, out Label left, out Label right)
        {
            left = new Label { Text = labelText, Font = new Font("Segoe UI", 9), ForeColor = Color.FromArgb(80, 80, 80), Location = new Point(20, y), AutoSize = true };
            right = new Label { Font = new Font("Segoe UI", 9, FontStyle.Bold), ForeColor = Color.FromArgb(20, 20, 20), Location = new Point(250, y), AutoSize = true };
        }

        private void LoadData()
        {
            lblPCName.Text = "Máy: " + _pc.Name + (_pc.IsVip ? " ⭐ VIP" : "");
            lblTimeValue.Text = _pc.GetElapsedTime();

            decimal timeFee = _pc.CalculateTimeFee();
            lblTimeFeeValue.Text = timeFee.ToString("N0") + " VNĐ";

            lstServices.Items.Clear();
            foreach (var item in _pc.Orders)
                lstServices.Items.Add($"{item.ServiceName} x{item.Quantity} = {item.Total:N0} VNĐ");

            if (_pc.Orders.Count == 0)
                lstServices.Items.Add("(Không có dịch vụ nào)");

            decimal serviceFee = _pc.GetServiceTotal();
            lblServiceFeeValue.Text = serviceFee.ToString("N0") + " VNĐ";

            decimal total = timeFee + serviceFee;
            lblTotalValue.Text = total.ToString("N0") + " VNĐ";
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                decimal timeFee = _pc.CalculateTimeFee();
                decimal serviceFee = _pc.GetServiceTotal();
                decimal total = timeFee + serviceFee;

                string payMethod = rdoCard.Checked ? "Thẻ ngân hàng"
                                 : rdoTransfer.Checked ? "Chuyển khoản"
                                 : "Tiền mặt";

                string sql = @"INSERT INTO Transactions (PCID, TimeFee, ServiceFee, TotalAmount, Note, PaymentMethod)
                               VALUES (@pcId, @timeFee, @serviceFee, @total, @note, @pay)";

                db.ExecuteNonQuery(sql, new[]
                {
                    new SqlParameter("@pcId", _pc.ID),
                    new SqlParameter("@timeFee", timeFee),
                    new SqlParameter("@serviceFee", serviceFee),
                    new SqlParameter("@total", total),
                    new SqlParameter("@note", $"Checkout {_pc.Name} lúc {SimulatedClock.Now:HH:mm}"),
                    new SqlParameter("@pay", payMethod)
                });

                db.ExecuteNonQuery("UPDATE OrderItems SET IsPaid = 1 WHERE PCID = @id AND IsPaid = 0",
                    new[] { new SqlParameter("@id", _pc.ID) });

                _receiptText = BuildReceipt(_pc, timeFee, serviceFee, total, payMethod);

                MessageBox.Show($"Thanh toán thành công!\nTổng tiền: {total:N0} VNĐ", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (MessageBox.Show("Bạn có muốn in hóa đơn không?", "In hóa đơn", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    PrintReceipt();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string BuildReceipt(Computer pc, decimal timeFee, decimal serviceFee, decimal total, string payMethod)
        {
            return $"HOA DON NETCAFE\nMay: {pc.Name}\nTien gio: {timeFee:N0}\nDich vu: {serviceFee:N0}\nTong: {total:N0}\nPTTT: {payMethod}";
        }

        private void PrintReceipt()
        {
            // Placeholder cho logic in ấn thực tế
            MessageBox.Show("Đang kết nối máy in...", "Print Service");
        }
    }
}