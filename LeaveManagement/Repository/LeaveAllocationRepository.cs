using LeaveManagement.Contracts;
using LeaveManagement.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveManagement.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CheckAllocation(int leaveTypeId, string employeeId)
        {
            var period = DateTime.Now.Year;
            return FindAll().Any(q => q.EmployeeId == employeeId && q.LeaveTypeId == leaveTypeId && q.Period == period);
        }

        public bool Create(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Add(entity);
            return Save();
        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            return _db.LeaveAllocations.Include(q => q.LeaveType).Include(q => q.Employee).ToList();
        }

        public LeaveAllocation FindById(int id)
        {
            return _db.LeaveAllocations.Include(q => q.LeaveType).Include(q => q.Employee).FirstOrDefault(q => q.Id == id);
        }

        public ICollection<LeaveAllocation> GetLeaveAllocationsByEmployee(string id)
        {
            var period = DateTime.Now.Year;
            return FindAll().Where(a => a.EmployeeId == id && a.Period == period).ToList();
        }

        public bool IsExists(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public bool Update(LeaveAllocation entity)
        {
            _db.LeaveAllocations.Update(entity);
            return Save();
        }
    }
}
