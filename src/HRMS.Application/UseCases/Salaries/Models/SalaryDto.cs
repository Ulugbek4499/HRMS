using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Application.UseCases.Positions.Models;
using HRMS.Domain.Entities.Positions;

namespace HRMS.Application.UseCases.Salaries.Models
{
    public class SalaryDto
    {
        public decimal SalaryAmount { get; set; }
        public int MonthlyWorkingHours { get; set; }
        public PositionDto Position { get; set; }
    }
}
