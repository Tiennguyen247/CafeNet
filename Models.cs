// ============================================================
// Models.cs
// Chứa tất cả các class dữ liệu (OOP Design)
// Namespace: CyberCafeManager
// ============================================================

using System;
using System.Collections.Generic;

namespace CyberCafeManager
{
    // ========================
    // ENUM: Trạng thái máy
    // ========================
    public enum PCStatus
    {
        Empty = 0,   // Trống - sẵn sàng dùng
        InUse = 1    // Đang có khách
    }

    // ============================================================
    // BASE CLASS: Computer
    // Đại diện cho 1 máy tính trong phòng net
    // ============================================================
    public class Computer
    {
        // ---------- Properties ----------
        public int ID { get; set; }
        public string Name { get; set; }
        public PCStatus Status { get; set; } = PCStatus.Empty;
        public DateTime? StartTime { get; set; }     // Null khi máy trống
        public decimal HourlyRate { get; set; } = 10000; // 10,000 VNĐ/giờ
        public bool IsVip { get; set; } = false;

        // Danh sách đồ ăn đã gọi trong phiên hiện tại
        public List<OrderItem> Orders { get; set; } = new List<OrderItem>();

        // ---------- Methods ----------

        // Bắt đầu phiên chơi
        public virtual void StartSession()
        {
            Status = PCStatus.InUse;
            StartTime = DateTime.Now;
            Orders.Clear(); // Xóa orders cũ
        }

        // Kết thúc phiên (reset về trạng thái trống)
        public virtual void EndSession()
        {
            Status = PCStatus.Empty;
            StartTime = null;
            Orders.Clear();
        }

        // Tính tiền giờ
        public virtual decimal CalculateTimeFee()
        {
            if (StartTime == null) return 0;

            if (StartTime == null) return 0;
            TimeSpan elapsed = SimulatedClock.Now - StartTime.Value;
            decimal minutes = (decimal)Math.Max(0, elapsed.TotalMinutes);
            if (minutes < 1) return 0;
            // Làm tròn lên: 1-60 phút = 10k, 61-120 phút = 20k
            decimal hours = Math.Ceiling(minutes / 60m);
            return hours * HourlyRate;
        }

        // Tổng tiền dịch vụ
        public decimal GetServiceTotal()
        {
            decimal total = 0;
            foreach (var item in Orders)
                total += item.Total;
            return total;
        }

        // Tổng hóa đơn = tiền giờ + tiền dịch vụ
        public virtual decimal CalculateTotalFee()
        {
            return CalculateTimeFee() + GetServiceTotal();
        }

        // Thời gian đã sử dụng (dạng string HH:mm:ss)
        public string GetElapsedTime()
        {
            if (StartTime == null) return "00:00:00";
            TimeSpan elapsed = SimulatedClock.Now - StartTime.Value;
            if (elapsed.TotalSeconds < 0) return "00:00:00";
            return elapsed.ToString(@"hh\:mm\:ss");
        }

        // Override ToString để debug dễ hơn
        public override string ToString()
        {
            return $"{Name} | {Status} | {HourlyRate:N0} VNĐ/h";
        }

        public string Notes { get; set; }
    }

    // ============================================================
    // DERIVED CLASS: VipComputer (Kế thừa từ Computer)
    // Máy VIP có giá cao hơn, có thể thêm tính năng đặc biệt
    // ============================================================
    public class VipComputer : Computer
    {
        public VipComputer()
        {
            HourlyRate = 15000; // VIP: 15,000 VNĐ/giờ (vs 10,000 thường)
            IsVip = true;
        }

        // VIP có thể override logic tính tiền nếu cần
        public override decimal CalculateTimeFee()
        {
            // Hiện tại giống base, nhưng dễ mở rộng sau
            return base.CalculateTimeFee();
        }
    }

    // ============================================================
    // CLASS: OrderItem
    // Đại diện cho 1 món đã gọi trong phiên chơi
    // ============================================================
    public class OrderItem
    {
        public int ItemID { get; set; }          // ID trong DB
        public int ServiceID { get; set; }       // ID dịch vụ
        public string ServiceName { get; set; }  // Tên dịch vụ
        public decimal UnitPrice { get; set; }   // Đơn giá
        public int Quantity { get; set; } = 1;   // Số lượng

        // Tổng tiền = đơn giá × số lượng (tính tự động, không lưu DB)
        public decimal Total => UnitPrice * Quantity;

        public override string ToString()
        {
            return $"{ServiceName} x{Quantity} = {Total:N0} VNĐ";
        }
    }

    // ============================================================
    // CLASS: Service (dùng cho ServiceForm)
    // Đại diện cho 1 dịch vụ trong danh mục
    // ============================================================
    public class Service
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsActive { get; set; } = true;

        public override string ToString() => $"{ServiceName} - {Price:N0} VNĐ (Kho: {Stock})";
    }
}