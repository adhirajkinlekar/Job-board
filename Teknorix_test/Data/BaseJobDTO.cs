namespace Teknorix_test.Data
{
    public class BaseJobDTO
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int LocationId { get; set; }

        public int DepartmentId { get; set; }

        public DateTime ClosingDate { get; set; }
    }
}
