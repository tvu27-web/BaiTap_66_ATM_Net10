using Microsoft.Data.SqlClient;

namespace DAL;

/// <summary>
/// Cấu hình chuỗi kết nối SQL Server cho BankAtmDB.
/// Tự động chuyển sang chế độ Mô phỏng (In-Memory) nếu chưa bật SQL Server để dễ dàng test ngay.
/// </summary>
public static class DbConnectionConfig
{
    // Đổi Server thành tên instance SQL Server trên máy bạn nếu cần (vd: .\SQLEXPRESS)
    public static string ConnectionString { get; set; } =
        "Server=.;Database=BankAtmDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;";

    public static bool IsDatabaseAvailable { get; private set; }
    public static string ConnectionStatusMessage { get; private set; } = "Chưa kiểm tra";

    public static bool TestConnection()
    {
        try
        {
            using var conn = new SqlConnection(ConnectionString);
            conn.Open();
            IsDatabaseAvailable = true;
            ConnectionStatusMessage = "Core Banking: Đã kết nối SQL Server (Online)";
            return true;
        }
        catch
        {
            IsDatabaseAvailable = false;
            ConnectionStatusMessage = "Core Banking: Chế độ Mô phỏng (In-Memory)";
            return false;
        }
    }
}
