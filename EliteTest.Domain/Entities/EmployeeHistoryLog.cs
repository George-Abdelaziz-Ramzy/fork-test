using EliteTest.Domain.Common;

namespace EliteTest.Domain.Entities;

public class EmployeeHistoryLog : BaseEntity
{
    public int EmployeeId { get; private set; }
    public virtual Employee? Employee { get; set; }
    public string ActionType { get; private set; }
    public DateTime ActionDate { get; private set; }


    public EmployeeHistoryLog(int employeeId, string actionType)
    {
        EmployeeId = employeeId;
        ActionType = actionType;
        ActionDate = DateTime.UtcNow;
    }
}
