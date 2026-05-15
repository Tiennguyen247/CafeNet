namespace CyberCafeManager
{
    partial class MainDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblRevenue = new System.Windows.Forms.Label();
            this.lblOnlineInfo = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnServices = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.lblSection = new System.Windows.Forms.Label();
            this.btnOpenPC = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.btnAddService = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvComputers = new System.Windows.Forms.DataGridView();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timerLive = new System.Windows.Forms.Timer(this.components);

            this.pnlHeader.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComputers)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ── pnlHeader ──────────────────────────────────────────
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(18, 40, 76);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 72;
            this.pnlHeader.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle, this.lblRevenue, this.lblOnlineInfo,
                this.lblDate, this.btnServices, this.btnReport });

            this.lblTitle.AutoSize = true;
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(12, 8);
            this.lblTitle.Text = "☕  NET CAFE";

            this.lblRevenue.AutoSize = true;
            this.lblRevenue.ForeColor = System.Drawing.Color.FromArgb(255, 200, 0);
            this.lblRevenue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRevenue.Location = new System.Drawing.Point(12, 44);
            this.lblRevenue.Text = "Doanh thu hôm nay: 0 VNĐ";

            this.lblOnlineInfo.AutoSize = true;
            this.lblOnlineInfo.ForeColor = System.Drawing.Color.FromArgb(150, 220, 150);
            this.lblOnlineInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOnlineInfo.Location = new System.Drawing.Point(320, 44);
            this.lblOnlineInfo.Text = "Máy đang dùng: 0/0";

            this.lblDate.AutoSize = true;
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(180, 200, 230);
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDate.Location = new System.Drawing.Point(560, 12);
            this.lblDate.Text = "---";

            this.btnServices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServices.BackColor = System.Drawing.Color.FromArgb(50, 80, 130);
            this.btnServices.ForeColor = System.Drawing.Color.White;
            this.btnServices.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnServices.Location = new System.Drawing.Point(820, 10);
            this.btnServices.Size = new System.Drawing.Size(110, 26);
            this.btnServices.Text = "⚙ Dịch Vụ";
            this.btnServices.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnServices.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 110, 170);
            this.btnServices.Click += new System.EventHandler(this.btnServices_Click);

            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.BackColor = System.Drawing.Color.FromArgb(50, 80, 130);
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnReport.Location = new System.Drawing.Point(820, 40);
            this.btnReport.Size = new System.Drawing.Size(110, 26);
            this.btnReport.Text = "📊 Báo Cáo";
            this.btnReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(80, 110, 170);
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);

            // ── pnlToolbar ─────────────────────────────────────────
            this.pnlToolbar.BackColor = System.Drawing.Color.FromArgb(230, 232, 240);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 44;
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.pnlToolbar.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSection, this.btnOpenPC, this.btnCheckout,
                this.btnAddService, this.btnRefresh });

            this.lblSection.AutoSize = true;
            this.lblSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSection.ForeColor = System.Drawing.Color.FromArgb(60, 70, 100);
            this.lblSection.Location = new System.Drawing.Point(8, 14);
            this.lblSection.Text = "DANH SÁCH MÁY TÍNH";

            this.btnOpenPC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenPC.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.btnOpenPC.ForeColor = System.Drawing.Color.White;
            this.btnOpenPC.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnOpenPC.Location = new System.Drawing.Point(200, 8);
            this.btnOpenPC.Size = new System.Drawing.Size(100, 28);
            this.btnOpenPC.Text = "▶ Mở Máy";
            this.btnOpenPC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpenPC.FlatAppearance.BorderSize = 0;
            this.btnOpenPC.Click += new System.EventHandler(this.btnOpenPC_Click);

            this.btnCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckout.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.btnCheckout.ForeColor = System.Drawing.Color.White;
            this.btnCheckout.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnCheckout.Location = new System.Drawing.Point(308, 8);
            this.btnCheckout.Size = new System.Drawing.Size(100, 28);
            this.btnCheckout.Text = "💰 Tính Tiền";
            this.btnCheckout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckout.FlatAppearance.BorderSize = 0;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);

            this.btnAddService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddService.BackColor = System.Drawing.Color.FromArgb(40, 150, 80);
            this.btnAddService.ForeColor = System.Drawing.Color.White;
            this.btnAddService.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnAddService.Location = new System.Drawing.Point(416, 8);
            this.btnAddService.Size = new System.Drawing.Size(110, 28);
            this.btnAddService.Text = "🍜 Gọi Dịch Vụ";
            this.btnAddService.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddService.FlatAppearance.BorderSize = 0;
            this.btnAddService.Click += new System.EventHandler(this.btnAddService_Click);

            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(200, 210, 230);
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnRefresh.Location = new System.Drawing.Point(880, 8);
            this.btnRefresh.Size = new System.Drawing.Size(90, 28);
            this.btnRefresh.Text = "🔄 Tải lại";
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // ── dgvComputers ───────────────────────────────────────
            // Dùng Rows.Add() trong code nên AutoGenerateColumns phải false
            this.dgvComputers.AllowUserToAddRows = false;
            this.dgvComputers.AllowUserToDeleteRows = false;
            this.dgvComputers.AutoGenerateColumns = false;
            this.dgvComputers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvComputers.BackgroundColor = System.Drawing.Color.White;
            this.dgvComputers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvComputers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvComputers.ColumnHeadersHeight = 36;
            this.dgvComputers.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(18, 40, 76);
            this.dgvComputers.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvComputers.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvComputers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colName, this.colStatus, this.colTime,
                this.colFee,  this.colRate,   this.colVip, this.colNotes });
            this.dgvComputers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvComputers.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dgvComputers.MultiSelect = false;
            this.dgvComputers.ReadOnly = true;
            this.dgvComputers.RowHeadersVisible = false;
            this.dgvComputers.RowTemplate.Height = 42;
            this.dgvComputers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvComputers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvComputers_CellDoubleClick);

            this.colName.HeaderText = "Tên máy"; this.colName.Name = "colName"; this.colName.FillWeight = 12;
            this.colStatus.HeaderText = "Trạng thái"; this.colStatus.Name = "colStatus"; this.colStatus.FillWeight = 15;
            this.colTime.HeaderText = "Thời gian"; this.colTime.Name = "colTime"; this.colTime.FillWeight = 15;
            this.colFee.HeaderText = "Tiền hiện tại"; this.colFee.Name = "colFee"; this.colFee.FillWeight = 16;
            this.colRate.HeaderText = "Giá/giờ"; this.colRate.Name = "colRate"; this.colRate.FillWeight = 13;
            this.colVip.HeaderText = "Loại"; this.colVip.Name = "colVip"; this.colVip.FillWeight = 10;
            this.colNotes.HeaderText = "Ghi chú"; this.colNotes.Name = "colNotes"; this.colNotes.FillWeight = 19;

            // ── pnlFooter ──────────────────────────────────────────
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(218, 220, 230);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 34;
            this.pnlFooter.Controls.Add(this.lblStatus);

            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(40, 100, 40);
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Location = new System.Drawing.Point(10, 9);
            this.lblStatus.Text = "✔ Sẵn sàng";

            // ── timerLive ──────────────────────────────────────────
            this.timerLive.Interval = 1000;
            this.timerLive.Tick += new System.EventHandler(this.timerLive_Tick);

            // ── Form ───────────────────────────────────────────────
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Size = new System.Drawing.Size(1024, 660);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Net Cafe";
            this.Controls.Add(this.dgvComputers);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFooter);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComputers)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblRevenue;
        private System.Windows.Forms.Label lblOnlineInfo;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnServices;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Panel pnlToolbar;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Button btnOpenPC;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvComputers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFee;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVip;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNotes;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timerLive;
    }
}