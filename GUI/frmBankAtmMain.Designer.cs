namespace GUI;

partial class frmBankAtmMain
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
        menuStripMain = new MenuStrip();
        mnuHeThong = new ToolStripMenuItem();
        mnuDangXuat = new ToolStripMenuItem();
        mnuKhoaManHinh = new ToolStripMenuItem();
        toolStripSeparator1 = new ToolStripSeparator();
        mnuThoat = new ToolStripMenuItem();
        mnuQuanLyAtm = new ToolStripMenuItem();
        mnuDanhSachAtm = new ToolStripMenuItem();
        mnuDoiSoatCuoiNgay = new ToolStripMenuItem();
        mnuCuaSo = new ToolStripMenuItem();
        mnuCascade = new ToolStripMenuItem();
        mnuTileHorizontal = new ToolStripMenuItem();
        mnuTileVertical = new ToolStripMenuItem();
        toolStripSeparator2 = new ToolStripSeparator();
        mnuDongTatCa = new ToolStripMenuItem();
        statusStripMain = new StatusStrip();
        lblTellerName = new ToolStripStatusLabel();
        lblCoreBankingStatus = new ToolStripStatusLabel();
        lblProgressText = new ToolStripStatusLabel();
        prgMainStatus = new ToolStripProgressBar();

        menuStripMain.SuspendLayout();
        statusStripMain.SuspendLayout();
        SuspendLayout();

        menuStripMain.Items.AddRange(new ToolStripItem[] { mnuHeThong, mnuQuanLyAtm, mnuCuaSo });
        menuStripMain.Location = new Point(0, 0);
        menuStripMain.Name = "menuStripMain";
        menuStripMain.Size = new Size(1184, 28);
        menuStripMain.TabIndex = 0;

        mnuHeThong.DropDownItems.AddRange(new ToolStripItem[] { mnuDangXuat, mnuKhoaManHinh, toolStripSeparator1, mnuThoat });
        mnuHeThong.Name = "mnuHeThong";
        mnuHeThong.Text = "Hệ thống";

        mnuDangXuat.Name = "mnuDangXuat";
        mnuDangXuat.Text = "Đăng xuất";
        mnuDangXuat.Click += mnuDangXuat_Click;

        mnuKhoaManHinh.Name = "mnuKhoaManHinh";
        mnuKhoaManHinh.Text = "Khóa màn hình";
        mnuKhoaManHinh.ShortcutKeys = Keys.Control | Keys.L;
        mnuKhoaManHinh.Click += mnuKhoaManHinh_Click;

        mnuThoat.Name = "mnuThoat";
        mnuThoat.Text = "Thoát";
        mnuThoat.ShortcutKeys = Keys.Alt | Keys.F4;
        mnuThoat.Click += mnuThoat_Click;

        mnuQuanLyAtm.DropDownItems.AddRange(new ToolStripItem[] { mnuDanhSachAtm, mnuDoiSoatCuoiNgay });
        mnuQuanLyAtm.Name = "mnuQuanLyAtm";
        mnuQuanLyAtm.Text = "Quản lý ATM";

        mnuDanhSachAtm.Name = "mnuDanhSachAtm";
        mnuDanhSachAtm.Text = "Danh sách Trạm ATM && Tồn quỹ";
        mnuDanhSachAtm.ShortcutKeys = Keys.Control | Keys.D1;
        mnuDanhSachAtm.Click += mnuDanhSachAtm_Click;

        mnuDoiSoatCuoiNgay.Name = "mnuDoiSoatCuoiNgay";
        mnuDoiSoatCuoiNgay.Text = "Đối soát Giao dịch Cuối ngày";
        mnuDoiSoatCuoiNgay.ShortcutKeys = Keys.Control | Keys.D2;
        mnuDoiSoatCuoiNgay.Click += mnuDoiSoatCuoiNgay_Click;

        mnuCuaSo.DropDownItems.AddRange(new ToolStripItem[] { mnuCascade, mnuTileHorizontal, mnuTileVertical, toolStripSeparator2, mnuDongTatCa });
        mnuCuaSo.Name = "mnuCuaSo";
        mnuCuaSo.Text = "Cửa sổ";

        mnuCascade.Name = "mnuCascade";
        mnuCascade.Text = "Xếp tầng (Cascade)";
        mnuCascade.Click += mnuCascade_Click;

        mnuTileHorizontal.Name = "mnuTileHorizontal";
        mnuTileHorizontal.Text = "Xếp ngang (Tile Horizontal)";
        mnuTileHorizontal.Click += mnuTileHorizontal_Click;

        mnuTileVertical.Name = "mnuTileVertical";
        mnuTileVertical.Text = "Xếp dọc (Tile Vertical)";
        mnuTileVertical.Click += mnuTileVertical_Click;

        mnuDongTatCa.Name = "mnuDongTatCa";
        mnuDongTatCa.Text = "Đóng tất cả";
        mnuDongTatCa.Click += mnuDongTatCa_Click;

        statusStripMain.Items.AddRange(new ToolStripItem[] { lblTellerName, lblCoreBankingStatus, lblProgressText, prgMainStatus });
        statusStripMain.Location = new Point(0, 695);
        statusStripMain.Name = "statusStripMain";
        statusStripMain.Size = new Size(1184, 26);
        statusStripMain.TabIndex = 1;

        lblTellerName.BorderSides = ToolStripStatusLabelBorderSides.Right;
        lblTellerName.Name = "lblTellerName";
        lblTellerName.Text = "Giao dịch viên trực: Vũ Minh Thành - 24810310190";

        lblCoreBankingStatus.BorderSides = ToolStripStatusLabelBorderSides.Right;
        lblCoreBankingStatus.Name = "lblCoreBankingStatus";
        lblCoreBankingStatus.Text = "Core Banking: Đang kết nối...";

        lblProgressText.Name = "lblProgressText";
        lblProgressText.Spring = true;
        lblProgressText.Text = "Sẵn sàng";
        lblProgressText.TextAlign = ContentAlignment.MiddleRight;

        prgMainStatus.Name = "prgMainStatus";
        prgMainStatus.Size = new Size(180, 18);
        prgMainStatus.Style = ProgressBarStyle.Continuous;
        prgMainStatus.Maximum = 100;
        prgMainStatus.Value = 0;

        AutoScaleDimensions = new SizeF(8F, 19F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1184, 721);
        Controls.Add(statusStripMain);
        Controls.Add(menuStripMain);
        Font = new Font("Segoe UI", 10F);
        IsMdiContainer = true;
        MainMenuStrip = menuStripMain;
        Name = "frmBankAtmMain";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Hệ thống MDI Điều hành Mạng lưới Cây ATM Ngân hàng (.NET 10)";
        WindowState = FormWindowState.Maximized;
        Load += frmBankAtmMain_Load;

        menuStripMain.ResumeLayout(false);
        menuStripMain.PerformLayout();
        statusStripMain.ResumeLayout(false);
        statusStripMain.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private MenuStrip menuStripMain = null!;
    private ToolStripMenuItem mnuHeThong = null!;
    private ToolStripMenuItem mnuDangXuat = null!;
    private ToolStripMenuItem mnuKhoaManHinh = null!;
    private ToolStripSeparator toolStripSeparator1 = null!;
    private ToolStripMenuItem mnuThoat = null!;
    private ToolStripMenuItem mnuQuanLyAtm = null!;
    private ToolStripMenuItem mnuDanhSachAtm = null!;
    private ToolStripMenuItem mnuDoiSoatCuoiNgay = null!;
    private ToolStripMenuItem mnuCuaSo = null!;
    private ToolStripMenuItem mnuCascade = null!;
    private ToolStripMenuItem mnuTileHorizontal = null!;
    private ToolStripMenuItem mnuTileVertical = null!;
    private ToolStripSeparator toolStripSeparator2 = null!;
    private ToolStripMenuItem mnuDongTatCa = null!;
    private StatusStrip statusStripMain = null!;
    private ToolStripStatusLabel lblTellerName = null!;
    private ToolStripStatusLabel lblCoreBankingStatus = null!;
    private ToolStripStatusLabel lblProgressText = null!;
    private ToolStripProgressBar prgMainStatus = null!;
}
