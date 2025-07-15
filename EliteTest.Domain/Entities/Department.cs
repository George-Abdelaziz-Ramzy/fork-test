using EliteTest.Domain.Common;

namespace EliteTest.Domain.Entities;

public sealed class Department : BaseEntity
{
    private readonly List<Employee> _employees = new();

    public Department(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Department name is required", nameof(name));

        Name = name;
    }

    public string Name { get; private set; }

    public IReadOnlyCollection<Employee> Employees => _employees.AsReadOnly();

    public void AddEmployee(Employee employee)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));

        if (employee.DepartmentId != Id)
            throw new InvalidOperationException("Employee does not belong to this department.");

        _employees.Add(employee);
    }
}
