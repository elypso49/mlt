namespace mlt.common.services;

public abstract class CrudService<T>(ICrudRepository<T> repository) : ICrudService<T>
    where T : class
{
    public Task<IEnumerable<T>> GetAll() => repository.GetAll();

    public Task<T> GetById(string id) => repository.GetById(id);

    public Task<T> Add(T result) => repository.Add(result);

    public Task<UpdateResult> Update(string id, T result) => repository.Update(id, result);

    public Task<DeleteResult> Delete(string id) => repository.Delete(id);
}