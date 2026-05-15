// ============================================================
// MainDashboard.cs
// ============================================================

using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public partial class MainDashboard : Form
    {
        private DatabaseHelper db = DatabaseHelper.Instance;
        private System.Collections.Generic.List<Computer> _computers
            = new System.Collections.Generic.List<Computer>();

        public MainDashboard()
        {
            InitializeComponent();
            LoadComputers();
            timerLive.Start();
        }

        private void LoadComputers()
        {
            try
            {
                _computers.Clear();
                dgvComputers.Rows.Clear();

                DataTable dt = db.GetAllComputers();

                foreach (DataRow row in dt.Rows)
                {
                    bool isVip = Convert.ToBoolean(row["IsVip"]);
                    Computer pc = isVip ? new VipComputer() : new Computer();

                    pc.ID = Convert.ToInt32(row["PCID"]);
                    pc.Name = row["PCName"].ToString();
                    pc.HourlyRate = Convert.ToDecimal(row["HourlyRate"]);
                    pc.Notes = row["Notes"] != DBNull.Value ? row["Notes"].ToString() : "";

                    int status = Convert.ToInt32(row["Status"]);
                    if (status == 1 && row["StartTime"] != DBNull.Value)
                    {
                        pc.Status = PCStatus.InUse;
                        pc.StartTime = Convert.ToDateTime(row["StartTime"]);

                        DataTable orders = db.GetUnpaidOrders(pc.ID);
                        foreach (DataRow o in orders.Rows)
                            pc.Orders.Add(new OrderItem
                            {
                                ItemID = Convert.ToInt32(o["ItemID"]),
                                ServiceID = Convert.ToInt32(o["ServiceID"]),
                                ServiceName = o["ServiceName"].ToString(),
                                UnitPrice = Convert.ToDecimal(o["UnitPrice"]),
                                Quantity = Convert.ToInt32(o["Quantity"])
                            });
                    }

                    _computers.Add(pc);

                    int idx = dgvComputers.Rows.Add(
                        pc.Name,
                        pc.Status == PCStatus.InUse ? "● ĐANG DÙNG" : "● TRỐNG",
                        pc.GetElapsedTime(),
                        pc.CalculateTotalFee().ToString("N0") + " VNĐ",
                        pc.HourlyRate.ToString("N0") + " VNĐ/h",
                        pc.IsVip ? "⭐ VIP" : "Thường",
                        pc.Notes);

                    ColorRow(dgvComputers.Rows[idx], pc.Status);
                }

                UpdateHeader();
                lblDate.Text = SimulatedClock.DisplayDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu:\n" + ex.Message,
                    "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ColorRow(DataGridViewRow row, PCStatus status)
        {
            if (status == PCStatus.InUse)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
                row.DefaultCellStyle.ForeColor = Color.FromArgb(140, 20, 20);
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(235, 248, 235);
                row.DefaultCellStyle.ForeColor = Color.FromArgb(20, 100, 20);
            }
        }

        private void timerLive_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < _computers.Count && i < dgvComputers.Rows.Count; i++)
            {
                var pc = _computers[i];
                var row = dgvComputers.Rows[i];
                row.Cells["colTime"].Value = pc.GetElapsedTime();
                row.Cells["colFee"].Value = pc.CalculateTotalFee().ToString("N0") + " VNĐ";
                row.Cells["colStatus"].Value = pc.Status == PCStatus.InUse
                                               ? "● ĐANG DÙNG" : "● TRỐNG";
            }
            UpdateHeader();
            lblDate.Text = SimulatedClock.DisplayDate;
        }

        private Computer GetSelectedPC()
        {
            if (dgvComputers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một máy.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
            int idx = dgvComputers.SelectedRows[0].Index;
            return idx < _computers.Count ? _computers[idx] : null;
        }

        private void btnOpenPC_Click(object sender, EventArgs e)
        {
            var pc = GetSelectedPC();
            if (pc == null) return;

            if (pc.Status == PCStatus.InUse)
            {
                MessageBox.Show("Máy đang được sử dụng.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Mở máy {pc.Name}?\nGiá: {pc.HourlyRate:N0} VNĐ/giờ",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                db.ExecuteNonQuery(
                    "UPDATE Computers SET Status = 1, StartTime = @now WHERE PCID = @id",
                    new[]
                    {
                        new SqlParameter("@now", SimulatedClock.Now),
                        new SqlParameter("@id",  pc.ID)
                    });
                pc.StartSession();

                int idx = _computers.IndexOf(pc);
                if (idx >= 0) ColorRow(dgvComputers.Rows[idx], pc.Status);

                UpdateHeader();
                SetStatus($"✔ Đã mở máy {pc.Name}");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            var pc = GetSelectedPC();
            if (pc == null) return;

            if (pc.Status != PCStatus.InUse)
            {
                MessageBox.Show("Máy chưa mở.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var form = new CheckoutForm(pc);
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    db.ExecuteNonQuery(
                        "UPDATE OrderItems SET IsPaid = 1 WHERE PCID = @id AND IsPaid = 0",
                        new[] { new SqlParameter("@id", pc.ID) });
                    db.ExecuteNonQuery(
                        "UPDATE Computers SET Status = 0, StartTime = NULL WHERE PCID = @id",
                        new[] { new SqlParameter("@id", pc.ID) });
                    pc.EndSession();

                    int idx = _computers.IndexOf(pc);
                    if (idx >= 0) ColorRow(dgvComputers.Rows[idx], pc.Status);

                    UpdateHeader();
                    SetStatus($"✔ Đã thanh toán máy {pc.Name}");
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            var pc = GetSelectedPC();
            if (pc == null) return;

            if (pc.Status != PCStatus.InUse)
            {
                MessageBox.Show("Máy chưa mở.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            new ServiceForm(pc).ShowDialog();

            DataTable orders = db.GetUnpaidOrders(pc.ID);
            pc.Orders.Clear();
            foreach (DataRow row in orders.Rows)
                pc.Orders.Add(new OrderItem
                {
                    ItemID = Convert.ToInt32(row["ItemID"]),
                    ServiceID = Convert.ToInt32(row["ServiceID"]),
                    ServiceName = row["ServiceName"].ToString(),
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                    Quantity = Convert.ToInt32(row["Quantity"])
                });
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            foreach (var pc in _computers)
            {
                if (pc.Status == PCStatus.InUse && pc.StartTime != null)
                {
                    decimal timeFee = pc.CalculateTimeFee();
                    decimal serviceFee = pc.GetServiceTotal();
                    decimal total = timeFee + serviceFee;

                    db.ExecuteNonQuery(
                        @"INSERT INTO Transactions
                            (PCID, TimeFee, ServiceFee, TotalAmount, Note, PaymentMethod, CheckoutTime)
                          VALUES
                            (@pcId, @timeFee, @serviceFee, @total, @note, N'Tự động', @checkoutTime)",
                        new[]
                        {
                            new SqlParameter("@pcId",         pc.ID),
                            new SqlParameter("@timeFee",      timeFee),
                            new SqlParameter("@serviceFee",   serviceFee),
                            new SqlParameter("@total",        total),
                            new SqlParameter("@checkoutTime", SimulatedClock.Now),
                            new SqlParameter("@note",
                                $"Auto-checkout {pc.Name} cuối ngày {SimulatedClock.Now:dd/MM/yyyy}")
                        });

                    db.ExecuteNonQuery(
                        "UPDATE OrderItems SET IsPaid = 1 WHERE PCID = @id AND IsPaid = 0",
                        new[] { new SqlParameter("@id", pc.ID) });
                    db.ExecuteNonQuery(
                        "UPDATE Computers SET Status = 0, StartTime = NULL WHERE PCID = @id",
                        new[] { new SqlParameter("@id", pc.ID) });
                    pc.EndSession();
                }
            }

            SimulatedClock.AdvanceDay();
            SetStatus($"⏭ Chuyển sang ngày {SimulatedClock.Now:dd/MM/yyyy}");
            LoadComputers();
        }

        private void dgvComputers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var pc = _computers[e.RowIndex];
            if (pc.Status == PCStatus.InUse)
                new CustomerLoginForm(pc).Show();
            else
                MessageBox.Show("Máy chưa mở.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnServices_Click(object sender, EventArgs e)
            => new ServiceForm(null).ShowDialog();

        private void btnReport_Click(object sender, EventArgs e)
            => new ReportForm().ShowDialog();

        private void UpdateHeader()
        {
            int online = 0;
            foreach (var pc in _computers)
                if (pc.Status == PCStatus.InUse) online++;
            lblOnlineInfo.Text = $"Máy đang dùng: {online}/{_computers.Count}";
            try
            {
                decimal rev = db.GetTodayRevenue();
                lblRevenue.Text = $"Doanh thu hôm nay: {rev:N0} VNĐ";
            }
            catch { }
        }

        private void SetStatus(string msg) => lblStatus.Text = msg;

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerLive?.Stop();
            timerLive?.Dispose();
            base.OnFormClosing(e);
        }
    }
}