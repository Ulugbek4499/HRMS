using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace HRMS.Application.UseCases.Positions.Notifications
{
    public record PositionDeletedNotification(string name) : INotification;

    public class PositionDeletedNotificationHandler : INotificationHandler<PositionDeletedNotification>
    {
        public Task Handle(PositionDeletedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($"HRMS: Position with {notification.name} name deleted.");

            return Task.CompletedTask;
        }
    }
}
