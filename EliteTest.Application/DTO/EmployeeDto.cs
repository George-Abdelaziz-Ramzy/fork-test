namespace EliteTest.Application.DTO;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public DepartmentDto? Department { get; set; }
    //public int DepartmentId { get; set; }
    //public string DepartmentName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
