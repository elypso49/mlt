namespace mlt.common.datas.dtos;

public class DeleteResponse : Acknowledgable<DeleteResponse>
{
    public long DeletedCount { get; init; }

    public override DeleteResponse ValidateResult()
    {
        if (IsAcknowledged)
            return this;

        throw new Exception("An exception occured, no feed has been removed");
    }
}