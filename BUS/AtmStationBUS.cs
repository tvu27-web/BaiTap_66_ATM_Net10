using DAL;
using DTO;

namespace BUS;

public class AtmStationBUS
{
    private readonly AtmStationDAL _dal = new();

    public string InitializeConnection()
    {
        DbConnectionConfig.TestConnection();
        return DbConnectionConfig.ConnectionStatusMessage;
    }

    public List<AtmStationDTO> GetAllStations() => _dal.GetAll();

    public (bool Success, string Message) AddStation(AtmStationDTO station)
    {
        var validation = ValidateStation(station);
        if (!validation.IsValid)
            return (false, validation.Error);

        bool inserted = _dal.Insert(station);
        return inserted
            ? (true, "Thêm trạm ATM thành công.")
            : (false, $"Mã trạm '{station.StationCode}' đã tồn tại trong hệ thống.");
    }

    public (bool Success, string Message) UpdateStation(AtmStationDTO station)
    {
        var validation = ValidateStation(station);
        if (!validation.IsValid)
            return (false, validation.Error);

        bool updated = _dal.Update(station);
        return updated
            ? (true, "Cập nhật thông tin trạm ATM thành công.")
            : (false, "Không tìm thấy mã trạm cần cập nhật.");
    }

    public (bool Success, string Message) DeleteStation(string stationCode)
    {
        if (string.IsNullOrWhiteSpace(stationCode))
            return (false, "Vui lòng chọn trạm ATM cần xóa.");

        bool deleted = _dal.Delete(stationCode.Trim());
        return deleted
            ? (true, "Xóa trạm ATM thành công.")
            : (false, "Không tìm thấy trạm ATM để xóa.");
    }

    private static (bool IsValid, string Error) ValidateStation(AtmStationDTO station)
    {
        if (string.IsNullOrWhiteSpace(station.StationCode))
            return (false, "Mã trạm không được để trống.");

        if (string.IsNullOrWhiteSpace(station.Address))
            return (false, "Địa chỉ đặt máy không được để trống.");

        if (station.CurrentCashBalance < 0)
            return (false, "Số tiền tồn quỹ không được nhỏ hơn 0.");

        station.StationCode = station.StationCode.Trim().ToUpperInvariant();
        station.Address = station.Address.Trim();
        return (true, string.Empty);
    }
}
