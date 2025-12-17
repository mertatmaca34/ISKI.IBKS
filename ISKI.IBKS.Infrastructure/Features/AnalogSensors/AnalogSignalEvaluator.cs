using ISKI.IBKS.Application.Features.AnalogSensors.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.Features.AnalogSensors;

public static class AnalogSignalEvaluator
{
    public static AnalogSignalStatus Evaluate(
        bool isAutoMode,
        double? value)
    {
        if (value is null)
            return AnalogSignalStatus.Undefined;

        if (isAutoMode && value == 0)
            return AnalogSignalStatus.Critical;

        return AnalogSignalStatus.Normal;
    }
}
