using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.SAIS.Exceptions;

public sealed class SaisApiException : Exception
{
    public SaisApiException(string message)
        : base(message)
    {
    }

    public SaisApiException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
