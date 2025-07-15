using AutoMapper;
using EliteTest.Application.Commands.Employee;
using EliteTest.Application.DTO;
using EliteTest.Application.Interfaces;
using EliteTest.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand command)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(string.Join(',', errors));
        }

        var employee = _mapper.Map<Employee>(command);
        await _unitOfWork.Repository<Employee>()
            .AddAsync(employee);
        await _unitOfWork.CommitAsync();
        return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(int id)
    {
        var employee = await _unitOfWork.Repository<Employee>()
            .GetByIdAsync(id);
        if (employee == null)
            return NotFound($"Employee with ID {id} not found.");

        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return Ok(employeeDto);
    }
}
