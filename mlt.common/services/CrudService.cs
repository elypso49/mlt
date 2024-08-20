using mlt.common.datas;
using mlt.common.datas.dtos;
using mlt.common.dtos.responses;

namespace mlt.common.services;

public abstract class CrudService<T>(ICrudRepository<T> repository) : BaseService, ICrudService<T>
    where T : class
{
    public Task<ResponseDto<IEnumerable<T>>> GetAll()
        => HandleDataRetrievement(async () => await repository.GetAll());

    public Task<ResponseDto<T>> GetById(string id)
        => HandleDataRetrievement(async () => await repository.GetById(id));

    public Task<ResponseDto<T>> Add(T result)
        => HandleDataRetrievement(async () => await repository.Add(result));

    public Task<ResponseDto<UpdateResponse>> Update(string id, T result)
        => HandleDataRetrievement(async () => await repository.Update(id, result));

    public Task<ResponseDto<DeleteResponse>> Delete(string id)
        => HandleDataRetrievement(async () => await repository.Delete(id));
}