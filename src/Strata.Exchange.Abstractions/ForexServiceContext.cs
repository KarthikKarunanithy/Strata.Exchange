using System;
using System.Collections.Generic;
using System.Text;

namespace Strata.Exchange.Abstractions
{
    public class ForexServiceContext
    {
        public string CorrelationId { get; set; }

        public string TenantId { get; set; }
    }
}
