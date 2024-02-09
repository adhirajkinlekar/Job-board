using System;
using System.Collections.Generic;

namespace Teknorix_test.Models;

public partial class Job
{
    public int JobId { get; set; }

    public int? DepartmentId { get; set; }

    public int CompanyLocationId { get; set; }

    public string JobCode { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly PostedDate { get; set; }

    public DateOnly ClosingDate { get; set; }

    public virtual CompanyLocation CompanyLocation { get; set; } = null!;

    public virtual Department? Department { get; set; }
}
