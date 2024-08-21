using mlt.common.dtos.responses;

namespace mlt.common.services;

public abstract class BaseService()
{
    protected static async Task<ResponseDto<T>> HandleDataRetrievement<T>(Func<Task<T>> process) where T : class?
    {
        try
        {
            var result = await process();
            if (result == null)
                return null!;

            return new() { Data = result };
        }
        catch (Exception e)
        {
            return ManageError<T>(e);
        }
    }

    protected static ResponseDto<T> ManageError<T>(Exception exception, ResponseDto<T>? existingResponse = null) where T : class?
    {
        Console.WriteLine(exception);
        var errors = new List<ResponseError> { new() { Message = exception.Message, Source = exception.Source, StackTrace = exception.StackTrace } };

        if (existingResponse != null)
        {
            existingResponse.Errors.AddRange(errors);
            return existingResponse;
        }

        return new() { Errors = errors };
    }

    protected static T HandleData<T, TU>(ResponseDto<T> handledResponse, ResponseDto<TU> mainResponse)
        where T : class
        where TU : class
    {
        if (handledResponse is { IsSuccess: true, Data: not null })
        {
            return handledResponse.Data;
        }

        mainResponse.Errors.AddRange(handledResponse.Errors);
        return null!;
    }
}