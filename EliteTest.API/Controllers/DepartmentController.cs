using AutoMapper;
using EliteTest.Application.Commands.Department;
using EliteTest.Application.DTO;
using EliteTest.Application.Interfaces;
using EliteTest.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EliteTest.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _unitOfWork.Repository<Department>()
            .GetAllAsync();
        if (departments?.Count() <= 0)
            return NotFound("No departments found.");

        var departmentDtos = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        return Ok(departmentDtos);
    }
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateDepartmentCommand model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(string.Join(',' , errors));
        }
        var department = _mapper.Map<Department>(model);
        await _unitOfWork.Repository<Department>().AddAsync(department);
        await _unitOfWork.CommitAsync();
        return CreatedAtAction(nameof(GetAll), new { id = department.Id }, _mapper.Map<DepartmentDto>(department));
    }
}
