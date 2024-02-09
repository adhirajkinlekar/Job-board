using System;
using System.Collections.Generic;

namespace Teknorix_test.Models;

public partial class CompanyLocation
{
    public int CompanyLocationId { get; set; }

    public string Title { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public int StateId { get; set; }

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();

    public virtual State State { get; set; } = null!;
}
