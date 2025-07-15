using EliteTest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteTest.Application.Filters;
public class EmployeeSearchCriteria
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public int? DepartmentId { get; set; }
    public EmployeeStatus? Status { get; set; }
}
