using mlt.common.datas.dtos;

namespace mlt.common.datas;

public interface ICrudRepository<T> where T:class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(string id);
    Task<T> Add(T result);
    Task<UpdateResult> Update(string id, T result);
    Task<DeleteResult> Delete(string id); 
}