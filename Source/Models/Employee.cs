namespace SCsProjectMaster.Source.Models;

public partial class Employee
{
    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<TimeStopped> TimeStoppeds { get; set; } = new List<TimeStopped>();

    public virtual ICollection<Project> ProjectsNavigation { get; set; } = new List<Project>();
}
