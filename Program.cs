// ============================================================
// Program.cs - Điểm khởi động của ứng dụng
// ============================================================

using System;
using System.Windows.Forms;

namespace CyberCafeManager
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Kiểm tra kết nối DB trước khi mở app
            var db = DatabaseHelper.Instance;

            // Mở MainDashboard
            Application.Run(new MainDashboard());
        }
    }
}