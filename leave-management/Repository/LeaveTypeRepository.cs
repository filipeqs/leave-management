using System;
using System.Linq;
using leave_management.Data;
using System.Collections.Generic;
using leave_management.Contracts;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<LeaveType>> FindAll() => await _db.LeaveTypes.ToListAsync();

        public async Task<LeaveType> FindById(int id) => await _db.LeaveTypes.SingleOrDefaultAsync(l => l.Id == id);

        public async Task<bool> Exists(int id) => await _db.LeaveTypes.AnyAsync(l => l.Id == id);

        public async Task<bool> Create(LeaveType entity)
        {
            await _db.LeaveTypes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Update(LeaveType entity)
        {
            _db.LeaveTypes.Update(entity);
            return await Save();
        }

        public async Task<bool> Delete(LeaveType entity)
        {
            _db.LeaveTypes.Remove(entity);
            return await Save();
        }

        public async Task<bool> Save() => await _db.SaveChangesAsync() > 0;
    }
}
