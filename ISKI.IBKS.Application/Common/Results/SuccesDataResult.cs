using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.Results;

internal class SuccesDataResult<T> : DataResult<T>
{
    public SuccesDataResult(T? data, Error error) : base(true, data, error)
    {
    }
    public SuccesDataResult(T? data) : base(true, data)
    {
    }
}
