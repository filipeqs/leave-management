using System;
using System.Linq;
using leave_management.Data;
using System.Collections.Generic;
using leave_management.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<LeaveAllocation>> FindAll() => await _db.LeaveAllocations.Include(l => l.LeaveType).ToListAsync();

        public async Task<LeaveAllocation> FindById(int id) => await _db.LeaveAllocations
            .Include(l => l.Employee)
            .Include(l => l.LeaveType)
            .SingleOrDefaultAsync(l => l.Id == id);

        public async Task<bool> Exists(int id) => await _db.LeaveAllocations.AnyAsync(l => l.Id == id);

        public async Task<bool> Create(LeaveAllocation entity)
        {
            await _db.LeaveAllocations.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save() => await _db.SaveChangesAsync() > 0;

        public async Task<bool> HasAllocation(int leaveTypeId, string employeeId)
        {
            var period = DateTime.Now.Year;
            return await _db.LeaveAllocations
                    .Include(l => l.LeaveType)
                    .AnyAsync(q => q.EmployeeId == employeeId && q.LeaveTypeId == leaveTypeId && q.Period == period);
        }

        public async Task<ICollection<LeaveAllocation>> GetLeaveAllocationsByEmployee(string employeeId)
        {
            var period = DateTime.Now.Year;
            return await _db.LeaveAllocations
                .Where(l => l.EmployeeId == employeeId && l.Period == period)
                .Include(l => l.LeaveType)
                .ToListAsync();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationByEmployeeAndType(string employeeId, int leaveTypeId)
        {
            var period = DateTime.Now.Year;
            return await _db.LeaveAllocations
                .Include(l => l.LeaveType)
                .SingleOrDefaultAsync(l => l.EmployeeId == employeeId && l.Period == period && l.LeaveTypeId == leaveTypeId);
        }
    }
}
