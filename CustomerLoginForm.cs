using System;
using System.Data;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public partial class CustomerLoginForm : Form
    {
        private Computer _pc;

        // ── Constructor không tham số để Designer render được ──────
        public CustomerLoginForm() : this(null) { }

        public CustomerLoginForm(Computer pc)
        {
            _pc = pc;
            InitializeComponent();

            // Hiển thị tên máy lên tiêu đề nếu có
            if (_pc != null)
            {
                this.Text = $"Đăng nhập — {_pc.Name}";
                lblSubtitle.Text = $"Máy: {_pc.Name}  |  Đăng nhập hội viên";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            Member member = null;
            DataTable dt = DatabaseHelper.Instance.GetMemberByPhone(phone);

            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                member = new Member
                {
                    MemberID = Convert.ToInt32(row["MemberID"]),
                    FullName = row["FullName"].ToString(),
                    Phone = row["Phone"].ToString(),
                    Balance = Convert.ToDecimal(row["Balance"]),
                    Points = Convert.ToInt32(row["Points"])
                };

                if (_pc != null)
                    _pc.CurrentMember = member;
            }
            else
            {
                // Số điện thoại không tìm thấy — hỏi tiếp tục hay không
                var ans = MessageBox.Show(
                    $"Không tìm thấy hội viên '{phone}'.\nTiếp tục với tư cách khách vãng lai?",
                    "Không tìm thấy", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (ans != DialogResult.Yes) return;
            }

            OpenDashboard(member);
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            OpenDashboard(null);
        }

        private void OpenDashboard(Member member)
        {
            var dashboard = new CustomerDashboard(_pc, member);
            dashboard.Show();
            this.Close();
        }
    }
}