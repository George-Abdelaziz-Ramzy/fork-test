using AutoMapper;
using EliteTest.Application.Commands.Employee;
using EliteTest.Application.DTO;
using EliteTest.Application.Filters;
using EliteTest.Application.Interfaces;
using EliteTest.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EliteTest.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCommand command)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(string.Join(',', errors));
        }
        try
        {
            var employee = _mapper.Map<Employee>(command);
            employee.AddHistoryLog(new EmployeeHistoryLog(0, "Inserted"));
            await _unitOfWork.Repository<Employee>()
                .AddAsync(employee);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, _mapper.Map<EmployeeDto>(employee));

        }catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError , new { message = "Error" ,  innerexception = ex.Message } );
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _unitOfWork.Repository<Employee>()
            .GetFirstWithIncludeAsync(
            where :e => e.Id == id,
            include: q => q.Include(e => e.Department)
            );
        if (employee == null)
            return NotFound($"Employee with ID {id} not found.");

        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return Ok(employeeDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _unitOfWork.Repository<Employee>()
            .GetByIdAsync(id);
        if (employee is null)
            return NotFound($"Employee with ID {id} not found.");

        //_unitOfWork.Repository<Employee>().Remove(employee);

        employee.Delete();

        await _unitOfWork.Repository<EmployeeHistoryLog>()
                .AddAsync(new EmployeeHistoryLog(employee.Id, "Deleted"));

        await _unitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeCommand command)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(string.Join(',', errors));
        }
        var employee = await _unitOfWork.Repository<Employee>()
            .GetByIdAsync(id);
        if (employee is null)
            return NotFound($"Employee with ID {id} not found.");
        _mapper.Map(command, employee);
        _unitOfWork.Repository<Employee>().Update(employee);
        await _unitOfWork.Repository<EmployeeHistoryLog>()
                .AddAsync(new EmployeeHistoryLog(employee.Id, "Updated"));
        await _unitOfWork.CommitAsync();
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchEmployees([FromQuery] EmployeeSearchCriteria criteria)
    {
        Expression<Func<Employee, bool>> conditions = e =>
                e.IsDeleted == false && 
                (string.IsNullOrEmpty(criteria.Name) || e.Name.Contains(criteria.Name)) &&
                (string.IsNullOrEmpty(criteria.Email) || e.Email.Contains(criteria.Email)) &&
                (!criteria.DepartmentId.HasValue || e.DepartmentId == criteria.DepartmentId) &&
                (!criteria.Status.HasValue || e.Status == criteria.Status);

        var result = await _unitOfWork.Repository<Employee>()
            .FindWithIncludeAsync(
            conditions,
            include: q => q.Include(e => e.Department)
            );

        var mapped = _mapper.Map<IEnumerable<EmployeeDto>>(result);
        return Ok(mapped);
    }
}
