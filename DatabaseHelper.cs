using System;
using System.Data;
using System.Data.SqlClient;

namespace CyberCafeManager
{
    public class DatabaseHelper
    {
        private static DatabaseHelper _instance;
        private static readonly object _lock = new object();

        // Đảm bảo Initial Catalog khớp với tên Database bạn tạo trong SQL
        private readonly string _connectionString =
            @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NetCafeDB;Integrated Security=True;";

        private DatabaseHelper() { }

        public static DatabaseHelper Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new DatabaseHelper();
                    return _instance;
                }
            }
        }

        public SqlConnection GetConnection() => new SqlConnection(_connectionString);

        public int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable ExecuteQuery(string sql, SqlParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    var dt = new DataTable();
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                    return dt;
                }
            }
        }

        public object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        // ============================================================
        // PHẦN THAY THẾ/BỔ SUNG: CÁC HÀM NGHIỆP VỤ KHỚP VỚI DATABASE.SQL
        // ============================================================

        // 1. Sửa lỗi thiếu GetTodayRevenue (Khớp với bảng Transactions của bạn)
        public decimal GetTodayRevenue()
        {
            string sql = "SELECT ISNULL(SUM(TotalAmount), 0) FROM Transactions WHERE CAST(CheckoutTime AS DATE) = CAST(GETDATE() AS DATE)";
            var result = ExecuteScalar(sql);
            return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        // 2. Sửa lỗi thiếu GetUnpaidOrders (Khớp với bảng OrderItems của bạn)
        public DataTable GetUnpaidOrders(int pcId = 0)
        {
            // Nếu pcId > 0, lọc theo máy. Nếu pcId = 0, lấy tất cả đơn chưa thanh toán.
            string sql = @"SELECT o.*, s.ServiceName 
                   FROM OrderItems o 
                   JOIN Services s ON o.ServiceID = s.ServiceID 
                   WHERE o.IsPaid = 0 ";

            if (pcId > 0)
            {
                sql += " AND o.PCID = @pcId ";
                SqlParameter[] parameters = { new SqlParameter("@pcId", pcId) };
                return ExecuteQuery(sql, parameters);
            }

            return ExecuteQuery(sql);
        }

        // 3. Lấy danh sách máy tính (Dùng cho UCPC)
        public DataTable GetAllComputers()
        {
            return ExecuteQuery("SELECT * FROM Computers WHERE IsActive = 1 ORDER BY PCName");
        }

        // 4. Lấy danh sách dịch vụ
        public DataTable GetAllServices()
        {
            return ExecuteQuery("SELECT * FROM Services WHERE IsActive = 1 ORDER BY ServiceName");
        }
    }
}