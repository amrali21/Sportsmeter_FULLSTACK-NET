namespace CRUD_Design.Models.DTO.Reports
{
    public class WeeklyReport
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }

        public float avgDist { get; set; }
        public float avgTime { get; set; }
    }
}
