using System;
using System.Data;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public partial class CustomerDashboard : Form
    {
        private Computer _pc;
        private Member _member;

        // ── Constructor không tham số để Designer render được ──────
        public CustomerDashboard() : this(null, null) { }

        public CustomerDashboard(Computer pc, Member member = null)
        {
            _pc = pc;
            _member = member;
            InitializeComponent();
            UpdateInfo();
            timerUpdate.Start();
        }

        // ── Cập nhật toàn bộ thông tin trên màn hình ──────────────
        private void UpdateInfo()
        {
            if (_pc == null) return;

            // Tên máy + badge VIP
            lblPCName.Text = _pc.Name + (_pc.IsVip ? "  ⭐" : "");

            // Tên hội viên hoặc khách
            lblMember.Text = _member != null
                ? $"👤 {_member.FullName}"
                : "Khách vãng lai";

            decimal timeFee = _pc.CalculateTimeFee();
            decimal svcFee = _pc.GetServiceTotal();

            lblTimeUsed.Text = _pc.GetElapsedTime();
            lblAmountUsed.Text = timeFee.ToString("N0") + " VNĐ";
            lblSvcTotal.Text = svcFee.ToString("N0") + " VNĐ";
            lblTotal.Text = $"Tổng cộng: {(timeFee + svcFee):N0} VNĐ";

            lblBalance.Text = _member != null
                ? _member.Balance.ToString("N0") + " VNĐ"
                : "---";
        }

        private void timerUpdate_Tick(object sender, EventArgs e) => UpdateInfo();

        // ── Nút Gọi Dịch Vụ ───────────────────────────────────────
        private void btnOrderFood_Click(object sender, EventArgs e)
        {
            if (_pc == null || _pc.Status != PCStatus.InUse)
            {
                MessageBox.Show("Máy chưa mở. Vui lòng liên hệ nhân viên.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Mở form gọi dịch vụ
            var sf = new ServiceForm(_pc);
            sf.ShowDialog();

            // Reload danh sách orders từ DB sau khi đóng ServiceForm
            ReloadOrders();
            UpdateInfo();
        }

        // ── Reload orders từ DB ────────────────────────────────────
        private void ReloadOrders()
        {
            if (_pc == null) return;
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
        }

        // ── Nút Đăng xuất ─────────────────────────────────────────
        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Nếu có member, hỏi có muốn checkout không
            if (_pc != null && _pc.Status == PCStatus.InUse)
            {
                decimal total = _pc.CalculateTimeFee() + _pc.GetServiceTotal();
                var ans = MessageBox.Show(
                    $"Bạn đang còn {total:N0} VNĐ chưa thanh toán.\n" +
                    "Đăng xuất sẽ không tự động thanh toán.\n\n" +
                    "Tiếp tục đăng xuất?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (ans != DialogResult.Yes) return;
            }

            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerUpdate?.Stop();
            timerUpdate?.Dispose();
            base.OnFormClosing(e);
        }
    }
}