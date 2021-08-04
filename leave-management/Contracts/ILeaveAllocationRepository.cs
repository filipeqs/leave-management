using leave_management.Data;
using System.Collections.Generic;

namespace leave_management.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        bool HasAllocation(int leaveTypeId, string employeeId);

        ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id);
        LeaveAllocation GetLeaveAllocationByEmployeeAndType(string id, int leaveTypeId);
    }
}
