using EliteTest.Domain.Common;
using EliteTest.Domain.Enums;

namespace EliteTest.Domain.Entities;

public sealed class Employee : BaseEntity
{
    private readonly List<EmployeeHistoryLog> _employeeHistoryLogs = new();
    public Employee(string name, string email, int departmentId, DateTime hireDate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required", nameof(email));

        Name = name;
        Email = email;
        DepartmentId = departmentId;
        Status = EmployeeStatus.Active;
        HireDate = hireDate;
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime HireDate { get; private set; }
    public int DepartmentId { get; private set; }

    public Department Department { get; set; }

    public EmployeeStatus Status { get; private set; }
    public IReadOnlyCollection<EmployeeHistoryLog> EmployeeHistoryLogs => _employeeHistoryLogs.AsReadOnly();
public void SetStatus(EmployeeStatus status) => Status = status;
}
