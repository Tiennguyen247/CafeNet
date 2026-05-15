namespace CyberCafeManager
{
    partial class CustomerDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlTop = new Panel();
            lblPCName = new Label();
            lblMember = new Label();
            pnlStats = new Panel();
            lblTimeCaption = new Label();
            lblTimeUsed = new Label();
            lblFeeCaption = new Label();
            lblAmountUsed = new Label();
            lblSvcCaption = new Label();
            lblSvcTotal = new Label();
            lblBalCaption = new Label();
            lblBalance = new Label();
            pnlButtons = new Panel();
            btnOrderFood = new Button();
            btnLogout = new Button();
            lblTotal = new Label();
            timerUpdate = new System.Windows.Forms.Timer(components);
            pnlTop.SuspendLayout();
            pnlStats.SuspendLayout();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.FromArgb(0, 140, 180);
            pnlTop.Controls.Add(lblPCName);
            pnlTop.Controls.Add(lblMember);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(305, 80);
            pnlTop.TabIndex = 3;
            // 
            // lblPCName
            // 
            lblPCName.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblPCName.ForeColor = Color.White;
            lblPCName.Location = new Point(0, 12);
            lblPCName.Name = "lblPCName";
            lblPCName.Size = new Size(300, 32);
            lblPCName.TabIndex = 0;
            lblPCName.Text = "---";
            lblPCName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMember
            // 
            lblMember.Font = new Font("Segoe UI", 9F);
            lblMember.ForeColor = Color.FromArgb(200, 240, 255);
            lblMember.Location = new Point(0, 48);
            lblMember.Name = "lblMember";
            lblMember.Size = new Size(300, 20);
            lblMember.TabIndex = 1;
            lblMember.Text = "Khách vãng lai";
            lblMember.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlStats
            // 
            pnlStats.BackColor = Color.FromArgb(245, 248, 255);
            pnlStats.Controls.Add(lblTimeCaption);
            pnlStats.Controls.Add(lblTimeUsed);
            pnlStats.Controls.Add(lblFeeCaption);
            pnlStats.Controls.Add(lblAmountUsed);
            pnlStats.Controls.Add(lblSvcCaption);
            pnlStats.Controls.Add(lblSvcTotal);
            pnlStats.Controls.Add(lblBalCaption);
            pnlStats.Controls.Add(lblBalance);
            pnlStats.Dock = DockStyle.Top;
            pnlStats.Location = new Point(0, 80);
            pnlStats.Name = "pnlStats";
            pnlStats.Padding = new Padding(16, 10, 16, 10);
            pnlStats.Size = new Size(305, 140);
            pnlStats.TabIndex = 2;
            // 
            // lblTimeCaption
            // 
            lblTimeCaption.AutoSize = true;
            lblTimeCaption.Font = new Font("Segoe UI", 9F);
            lblTimeCaption.ForeColor = Color.Gray;
            lblTimeCaption.Location = new Point(16, 14);
            lblTimeCaption.Name = "lblTimeCaption";
            lblTimeCaption.Size = new Size(106, 15);
            lblTimeCaption.TabIndex = 0;
            lblTimeCaption.Text = "Thời gian sử dụng:";
            // 
            // lblTimeUsed
            // 
            lblTimeUsed.AutoSize = true;
            lblTimeUsed.Font = new Font("Consolas", 11F, FontStyle.Bold);
            lblTimeUsed.ForeColor = Color.FromArgb(20, 20, 20);
            lblTimeUsed.Location = new Point(170, 12);
            lblTimeUsed.Name = "lblTimeUsed";
            lblTimeUsed.Size = new Size(72, 18);
            lblTimeUsed.TabIndex = 1;
            lblTimeUsed.Text = "00:00:00";
            // 
            // lblFeeCaption
            // 
            lblFeeCaption.AutoSize = true;
            lblFeeCaption.Font = new Font("Segoe UI", 9F);
            lblFeeCaption.ForeColor = Color.Gray;
            lblFeeCaption.Location = new Point(16, 44);
            lblFeeCaption.Name = "lblFeeCaption";
            lblFeeCaption.Size = new Size(53, 15);
            lblFeeCaption.TabIndex = 2;
            lblFeeCaption.Text = "Tiền giờ:";
            // 
            // lblAmountUsed
            // 
            lblAmountUsed.AutoSize = true;
            lblAmountUsed.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblAmountUsed.ForeColor = Color.FromArgb(200, 100, 0);
            lblAmountUsed.Location = new Point(170, 44);
            lblAmountUsed.Name = "lblAmountUsed";
            lblAmountUsed.Size = new Size(48, 17);
            lblAmountUsed.TabIndex = 3;
            lblAmountUsed.Text = "0 VNĐ";
            // 
            // lblSvcCaption
            // 
            lblSvcCaption.AutoSize = true;
            lblSvcCaption.Font = new Font("Segoe UI", 9F);
            lblSvcCaption.ForeColor = Color.Gray;
            lblSvcCaption.Location = new Point(16, 74);
            lblSvcCaption.Name = "lblSvcCaption";
            lblSvcCaption.Size = new Size(50, 15);
            lblSvcCaption.TabIndex = 4;
            lblSvcCaption.Text = "Dịch vụ:";
            // 
            // lblSvcTotal
            // 
            lblSvcTotal.AutoSize = true;
            lblSvcTotal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSvcTotal.ForeColor = Color.FromArgb(40, 120, 60);
            lblSvcTotal.Location = new Point(170, 74);
            lblSvcTotal.Name = "lblSvcTotal";
            lblSvcTotal.Size = new Size(48, 17);
            lblSvcTotal.TabIndex = 5;
            lblSvcTotal.Text = "0 VNĐ";
            // 
            // lblBalCaption
            // 
            lblBalCaption.AutoSize = true;
            lblBalCaption.Font = new Font("Segoe UI", 9F);
            lblBalCaption.ForeColor = Color.Gray;
            lblBalCaption.Location = new Point(16, 104);
            lblBalCaption.Name = "lblBalCaption";
            lblBalCaption.Size = new Size(52, 15);
            lblBalCaption.TabIndex = 6;
            lblBalCaption.Text = "Số dư ví:";
            // 
            // lblBalance
            // 
            lblBalance.AutoSize = true;
            lblBalance.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblBalance.ForeColor = Color.FromArgb(0, 100, 180);
            lblBalance.Location = new Point(170, 104);
            lblBalance.Name = "lblBalance";
            lblBalance.Size = new Size(23, 17);
            lblBalance.TabIndex = 7;
            lblBalance.Text = "---";
            // 
            // pnlButtons
            // 
            pnlButtons.BackColor = Color.White;
            pnlButtons.Controls.Add(btnOrderFood);
            pnlButtons.Controls.Add(btnLogout);
            pnlButtons.Dock = DockStyle.Fill;
            pnlButtons.Location = new Point(0, 264);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Padding = new Padding(16, 12, 16, 12);
            pnlButtons.Size = new Size(305, 137);
            pnlButtons.TabIndex = 0;
            // 
            // btnOrderFood
            // 
            btnOrderFood.BackColor = Color.FromArgb(40, 150, 80);
            btnOrderFood.Cursor = Cursors.Hand;
            btnOrderFood.FlatAppearance.BorderSize = 0;
            btnOrderFood.FlatStyle = FlatStyle.Flat;
            btnOrderFood.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnOrderFood.ForeColor = Color.White;
            btnOrderFood.Location = new Point(16, 12);
            btnOrderFood.Name = "btnOrderFood";
            btnOrderFood.Size = new Size(268, 48);
            btnOrderFood.TabIndex = 0;
            btnOrderFood.Text = "🍜  Gọi Dịch Vụ";
            btnOrderFood.UseVisualStyleBackColor = false;
            btnOrderFood.Click += btnOrderFood_Click;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(160, 50, 50);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(16, 72);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(268, 38);
            btnLogout.TabIndex = 1;
            btnLogout.Text = "🚪  Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // lblTotal
            // 
            lblTotal.BackColor = Color.FromArgb(18, 40, 76);
            lblTotal.Dock = DockStyle.Top;
            lblTotal.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTotal.ForeColor = Color.Gold;
            lblTotal.Location = new Point(0, 220);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(305, 44);
            lblTotal.TabIndex = 1;
            lblTotal.Text = "Tổng: 0 VNĐ";
            lblTotal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timerUpdate
            // 
            timerUpdate.Interval = 1000;
            timerUpdate.Tick += timerUpdate_Tick;
            // 
            // CustomerDashboard
            // 
            BackColor = Color.White;
            ClientSize = new Size(305, 401);
            Controls.Add(pnlButtons);
            Controls.Add(lblTotal);
            Controls.Add(pnlStats);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CustomerDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "g6";
            pnlTop.ResumeLayout(false);
            pnlStats.ResumeLayout(false);
            pnlStats.PerformLayout();
            pnlButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblPCName;
        private System.Windows.Forms.Label lblMember;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblTimeCaption;
        private System.Windows.Forms.Label lblTimeUsed;
        private System.Windows.Forms.Label lblFeeCaption;
        private System.Windows.Forms.Label lblAmountUsed;
        private System.Windows.Forms.Label lblSvcCaption;
        private System.Windows.Forms.Label lblSvcTotal;
        private System.Windows.Forms.Label lblBalCaption;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnOrderFood;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Timer timerUpdate;
    }
}