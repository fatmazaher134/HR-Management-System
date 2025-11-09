using HRMS.Interfaces.Services;
using HRMS.Models;

namespace HRMS.Services.Impelmentation
{
    public class LeaveRequestServices : ILeaveRequestServices
    {
        public Task<LeaveRequest> AddAsync(LeaveRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ApproveAsync(int id, int approvedById)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LeaveRequest>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<LeaveRequest?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RejectAsync(int id, string reason)
        {
            throw new NotImplementedException();
        }
    }
}
