namespace CyberCafeManager
{
    partial class CheckoutForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.lblPCName = new System.Windows.Forms.Label();
            this.lblTimeCaption = new System.Windows.Forms.Label();
            this.lblTimeValue = new System.Windows.Forms.Label();
            this.lblTimeFeeCaption = new System.Windows.Forms.Label();
            this.lblTimeFeeValue = new System.Windows.Forms.Label();
            this.lblSvcCaption = new System.Windows.Forms.Label();
            this.lstServices = new System.Windows.Forms.ListBox();
            this.lblSvcFeeCaption = new System.Windows.Forms.Label();
            this.lblSvcFeeValue = new System.Windows.Forms.Label();
            this.pnlTotal = new System.Windows.Forms.Panel();
            this.lblTotalCaption = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.grpPayment = new System.Windows.Forms.GroupBox();
            this.rdoCash = new System.Windows.Forms.RadioButton();
            this.rdoCard = new System.Windows.Forms.RadioButton();
            this.rdoTransfer = new System.Windows.Forms.RadioButton();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            this.pnlTotal.SuspendLayout();
            this.grpPayment.SuspendLayout();
            this.SuspendLayout();

            // ── pnlHeader ──────────────────────────────────────────
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(18, 40, 76);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 56;
            this.pnlHeader.Controls.Add(this.lblTitle);

            this.lblTitle.AutoSize = false;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Text = "💰  THANH TOÁN";

