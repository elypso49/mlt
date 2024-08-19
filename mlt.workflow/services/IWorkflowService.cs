namespace mlt.workflow.services;

public interface IWorkflowService
{
    public Task<bool> DownloadAll();
}