namespace DTO;

public class ReconciliationRecordDTO
{
    public int LogId { get; set; }
    public string BatchCode { get; set; } = string.Empty;
    public DateTime RunTime { get; set; } = DateTime.Now;
    public int TotalTransactions { get; set; }
    public int MatchedCount { get; set; }
    public int DiscrepancyCount { get; set; }
    public decimal TotalAmountVnd { get; set; }
    public string ExecutionStatus { get; set; } = "Hoàn tất";
    public double DurationSeconds { get; set; }
}
