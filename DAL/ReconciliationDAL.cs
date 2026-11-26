using DTO;
using Microsoft.Data.SqlClient;

namespace DAL;

public class ReconciliationDAL
{
    private static readonly List<ReconciliationRecordDTO> _memoryLogs = [];
    private static int _nextId = 1;

    public List<ReconciliationRecordDTO> GetHistory()
    {
        if (!DbConnectionConfig.IsDatabaseAvailable)
            return _memoryLogs.OrderByDescending(x => x.RunTime).ToList();

        var list = new List<ReconciliationRecordDTO>();
        const string sql = @"
            SELECT LogId, BatchCode, RunTime, TotalTransactions, MatchedCount,
                   DiscrepancyCount, TotalAmountVnd, ExecutionStatus, DurationSeconds
            FROM dbo.AtmReconciliationLogs
            ORDER BY RunTime DESC";

        using var conn = new SqlConnection(DbConnectionConfig.ConnectionString);
        using var cmd = new SqlCommand(sql, conn);
        conn.Open();
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new ReconciliationRecordDTO
            {
                LogId = reader.GetInt32(0),
                BatchCode = reader.GetString(1),
                RunTime = reader.GetDateTime(2),
                TotalTransactions = reader.GetInt32(3),
                MatchedCount = reader.GetInt32(4),
                DiscrepancyCount = reader.GetInt32(5),
                TotalAmountVnd = reader.GetDecimal(6),
                ExecutionStatus = reader.GetString(7),
                DurationSeconds = reader.GetDouble(8)
            });
        }
        return list;
    }

    public void SaveLog(ReconciliationRecordDTO record)
    {
        if (!DbConnectionConfig.IsDatabaseAvailable)
        {
            record.LogId = _nextId++;
            _memoryLogs.Add(record);
            return;
        }

        const string sql = @"
            INSERT INTO dbo.AtmReconciliationLogs
                (BatchCode, RunTime, TotalTransactions, MatchedCount, DiscrepancyCount, TotalAmountVnd, ExecutionStatus, DurationSeconds)
            VALUES
                (@BatchCode, @RunTime, @TotalTransactions, @MatchedCount, @DiscrepancyCount, @TotalAmountVnd, @ExecutionStatus, @DurationSeconds)";

        using var conn = new SqlConnection(DbConnectionConfig.ConnectionString);
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@BatchCode", record.BatchCode);
        cmd.Parameters.AddWithValue("@RunTime", record.RunTime);
        cmd.Parameters.AddWithValue("@TotalTransactions", record.TotalTransactions);
        cmd.Parameters.AddWithValue("@MatchedCount", record.MatchedCount);
        cmd.Parameters.AddWithValue("@DiscrepancyCount", record.DiscrepancyCount);
        cmd.Parameters.AddWithValue("@TotalAmountVnd", record.TotalAmountVnd);
        cmd.Parameters.AddWithValue("@ExecutionStatus", record.ExecutionStatus);
        cmd.Parameters.AddWithValue("@DurationSeconds", record.DurationSeconds);

        conn.Open();
        cmd.ExecuteNonQuery();
    }
}
