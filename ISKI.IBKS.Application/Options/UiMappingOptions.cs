using System.Collections.Generic;

namespace ISKI.IBKS.Application.Common.IoT.Plc;

public class UiMappingOptions
{
    public List<PlcParameterMapping> Mappings { get; set; } = new();
}

public class PlcParameterMapping
{
    public string ParameterCode { get; set; } = string.Empty;
    public string PlcTagName { get; set; } = string.Empty;
    public string Unit { get; set; } = string.Empty;
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }
}
