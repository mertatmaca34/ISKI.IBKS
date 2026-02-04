namespace ISKI.IBKS.Application.Common.Extensions;
public static class NumericExtensions
{
    public static double ToDouble(this object? input)
    {
        if (input is null)
            return 0d;

        return input switch
        {
            double d => d,
            float f => f,
            int i => i,
            long l => l,
            decimal m => (double)m,
            string s when double.TryParse(s, out var val) => val,
            _ => 0d
        };
    }
}

