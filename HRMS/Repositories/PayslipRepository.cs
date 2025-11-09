using HRMS.Data;
using HRMS.Interfaces;
using HRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Repositories;

public class PayslipRepository : GenericRepository<Payslip>, IPayslipRepository
{
    public PayslipRepository(ApplicationDbContext context) : base(context)
    {
    }
}
