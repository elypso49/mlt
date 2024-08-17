using Microsoft.Extensions.DependencyInjection;
using mlt.workflow.services;

namespace mlt.workflow;

public static class ServiceInjection
{
    public static IServiceCollection GetWorkflowDependencyInjection(this IServiceCollection services)
        => services.AddScoped<IWorkflowService, WorkflowService>();
}