-- =====================================================================
-- SCRIPT KHỞI TẠO CƠ SỞ DỮ LIỆU CHO BÀI TẬP 6.6 (.NET 10 - 3-TIER)
-- Hệ thống MDI Điều hành Mạng lưới Cây ATM Ngân hàng
-- =====================================================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'BankAtmDB')
BEGIN
    CREATE DATABASE BankAtmDB;
END
GO

USE BankAtmDB;
GO

-- 1. Bảng Danh sách Trạm ATM & Tồn quỹ
IF OBJECT_ID('dbo.AtmStations', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AtmStations (
        StationCode        NVARCHAR(20)  PRIMARY KEY,
        Address            NVARCHAR(255) NOT NULL,
        CurrentCashBalance DECIMAL(18,0) NOT NULL DEFAULT 0,
        Status             NVARCHAR(50)  NOT NULL DEFAULT N'Hoạt động',
        ImagePath          NVARCHAR(500) NULL,
        LastUpdated        DATETIME      NOT NULL DEFAULT GETDATE()
    );
END
GO

-- 2. Bảng Nhật ký Đối soát Giao dịch ATM Cuối ngày
IF OBJECT_ID('dbo.AtmReconciliationLogs', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AtmReconciliationLogs (
        LogId               INT IDENTITY(1,1) PRIMARY KEY,
        BatchCode           NVARCHAR(30)  NOT NULL,
        RunTime             DATETIME      NOT NULL DEFAULT GETDATE(),
        TotalTransactions   INT           NOT NULL,
        MatchedCount        INT           NOT NULL,
        DiscrepancyCount    INT           NOT NULL,
        TotalAmountVnd      DECIMAL(18,0) NOT NULL,
        ExecutionStatus     NVARCHAR(50)  NOT NULL,
        DurationSeconds     FLOAT         NOT NULL
    );
END
GO

-- Dữ liệu mẫu ban đầu
IF NOT EXISTS (SELECT 1 FROM dbo.AtmStations)
BEGIN
    INSERT INTO dbo.AtmStations (StationCode, Address, CurrentCashBalance, Status, ImagePath)
    VALUES
        (N'ATM-HD01', N'Số 1 Phố Xốm, Phú Lương, Hà Đông, Hà Nội', 1250000000, N'Hoạt động', NULL),
        (N'ATM-HD02', N'Quang Trung, Hà Đông, Hà Nội',             820000000,  N'Hoạt động', NULL),
        (N'ATM-CG01', N'144 Xuân Thủy, Cầu Giấy, Hà Nội',          145000000,  N'Sắp hết tiền', NULL),
        (N'ATM-DD01', N'1160 Đường Láng, Đống Đa, Hà Nội',         0,          N'Bảo trì', NULL),
        (N'ATM-HK01', N'198 Trần Quang Khải, Hoàn Kiếm, Hà Nội',   1890000000, N'Hoạt động', NULL);
END
GO
