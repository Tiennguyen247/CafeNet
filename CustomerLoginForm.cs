using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public class CustomerLoginForm : Form
    {
        private Computer _pc;

        public CustomerLoginForm(Computer pc)
        {
            _pc = pc;
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Đăng nhập";
            this.Size = new Size(360, 460);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Logo tròn
            var pic = new PictureBox { Size = new Size(76, 76), Location = new Point(142, 25) };
            pic.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillEllipse(
                    new SolidBrush(Color.FromArgb(0, 180, 220)), 0, 0, 74, 74);
            };

            var lblTitle = new Label
            {
                Text = "Đăng nhập",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                AutoSize = true,
                Location = new Point(95, 115)
            };

            var txtPhone = new TextBox
            {
                Text = "0000000000",
                Location = new Point(30, 170),
                Size = new Size(290, 32),
                Font = new Font("Segoe UI", 11),
                BorderStyle = BorderStyle.FixedSingle
            };

            var txtPass = new TextBox
            {
                Text = "0000",
                Location = new Point(30, 220),
                Size = new Size(290, 32),
                Font = new Font("Segoe UI", 11),
                PasswordChar = '●',
                BorderStyle = BorderStyle.FixedSingle
            };

            var btnLogin = new Button
            {
                Text = "Đăng nhập",
                Location = new Point(30, 275),
                Size = new Size(290, 42),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(0, 150, 200),
                Font = new Font("Segoe UI", 11),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderColor = Color.FromArgb(0, 150, 200);
            btnLogin.Click += (s, e) =>
            {
                string phone = txtPhone.Text.Trim();
                Member member = null;

                // Tìm đúng SĐT trong DB
                var dt = DatabaseHelper.Instance.GetMemberByPhone(phone);
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
                    _pc.CurrentMember = member; // Gắn vào máy
                }

                new CustomerDashboard(_pc, member).Show();
                this.Close();
            };

            var lblOr = new Label
            {
                Text = "Hoặc",
                Location = new Point(165, 330),
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9)
            };

            var chk = new CheckBox
            {
                Text = "Nạp thẻ",
                Location = new Point(30, 355),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };

            this.AcceptButton = btnLogin;
            this.Controls.AddRange(new Control[]
                { pic, lblTitle, txtPhone, txtPass, btnLogin, lblOr, chk });
        }
    }
}