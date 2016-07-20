using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.DataAccess.EventHandler.Auditor
{
    public enum AuditType
    {
        Created,
        Updated,
        Deleted,
        Unchanged
    }
}
