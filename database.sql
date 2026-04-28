-- ============================================================
-- CYBER CAFE MANAGER - FULL DATABASE SCRIPT
-- ============================================================

-- 1. Bảng Computers: Danh sách máy tính
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Computers')
CREATE TABLE Computers (
    PCID        INT IDENTITY(1,1) PRIMARY KEY,
    PCName      NVARCHAR(50)    NOT NULL,
    Status      INT             DEFAULT 0,      -- 0: Trống | 1: Đang dùng
    StartTime   DATETIME        NULL,           
    HourlyRate  DECIMAL(18,2)   DEFAULT 10000, 
    IsVip       BIT             DEFAULT 0,      
    IsActive    BIT             DEFAULT 1       
);
GO

-- 2. Bảng Services: Danh mục đồ ăn/uống
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Services')
CREATE TABLE Services (
    ServiceID   INT IDENTITY(1,1) PRIMARY KEY,
    ServiceName NVARCHAR(100)   NOT NULL,
    Price       DECIMAL(18,2)   NOT NULL,
    Stock       INT             DEFAULT 0,
    IsActive    BIT             DEFAULT 1
);
GO

-- 3. Bảng OrderItems: Đồ ăn đã gọi theo từng máy
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OrderItems')
CREATE TABLE OrderItems (
    ItemID      INT IDENTITY(1,1) PRIMARY KEY,
    PCID        INT             NOT NULL REFERENCES Computers(PCID),
    ServiceID   INT             NOT NULL REFERENCES Services(ServiceID),
    Quantity    INT             DEFAULT 1,
    UnitPrice   DECIMAL(18,2)   NOT NULL,   
    IsPaid      BIT             DEFAULT 0,  
    OrderTime   DATETIME        DEFAULT GETDATE()
);
GO

-- 4. Bảng Transactions: Lịch sử thanh toán (Sửa lỗi báo cáo)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Transactions')
CREATE TABLE Transactions (
    TransID         INT IDENTITY(1,1) PRIMARY KEY,
    PCID            INT             NOT NULL REFERENCES Computers(PCID),
    TimeFee         DECIMAL(18,2)   DEFAULT 0,  
    ServiceFee      DECIMAL(18,2)   DEFAULT 0,  
    TotalAmount     DECIMAL(18,2)   NOT NULL,
    CheckoutTime    DATETIME        DEFAULT GETDATE(),
    Note            NVARCHAR(255)   NULL
);
GO

-- 5. DỮ LIỆU MẪU (Chỉ nạp nếu bảng trống)
IF NOT EXISTS (SELECT TOP 1 1 FROM Computers)
BEGIN
    INSERT INTO Computers (PCName, HourlyRate, IsVip) VALUES
    ('PC-01', 10000, 0), ('PC-02', 10000, 0), ('PC-03', 10000, 0),
    ('PC-04', 10000, 0), ('PC-05', 10000, 0), ('VIP-01', 15000, 1), 
    ('VIP-02', 15000, 1);
END
GO

IF NOT EXISTS (SELECT TOP 1 1 FROM Services)
BEGIN
    INSERT INTO Services (ServiceName, Price, Stock) VALUES
    (N'Mì tôm Hảo Hảo', 5000, 100), 
    (N'Sting Dâu', 15000, 50),
    (N'Nước suối Lavie', 7000, 100), 
    (N'Bánh mì que', 10000, 30);
END
GO

-- 6. VIEW BÁO CÁO (Dành cho chức năng thống kê)
IF EXISTS (SELECT * FROM sys.views WHERE name = 'vw_TodayRevenue')
    DROP VIEW vw_TodayRevenue;
GO
CREATE VIEW vw_TodayRevenue AS
    SELECT 
        ISNULL(SUM(TotalAmount), 0) AS TotalRevenue,
        COUNT(*) AS TotalSessions
    FROM Transactions
    WHERE CAST(CheckoutTime AS DATE) = CAST(GETDATE() AS DATE);
GO

-- KIỂM TRA LẠI CÁC BẢNG ĐÃ TẠO
SELECT 'Table' AS Status, name FROM sys.tables;
USE NetCafeDB;
GO
-- Thay thế chính xác bằng User hiển thị trong bảng thông báo lỗi của bạn
ALTER AUTHORIZATION ON DATABASE::NetCafeDB TO [HP\nt803];
GO