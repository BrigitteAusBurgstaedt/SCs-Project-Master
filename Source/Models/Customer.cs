using System;
using System.Collections.Generic;

namespace SCs_Project_Master.Source.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int? Priority { get; set; }

    public string WebsiteUri { get; set; }

    public string PhoneNumber { get; set; }

    public string EMailAddress { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
