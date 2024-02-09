using Teknorix_test.Models;

namespace Teknorix_test.Data
{
    public class GetJobDTO
    {
        public int Id { get; set; }

        public string Code { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public GetLocationDTO Location { get; set; } = null!;

        public GetDepartmentDTO? Department { get; set; } 

        public DateOnly PostedDate { get; set; }

        public DateOnly ClosingDate { get; set; }
    }

    public class GetLocationDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string City { get; set; } = null!;

        public string State { get; set; } = null!;

        public string Country { get; set; } = null!;

        public string? Zip { get; set; } = null!;
    }

    public class GetDepartmentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
    }

}
