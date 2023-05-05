using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAICAK.PettyCashReconciliation.Production
{
    public class AccountSummation
    {
        public double? MpesaCharges { get; set; }
        public double? VehicleMaintenance { get; set; }
        public double? Catering { get; set; }
        public double? Businesspromotion { get; set; }
        public double? ToiletriesDetergents { get; set; }
        public double? MealsTransport { get; set; }
        public double? Localtravel { get; set; }
        public double? Miscelleneous { get; set; }
        public Lookups lookups { get; set; }
    }
}
