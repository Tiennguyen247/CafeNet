using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CyberCafeManager
{
    public class ReportForm : Form
    {
        private DatabaseHelper db = DatabaseHelper.Instance;

        private DateTimePicker dtpFrom, dtpTo;
        private TabControl tabControl;

        // Tab 1
        private DataGridView dgvRevenue;
        private Label lblTotalRevenue, lblTotalSessions;

        // Tab 2 — chart
        private Panel pnlChart;
        private DataTable _chartData;

        // Tab 3
        private DataGridView dgvServices;
        private Label lblServiceTotal;

        public ReportForm()
        {
            InitializeComponents();
            LoadReport();
        }

        // ============================================================
        // XÂY DỰNG GIAO DIỆN
        // ============================================================
        private void InitializeComponents()
        {
            this.Text = "Báo cáo tài chính";
            this.Size = new Size(760, 560);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.MinimizeBox = false;

            // Header
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 52,
                BackColor = Color.FromArgb(18, 40, 76)
            };
            var lblTitle = new Label
            {
                Text = "📊  BÁO CÁO TÀI CHÍNH",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlHeader.Controls.Add(lblTitle);

            // Filter bar
            var pnlFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 46,
                BackColor = Color.White,
                Padding = new Padding(12, 8, 12, 8)
            };

            var lblFrom = new Label
            {
                Text = "Từ ngày:",
                Location = new Point(10, 14),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };

            dtpFrom = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Value = SimulatedClock.Now.AddDays(-6).Date,
                Location = new Point(75, 10),
                Size = new Size(115, 26),
                Font = new Font("Segoe UI", 9)
            };

            var lblTo = new Label
            {
                Text = "Đến ngày:",
                Location = new Point(205, 14),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };

            dtpTo = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Value = SimulatedClock.Now.Date,
                Location = new Point(278, 10),
                Size = new Size(115, 26),
                Font = new Font("Segoe UI", 9)
            };

            var btnLoad = new Button
            {
                Text = "🔍 Xem báo cáo",
                Location = new Point(408, 8),
                Size = new Size(130, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(18, 40, 76),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            btnLoad.FlatAppearance.BorderSize = 0;
            btnLoad.Click += (s, e) => LoadReport();

            pnlFilter.Controls.AddRange(new Control[]
                { lblFrom, dtpFrom, lblTo, dtpTo, btnLoad });

            // TabControl
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 9)
            };

            var tab1 = new TabPage("📅  Doanh thu theo ngày") { BackColor = Color.White };
            var tab2 = new TabPage("📊  Biểu đồ") { BackColor = Color.White };
            var tab3 = new TabPage("🍜  Thống kê dịch vụ") { BackColor = Color.White };

            BuildTab1(tab1);
            BuildTab2(tab2);
            BuildTab3(tab3);

            tabControl.TabPages.AddRange(new[] { tab1, tab2, tab3 });

            // Khi chuyển sang tab biểu đồ → vẽ lại
            tabControl.SelectedIndexChanged += (s, e) =>
            {
                if (tabControl.SelectedIndex == 1) pnlChart?.Invalidate();
            };

            var pnlContent = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            pnlContent.Controls.Add(tabControl);

            this.Controls.Add(pnlContent);
            this.Controls.Add(pnlFilter);
            this.Controls.Add(pnlHeader);
        }

        // ============================================================
        // TAB 1 — DOANH THU THEO NGÀY
        // ============================================================
        private void BuildTab1(TabPage tab)
        {
            dgvRevenue = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvRevenue.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(50, 70, 110);
            dgvRevenue.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRevenue.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            var pnlSummary = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 38,
                BackColor = Color.FromArgb(235, 240, 255)
            };
            lblTotalSessions = new Label
            {
                Text = "Tổng phiên: 0",
                Location = new Point(12, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            lblTotalRevenue = new Label
            {
                Text = "Tổng doanh thu: 0 VNĐ",
                Location = new Point(200, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(18, 40, 76)
            };
            pnlSummary.Controls.AddRange(new Control[] { lblTotalSessions, lblTotalRevenue });

            tab.Controls.Add(dgvRevenue);
            tab.Controls.Add(pnlSummary);
        }

        // ============================================================
        // TAB 2 — BIỂU ĐỒ (vẽ bằng Graphics, không cần thư viện ngoài)
        // ============================================================
        private void BuildTab2(TabPage tab)
        {
            pnlChart = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
            pnlChart.Paint += PnlChart_Paint;
            tab.Controls.Add(pnlChart);
        }

        private void PnlChart_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (_chartData == null || _chartData.Rows.Count == 0)
            {
                g.DrawString("Chưa có dữ liệu — nhấn 'Xem báo cáo' để tải.",
                    new Font("Segoe UI", 10), Brushes.Gray,
                    new PointF(pnlChart.Width / 2f - 160, pnlChart.Height / 2f - 10));
                return;
            }

            int padL = 72, padB = 48, padT = 30, padR = 20;
            int chartW = pnlChart.Width - padL - padR;
            int chartH = pnlChart.Height - padT - padB;
            int count = _chartData.Rows.Count;

            // Tìm max
            decimal maxVal = 1;
            foreach (DataRow row in _chartData.Rows)
            {
                decimal v = Convert.ToDecimal(row["DoanhThu"]);
                if (v > maxVal) maxVal = v;
            }

            // Gridlines + nhãn Y
            int gridLines = 5;
            for (int i = 0; i <= gridLines; i++)
            {
                int gy = padT + chartH - i * chartH / gridLines;
                g.DrawLine(Pens.LightGray, padL, gy, padL + chartW, gy);
                decimal val = maxVal / gridLines * i;
                g.DrawString((val / 1000m).ToString("N0") + "k",
                    new Font("Segoe UI", 7), Brushes.Gray, 4, gy - 8);
            }

            // Trục
            using var axisPen = new Pen(Color.Gray, 1.5f);
            g.DrawLine(axisPen, padL, padT, padL, padT + chartH);
            g.DrawLine(axisPen, padL, padT + chartH, padL + chartW, padT + chartH);

            // Cột
            int spacing = chartW / count;
            int barW = Math.Max(14, spacing - 10);
            using var barBrush = new SolidBrush(Color.FromArgb(30, 100, 200));

            for (int i = 0; i < count; i++)
            {
                decimal val = Convert.ToDecimal(_chartData.Rows[i]["DoanhThu"]);
                int barH = (int)(val / maxVal * chartH);
                int bx = padL + i * spacing + (spacing - barW) / 2;
                int by = padT + chartH - barH;

                // Cột với bo góc trên
                using var path = new GraphicsPath();
                int radius = 4;
                if (barH > radius * 2)
                {
                    path.AddArc(bx, by, radius * 2, radius * 2, 180, 90);
                    path.AddArc(bx + barW - radius * 2, by, radius * 2, radius * 2, 270, 90);
                    path.AddLine(bx + barW, by + radius, bx + barW, padT + chartH);
                    path.AddLine(bx, padT + chartH, bx, by + radius);
                }
                else
                {
                    path.AddRectangle(new Rectangle(bx, by, barW, barH));
                }
                g.FillPath(barBrush, path);

                // Giá trị
                if (val > 0)
                    g.DrawString((val / 1000m).ToString("N0") + "k",
                        new Font("Segoe UI", 7, FontStyle.Bold), Brushes.DarkBlue,
                        bx - 2, by - 16);

                // Nhãn X
                string dateStr = Convert.ToDateTime(_chartData.Rows[i]["Ngay"])
                                        .ToString("dd/MM");
                g.DrawString(dateStr, new Font("Segoe UI", 7.5f), Brushes.DimGray,
                    bx + barW / 2f - 14, padT + chartH + 6);
            }
        }

        // ============================================================
        // TAB 3 — THỐNG KÊ DỊCH VỤ
        // ============================================================
        private void BuildTab3(TabPage tab)
        {
            dgvServices = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 9),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvServices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 120, 60);
            dgvServices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvServices.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            var pnlBot = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 38,
                BackColor = Color.FromArgb(235, 248, 235)
            };
            lblServiceTotal = new Label
            {
                Text = "Tổng doanh thu từ dịch vụ: 0 VNĐ",
                Location = new Point(12, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 120, 60)
            };
            pnlBot.Controls.Add(lblServiceTotal);

            tab.Controls.Add(dgvServices);
            tab.Controls.Add(pnlBot);
        }

        // ============================================================
        // TẢI DỮ LIỆU
        // ============================================================
        private void LoadReport()
        {
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date;

            LoadTab1(from, to);
            LoadTab3(from, to);
            pnlChart?.Invalidate();
        }

        private void LoadTab1(DateTime from, DateTime to)
        {
            try
            {
                var dt = db.GetRevenueByDate(from, to);
                _chartData = dt;

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
                        sess, rev.ToString("N0"));
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

        private void LoadTab3(DateTime from, DateTime to)
        {
            try
            {
                var dt = db.GetServiceStats(from, to);

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
                lblServiceTotal.Text =
                    $"Tổng doanh thu từ dịch vụ: {total:N0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thống kê dịch vụ: " + ex.Message);
            }
        }
    }
}