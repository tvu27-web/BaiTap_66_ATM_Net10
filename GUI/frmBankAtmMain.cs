using BUS;

namespace GUI;

public partial class frmBankAtmMain : Form
{
    private readonly AtmStationBUS _atmBus = new();
    private string _currentTeller = "Vũ Minh Thành - 24810310190 (GDV-01)";

    public frmBankAtmMain()
    {
        InitializeComponent();
    }

    private void frmBankAtmMain_Load(object sender, EventArgs e)
    {
        lblTellerName.Text = $"Giao dịch viên trực: {_currentTeller}";
        string connStatus = _atmBus.InitializeConnection();
        lblCoreBankingStatus.Text = connStatus;
        lblCoreBankingStatus.ForeColor = connStatus.Contains("Online")
            ? Color.DarkGreen
            : Color.DarkOrange;
    }

    /// <summary>
    /// Điều hướng MDI chuẩn mực: duyệt this.MdiChildren để chống mở trùng lặp Form con.
    /// </summary>
    private void OpenOrActivateChildForm<T>(Func<T> factory) where T : Form
    {
        foreach (Form child in this.MdiChildren)
        {
            if (child is T existingForm)
            {
                if (existingForm.WindowState == FormWindowState.Minimized)
                    existingForm.WindowState = FormWindowState.Normal;

                existingForm.Activate();
                existingForm.BringToFront();
                return;
            }
        }

        T newForm = factory();
        newForm.MdiParent = this;
        newForm.Show();
    }

    public void UpdateReconciliationProgress(int percent, string statusText)
    {
        if (IsDisposed || !IsHandleCreated) return;

        if (InvokeRequired)
        {
            Invoke(new Action(() => UpdateReconciliationProgress(percent, statusText)));
            return;
        }

        prgMainStatus.Value = Math.Clamp(percent, 0, 100);
        lblProgressText.Text = statusText;
    }

    private void mnuDanhSachAtm_Click(object sender, EventArgs e)
        => OpenOrActivateChildForm(() => new frmAtmStations());

    private void mnuDoiSoatCuoiNgay_Click(object sender, EventArgs e)
        => OpenOrActivateChildForm(() => new frmAtmReconciliation());

    private void mnuCascade_Click(object sender, EventArgs e)
        => this.LayoutMdi(MdiLayout.Cascade);

    private void mnuTileHorizontal_Click(object sender, EventArgs e)
        => this.LayoutMdi(MdiLayout.TileHorizontal);

    private void mnuTileVertical_Click(object sender, EventArgs e)
        => this.LayoutMdi(MdiLayout.TileVertical);

    private void mnuDongTatCa_Click(object sender, EventArgs e)
    {
        foreach (Form child in this.MdiChildren)
            child.Close();
    }

    private void mnuDangXuat_Click(object sender, EventArgs e)
    {
        var confirm = MessageBox.Show(
            "Bạn có chắc chắn muốn đăng xuất phiên làm việc hiện tại?\nTất cả cửa sổ nghiệp vụ đang mở sẽ được đóng lại.",
            "Xác nhận Đăng xuất",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        foreach (Form child in this.MdiChildren)
            child.Close();

        string? newName = ShowInputDialog("Đăng nhập ca trực mới", "Nhập tên Giao dịch viên:", _currentTeller);
        if (!string.IsNullOrWhiteSpace(newName))
        {
            _currentTeller = newName.Trim();
            lblTellerName.Text = $"Giao dịch viên trực: {_currentTeller}";
            UpdateReconciliationProgress(0, "Đã chuyển phiên làm việc mới.");
        }
    }

    private void mnuKhoaManHinh_Click(object sender, EventArgs e)
    {
        menuStripMain.Enabled = false;
        foreach (Form child in this.MdiChildren)
            child.Enabled = false;

        while (true)
        {
            string? pwd = ShowInputDialog(
                "Màn hình đang khóa",
                "Nhập mật khẩu mở khóa (mặc định: 123456):",
                string.Empty,
                isPassword: true);

            if (pwd == "123456")
                break;

            MessageBox.Show("Mật khẩu không đúng! Vui lòng nhập '123456' để mở khóa.",
                "Khóa màn hình", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        menuStripMain.Enabled = true;
        foreach (Form child in this.MdiChildren)
            child.Enabled = true;
    }

    private void mnuThoat_Click(object sender, EventArgs e)
    {
        var confirm = MessageBox.Show(
            "Bạn có chắc chắn muốn thoát khỏi hệ thống điều hành ATM?",
            "Xác nhận Thoát",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm == DialogResult.Yes)
            Application.Exit();
    }

    private static string? ShowInputDialog(string title, string prompt, string defaultValue, bool isPassword = false)
    {
        using var form = new Form
        {
            Width = 420,
            Height = 180,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = title,
            StartPosition = FormStartPosition.CenterParent,
            MaximizeBox = false,
            MinimizeBox = false,
            ControlBox = !isPassword
        };

        var lbl = new Label { Left = 16, Top = 18, Width = 370, Text = prompt };
        var txt = new TextBox
        {
            Left = 16,
            Top = 48,
            Width = 370,
            Text = defaultValue,
            UseSystemPasswordChar = isPassword
        };
        var btnOk = new Button
        {
            Text = "Xác nhận",
            Left = 290,
            Width = 96,
            Top = 90,
            DialogResult = DialogResult.OK
        };

        form.Controls.AddRange(new Control[] { lbl, txt, btnOk });
        form.AcceptButton = btnOk;

        return form.ShowDialog() == DialogResult.OK ? txt.Text : null;
    }
}
