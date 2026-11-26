namespace GUI;

partial class frmAtmReconciliation
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlTop = new Panel();
        lblTitle = new Label();
        btnStartReconcile = new Button();
        btnStopReconcile = new Button();
        prgLocal = new ProgressBar();
        lblStatus = new Label();
        dgvHistory = new DataGridView();

        pnlTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
        SuspendLayout();

        pnlTop.Dock = DockStyle.Top;
        pnlTop.Height = 165;
        pnlTop.Padding = new Padding(15);
        pnlTop.BackColor = Color.FromArgb(245, 248, 252);

        lblTitle.Location = new Point(18, 12);
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTitle.ForeColor = Color.Navy;
        lblTitle.Text = "ĐỐI SOÁT SAO KÊ GIAO DỊCH ATM CUỐI NGÀY (ASYNC / AWAIT)";

        btnStartReconcile.Location = new Point(18, 52);
        btnStartReconcile.Size = new Size(210, 40);
        btnStartReconcile.Text = "Chạy Đối Soát Tự Động";
        btnStartReconcile.BackColor = Color.SeaGreen;
        btnStartReconcile.ForeColor = Color.White;
        btnStartReconcile.FlatStyle = FlatStyle.Flat;
        btnStartReconcile.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnStartReconcile.Click += btnStartReconcile_Click;

        btnStopReconcile.Location = new Point(242, 52);
        btnStopReconcile.Size = new Size(160, 40);
        btnStopReconcile.Text = "Dừng Đối Soát";
        btnStopReconcile.BackColor = Color.Crimson;
        btnStopReconcile.ForeColor = Color.White;
        btnStopReconcile.FlatStyle = FlatStyle.Flat;
        btnStopReconcile.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnStopReconcile.Enabled = false;
        btnStopReconcile.Click += btnStopReconcile_Click;

        prgLocal.Location = new Point(18, 105);
        prgLocal.Size = new Size(860, 22);
        prgLocal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        prgLocal.Minimum = 0;
        prgLocal.Maximum = 100;

        lblStatus.Location = new Point(18, 132);
        lblStatus.AutoSize = true;
        lblStatus.ForeColor = Color.FromArgb(50, 50, 50);
        lblStatus.Text = "Trạng thái: Sẵn sàng chạy đối soát cuối ngày.";

        pnlTop.Controls.AddRange(new Control[] { lblTitle, btnStartReconcile, btnStopReconcile, prgLocal, lblStatus });

        dgvHistory.Dock = DockStyle.Fill;
        dgvHistory.AllowUserToAddRows = false;
        dgvHistory.AllowUserToDeleteRows = false;
        dgvHistory.ReadOnly = true;
        dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvHistory.RowHeadersVisible = false;

        AutoScaleDimensions = new SizeF(8F, 19F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(900, 520);
        Controls.Add(dgvHistory);
        Controls.Add(pnlTop);
        Font = new Font("Segoe UI", 10F);
        Name = "frmAtmReconciliation";
        Text = "Đối soát Giao dịch ATM Cuối ngày";
        Load += frmAtmReconciliation_Load;
        FormClosing += frmAtmReconciliation_FormClosing;

        pnlTop.ResumeLayout(false);
        pnlTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
        ResumeLayout(false);
    }

    private Panel pnlTop = null!;
    private Label lblTitle = null!;
    private Button btnStartReconcile = null!;
    private Button btnStopReconcile = null!;
    private ProgressBar prgLocal = null!;
    private Label lblStatus = null!;
    private DataGridView dgvHistory = null!;
}
