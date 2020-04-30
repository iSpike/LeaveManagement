using LeaveManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Contracts
{
    public interface ILeaveAllocationRepository : IRepositoyBase<LeaveAllocation>
    {
        bool CheckAllocation(int leaveTypeId, string employeeId);

        ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id);

        LeaveAllocation GetLeaveAllocationsByEmployeeAndType(string id, int leaveTypeId);
    }
}
