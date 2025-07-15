using System.ComponentModel.DataAnnotations;


namespace EliteTest.Application.Commands.Department;
public record CreateDepartmentCommand(
    [Required]
    string name
    );
