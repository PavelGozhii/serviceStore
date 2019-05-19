using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.Model
{
    class PriceList
    {
        public string NameService { get; set; }

        public string ServicePrice { get; set; }

        public string DiscountSize { get; set; }

        public string NameDiscount { get; set; }

        public string DetailType { get; set; }

        public PriceList(string nameService, string servicePrice, string discountSize, string nameDiscount, string detailType)
        {
            NameService = nameService;
            ServicePrice = servicePrice;
            DiscountSize = discountSize;
            NameDiscount = nameDiscount;
            DetailType = detailType;
        }
    }
}
