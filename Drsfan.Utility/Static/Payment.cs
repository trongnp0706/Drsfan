using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drsfan.Utility.Static
{
    public static class Payment
    {
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusDelayedPayment = "DelayPaymentToApproved";
        public const string StatusRejected = "Rejected";
    }
}
