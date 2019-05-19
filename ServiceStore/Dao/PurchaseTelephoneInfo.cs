using ServiceStore.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.Dao
{
    class PurchaseInfoDao
    {
        SqlConnection connection = null;
        DetailDao detailtDao;
        CustomerDao customerDao;
        TelephoneDao telephoneDao;
        DiscountDao discountDao;
        PurchaseDao purchaseDao;
        ProducerDao producerDao;
        TrademarkDao trademarkDao;
        TelephoneModelDao telephoneModelDao;
        PurchaseServiceDao purchaseServiceDao;

        public PurchaseInfoDao(SqlConnection connection)
        {
            this.connection = connection;
            discountDao = new DiscountDao(connection);
            detailtDao = new DetailDao(connection);
            customerDao = new CustomerDao(connection);
            telephoneDao = new TelephoneDao(connection);
            producerDao = new ProducerDao(connection);
            purchaseDao = new PurchaseDao(connection);
            trademarkDao = new TrademarkDao(connection);
            telephoneModelDao = new TelephoneModelDao(connection);
            purchaseServiceDao = new PurchaseServiceDao(connection);
        }

        public List<PurchaseInfo> SelectPurchaseInfo()
        {
            List<Purchase> purchases = purchaseDao.SelectAllPurchses();
            List<PurchaseInfo> purchaseInfos = new List<PurchaseInfo>();
            
            for(int i = 0; i < purchases.Count; i++)
            {
                string C_Purchase = purchases[i].C_Purchase;
                string C_Customer = purchases[i].C_Customer;
                string Status = purchases[i].Status;
                string IMEI = purchases[i].IMEI;
                string C_TelephoneModel = telephoneDao.SelectTelephoneById(IMEI).C_TelephoneModel;
                string C_Tradeamark = telephoneModelDao.SelectTelephoneModelById(C_TelephoneModel).C_Trademark;
                string C_Producer = trademarkDao.SelectTrademarkById(C_Tradeamark).C_Producer;
                string C_Service = purchaseServiceDao.SelectServicePurchasById(C_Purchase).Service;

                PurchaseInfo purchaseInfo = new PurchaseInfo(C_Customer, Status, IMEI, C_TelephoneModel, C_Producer, C_Tradeamark, C_Service);
                purchaseInfos.Add(purchaseInfo);

            }   
            return purchaseInfos;
        }

    }
}
