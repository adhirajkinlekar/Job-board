using System;
using System.Collections.Generic;

namespace Teknorix_test.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Industry { get; set; }

    public string ContactEmail { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public virtual ICollection<CompanyLocation> CompanyLocations { get; set; } = new List<CompanyLocation>();
}
