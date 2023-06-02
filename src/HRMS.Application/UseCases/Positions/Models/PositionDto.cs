using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain.Entities.Departments;
using HRMS.Domain.Entities.Employees;
using HRMS.Domain.Entities.Salaries;

namespace HRMS.Application.UseCases.Positions.Models
{
    public class PositionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Salary Salary { get; set; }

        public Department Department { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
