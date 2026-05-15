using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public partial class ServiceForm : Form
    {
        private Computer _currentPC;
        private bool IsOrderMode => _currentPC != null;
        private DatabaseHelper db = DatabaseHelper.Instance;
        private int _editingId = -1;

        public ServiceForm() : this(null) { }

        public ServiceForm(Computer pc)
        {
            _currentPC = pc;
            InitializeComponent();
            ApplyMode();
            LoadServices();
        }

        private void ApplyMode()
        {
            if (IsOrderMode)
            {
                lblHeader.Text = $"🍜  GỌI ĐỒ ĂN — {_currentPC.Name}";
                pnlHeader.BackColor = Color.FromArgb(40, 120, 60);
                this.Text = $"Gọi dịch vụ: {_currentPC.Name}";
                txtName.ReadOnly = true;
                txtPrice.ReadOnly = true;
                txtStock.ReadOnly = true;
                txtName.BackColor = Color.FromArgb(240, 240, 240);
                txtPrice.BackColor = Color.FromArgb(240, 240, 240);
                txtStock.BackColor = Color.FromArgb(240, 240, 240);
                btnSave.Visible = false;
                btnCancel.Visible = false;
                btnAdd.Visible = false;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                lblQtyCaption.Visible = true;
                nudQuantity.Visible = true;
                btnAddToPC.Visible = true;
                btnClose.Visible = true;
                btnAddToPC.Text = $"➕ Thêm vào {_currentPC.Name}";
            }
            else
            {
                lblQtyCaption.Visible = false;
                nudQuantity.Visible = false;
                btnAddToPC.Visible = false;
                btnClose.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = true;
                btnAdd.Visible = true;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
        }

        private void LoadServices(string search = "")
        {
            try
            {
                string sql = @"
                    SELECT ServiceID, ServiceName, Price, Stock
                    FROM Services
                    WHERE IsActive = 1
                      AND (@search = '' OR ServiceName LIKE '%' + @search + '%')
                    ORDER BY ServiceName";

                DataTable dt = db.ExecuteQuery(sql, new[]
                {
                    new SqlParameter("@search", search)
                });

                dgvServices.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dịch vụ: " + ex.Message);
            }
        }

        // ═══════════════════════════════════════════════════════════
        // GIẢI PHÁP GỐC RỄ: Đọc qua DataBoundItem thay vì tên cột
        // → Không bao giờ bị lỗi dù Designer đổi Name của cột DGV
        // ═══════════════════════════════════════════════════════════
        private DataRowView GetSelectedRow()
        {
            if (dgvServices.SelectedRows.Count == 0) return null;
            var gridRow = dgvServices.SelectedRows[0];
            if (gridRow.IsNewRow) return null;
            return gridRow.DataBoundItem as DataRowView;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadServices(txtSearch.Text.Trim());
        }

        private void dgvServices_SelectionChanged(object sender, EventArgs e)
        {
            var drv = GetSelectedRow();
            if (drv == null) return;

            // Đọc từ DataRow — luôn đúng, không phụ thuộc tên cột DGV
            txtName.Text = drv["ServiceName"]?.ToString() ?? "";
            txtPrice.Text = drv["Price"]?.ToString() ?? "";
            txtStock.Text = drv["Stock"]?.ToString() ?? "";
            lblError.Text = "";
        }

        // Handler trống: Designer có thể tự sinh event này khi kéo thả
        private void dgvServices_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        // ── ORDER MODE ────────────────────────────────────────────
        private void btnAddToPC_Click(object sender, EventArgs e)
        {
            var drv = GetSelectedRow();
            if (drv == null)
            {
                lblError.Text = "Vui lòng chọn dịch vụ!";
                return;
            }

            int svcId = Convert.ToInt32(drv["ServiceID"]);
            int stock = Convert.ToInt32(drv["Stock"]);
            int qty = (int)nudQuantity.Value;
            decimal unit = Convert.ToDecimal(drv["Price"]);
            string name = drv["ServiceName"]?.ToString() ?? "";

            if (stock < qty)
            {
                lblError.Text = $"Không đủ hàng! Tồn kho: {stock}";
                return;
            }

            try
            {
                db.ExecuteNonQuery(
                    "INSERT INTO OrderItems (PCID, ServiceID, Quantity, UnitPrice) VALUES (@pcId,@sid,@qty,@price)",
                    new[]
                    {
                        new SqlParameter("@pcId",  _currentPC.ID),
                        new SqlParameter("@sid",   svcId),
                        new SqlParameter("@qty",   qty),
                        new SqlParameter("@price", unit)
                    });

                db.ExecuteNonQuery(
                    "UPDATE Services SET Stock = Stock - @qty WHERE ServiceID = @sid",
                    new[]
                    {
                        new SqlParameter("@qty", qty),
                        new SqlParameter("@sid", svcId)
                    });

                MessageBox.Show(
                    $"✔ Đã thêm: {name} x{qty} = {unit * qty:N0} VNĐ\nvào {_currentPC.Name}",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                nudQuantity.Value = 1;
                lblError.Text = "";
                LoadServices();
            }
            catch (Exception ex) { lblError.Text = "Lỗi: " + ex.Message; }
        }

        // ── CRUD MODE ─────────────────────────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            _editingId = -1;
            lblError.Text = "Nhập thông tin rồi bấm Lưu";
            lblError.ForeColor = Color.FromArgb(0, 80, 180);
            txtName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var drv = GetSelectedRow();
            if (drv == null) { lblError.Text = "Chọn dịch vụ cần sửa!"; return; }
            _editingId = Convert.ToInt32(drv["ServiceID"]);
            lblError.Text = "Sửa thông tin rồi bấm Lưu";
            lblError.ForeColor = Color.FromArgb(0, 80, 180);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { lblError.Text = "Nhập tên dịch vụ!"; return; }
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            { lblError.Text = "Đơn giá không hợp lệ!"; return; }
            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0) stock = 0;

            try
            {
                if (_editingId == -1)
                {
                    db.ExecuteNonQuery(
                        "INSERT INTO Services (ServiceName, Price, Stock) VALUES (@n,@p,@s)",
                        new[]
                        {
                            new SqlParameter("@n", txtName.Text.Trim()),
                            new SqlParameter("@p", price),
                            new SqlParameter("@s", stock)
                        });
                    MessageBox.Show("✔ Đã thêm dịch vụ mới!");
                }
                else
                {
                    db.ExecuteNonQuery(
                        "UPDATE Services SET ServiceName=@n, Price=@p, Stock=@s WHERE ServiceID=@id",
                        new[]
                        {
                            new SqlParameter("@n",  txtName.Text.Trim()),
                            new SqlParameter("@p",  price),
                            new SqlParameter("@s",  stock),
                            new SqlParameter("@id", _editingId)
                        });
                    MessageBox.Show("✔ Đã cập nhật!");
                }
                ClearForm();
                LoadServices();
            }
            catch (Exception ex) { lblError.Text = "Lỗi: " + ex.Message; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            dgvServices.ClearSelection();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var drv = GetSelectedRow();
            if (drv == null) { lblError.Text = "Chọn dịch vụ cần xoá!"; return; }

            int id = Convert.ToInt32(drv["ServiceID"]);
            string nm = drv["ServiceName"]?.ToString() ?? "";

            if (MessageBox.Show($"Xoá '{nm}'?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                db.ExecuteNonQuery("UPDATE Services SET IsActive=0 WHERE ServiceID=@id",
                    new[] { new SqlParameter("@id", id) });
                MessageBox.Show("✔ Đã xoá!");
                ClearForm();
                LoadServices();
            }
            catch (Exception ex) { lblError.Text = "Lỗi: " + ex.Message; }
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtPrice.Text = "";
            txtStock.Text = "";
            lblError.Text = "";
            lblError.ForeColor = Color.Crimson;
            _editingId = -1;
        }
    }
}