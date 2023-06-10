using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace HRMS.Application.UseCases.Employees.Notifications
{
    public record EmployeeDeletedNotification(string name) : INotification;

    public class EmployeeDeletedNotificationHandler : INotificationHandler<EmployeeDeletedNotification>
    {
        public Task Handle(EmployeeDeletedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($"HRMS: Employee with {notification.name} name deleted.");

            return Task.CompletedTask;
        }
    }
}
