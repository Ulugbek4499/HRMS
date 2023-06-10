using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace HRMS.Application.UseCases.Positions.Notifications
{
    public record PositionCreatedNotification(string name) : INotification;

    public class PositionCreatedNotificationHandler : INotificationHandler<PositionCreatedNotification>
    {
        public Task Handle(PositionCreatedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($"HRMS: New Position created with {notification.name} name.");

            return Task.CompletedTask;
        }
    }
}
