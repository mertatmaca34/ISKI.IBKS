using System.Globalization;

namespace ISKI.IBKS.Presentation.WinForms.Extensions;

public static class NumericFormattingExtensions
{
    public static string ToUiValue(
        this double? value,
        int decimals = 2,
        string empty = "-")
    {
        if (!value.HasValue)
            return empty;

        return value.Value
            .ToString($"F{decimals}", CultureInfo.InvariantCulture);
    }
}
