using System;
using System.Collections.Generic;

namespace SCs_Project_Master.Source.Logic;

public partial class Address
{
    public int Id { get; set; }

    public string? AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? AddressLine3 { get; set; }

    public string? Street { get; set; }

    public string? HouseNumber { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
