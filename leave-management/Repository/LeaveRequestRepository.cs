using System;
using System.Linq;
using leave_management.Data;
using System.Collections.Generic;
using leave_management.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<LeaveRequest>> FindAll() => await _db.LeaveRequests
            .Include(l => l.RequestingEmployee)
            .Include(l => l.ApprovedBy)
            .Include(l => l.LeaveType)
            .ToListAsync();

        public async Task<LeaveRequest> FindById(int id) => await _db.LeaveRequests
            .Include(l => l.RequestingEmployee)
            .Include(l => l.ApprovedBy)
            .Include(l => l.LeaveType)
            .SingleOrDefaultAsync(l => l.Id == id);

        public async Task<bool> Exists(int id) => await _db.LeaveRequests.AnyAsync(l => l.Id == id);

        public async Task<bool> Create(LeaveRequest entity)
        {
            await _db.LeaveRequests.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save() => await _db.SaveChangesAsync() > 0;

        public async Task<ICollection<LeaveRequest>> GetLeaveRequestsByEmployee(string employeeId) => 
            await _db.LeaveRequests
            .Where(l => l.RequestingEmployeeId == employeeId)
            .Include(l => l.RequestingEmployee)
            .Include(l => l.ApprovedBy)
            .Include(l => l.LeaveType)
            .ToListAsync();

    }
}
