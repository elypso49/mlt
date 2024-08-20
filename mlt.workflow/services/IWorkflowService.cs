using mlt.common.dtos.responses;
using mlt.common.dtos.workflows;

namespace mlt.workflow.services;

public interface IWorkflowService
{
    public Task<ResponseDto<WorkflowResponse>> DownloadAll();
}