using leave_management.Data;
using System.Collections.Generic;

namespace leave_management.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoryBase<LeaveAllocation>
    {
        bool HasAllocation(int leaveTypeId, string employeeId);

        ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string employeeId);
        LeaveAllocation GetLeaveAllocationByEmployeeAndType(string employeeId, int leaveTypeId);
    }
}
