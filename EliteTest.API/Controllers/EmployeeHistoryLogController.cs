using AutoMapper;
using EliteTest.Application.DTO;
using EliteTest.Application.Interfaces;
using EliteTest.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EliteTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeHistoryLogController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeHistoryLogController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetHistoryLogs(int employeeId)
        {
            var logs = await _unitOfWork.Repository<EmployeeHistoryLog>()
                .FindWithIncludeAsync(
                    conditions: log => log.EmployeeId == employeeId,
                    include: q => q.Include(log => log.Employee).Include(d => d.Employee!.Department)
                );
            if (logs is null)
                return NotFound($"No history logs found for employee with ID {employeeId}.");


            var logsDto = _mapper.Map<IEnumerable<EmployeeLogsDto>>(logs);

            return Ok(logsDto);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchHistoryLogs([FromQuery] string? actionType, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            Expression<Func<EmployeeHistoryLog, bool>> conditions = log =>
            log.ActionType.Contains(actionType) || string.IsNullOrEmpty(actionType) &&
            (!startDate.HasValue || log.ActionDate >= startDate.Value) &&
            (!endDate.HasValue || log.ActionDate <= endDate.Value);

            var logs = await _unitOfWork.Repository<EmployeeHistoryLog>()
                .FindWithIncludeAsync(
                conditions, 
                q => q.Include(log => log.Employee).Include(d=>d.Employee!.Department)
                );

            //mapping
            var logsDto = _mapper.Map<IEnumerable<EmployeeLogsDto>>(logs);
            return Ok(logsDto);
        }
    }
}
