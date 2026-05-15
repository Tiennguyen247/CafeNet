namespace CyberCafeManager
{
    partial class CustomerLoginForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlTop = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            pnlForm = new Panel();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblPass = new Label();
            txtPass = new TextBox();
            btnLogin = new Button();
            lblOr = new Label();
            btnGuest = new Button();
            pnlTop.SuspendLayout();
            pnlForm.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(0, 150, 200);
            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(lblSubtitle);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(324, 100);
            pnlTop.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(340, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "☕ NET CAFE";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(200, 240, 255);
            lblSubtitle.Location = new Point(0, 62);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(340, 20);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Đăng nhập tài khoản hội viên";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlForm
            // 
            pnlForm.BackColor = Color.White;
            pnlForm.Controls.Add(lblPhone);
            pnlForm.Controls.Add(txtPhone);
            pnlForm.Controls.Add(lblPass);
            pnlForm.Controls.Add(txtPass);
            pnlForm.Controls.Add(btnLogin);
            pnlForm.Controls.Add(lblOr);
            pnlForm.Controls.Add(btnGuest);
            pnlForm.Dock = DockStyle.Fill;
            pnlForm.Location = new Point(0, 100);
            pnlForm.Name = "pnlForm";
            pnlForm.Padding = new Padding(30, 20, 30, 20);
            pnlForm.Size = new Size(324, 281);
            pnlForm.TabIndex = 0;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 9F);
            lblPhone.Location = new Point(30, 20);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(79, 15);
            lblPhone.TabIndex = 0;
            lblPhone.Text = "Số điện thoại:";
            // 
            // txtPhone
            // 
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Font = new Font("Segoe UI", 11F);
            txtPhone.Location = new Point(30, 40);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(280, 27);
            txtPhone.TabIndex = 1;
            txtPhone.Text = "0000000000";
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Font = new Font("Segoe UI", 9F);
            lblPass.Location = new Point(30, 82);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(60, 15);
            lblPass.TabIndex = 2;
            lblPass.Text = "Mật khẩu:";
            // 
            // txtPass
            // 
            txtPass.BorderStyle = BorderStyle.FixedSingle;
            txtPass.Font = new Font("Segoe UI", 11F);
            txtPass.Location = new Point(30, 102);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '●';
            txtPass.Size = new Size(280, 27);
            txtPass.TabIndex = 3;
            txtPass.Text = "0000";
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(0, 150, 200);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(30, 150);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(280, 40);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblOr
            // 
            lblOr.AutoSize = true;
            lblOr.Font = new Font("Segoe UI", 9F);
            lblOr.ForeColor = Color.Gray;
            lblOr.Location = new Point(155, 202);
            lblOr.Name = "lblOr";
            lblOr.Size = new Size(63, 15);
            lblOr.TabIndex = 5;
            lblOr.Text = "— hoặc —";
            // 
            // btnGuest
            // 
            btnGuest.BackColor = Color.White;
            btnGuest.Cursor = Cursors.Hand;
            btnGuest.FlatAppearance.BorderColor = Color.FromArgb(0, 150, 200);
            btnGuest.FlatStyle = FlatStyle.Flat;
            btnGuest.Font = new Font("Segoe UI", 10F);
            btnGuest.ForeColor = Color.FromArgb(0, 150, 200);
            btnGuest.Location = new Point(30, 222);
            btnGuest.Name = "btnGuest";
            btnGuest.Size = new Size(280, 36);
            btnGuest.TabIndex = 6;
            btnGuest.Text = "Tiếp tục không đăng nhập";
            btnGuest.UseVisualStyleBackColor = false;
            btnGuest.Click += btnGuest_Click;
            // 
            // CustomerLoginForm
            // 
            AcceptButton = btnLogin;
            BackColor = Color.White;
            ClientSize = new Size(324, 381);
            Controls.Add(pnlForm);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomerLoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập";
            pnlTop.ResumeLayout(false);
            pnlForm.ResumeLayout(false);
            pnlForm.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblOr;
        private System.Windows.Forms.Button btnGuest;
    }
}