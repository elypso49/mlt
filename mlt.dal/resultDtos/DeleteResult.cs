namespace mlt.dal.resultDtos;

public class DeleteResult : Acknowledgable<DeleteResult>
{
    public long DeletedCount { get; init; }

    public override DeleteResult ValidateResult()
    {
        if (IsAcknowledged)
            return this;

        throw new Exception("An exception occured, no feed has been removed");
    }
}