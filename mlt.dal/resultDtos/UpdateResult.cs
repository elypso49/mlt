namespace mlt.dal.repositories;

public class UpdateResult : Acknowledgable<UpdateResult>
{
    public bool IsModifiedCountAvailable { get; init; }
    public long MatchedCount { get; init; }
    public long ModifiedCount { get; init; }
    public Guid UpsertedId { get; init; }

    public override UpdateResult ValidateResult()
    {
        if (IsAcknowledged)
            return this;
        
        throw new Exception("An exception occured, no feed has been updated");
    }
}