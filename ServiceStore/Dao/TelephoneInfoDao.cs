using ServiceStore.Model;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ServiceStore.Dao
{
    class TelephoneInfoDao
    {
        SqlConnection connection;
        ProducerDao producerDao;
        TrademarkDao trademarkDao;
        TelephoneModelDao telephoneModelDao;

        public TelephoneInfoDao(SqlConnection connection)
        {
            this.connection = connection ;
            producerDao = new ProducerDao(connection);
            trademarkDao = new TrademarkDao(connection);
            telephoneModelDao = new TelephoneModelDao(connection);
        }

        public List<TelephoneInfo> SelectAllTelephoneInfo()
        {
            List<TelephoneModel> telephoneModels = telephoneModelDao.selectAllTelephomeModel();
            List<Producer> producers = producerDao.SelectAllProducers();
            List<Trademark> trademarks = trademarkDao.SelectAllTrademark();
            List<TelephoneInfo> telephoneInfos = new List<TelephoneInfo>();

            for(int i = 0; i < telephoneModels.Count; i++)
            {
                TelephoneInfo telephoneInfo = new TelephoneInfo(
                   telephoneModels[i].Name, telephoneModels[i].Category, telephoneModels[i].OperatingSystem, telephoneModels[i].MemorySize,
                   telephoneModels[i].Processor, telephoneModels[i].C_Trademark, trademarkDao.SelectTrademarkById(telephoneModels[i].C_Trademark).C_Producer);
                telephoneInfos.Add(telephoneInfo);
            }
            return telephoneInfos;
        }

    }
}
