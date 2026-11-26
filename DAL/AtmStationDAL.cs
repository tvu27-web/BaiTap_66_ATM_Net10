using DTO;
using Microsoft.Data.SqlClient;

namespace DAL;

public class AtmStationDAL
{
    private static readonly List<AtmStationDTO> _memoryStore =
    [
        new AtmStationDTO
        {
            StationCode = "ATM-HD01",
            Address = "Số 1 Phố Xốm, Phú Lương, Hà Đông, Hà Nội",
            CurrentCashBalance = 1_250_000_000m,
            Status = "Hoạt động",
            LastUpdated = DateTime.Now
        },
        new AtmStationDTO
        {
            StationCode = "ATM-HD02",
            Address = "Quang Trung, Hà Đông, Hà Nội",
            CurrentCashBalance = 820_000_000m,
            Status = "Hoạt động",
            LastUpdated = DateTime.Now
        },
        new AtmStationDTO
        {
            StationCode = "ATM-CG01",
            Address = "144 Xuân Thủy, Cầu Giấy, Hà Nội",
            CurrentCashBalance = 145_000_000m,
            Status = "Sắp hết tiền",
            LastUpdated = DateTime.Now
        },
        new AtmStationDTO
        {
            StationCode = "ATM-DD01",
            Address = "1160 Đường Láng, Đống Đa, Hà Nội",
            CurrentCashBalance = 0m,
            Status = "Bảo trì",
            LastUpdated = DateTime.Now
        },
        new AtmStationDTO
        {
            StationCode = "ATM-HK01",
            Address = "198 Trần Quang Khải, Hoàn Kiếm, Hà Nội",
            CurrentCashBalance = 1_890_000_000m,
            Status = "Hoạt động",
            LastUpdated = DateTime.Now
        }
    ];

    public List<AtmStationDTO> GetAll()
    {
        if (!DbConnectionConfig.IsDatabaseAvailable)
            return _memoryStore.OrderBy(x => x.StationCode).ToList();

        var list = new List<AtmStationDTO>();
        const string sql = @"
            SELECT StationCode, Address, CurrentCashBalance, Status, ImagePath, LastUpdated
            FROM dbo.AtmStations
            ORDER BY StationCode";

        using var conn = new SqlConnection(DbConnectionConfig.ConnectionString);
        using var cmd = new SqlCommand(sql, conn);
        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new AtmStationDTO
            {
                StationCode = reader.GetString(0),
                Address = reader.GetString(1),
                CurrentCashBalance = reader.GetDecimal(2),
                Status = reader.GetString(3),
                ImagePath = reader.IsDBNull(4) ? null : reader.GetString(4),
                LastUpdated = reader.GetDateTime(5)
            });
        }
        return list;
    }

    public bool Insert(AtmStationDTO station)
    {
        if (!DbConnectionConfig.IsDatabaseAvailable)
        {
            if (_memoryStore.Any(x => x.StationCode.Equals(station.StationCode, StringComparison.OrdinalIgnoreCase)))
                return false;

            station.LastUpdated = DateTime.Now;
            _memoryStore.Add(station);
            return true;
        }

        const string sql = @"
            INSERT INTO dbo.AtmStations (StationCode, Address, CurrentCashBalance, Status, ImagePath, LastUpdated)
            VALUES (@StationCode, @Address, @CurrentCashBalance, @Status, @ImagePath, GETDATE())";

        using var conn = new SqlConnection(DbConnectionConfig.ConnectionString);
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@StationCode", station.StationCode);
        cmd.Parameters.AddWithValue("@Address", station.Address);
        cmd.Parameters.AddWithValue("@CurrentCashBalance", station.CurrentCashBalance);
        cmd.Parameters.AddWithValue("@Status", station.Status);
        cmd.Parameters.AddWithValue("@ImagePath", (object?)station.ImagePath ?? DBNull.Value);

        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Update(AtmStationDTO station)
    {
        if (!DbConnectionConfig.IsDatabaseAvailable)
        {
            var existing = _memoryStore.FirstOrDefault(x =>
                x.StationCode.Equals(station.StationCode, StringComparison.OrdinalIgnoreCase));
            if (existing is null) return false;

            existing.Address = station.Address;
            existing.CurrentCashBalance = station.CurrentCashBalance;
            existing.Status = station.Status;
            existing.ImagePath = station.ImagePath;
            existing.LastUpdated = DateTime.Now;
            return true;
        }

        const string sql = @"
            UPDATE dbo.AtmStations
            SET Address = @Address,
                CurrentCashBalance = @CurrentCashBalance,
                Status = @Status,
                ImagePath = @ImagePath,
                LastUpdated = GETDATE()
            WHERE StationCode = @StationCode";

        using var conn = new SqlConnection(DbConnectionConfig.ConnectionString);
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@StationCode", station.StationCode);
        cmd.Parameters.AddWithValue("@Address", station.Address);
        cmd.Parameters.AddWithValue("@CurrentCashBalance", station.CurrentCashBalance);
        cmd.Parameters.AddWithValue("@Status", station.Status);
        cmd.Parameters.AddWithValue("@ImagePath", (object?)station.ImagePath ?? DBNull.Value);

        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }

    public bool Delete(string stationCode)
    {
        if (!DbConnectionConfig.IsDatabaseAvailable)
        {
            var existing = _memoryStore.FirstOrDefault(x =>
                x.StationCode.Equals(stationCode, StringComparison.OrdinalIgnoreCase));
            if (existing is null) return false;
            _memoryStore.Remove(existing);
            return true;
        }

        const string sql = "DELETE FROM dbo.AtmStations WHERE StationCode = @StationCode";
        using var conn = new SqlConnection(DbConnectionConfig.ConnectionString);
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@StationCode", stationCode);

        conn.Open();
        return cmd.ExecuteNonQuery() > 0;
    }
}
