using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public partial class CheckoutForm : Form
    {
        private Computer _pc;
        private DatabaseHelper db = DatabaseHelper.Instance;

        // ── Constructor không tham số để Designer render được ──────
        public CheckoutForm() : this(null) { }

        public CheckoutForm(Computer pc)
        {
            _pc = pc;
            InitializeComponent();
            if (_pc != null) LoadData();
        }

        private void LoadData()
        {
            lblPCName.Text = "Máy: " + _pc.Name + (_pc.IsVip ? "  ⭐ VIP" : "");
            lblTimeValue.Text = _pc.GetElapsedTime();

            decimal timeFee = _pc.CalculateTimeFee();
            lblTimeFeeValue.Text = timeFee.ToString("N0") + " VNĐ";

            lstServices.Items.Clear();
            foreach (var item in _pc.Orders)
                lstServices.Items.Add($"{item.ServiceName}  x{item.Quantity}  =  {item.Total:N0} VNĐ");

            if (_pc.Orders.Count == 0)
                lstServices.Items.Add("(Không có dịch vụ)");

            decimal svcFee = _pc.GetServiceTotal();
            lblSvcFeeValue.Text = svcFee.ToString("N0") + " VNĐ";
            lblTotalValue.Text = (timeFee + svcFee).ToString("N0") + " VNĐ";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (_pc == null) return;

            try
            {
                decimal timeFee = _pc.CalculateTimeFee();
                decimal svcFee = _pc.GetServiceTotal();
                decimal total = timeFee + svcFee;

                string pay = rdoCard.Checked ? "Thẻ ngân hàng"
                           : rdoTransfer.Checked ? "Chuyển khoản"
                           : "Tiền mặt";

                // Ghi giao dịch
                db.ExecuteNonQuery(
                    @"INSERT INTO Transactions
                        (PCID, TimeFee, ServiceFee, TotalAmount, Note, PaymentMethod, CheckoutTime)
                      VALUES
                        (@pcId, @timeFee, @svcFee, @total, @note, @pay, @ct)",
                    new[]
                    {
                        new SqlParameter("@pcId",    _pc.ID),
                        new SqlParameter("@timeFee", timeFee),
                        new SqlParameter("@svcFee",  svcFee),
                        new SqlParameter("@total",   total),
                        new SqlParameter("@note",    $"Checkout {_pc.Name} lúc {SimulatedClock.Now:HH:mm}"),
                        new SqlParameter("@pay",     pay),
                        new SqlParameter("@ct",      SimulatedClock.Now)
                    });

                // Đánh dấu đã thanh toán
                db.ExecuteNonQuery(
                    "UPDATE OrderItems SET IsPaid=1 WHERE PCID=@id AND IsPaid=0",
                    new[] { new SqlParameter("@id", _pc.ID) });

                MessageBox.Show(
                    $"✔ Thanh toán thành công!\n\n" +
                    $"Máy     : {_pc.Name}\n" +
                    $"Thời gian: {_pc.GetElapsedTime()}\n" +
                    $"Tổng    : {total:N0} VNĐ\n" +
                    $"Hình thức: {pay}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}