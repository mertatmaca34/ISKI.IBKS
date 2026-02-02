using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.RemoteApi.Exceptions;

public sealed class RemoteApiException : Exception
{
    public RemoteApiException(string message)
        : base(message)
    {
    }

    public RemoteApiException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

