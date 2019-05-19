using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.Model
{
    class PurchaseService
    {
        public string Purchase { get; set; }
        
        public string Service { get; set; }

        public PurchaseService(string purchase, string service)
        {
            Purchase = purchase;
            Service = service;
        }
    }
}