            // ── pnlInfo ────────────────────────────────────────────
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfo.Padding = new System.Windows.Forms.Padding(18, 10, 18, 10);
            this.pnlInfo.BackColor = System.Drawing.Color.White;
            this.pnlInfo.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblPCName,
                this.lblTimeCaption,    this.lblTimeValue,
                this.lblTimeFeeCaption, this.lblTimeFeeValue,
                this.lblSvcCaption,     this.lstServices,
                this.lblSvcFeeCaption,  this.lblSvcFeeValue,
                this.pnlTotal,          this.grpPayment,
                this.btnConfirm,        this.btnCancel });

            // lblPCName
            this.lblPCName.AutoSize = true;
            this.lblPCName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPCName.ForeColor = System.Drawing.Color.FromArgb(18, 40, 76);
            this.lblPCName.Location = new System.Drawing.Point(18, 10);
            this.lblPCName.Text = "Máy: ---";

            // Row: thời gian
            this.lblTimeCaption.AutoSize = true;
            this.lblTimeCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTimeCaption.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblTimeCaption.Location = new System.Drawing.Point(18, 42);
            this.lblTimeCaption.Text = "⏱  Thời gian dùng:";

            this.lblTimeValue.AutoSize = true;
            this.lblTimeValue.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTimeValue.Location = new System.Drawing.Point(230, 42);
            this.lblTimeValue.Text = "00:00:00";

            // Row: tiền giờ
            this.lblTimeFeeCaption.AutoSize = true;
            this.lblTimeFeeCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTimeFeeCaption.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblTimeFeeCaption.Location = new System.Drawing.Point(18, 68);
            this.lblTimeFeeCaption.Text = "🕐  Tiền giờ:";

            this.lblTimeFeeValue.AutoSize = true;
            this.lblTimeFeeValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblTimeFeeValue.Location = new System.Drawing.Point(230, 68);
            this.lblTimeFeeValue.Text = "0 VNĐ";

            // Danh sách dịch vụ
            this.lblSvcCaption.AutoSize = true;
            this.lblSvcCaption.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSvcCaption.Location = new System.Drawing.Point(18, 98);
            this.lblSvcCaption.Text = "🍜  Dịch vụ đã gọi:";

            this.lstServices.Font = new System.Drawing.Font("Consolas", 9F);
            this.lstServices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstServices.Location = new System.Drawing.Point(18, 118);
            this.lstServices.Size = new System.Drawing.Size(364, 90);

            // Row: tổng dịch vụ
            this.lblSvcFeeCaption.AutoSize = true;
            this.lblSvcFeeCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSvcFeeCaption.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblSvcFeeCaption.Location = new System.Drawing.Point(18, 218);
            this.lblSvcFeeCaption.Text = "🛒  Tổng dịch vụ:";

            this.lblSvcFeeValue.AutoSize = true;
            this.lblSvcFeeValue.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblSvcFeeValue.Location = new System.Drawing.Point(230, 218);
            this.lblSvcFeeValue.Text = "0 VNĐ";

            // ── pnlTotal ───────────────────────────────────────────
            this.pnlTotal.BackColor = System.Drawing.Color.FromArgb(18, 40, 76);
            this.pnlTotal.Location = new System.Drawing.Point(18, 248);
            this.pnlTotal.Size = new System.Drawing.Size(364, 48);
            this.pnlTotal.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTotalCaption, this.lblTotalValue });

            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.ForeColor = System.Drawing.Color.White;
            this.lblTotalCaption.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalCaption.Location = new System.Drawing.Point(14, 12);
            this.lblTotalCaption.Text = "TỔNG CỘNG:";

            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.ForeColor = System.Drawing.Color.Gold;
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.Location = new System.Drawing.Point(200, 8);
            this.lblTotalValue.Text = "0 VNĐ";

            // ── grpPayment ─────────────────────────────────────────
            this.grpPayment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpPayment.Location = new System.Drawing.Point(18, 306);
            this.grpPayment.Size = new System.Drawing.Size(364, 52);
            this.grpPayment.Text = "💳  Phương thức thanh toán";
            this.grpPayment.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.rdoCash, this.rdoCard, this.rdoTransfer });

            this.rdoCash.AutoSize = true;
            this.rdoCash.Checked = true;
            this.rdoCash.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rdoCash.Location = new System.Drawing.Point(10, 22);
            this.rdoCash.Text = "💵 Tiền mặt";

            this.rdoCard.AutoSize = true;
            this.rdoCard.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rdoCard.Location = new System.Drawing.Point(110, 22);
            this.rdoCard.Text = "💳 Thẻ";

            this.rdoTransfer.AutoSize = true;
            this.rdoTransfer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rdoTransfer.Location = new System.Drawing.Point(200, 22);
            this.rdoTransfer.Text = "📲 Chuyển khoản";

            // ── Buttons ────────────────────────────────────────────
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(30, 150, 70);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.Location = new System.Drawing.Point(18, 370);
            this.btnConfirm.Size = new System.Drawing.Size(192, 42);
            this.btnConfirm.Text = "✔ Xác nhận thanh toán";
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);

            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Location = new System.Drawing.Point(218, 370);
            this.btnCancel.Size = new System.Drawing.Size(164, 42);
            this.btnCancel.Text = "✕ Huỷ";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderSize = 0;

            // ── Form ───────────────────────────────────────────────
            this.AcceptButton = this.btnConfirm;
            this.BackColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(420, 500);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thanh Toán";
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            this.pnlTotal.ResumeLayout(false);
            this.pnlTotal.PerformLayout();
            this.grpPayment.ResumeLayout(false);
            this.grpPayment.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlInfo;
        private System.Windows.Forms.Label lblPCName;
        private System.Windows.Forms.Label lblTimeCaption;
        private System.Windows.Forms.Label lblTimeValue;
        private System.Windows.Forms.Label lblTimeFeeCaption;
        private System.Windows.Forms.Label lblTimeFeeValue;
        private System.Windows.Forms.Label lblSvcCaption;
        private System.Windows.Forms.ListBox lstServices;
        private System.Windows.Forms.Label lblSvcFeeCaption;
        private System.Windows.Forms.Label lblSvcFeeValue;
        private System.Windows.Forms.Panel pnlTotal;
        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.GroupBox grpPayment;
        private System.Windows.Forms.RadioButton rdoCash;
        private System.Windows.Forms.RadioButton rdoCard;
        private System.Windows.Forms.RadioButton rdoTransfer;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}