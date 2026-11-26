using System.Diagnostics;
using DAL;
using DTO;

namespace BUS;

public class ReconciliationProgressInfo
{
    public int Percent { get; set; }
    public string StatusMessage { get; set; } = string.Empty;
    public int ProcessedTransactions { get; set; }
    public int TotalTransactions { get; set; }
}

public class ReconciliationBUS
{
    private readonly ReconciliationDAL _dal = new();

    public List<ReconciliationRecordDTO> GetHistory() => _dal.GetHistory();

    /// <summary>
    /// Đối soát bất đồng bộ hàng trăm nghìn giao dịch ATM (4 - 6 giây) bằng Task.Run & Task.Delay.
    /// Hỗ trợ dừng giữa chừng bằng CancellationToken.
    /// </summary>
    public async Task<ReconciliationRecordDTO> RunEndOfDayReconciliationAsync(
        IProgress<ReconciliationProgressInfo>? progress,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();

        var result = await Task.Run(async () =>
        {
            int totalTransactions = Random.Shared.Next(180_000, 320_000);
            int totalSteps = 20;
            int delayPerStepMs = Random.Shared.Next(220, 285); // ~4.4 - 5.7 giây

            int matched = 0;
            int discrepancy = 0;
            decimal totalAmount = 0;

            string[] phases =
            [
                "Đang kết nối máy chủ Core Banking & tải sao kê...",
                "Đang giải mã lô giao dịch rút tiền mặt (ATM Cash)...",
                "Đang đối chiếu giao dịch chuyển khoản nội bộ & NAPAS...",
                "Đang kiểm tra sai lệch tồn quỹ & bút toán treo...",
                "Đang tổng hợp báo cáo đối soát cuối ngày..."
            ];

            for (int step = 1; step <= totalSteps; step++)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(delayPerStepMs, cancellationToken);

                int processed = (int)((long)totalTransactions * step / totalSteps);
                int batchDelta = processed - (matched + discrepancy);
                int batchDiscrepancy = Random.Shared.Next(0, 4);
                discrepancy += batchDiscrepancy;
                matched += Math.Max(0, batchDelta - batchDiscrepancy);
                totalAmount += batchDelta * (decimal)Random.Shared.Next(500_000, 2_500_000);

                int phaseIndex = Math.Min((step - 1) * phases.Length / totalSteps, phases.Length - 1);
                int percent = step * 100 / totalSteps;

                progress?.Report(new ReconciliationProgressInfo
                {
                    Percent = percent,
                    ProcessedTransactions = processed,
                    TotalTransactions = totalTransactions,
                    StatusMessage = $"{phases[phaseIndex]} ({processed:N0}/{totalTransactions:N0} GD)"
                });
            }

            stopwatch.Stop();

            return new ReconciliationRecordDTO
            {
                BatchCode = $"EOD-{DateTime.Now:yyyyMMdd-HHmmss}",
                RunTime = DateTime.Now,
                TotalTransactions = totalTransactions,
                MatchedCount = matched,
                DiscrepancyCount = discrepancy,
                TotalAmountVnd = totalAmount,
                ExecutionStatus = "Hoàn tất",
                DurationSeconds = Math.Round(stopwatch.Elapsed.TotalSeconds, 2)
            };
        }, cancellationToken);

        _dal.SaveLog(result);
        return result;
    }
}
