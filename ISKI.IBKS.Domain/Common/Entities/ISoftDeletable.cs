using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Domain.Common.Entities;

public interface ISoftDeletable
{
    bool IsDeleted { get; }
}

