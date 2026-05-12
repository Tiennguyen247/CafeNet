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
            string sql = @"SELECT ISNULL(SUM(TotalAmount), 0) 
                   FROM Transactions 
                   WHERE CAST(CheckoutTime AS DATE) = @today";

            var result = ExecuteScalar(sql, new[]
            {
        new SqlParameter("@today", SimulatedClock.Now.Date)
    });

            return result != null && result != DBNull.Value
                ? Convert.ToDecimal(result) : 0;
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

        // Doanh thu theo từng ngày trong khoảng thời gian
        public DataTable GetRevenueByDate(DateTime from, DateTime to)
        {
            string sql = @"
        SELECT 
            CAST(CheckoutTime AS DATE)  AS Ngay,
            COUNT(*)                    AS SoPhien,
            SUM(TotalAmount)            AS DoanhThu
        FROM Transactions
        WHERE CheckoutTime >= @from AND CheckoutTime <= @to
        GROUP BY CAST(CheckoutTime AS DATE)
        ORDER BY Ngay";

            return ExecuteQuery(sql, new[]
            {
        new SqlParameter("@from", from.Date),
        new SqlParameter("@to",   to.Date.AddDays(1).AddSeconds(-1))
    });
        }

        // Thống kê dịch vụ đã bán trong khoảng thời gian
        public DataTable GetServiceStats(DateTime from, DateTime to)
        {
            string sql = @"
        SELECT 
            s.ServiceName,
            SUM(oi.Quantity)                AS SoLuong,
            SUM(oi.Quantity * oi.UnitPrice) AS DoanhThu
        FROM OrderItems oi
        JOIN Services s ON oi.ServiceID = s.ServiceID
        WHERE oi.IsPaid = 1
          AND oi.OrderTime >= @from AND oi.OrderTime <= @to
        GROUP BY s.ServiceName
        ORDER BY DoanhThu DESC";

            return ExecuteQuery(sql, new[]
            {
        new SqlParameter("@from", from.Date),
        new SqlParameter("@to",   to.Date.AddDays(1).AddSeconds(-1))
    });
        }

        // Tìm chính xác theo SĐT (dùng cho login)
        // ============================================================
        // HỘI VIÊN — các method còn thiếu
        // ============================================================

        // Lấy tất cả hội viên đang active
        public DataTable GetAllMembers()
        {
            return ExecuteQuery(
                "SELECT * FROM Members WHERE IsActive = 1 ORDER BY FullName");
        }

        // Tìm hội viên theo SĐT (LIKE — dùng cho ô search)
        public DataTable FindMemberByPhone(string phone)
        {
            return ExecuteQuery(
                "SELECT * FROM Members WHERE Phone LIKE @phone AND IsActive = 1",
                new[] { new SqlParameter("@phone", "%" + phone + "%") });
        }

        // Tìm chính xác theo SĐT (dùng cho login màn hình khách)
        public DataTable GetMemberByPhone(string phone)
        {
            return ExecuteQuery(
                "SELECT * FROM Members WHERE Phone = @phone AND IsActive = 1",
                new[] { new SqlParameter("@phone", phone) });
        }

        // Nạp tiền vào ví hội viên
        public void TopUpMember(int memberId, decimal amount, string note)
        {
            // Cộng tiền vào Balance, cộng điểm (1 điểm mỗi 10k)
            ExecuteNonQuery(
                @"UPDATE Members 
          SET Balance = Balance + @amount,
              Points  = Points  + @pts
          WHERE MemberID = @id",
                new[]
                {
            new SqlParameter("@amount", amount),
            new SqlParameter("@pts",    (int)(amount / 10000)),
            new SqlParameter("@id",     memberId)
                });

            // Ghi vào lịch sử giao dịch
            ExecuteNonQuery(
                @"INSERT INTO MemberTransactions (MemberID, Amount, TxType, Note)
          VALUES (@id, @amt, 'TopUp', @note)",
                new[]
                {
            new SqlParameter("@id",   memberId),
            new SqlParameter("@amt",  amount),
            new SqlParameter("@note", note)
                });
        }
    }
}