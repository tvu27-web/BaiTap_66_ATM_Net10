namespace GUI;

partial class frmAtmStations
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
        grpInfo = new GroupBox();
        lblCode = new Label();
        txtStationCode = new TextBox();
        lblAddress = new Label();
        txtAddress = new TextBox();
        lblCash = new Label();
        nudCashBalance = new NumericUpDown();
        lblStatus = new Label();
        cboStatus = new ComboBox();
        picAtmPhoto = new PictureBox();
        btnChoosePhoto = new Button();
        lblImagePath = new Label();
        btnAdd = new Button();
        btnUpdate = new Button();
        btnDelete = new Button();
        btnClear = new Button();
        dgvStations = new DataGridView();

        grpInfo.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudCashBalance).BeginInit();
        ((System.ComponentModel.ISupportInitialize)picAtmPhoto).BeginInit();
        ((System.ComponentModel.ISupportInitialize)dgvStations).BeginInit();
        SuspendLayout();

        grpInfo.Dock = DockStyle.Top;
        grpInfo.Height = 250;
        grpInfo.Text = "Thông tin Trạm ATM & Ảnh chụp hiện trường";
        grpInfo.Padding = new Padding(10);

        lblCode.Location = new Point(20, 35);
        lblCode.AutoSize = true;
        lblCode.Text = "Mã trạm ATM:";

        txtStationCode.Location = new Point(155, 32);
        txtStationCode.Size = new Size(210, 27);

        lblStatus.Location = new Point(390, 35);
        lblStatus.AutoSize = true;
        lblStatus.Text = "Trạng thái:";

        cboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cboStatus.Items.AddRange(new object[] { "Hoạt động", "Sắp hết tiền", "Bảo trì", "Ngừng hoạt động" });
        cboStatus.Location = new Point(480, 32);
        cboStatus.Size = new Size(190, 27);

        lblAddress.Location = new Point(20, 78);
        lblAddress.AutoSize = true;
        lblAddress.Text = "Địa chỉ đặt máy:";

        txtAddress.Location = new Point(155, 75);
        txtAddress.Size = new Size(515, 27);

        lblCash.Location = new Point(20, 121);
        lblCash.AutoSize = true;
        lblCash.Text = "Tồn quỹ (VNĐ):";

        nudCashBalance.Location = new Point(155, 118);
        nudCashBalance.Size = new Size(240, 27);
        nudCashBalance.Maximum = 50000000000M;
        nudCashBalance.Increment = 50000000M;
        nudCashBalance.ThousandsSeparator = true;

        picAtmPhoto.BorderStyle = BorderStyle.FixedSingle;
        picAtmPhoto.Location = new Point(700, 25);
        picAtmPhoto.Size = new Size(230, 155);
        picAtmPhoto.SizeMode = PictureBoxSizeMode.Zoom;
        picAtmPhoto.BackColor = Color.WhiteSmoke;

        btnChoosePhoto.Location = new Point(700, 188);
        btnChoosePhoto.Size = new Size(230, 34);
        btnChoosePhoto.Text = "Đính kèm ảnh hiện trường...";
        btnChoosePhoto.Click += btnChoosePhoto_Click;

        lblImagePath.Location = new Point(20, 160);
        lblImagePath.Size = new Size(650, 22);
        lblImagePath.ForeColor = Color.DimGray;
        lblImagePath.Text = "Ảnh hiện trường: (Chưa đính kèm)";

        btnAdd.Location = new Point(155, 192);
        btnAdd.Size = new Size(115, 36);
        btnAdd.Text = "Thêm mới";
        btnAdd.BackColor = Color.SeaGreen;
        btnAdd.ForeColor = Color.White;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.Click += btnAdd_Click;

        btnUpdate.Location = new Point(282, 192);
        btnUpdate.Size = new Size(115, 36);
        btnUpdate.Text = "Cập nhật";
        btnUpdate.BackColor = Color.RoyalBlue;
        btnUpdate.ForeColor = Color.White;
        btnUpdate.FlatStyle = FlatStyle.Flat;
        btnUpdate.Click += btnUpdate_Click;

        btnDelete.Location = new Point(409, 192);
        btnDelete.Size = new Size(115, 36);
        btnDelete.Text = "Xóa trạm";
        btnDelete.BackColor = Color.IndianRed;
        btnDelete.ForeColor = Color.White;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.Click += btnDelete_Click;

        btnClear.Location = new Point(536, 192);
        btnClear.Size = new Size(134, 36);
        btnClear.Text = "Làm mới form";
        btnClear.Click += btnClear_Click;

        grpInfo.Controls.AddRange(new Control[]
        {
            lblCode, txtStationCode, lblStatus, cboStatus,
            lblAddress, txtAddress, lblCash, nudCashBalance,
            picAtmPhoto, btnChoosePhoto, lblImagePath,
            btnAdd, btnUpdate, btnDelete, btnClear
        });

        dgvStations.Dock = DockStyle.Fill;
        dgvStations.AllowUserToAddRows = false;
        dgvStations.AllowUserToDeleteRows = false;
        dgvStations.ReadOnly = true;
        dgvStations.MultiSelect = false;
        dgvStations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvStations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvStations.RowHeadersVisible = false;
        dgvStations.SelectionChanged += dgvStations_SelectionChanged;

        AutoScaleDimensions = new SizeF(8F, 19F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(960, 580);
        Controls.Add(dgvStations);
        Controls.Add(grpInfo);
        Font = new Font("Segoe UI", 10F);
        Name = "frmAtmStations";
        Text = "Quản lý Danh sách Trạm ATM & Tồn quỹ";
        Load += frmAtmStations_Load;

        grpInfo.ResumeLayout(false);
        grpInfo.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudCashBalance).EndInit();
        ((System.ComponentModel.ISupportInitialize)picAtmPhoto).EndInit();
        ((System.ComponentModel.ISupportInitialize)dgvStations).EndInit();
        ResumeLayout(false);
    }

    private GroupBox grpInfo = null!;
    private Label lblCode = null!;
    private TextBox txtStationCode = null!;
    private Label lblAddress = null!;
    private TextBox txtAddress = null!;
    private Label lblCash = null!;
    private NumericUpDown nudCashBalance = null!;
    private Label lblStatus = null!;
    private ComboBox cboStatus = null!;
    private PictureBox picAtmPhoto = null!;
    private Button btnChoosePhoto = null!;
    private Label lblImagePath = null!;
    private Button btnAdd = null!;
    private Button btnUpdate = null!;
    private Button btnDelete = null!;
    private Button btnClear = null!;
    private DataGridView dgvStations = null!;
}
