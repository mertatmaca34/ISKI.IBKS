using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.Results;

public class ErrorDataResult<T> : DataResult<T>
{

    public ErrorDataResult(T? value, Error error) : base(false, value, error)
    {
    }
    public ErrorDataResult(T? value) : base(false, value)
    {
    }
}
