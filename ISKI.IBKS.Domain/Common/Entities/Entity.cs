using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Domain.Common.Entities;

public abstract class Entity <TId>
{
    public required TId Id { get; set; }
}
