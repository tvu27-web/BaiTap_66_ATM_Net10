namespace DTO;

public class AtmStationDTO
{
    public string StationCode { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal CurrentCashBalance { get; set; }
    public string Status { get; set; } = "Hoạt động";
    public string? ImagePath { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.Now;
}
