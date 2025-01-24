namespace CRUD_Design.Models.DTO.RunInfo
{
    public class DateSearchDTO
    {
        public string dateFrom { get; set; }
        public string? dateTo { get; set; }
        public float? distance { get; set; }
        public float? time { get; set; }
    }
}
