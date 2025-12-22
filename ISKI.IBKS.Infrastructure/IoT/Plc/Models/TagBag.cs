using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.IoT.Plc.Models;

public sealed class TagBag
{
    private readonly Dictionary<string, object?> _values =
        new(StringComparer.OrdinalIgnoreCase);

    public void Set<T>(string key, T? value)
        => _values[key] = value;

    public bool TryGet<T>(string key, out T? value)
    {
        if (_values.TryGetValue(key, out var obj) && obj is T t)
        {
            value = t;
            return true;
        }

        value = default;
        return false;
    }

    public T? GetOrDefault<T>(string key, T? defaultValue = default)
        => TryGet<T>(key, out var v) ? v : defaultValue;
}
