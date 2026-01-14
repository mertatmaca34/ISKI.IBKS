using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.Results;

internal class SuccessResult : Result
{
    protected SuccessResult() : base(true, Error.None)
    {
    }
}
