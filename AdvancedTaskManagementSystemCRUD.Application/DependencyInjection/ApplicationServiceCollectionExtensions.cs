using AdvancedTaskManagementSystemCRUD.Application.Services;
using AdvancedTaskManagementSystemCRUD.Application.Services.Account;
using AdvancedTaskManagementSystemCRUD.Application.Services.Authorization;
using AdvancedTaskManagementSystemCRUD.Application.Services.Comment;
using AdvancedTaskManagementSystemCRUD.Application.Services.ChangesHistory;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configuration;
using AdvancedTaskManagementSystemCRUD.Application.Services.Project;
using AdvancedTaskManagementSystemCRUD.Application.Services.Reports;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task;
using Microsoft.Extensions.DependencyInjection;

namespace AdvancedTaskManagementSystemCRUD.Application.DependencyInjection;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IConfigurationService, ConfigurationService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectAccessService, ProjectAccessService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IChangesHistoryService, ChangesHistoryService>();
        return services;
    }
}
