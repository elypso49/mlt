namespace mlt.common.services;

public interface ICrudService<T>
    where T : class
{
    public Task<IEnumerable<T>> GetAll();

    public Task<T> GetById(string id);

    public Task<T> Add(T feed);

    public Task<UpdateResponse> Update(string id, T feed);

    public Task<DeleteResponse> Delete(string id);
}