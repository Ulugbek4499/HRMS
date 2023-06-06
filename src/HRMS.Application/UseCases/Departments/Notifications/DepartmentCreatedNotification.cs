using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace HRMS.Application.UseCases.Departments.Notifications
{
    public record DepartmentCreatedNotification(string name) : INotification;

    public class DepartmentCreatedLogNotificationHandler : INotificationHandler<DepartmentCreatedNotification>
    {
        public Task Handle(DepartmentCreatedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($"HRMS: New department created with {notification.name} name.");

            return Task.CompletedTask;
        }
    }

    public class DepartmentCreatedConsoleNotificationHandler : INotificationHandler<DepartmentCreatedNotification>
    {
        public async Task Handle(DepartmentCreatedNotification notification, CancellationToken cancellationToken)
        {
            await Console.Out.WriteLineAsync($"HRMS: New department created with {notification.name} name.");
        }
    }
}
