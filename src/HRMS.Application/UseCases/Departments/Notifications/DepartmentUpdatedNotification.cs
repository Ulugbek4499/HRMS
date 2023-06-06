using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMS.Domain.Entities.Departments;
using MediatR;
using Serilog;

namespace HRMS.Application.UseCases.Departments.Notifications
{

    public class DepartmentUpdatedNotification : INotification
    {
        public Department CurrentDepartment { get; set; }
        public Department UpdatedDepartment { get; set; }
    }

    public class DepartmentUpdatedNotificationHandler : INotificationHandler<DepartmentUpdatedNotification>
    {
        public Task Handle(DepartmentUpdatedNotification notification, CancellationToken cancellationToken)
        {

            Log.Information($"HRMS: Update department notification\nCurrent department: {notification.CurrentDepartment}\n" +
                $"Updated department: {notification.UpdatedDepartment}");

            return Task.CompletedTask;
        }
    }

}
