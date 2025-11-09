using HRMS.Interfaces.Services;
using HRMS.Models;

namespace HRMS.Services.Impelmentation
{
    public class AttendanceRecordServices : IAttendanceRecordServices
    {
        public Task<AttendanceRecord> AddAsync(AttendanceRecord record)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckInAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckOutAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AttendanceRecord>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AttendanceRecord>> GetByEmployeeIdAsync(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<AttendanceRecord?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AttendanceRecord record)
        {
            throw new NotImplementedException();
        }
    }

}
