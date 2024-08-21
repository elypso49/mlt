using mlt.common.datas.dtos;
using mlt.common.dtos.responses;

namespace mlt.common.services;

public interface ICrudService<T>
    where T : class
{
    public Task<ResponseDto<IEnumerable<T>>> GetAll();
    public Task<ResponseDto<T>> GetById(string id);
    public Task<ResponseDto<T>> Add(T feed);
    public Task<ResponseDto<UpdateResponse>> Update(string id, T feed);
    public Task<ResponseDto<DeleteResponse>> Delete(string id);
}