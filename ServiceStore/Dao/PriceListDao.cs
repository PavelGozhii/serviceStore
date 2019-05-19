using ServiceStore.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.Dao
{
    class PriceListDao
    {
        SqlConnection connection = null;
        ServiceDao serviceDao;
        DiscountDao discountDao;
        DetailDao detailtDao;

        public PriceListDao(SqlConnection connection)
        {
            this.connection = connection;
            serviceDao = new ServiceDao(connection);
            discountDao = new DiscountDao(connection);
            detailtDao = new DetailDao(connection);
        }

        public List<PriceList> SelectPriceList()
        {
            List<Service> services = serviceDao.SelectAllService();
            List<Discount> discounts = discountDao.SelectAllDiscounts();
            int discount = 0;
            string discountName = "WithoutDiscount";
            for(int i = 0; i < discounts.Count; i++)
            {
                if(DateTime.Parse(discounts[i].Starting) < DateTime.Now
                    && DateTime.Parse(discounts[i].Ending) > DateTime.Now
                    && discounts[i].Size > discount)
                {
                    discount = discounts[i].Size;
                    discountName = discounts[i].Name;
                }
            }
            List<PriceList> priceLists = new List<PriceList>();
            for(int i = 0; i < services.Count; i++)
            {
                PriceList priceList = new PriceList(services[i].Name, services[i].Price.ToString(), 
                    discount.ToString(), discountName, detailtDao.SelectDetailById(services[i].Detail).Type);
                priceLists.Add(priceList);
            }
            return priceLists;
        }
    }
}
