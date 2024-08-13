namespace mlt.common.services;

public interface ICrudService<T>
    where T : class
{
    public Task<IEnumerable<T>> GetAll();

    public Task<T> GetById(string id);

    public Task<T> Add(T feed);

    public Task<UpdateResult> Update(string id, T feed);

    public Task<DeleteResult> Delete(string id);
}