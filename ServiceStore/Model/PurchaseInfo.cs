using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.Model
{
    class PurchaseInfo
    {
        public string Customer { get; set; }
        
        public string Status { get; set; }
        
        public string IMEI { get; set; }

        public string TelephoneModel { get; set; }
        
        public string Producer { get; set; }

        public string Trademark { get; set; }

        public string Service { get; set; }

        public PurchaseInfo(string customer, string status, string iMEI, string telephoneModel, string producer, string trademark, string Service)
        {
            Customer = customer;
            Status = status;
            IMEI = iMEI;
            TelephoneModel = telephoneModel;
            Producer = producer;
            Trademark = trademark;
            this.Service = Service;
        }
    }
}
