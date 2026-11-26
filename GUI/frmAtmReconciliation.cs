using BUS;

namespace GUI;

public partial class frmAtmReconciliation : Form
{
    private readonly ReconciliationBUS _bus = new();
    private CancellationTokenSource? _cts;

    public frmAtmReconciliation()
    {
        InitializeComponent();
    }

    private void frmAtmReconciliation_Load(object sender, EventArgs e)
    {
        ConfigureGrid();
        LoadHistory();
    }

    private void ConfigureGrid()
    {
        dgvHistory.AutoGenerateColumns = false;
        dgvHistory.Columns.Clear();

        dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "BatchCode",
            HeaderText = "Mã lô đối soát",
            FillWeight = 18
        });
        dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "RunTime",
            HeaderText = "Thời điểm chạy",
            FillWeight = 16,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm:ss" }
        });
        dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "TotalTransactions",
            HeaderText = "Tổng GD",
            FillWeight = 12,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "MatchedCount",
            HeaderText = "Khớp lệnh",
            FillWeight = 12,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "DiscrepancyCount",
            HeaderText = "Sai lệch",
            FillWeight = 10,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "TotalAmountVnd",
            HeaderText = "Tổng giá trị (VNĐ)",
            FillWeight = 18,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
        dgvHistory.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "DurationSeconds",
            HeaderText = "Thời gian (s)",
            FillWeight = 10,
            DefaultCellStyle = new DataGridViewCellStyle { Format = "N2", Alignment = DataGridViewContentAlignment.MiddleRight }
        });
    }

    private void LoadHistory()
    {
        dgvHistory.DataSource = null;
        dgvHistory.DataSource = _bus.GetHistory();
    }

    private void ReportToParent(int percent, string message)
    {
        if (this.MdiParent is frmBankAtmMain parent)
            parent.UpdateReconciliationProgress(percent, message);
    }

    private async void btnStartReconcile_Click(object sender, EventArgs e)
    {
        btnStartReconcile.Enabled = false;
        btnStopReconcile.Enabled = true;
        prgLocal.Value = 0;
        lblStatus.ForeColor = Color.DarkBlue;
        lblStatus.Text = "Đang khởi tạo tiến trình đối soát bất đồng bộ...";

        _cts = new CancellationTokenSource();

        var progress = new Progress<ReconciliationProgressInfo>(info =>
        {
            prgLocal.Value = info.Percent;
            lblStatus.Text = $"{info.Percent}% - {info.StatusMessage}";
            ReportToParent(info.Percent, $"Đối soát ATM: {info.Percent}%");
        });

        try
        {
            var result = await _bus.RunEndOfDayReconciliationAsync(progress, _cts.Token);

            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Text = $"Hoàn tất lô {result.BatchCode}: {result.TotalTransactions:N0} GD | Khớp: {result.MatchedCount:N0} | Lệch: {result.DiscrepancyCount:N0} ({result.DurationSeconds:F2}s)";
            ReportToParent(100, $"Đối soát hoàn tất ({result.BatchCode})");
            LoadHistory();
        }
        catch (OperationCanceledException)
        {
            lblStatus.ForeColor = Color.DarkOrange;
            lblStatus.Text = "Đã hủy tiến trình đối soát theo yêu cầu người dùng.";
            prgLocal.Value = 0;
            ReportToParent(0, "Tiến trình đối soát đã bị dừng.");
        }
        catch (Exception ex)
        {
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = $"Lỗi đối soát: {ex.Message}";
            ReportToParent(0, "Lỗi khi chạy đối soát.");
        }
        finally
        {
            _cts.Dispose();
            _cts = null;
            btnStartReconcile.Enabled = true;
            btnStopReconcile.Enabled = false;
        }
    }

    private void btnStopReconcile_Click(object sender, EventArgs e)
    {
        if (_cts is not null && !_cts.IsCancellationRequested)
        {
            btnStopReconcile.Enabled = false;
            lblStatus.Text = "Đang gửi tín hiệu dừng đối soát...";
            _cts.Cancel();
        }
    }

    private void frmAtmReconciliation_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (_cts is not null && !_cts.IsCancellationRequested)
            _cts.Cancel();

        ReportToParent(0, "Sẵn sàng");
    }
}
