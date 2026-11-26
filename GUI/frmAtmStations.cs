using BUS;
using DTO;

namespace GUI;

public partial class frmAtmStations : Form
{
    private readonly AtmStationBUS _bus = new();
    private string? _selectedImagePath;

    public frmAtmStations()
    {
        InitializeComponent();
    }

    private void frmAtmStations_Load(object sender, EventArgs e)
    {
        cboStatus.SelectedIndex = 0;
        ConfigureGrid();
        LoadStations();
    }

    private void ConfigureGrid()
    {
        dgvStations.AutoGenerateColumns = false;
        dgvStations.Columns.Clear();

        dgvStations.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "StationCode",
            DataPropertyName = "StationCode",
            HeaderText = "Mã trạm",
            FillWeight = 15
        });

        dgvStations.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Address",
            DataPropertyName = "Address",
            HeaderText = "Địa chỉ đặt máy",
            FillWeight = 38
        });

        dgvStations.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "CurrentCashBalance",
            DataPropertyName = "CurrentCashBalance",
            HeaderText = "Tồn quỹ hiện tại (VNĐ)",
            FillWeight = 20,
            DefaultCellStyle = new DataGridViewCellStyle
            {
                Format = "N0",
                Alignment = DataGridViewContentAlignment.MiddleRight
            }
        });

        dgvStations.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Status",
            DataPropertyName = "Status",
            HeaderText = "Trạng thái hoạt động",
            FillWeight = 15
        });

        dgvStations.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "ImagePath",
            DataPropertyName = "ImagePath",
            HeaderText = "Ảnh hiện trường",
            FillWeight = 12
        });
    }

    private void LoadStations()
    {
        dgvStations.DataSource = null;
        dgvStations.DataSource = _bus.GetAllStations();
    }

    private void dgvStations_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvStations.CurrentRow?.DataBoundItem is not AtmStationDTO station)
            return;

        txtStationCode.Text = station.StationCode;
        txtAddress.Text = station.Address;
        nudCashBalance.Value = Math.Clamp(station.CurrentCashBalance, nudCashBalance.Minimum, nudCashBalance.Maximum);
        cboStatus.SelectedItem = station.Status;
        if (cboStatus.SelectedIndex < 0) cboStatus.SelectedIndex = 0;

        LoadPreviewImage(station.ImagePath);
    }

    private void btnChoosePhoto_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Chọn ảnh chụp hiện trường trạm ATM",
            Filter = "Tệp hình ảnh (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|Tất cả tệp (*.*)|*.*"
        };

        if (dialog.ShowDialog(this) == DialogResult.OK)
            LoadPreviewImage(dialog.FileName);
    }

    private void LoadPreviewImage(string? path)
    {
        _selectedImagePath = path;

        if (picAtmPhoto.Image is not null)
        {
            var old = picAtmPhoto.Image;
            picAtmPhoto.Image = null;
            old.Dispose();
        }

        if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(path);
                using var ms = new MemoryStream(bytes);
                picAtmPhoto.Image = Image.FromStream(ms);
                lblImagePath.Text = $"Ảnh hiện trường: {Path.GetFileName(path)}";
            }
            catch
            {
                lblImagePath.Text = "Không thể đọc tệp ảnh đã chọn.";
            }
        }
        else
        {
            lblImagePath.Text = "Ảnh hiện trường: (Chưa đính kèm)";
        }
    }

    private AtmStationDTO ReadFormInput() => new()
    {
        StationCode = txtStationCode.Text,
        Address = txtAddress.Text,
        CurrentCashBalance = nudCashBalance.Value,
        Status = cboStatus.SelectedItem?.ToString() ?? "Hoạt động",
        ImagePath = _selectedImagePath
    };

    private void btnAdd_Click(object sender, EventArgs e)
    {
        var result = _bus.AddStation(ReadFormInput());
        MessageBox.Show(result.Message, "Quản lý Trạm ATM",
            MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

        if (result.Success)
            LoadStations();
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        var result = _bus.UpdateStation(ReadFormInput());
        MessageBox.Show(result.Message, "Quản lý Trạm ATM",
            MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

        if (result.Success)
            LoadStations();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtStationCode.Text))
        {
            MessageBox.Show("Vui lòng chọn trạm ATM cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var confirm = MessageBox.Show(
            $"Bạn có chắc chắn muốn xóa trạm ATM '{txtStationCode.Text}'?",
            "Xác nhận Xóa",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes) return;

        var result = _bus.DeleteStation(txtStationCode.Text);
        MessageBox.Show(result.Message, "Quản lý Trạm ATM",
            MessageBoxButtons.OK, result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

        if (result.Success)
        {
            ClearInputs();
            LoadStations();
        }
    }

    private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

    private void ClearInputs()
    {
        txtStationCode.Clear();
        txtAddress.Clear();
        nudCashBalance.Value = 0;
        cboStatus.SelectedIndex = 0;
        LoadPreviewImage(null);
        txtStationCode.Focus();
    }
}
