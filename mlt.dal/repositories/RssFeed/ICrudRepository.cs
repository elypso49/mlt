using mlt.dal.resultDtos;

namespace mlt.dal.repositories.RssFeed;

public interface ICrudRepository<T> where T:class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(string id);
    Task<T> Add(T result);
    Task<UpdateResult> Update(string id, T result);
    Task<DeleteResult> Delete(string id); 
}