namespace ServiceStore.Model
{
    public class DetailModel
    {
        public string C_DetailModel { get; set; }

        public double Price { get; set; }

        public string C_TelephoneModel { get; set; }

        public string C_Detail { get; set; }

        public DetailModel(string c_DetailModel, double price,
            string c_TelephoneModel, string c_Detail)
        {
            C_DetailModel = c_DetailModel;
            Price = price;
            C_TelephoneModel = c_TelephoneModel;
            C_Detail = c_Detail;
        }
    }
}