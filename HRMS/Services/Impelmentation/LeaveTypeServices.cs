using HRMS.Interfaces.Services;
using HRMS.Models;

namespace HRMS.Services.Impelmentation
{
    public class LeaveTypeServices : ILeaveTypeServices
    {
        public Task<LeaveType> AddAsync(LeaveType leaveType)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LeaveType>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<LeaveType?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(LeaveType leaveType)
        {
            throw new NotImplementedException();
        }
    }
}
