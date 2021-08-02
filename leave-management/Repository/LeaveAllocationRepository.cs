using System;
using System.Linq;
using leave_management.Data;
using System.Collections.Generic;
using leave_management.Contracts;
using Microsoft.EntityFrameworkCore;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICollection<LeaveAllocation> FindAll() => _db.LeaveAllocations.Include(l => l.LeaveType).ToList();

        public LeaveAllocation FindById(int id) => _db.LeaveAllocations.SingleOrDefault(l => l.Id == id);
        public bool Exists(int id) => _db.LeaveAllocations.Any(l => l.Id == id);

        public bool Create(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return Save();
        }

        public bool Save() => _db.SaveChanges() < 0;

        public bool HasAllocation(int leaveTypeId, string employeeId)
        {
            var period = DateTime.Now.Year;
            return FindAll().Any(q => q.EmployeeId == employeeId && q.LeaveTypeId == leaveTypeId && q.Period == period);
        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id)
        {
            var period = DateTime.Now.Year;
            return _db.LeaveAllocations.Where(l => l.EmployeeId == id && l.Period == period)
                                        .Include(l => l.LeaveType)
                                        .ToList();
        }
    }
}
