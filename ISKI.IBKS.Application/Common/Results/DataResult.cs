using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.Results;

public class DataResult<T> : Result, IDataResult<T>
{
    public T? Data { get; }

    public DataResult(bool isSuccess, T? data, Error error)
        : base(isSuccess, error)
    {
        Data = data;
    }

    public DataResult(bool isSuccess, T? data)
        : base(isSuccess)
    {
        Data = data;
    }

    public static DataResult<T> Success(T data) =>
        new(true, data, Error.None);

    public static new DataResult<T> Failure(Error error) =>
        new(false, default, error);
}
