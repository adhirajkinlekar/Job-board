using Microsoft.AspNetCore.Mvc;

namespace Teknorix_test.Data
{
    public class JobQueryDTO
    {

        public string? Q { get; set; } = string.Empty;

        [FromQuery]
        public int? PageNo { get; set; }

        [FromQuery]
        public int? PageSize { get; set; }

        public int? DepartmentId { get; set; }

        [FromQuery]
        public int? LocationId { get; set; }
    }
}
