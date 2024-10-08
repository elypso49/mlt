﻿namespace mlt.common.datas.dtos;

public class UpdateResponse : Acknowledgable<UpdateResponse>
{
    public bool IsModifiedCountAvailable { get; init; }
    public long MatchedCount { get; init; }
    public long ModifiedCount { get; init; }
    public Guid UpsertedId { get; init; }

    public override UpdateResponse ValidateResult()
    {
        if (IsAcknowledged)
            return this;

        throw new Exception("An exception occured, no feed has been updated");
    }
}