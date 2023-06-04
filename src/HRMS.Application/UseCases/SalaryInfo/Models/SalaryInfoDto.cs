using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.UseCases.SalaryInfo.Models
{
    public class SalaryInfoDto
    {
        public string Name { get; set; }
        public string PositionName { get; set; }
        public int FixedWorkingHours { get; set; }
        public double ActualWorkingHours { get; set; }
        public decimal FixedSalary { get; set; }
        public decimal ActualSalary { get; set; }
    }
}
