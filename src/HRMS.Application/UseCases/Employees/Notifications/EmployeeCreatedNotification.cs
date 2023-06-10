using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace HRMS.Application.UseCases.Employees.Notifications
{
    public record EmployeeCreatedNotification(string name) : INotification;

    public class EmployeeCreatedNotificationHandler : INotificationHandler<EmployeeCreatedNotification>
    {
        public Task Handle(EmployeeCreatedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($"HRMS: New Employee created with {notification.name} name.");

            return Task.CompletedTask;
        }
    }
}
