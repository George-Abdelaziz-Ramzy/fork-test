using EliteTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteTest.Application.DTO;
public class EmployeeLogsDto
{
    public int Id { get; set; }
    public string ActionType { get; private set; }
    public DateTime ActionDate { get; private set; }
    public int EmployeeId { get; private set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string EmployeeEmail { get; set; } = string.Empty;
    public DateTime EmployeeHireDate { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}
