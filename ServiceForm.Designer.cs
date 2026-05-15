namespace CyberCafeManager
{
    partial class ServiceForm
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
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.dgvServices = new System.Windows.Forms.DataGridView();
            this.colServiceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStock = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // ── Right panel dùng TableLayoutPanel để responsive ────
            this.tblRight = new System.Windows.Forms.TableLayoutPanel();
            this.lblNameCaption = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPriceCaption = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblStockCaption = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lblQtyCaption = new System.Windows.Forms.Label();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnAddToPC = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).BeginInit();
            this.tblRight.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.SuspendLayout();

            // ── pnlHeader ──────────────────────────────────────────
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(18, 40, 76);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 52;
            this.pnlHeader.Controls.Add(this.lblHeader);

            this.lblHeader.AutoSize = false;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHeader.Text = "🛒  QUẢN LÝ DỊCH VỤ";

            // ── pnlSearch ──────────────────────────────────────────
            this.pnlSearch.BackColor = System.Drawing.Color.White;
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Height = 40;
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.pnlSearch.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblSearch, this.txtSearch });

            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSearch.Location = new System.Drawing.Point(10, 12);
            this.lblSearch.Text = "🔍 Tìm kiếm:";

            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearch.Location = new System.Drawing.Point(90, 9);
            this.txtSearch.Size = new System.Drawing.Size(240, 23);
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left
                                  | System.Windows.Forms.AnchorStyles.Top;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // ── splitMain ──────────────────────────────────────────
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.SplitterDistance = 500;
            this.splitMain.Panel1.Controls.Add(this.dgvServices);
            this.splitMain.Panel2.Controls.Add(this.tblRight);

            // ── dgvServices ────────────────────────────────────────
            this.dgvServices.AllowUserToAddRows = false;
            this.dgvServices.AllowUserToDeleteRows = false;
            this.dgvServices.AutoGenerateColumns = false;
            this.dgvServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvServices.BackgroundColor = System.Drawing.Color.White;
            this.dgvServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvServices.ColumnHeadersHeight = 34;
            this.dgvServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvServices.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(50, 70, 110);
            this.dgvServices.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvServices.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.dgvServices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colServiceID, this.colServiceName, this.colPrice, this.colStock });
            this.dgvServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvServices.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dgvServices.MultiSelect = false;
            this.dgvServices.ReadOnly = true;
            this.dgvServices.RowHeadersVisible = false;
            this.dgvServices.RowTemplate.Height = 36;
            this.dgvServices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServices.SelectionChanged += new System.EventHandler(this.dgvServices_SelectionChanged);
            this.dgvServices.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvServices_CellContentClick);

            this.colServiceID.HeaderText = "ID";
            this.colServiceID.Name = "colServiceID";
            this.colServiceID.DataPropertyName = "ServiceID";
            this.colServiceID.Visible = false;

            this.colServiceName.HeaderText = "Tên dịch vụ";
            this.colServiceName.Name = "colServiceName";
            this.colServiceName.DataPropertyName = "ServiceName";
            this.colServiceName.FillWeight = 50;

            this.colPrice.HeaderText = "Đơn giá (VNĐ)";
            this.colPrice.Name = "colPrice";
            this.colPrice.DataPropertyName = "Price";
            this.colPrice.DefaultCellStyle.Format = "N0";
            this.colPrice.FillWeight = 28;

            this.colStock.HeaderText = "Tồn kho";
            this.colStock.Name = "colStock";
            this.colStock.DataPropertyName = "Stock";
            this.colStock.FillWeight = 22;

            // ══════════════════════════════════════════════════════
            // tblRight — TableLayoutPanel: 1 cột, các hàng tự co giãn
            // Row 0: label Tên        Row 1: txtName
            // Row 2: label Giá        Row 3: txtPrice
            // Row 4: label Tồn kho    Row 5: txtStock
            // Row 6: lblError
            // Row 7: pnlButtons (fill)
            // ══════════════════════════════════════════════════════
            this.tblRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRight.BackColor = System.Drawing.Color.White;
            this.tblRight.Padding = new System.Windows.Forms.Padding(10, 10, 10, 10);
            this.tblRight.ColumnCount = 1;
            this.tblRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(
                System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRight.RowCount = 8;
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));   // 0 label
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));   // 1 txt
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));   // 2 label
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));   // 3 txt
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));   // 4 label
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));   // 5 txt
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));   // 6 error
            this.tblRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F)); // 7 buttons fill

            // Label Tên dịch vụ
            this.lblNameCaption.AutoSize = true;
            this.lblNameCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNameCaption.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblNameCaption.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblNameCaption.Text = "Tên dịch vụ:";
            this.tblRight.Controls.Add(this.lblNameCaption, 0, 0);

            // TextBox Tên
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtName.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.tblRight.Controls.Add(this.txtName, 0, 1);

            // Label Đơn giá
            this.lblPriceCaption.AutoSize = true;
            this.lblPriceCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPriceCaption.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblPriceCaption.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblPriceCaption.Text = "Đơn giá (VNĐ):";
            this.tblRight.Controls.Add(this.lblPriceCaption, 0, 2);

            // TextBox Giá
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrice.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.tblRight.Controls.Add(this.txtPrice, 0, 3);

            // Label Tồn kho
            this.lblStockCaption.AutoSize = true;
            this.lblStockCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStockCaption.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblStockCaption.Margin = new System.Windows.Forms.Padding(0, 8, 0, 2);
            this.lblStockCaption.Text = "Tồn kho:";
            this.tblRight.Controls.Add(this.lblStockCaption, 0, 4);

            // TextBox Tồn kho
            this.txtStock.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStock.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.tblRight.Controls.Add(this.txtStock, 0, 5);

            // Label lỗi
            this.lblError.AutoSize = false;
            this.lblError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblError.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblError.ForeColor = System.Drawing.Color.Crimson;
            this.lblError.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.lblError.MinimumSize = new System.Drawing.Size(0, 28);
            this.tblRight.Controls.Add(this.lblError, 0, 6);

            // pnlButtons — chứa toàn bộ nút, Dock=Fill để chiếm phần còn lại
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtons.BackColor = System.Drawing.Color.White;
            this.tblRight.Controls.Add(this.pnlButtons, 0, 7);

            // ── Các nút bên trong pnlButtons ──────────────────────
            // Dùng Anchor Top|Left|Right để nút luôn co giãn theo chiều ngang

            // btnSave + btnCancel (cùng hàng — dùng FlowLayoutPanel phụ)
            var flowTop = new System.Windows.Forms.FlowLayoutPanel();
            flowTop.Dock = System.Windows.Forms.DockStyle.Top;
            flowTop.AutoSize = true;
            flowTop.Margin = new System.Windows.Forms.Padding(0);
            flowTop.Padding = new System.Windows.Forms.Padding(0);
            flowTop.BackColor = System.Drawing.Color.White;

            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.Size = new System.Drawing.Size(120, 34);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0, 0, 4, 6);
            this.btnSave.Text = "💾 Lưu";
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(160, 50, 50);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Size = new System.Drawing.Size(100, 34);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnCancel.Text = "✕ Huỷ";
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            flowTop.Controls.AddRange(new System.Windows.Forms.Control[] { this.btnSave, this.btnCancel });

            // btnAdd, btnEdit, btnDelete — mỗi nút 1 hàng, Dock Fill
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(40, 140, 60);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAdd.Height = 34;
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnAdd.Text = "➕ Thêm mới";
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(180, 120, 0);
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEdit.Height = 34;
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btnEdit.Text = "✏️ Sửa";
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);

            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDelete.Height = 34;
            this.btnDelete.Text = "🗑️ Xoá";
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // Order mode: lblQtyCaption + nudQuantity (FlowPanel)
            var flowQty = new System.Windows.Forms.FlowLayoutPanel();
            flowQty.Dock = System.Windows.Forms.DockStyle.Top;
            flowQty.AutoSize = true;
            flowQty.BackColor = System.Drawing.Color.White;
            flowQty.Margin = new System.Windows.Forms.Padding(0);
            flowQty.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);

            this.lblQtyCaption.AutoSize = true;
            this.lblQtyCaption.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblQtyCaption.Margin = new System.Windows.Forms.Padding(0, 6, 8, 0);
            this.lblQtyCaption.Text = "Số lượng:";

            this.nudQuantity.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nudQuantity.Minimum = 1;
            this.nudQuantity.Maximum = 99;
            this.nudQuantity.Value = 1;
            this.nudQuantity.Size = new System.Drawing.Size(80, 26);

            flowQty.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblQtyCaption, this.nudQuantity });

            this.btnAddToPC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToPC.BackColor = System.Drawing.Color.FromArgb(40, 130, 60);
            this.btnAddToPC.ForeColor = System.Drawing.Color.White;
            this.btnAddToPC.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnAddToPC.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddToPC.Height = 40;
            this.btnAddToPC.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.btnAddToPC.Text = "➕ Thêm vào máy";
            this.btnAddToPC.FlatAppearance.BorderSize = 0;
            this.btnAddToPC.Click += new System.EventHandler(this.btnAddToPC_Click);

            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(140, 140, 150);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClose.Height = 34;
            this.btnClose.Text = "✕ Đóng";
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.FlatAppearance.BorderSize = 0;

            // Thứ tự Add vào pnlButtons — Dock.Top xếp ngược từ dưới lên
            // Nên add theo thứ tự ngược để hiển thị đúng
            this.pnlButtons.Controls.Add(this.btnDelete);   // bottom-most → add first with DockTop stacking
            this.pnlButtons.Controls.Add(this.btnEdit);
            this.pnlButtons.Controls.Add(this.btnAdd);
            this.pnlButtons.Controls.Add(flowTop);
            // Order mode
            this.pnlButtons.Controls.Add(this.btnClose);
            this.pnlButtons.Controls.Add(this.btnAddToPC);
            this.pnlButtons.Controls.Add(flowQty);

            // ── Form ───────────────────────────────────────────────
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Size = new System.Drawing.Size(860, 560);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý Dịch Vụ";
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlHeader);

            this.pnlHeader.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlSearch.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServices)).EndInit();
            this.tblRight.ResumeLayout(false);
            this.tblRight.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.ResumeLayout(false);
        }

        // ── Fields ─────────────────────────────────────────────────
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.DataGridView dgvServices;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServiceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServiceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStock;
        private System.Windows.Forms.TableLayoutPanel tblRight;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Label lblNameCaption;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPriceCaption;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblStockCaption;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblQtyCaption;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Button btnAddToPC;
        private System.Windows.Forms.Button btnClose;
    }
}