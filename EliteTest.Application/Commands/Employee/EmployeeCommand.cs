using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteTest.Application.Commands.Employee;
public record EmployeeCommand(
    [Required]
    [MaxLength(50)] 
    string Name,
    [Required]
    [EmailAddress]
    [MaxLength(100)] 
    string Email,
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    int DepartmentId,
    [Required] 
    DateTime HireDate
    );
