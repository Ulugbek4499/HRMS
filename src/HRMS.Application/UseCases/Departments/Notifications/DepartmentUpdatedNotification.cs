using HRMS.Domain.Entities.Departments;
using MediatR;
using Serilog;

namespace HRMS.Application.UseCases.Departments.Notifications
{
    public record DepartmentUpdatedNotification(string UpdatedDepartmentName) : INotification;
    public class DepartmentUpdatedNotificationHandler : INotificationHandler<DepartmentUpdatedNotification>
    {
        public Task Handle(DepartmentUpdatedNotification notification, CancellationToken cancellationToken)
        {

            Log.Information($"HRMS: Update department notification "+$"Updated department: {notification.UpdatedDepartmentName}");

            return Task.CompletedTask;
        }
    }
}
