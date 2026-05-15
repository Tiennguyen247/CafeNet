using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public partial class ReportForm : Form
    {
        private DatabaseHelper db = DatabaseHelper.Instance;

        public ReportForm()
        {
            InitializeComponent();
            dtpFrom.Value = SimulatedClock.Now.AddDays(-6).Date;
            dtpTo.Value = SimulatedClock.Now.Date;
            LoadReport();
        }

        private void btnLoad_Click(object sender, EventArgs e) => LoadReport();

        private void LoadReport()
        {
            LoadTabRevenue(dtpFrom.Value.Date, dtpTo.Value.Date);
            LoadTabServices(dtpFrom.Value.Date, dtpTo.Value.Date);
        }

        private void LoadTabRevenue(DateTime from, DateTime to)
        {
            try
            {
                DataTable dt = db.GetRevenueByDate(from, to);

                var display = new DataTable();
                display.Columns.Add("Ngày", typeof(string));
                display.Columns.Add("Số phiên", typeof(int));
                display.Columns.Add("Doanh thu (VNĐ)", typeof(string));

                int totalSessions = 0;
                decimal totalRevenue = 0;

                foreach (DataRow row in dt.Rows)
                {
                    int sess = Convert.ToInt32(row["SoPhien"]);
                    decimal rev = Convert.ToDecimal(row["DoanhThu"]);
                    totalSessions += sess;
                    totalRevenue += rev;
                    display.Rows.Add(
                        Convert.ToDateTime(row["Ngay"]).ToString("dd/MM/yyyy"),
                        sess,
                        rev.ToString("N0"));
                }

                dgvRevenue.DataSource = display;
                lblTotalSessions.Text = $"Tổng phiên: {totalSessions}";
                lblTotalRevenue.Text = $"Tổng doanh thu: {totalRevenue:N0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message);
            }
        }

        private void LoadTabServices(DateTime from, DateTime to)
        {
            try
            {
                DataTable dt = db.GetServiceStats(from, to);

                var display = new DataTable();
                display.Columns.Add("Tên dịch vụ", typeof(string));
                display.Columns.Add("Số lượng bán", typeof(int));
                display.Columns.Add("Doanh thu (VNĐ)", typeof(string));

                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    decimal rev = Convert.ToDecimal(row["DoanhThu"]);
                    total += rev;
                    display.Rows.Add(
                        row["ServiceName"].ToString(),
                        Convert.ToInt32(row["SoLuong"]),
                        rev.ToString("N0"));
                }

                dgvServices.DataSource = display;
                lblSvcTotal.Text = $"Tổng doanh thu từ dịch vụ: {total:N0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê dịch vụ: " + ex.Message);
            }
        }
    }
}