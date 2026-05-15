namespace CyberCafeManager
{
    partial class ReportForm
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
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnLoad = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabRevenue = new System.Windows.Forms.TabPage();
            this.dgvRevenue = new System.Windows.Forms.DataGridView();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSessions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRevenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblTotalSessions = new System.Windows.Forms.Label();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.tabServices = new System.Windows.Forms.TabPage();
            this.dgvServices = new System.Windows.Forms.DataGridView();
            this.colSvcName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSvcRevenue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlSvcSummary = new System.Windows.Forms.Panel();
            this.lblSvcTotal = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabRevenue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenue)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.tabServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).BeginInit();
            this.pnlSvcSummary.SuspendLayout();
            this.SuspendLayout();

            // ── pnlHeader ──────────────────────────────────────────
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(18, 40, 76);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 50;
            this.pnlHeader.Controls.Add(this.lblHeader);

            this.lblHeader.AutoSize = false;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHeader.Text = "📊  BÁO CÁO TÀI CHÍNH";

            // ── pnlFilter ──────────────────────────────────────────
            this.pnlFilter.BackColor = System.Drawing.Color.White;
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Height = 44;
            this.pnlFilter.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblFrom, this.dtpFrom, this.lblTo, this.dtpTo, this.btnLoad });

            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFrom.Location = new System.Drawing.Point(10, 13);
            this.lblFrom.Text = "Từ ngày:";

            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpFrom.Location = new System.Drawing.Point(72, 10);
            this.dtpFrom.Size = new System.Drawing.Size(112, 24);

            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTo.Location = new System.Drawing.Point(196, 13);
            this.lblTo.Text = "Đến ngày:";

            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTo.Location = new System.Drawing.Point(268, 10);
            this.dtpTo.Size = new System.Drawing.Size(112, 24);

            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.BackColor = System.Drawing.Color.FromArgb(18, 40, 76);
            this.btnLoad.ForeColor = System.Drawing.Color.White;
            this.btnLoad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLoad.Location = new System.Drawing.Point(392, 8);
            this.btnLoad.Size = new System.Drawing.Size(120, 28);
            this.btnLoad.Text = "🔍 Xem báo cáo";
            this.btnLoad.FlatAppearance.BorderSize = 0;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);

            // ── tabControl ─────────────────────────────────────────
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl.TabPages.AddRange(new System.Windows.Forms.TabPage[] {
                this.tabRevenue, this.tabServices });

            // ── tabRevenue ─────────────────────────────────────────
            this.tabRevenue.BackColor = System.Drawing.Color.White;
            this.tabRevenue.Text = "📅  Doanh thu theo ngày";
            this.tabRevenue.Controls.Add(this.dgvRevenue);
            this.tabRevenue.Controls.Add(this.pnlSummary);

            // FIX: AutoGenerateColumns = false
            this.dgvRevenue.AllowUserToAddRows = false;
            this.dgvRevenue.AllowUserToDeleteRows = false;
            this.dgvRevenue.AutoGenerateColumns = false;
            this.dgvRevenue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRevenue.BackgroundColor = System.Drawing.Color.White;
            this.dgvRevenue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRevenue.ColumnHeadersHeight = 34;
            this.dgvRevenue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRevenue.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(50, 70, 110);
            this.dgvRevenue.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvRevenue.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvRevenue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colDate, this.colSessions, this.colRevenue });
            this.dgvRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRevenue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvRevenue.ReadOnly = true;
            this.dgvRevenue.RowHeadersVisible = false;
            this.dgvRevenue.RowTemplate.Height = 36;
            this.dgvRevenue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // DataPropertyName khớp với DataTable tạo trong LoadTabRevenue()
            // display.Columns: "Ngày", "Số phiên", "Doanh thu (VNĐ)"
            this.colDate.HeaderText = "Ngày";
            this.colDate.Name = "colDate";
            this.colDate.DataPropertyName = "Ngày";
            this.colDate.FillWeight = 30;

            this.colSessions.HeaderText = "Số phiên";
            this.colSessions.Name = "colSessions";
            this.colSessions.DataPropertyName = "Số phiên";
            this.colSessions.FillWeight = 25;

            this.colRevenue.HeaderText = "Doanh thu (VNĐ)";
            this.colRevenue.Name = "colRevenue";
            this.colRevenue.DataPropertyName = "Doanh thu (VNĐ)";
            this.colRevenue.FillWeight = 45;

            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(235, 240, 255);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummary.Height = 36;
            this.pnlSummary.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTotalSessions, this.lblTotalRevenue });

            this.lblTotalSessions.AutoSize = true;
            this.lblTotalSessions.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotalSessions.Location = new System.Drawing.Point(12, 10);
            this.lblTotalSessions.Text = "Tổng phiên: 0";

            this.lblTotalRevenue.AutoSize = true;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.FromArgb(18, 40, 76);
            this.lblTotalRevenue.Location = new System.Drawing.Point(200, 10);
            this.lblTotalRevenue.Text = "Tổng doanh thu: 0 VNĐ";

            // ── tabServices ────────────────────────────────────────
            this.tabServices.BackColor = System.Drawing.Color.White;
            this.tabServices.Text = "🍜  Thống kê dịch vụ";
            this.tabServices.Controls.Add(this.dgvServices);
            this.tabServices.Controls.Add(this.pnlSvcSummary);

            // FIX: AutoGenerateColumns = false
            this.dgvServices.AllowUserToAddRows = false;
            this.dgvServices.AllowUserToDeleteRows = false;
            this.dgvServices.AutoGenerateColumns = false;
            this.dgvServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServices.BackgroundColor = System.Drawing.Color.White;
            this.dgvServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvServices.ColumnHeadersHeight = 34;
            this.dgvServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvServices.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(40, 120, 60);
            this.dgvServices.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvServices.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvServices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colSvcName, this.colQty, this.colSvcRevenue });
            this.dgvServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServices.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvServices.ReadOnly = true;
            this.dgvServices.RowHeadersVisible = false;
            this.dgvServices.RowTemplate.Height = 36;
            this.dgvServices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;

            // DataPropertyName khớp với DataTable tạo trong LoadTabServices()
            // display.Columns: "Tên dịch vụ", "Số lượng bán", "Doanh thu (VNĐ)"
            this.colSvcName.HeaderText = "Tên dịch vụ";
            this.colSvcName.Name = "colSvcName";
            this.colSvcName.DataPropertyName = "Tên dịch vụ";
            this.colSvcName.FillWeight = 45;

            this.colQty.HeaderText = "Số lượng bán";
            this.colQty.Name = "colQty";
            this.colQty.DataPropertyName = "Số lượng bán";
            this.colQty.FillWeight = 25;

            this.colSvcRevenue.HeaderText = "Doanh thu (VNĐ)";
            this.colSvcRevenue.Name = "colSvcRevenue";
            this.colSvcRevenue.DataPropertyName = "Doanh thu (VNĐ)";
            this.colSvcRevenue.FillWeight = 30;

            this.pnlSvcSummary.BackColor = System.Drawing.Color.FromArgb(235, 248, 235);
            this.pnlSvcSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSvcSummary.Height = 36;
            this.pnlSvcSummary.Controls.Add(this.lblSvcTotal);

            this.lblSvcTotal.AutoSize = true;
            this.lblSvcTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSvcTotal.ForeColor = System.Drawing.Color.FromArgb(40, 120, 60);
            this.lblSvcTotal.Location = new System.Drawing.Point(12, 10);
            this.lblSvcTotal.Text = "Tổng doanh thu từ dịch vụ: 0 VNĐ";

            // ── Form ───────────────────────────────────────────────
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimizeBox = false;
            this.Size = new System.Drawing.Size(700, 520);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Báo cáo tài chính";
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabRevenue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRevenue)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.tabServices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).EndInit();
            this.pnlSvcSummary.ResumeLayout(false);
            this.pnlSvcSummary.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabRevenue;
        private System.Windows.Forms.DataGridView dgvRevenue;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSessions;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRevenue;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblTotalSessions;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.TabPage tabServices;
        private System.Windows.Forms.DataGridView dgvServices;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSvcName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSvcRevenue;
        private System.Windows.Forms.Panel pnlSvcSummary;
        private System.Windows.Forms.Label lblSvcTotal;
    }
}