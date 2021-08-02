using System;
using System.Linq;
using leave_management.Data;
using System.Collections.Generic;
using leave_management.Contracts;

namespace leave_management.Repository
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ICollection<LeaveRequest> FindAll() => _db.LeaveRequests.ToList();

        public LeaveRequest FindById(int id) => _db.LeaveRequests.SingleOrDefault(l => l.Id == id);

        public bool Exists(int id) => _db.LeaveRequests.Any(l => l.Id == id);

        public bool Create(LeaveRequest entity)
        {
            _db.LeaveRequests.Add(entity);
            return Save();
        }

        public bool Update(LeaveRequest entity)
        {
            _db.LeaveRequests.Update(entity);
            return Save();
        }

        public bool Delete(LeaveRequest entity)
        {
            _db.LeaveRequests.Remove(entity);
            return Save();
        }

        public bool Save() => _db.SaveChanges() > 0;
    }
}
