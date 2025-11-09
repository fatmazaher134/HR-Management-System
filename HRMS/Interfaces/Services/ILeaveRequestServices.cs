using HRMS.Models;

namespace HRMS.Interfaces.Services
{
    public interface ILeaveRequestServices
    {
        Task<IEnumerable<LeaveRequest>> GetAllAsync();
        Task<LeaveRequest?> GetByIdAsync(int id);
        Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(int employeeId);
        Task<LeaveRequest> AddAsync(LeaveRequest request);
        Task<bool> ApproveAsync(int id, int approvedById);
        Task<bool> RejectAsync(int id, string reason);
        Task<bool> CancelAsync(int id);
    }

}
