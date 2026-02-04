using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.Results;

public interface IResult
{
    public bool IsSuccess { get; }
    public Error Error { get; }
}
