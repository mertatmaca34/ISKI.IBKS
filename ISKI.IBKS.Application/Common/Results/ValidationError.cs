using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Application.Common.Results;

public sealed class ValidationError : Error
{
    public string Field { get; }

    private ValidationError(string field, string code, string message)
        : base(code, message)
    {
        Field = field;
    }

    public static ValidationError Create(string field, string message)
        => new(field, "VALIDATION_ERROR", message);
}
