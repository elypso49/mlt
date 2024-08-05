namespace mlt.dal.resultDtos;

public abstract class Acknowledgable<T> where T:class
{
    public bool IsAcknowledged { get; init; }
    public abstract T ValidateResult();
}