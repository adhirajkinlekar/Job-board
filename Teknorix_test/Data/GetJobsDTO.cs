namespace Teknorix_test.Data
{
    public class JobListDTO
    {
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string? Department { get; set; }

        public DateOnly PostedDate { get; set; }

        public DateOnly ClosingDate { get; set; }
    }

    public class GetJobsDTO
    {
        public List<JobListDTO>? Jobs { get; set; }

        public int Total { get; set; }
    }
}
