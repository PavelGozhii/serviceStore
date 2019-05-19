namespace ServiceStore.Model
{
    public class Purchase
    {
        public string C_Purchase { get; set; }

        public string Status { get; set; }

        public string DatePurchase { get; set; }

        public string C_Customer { get; set; }

        public string IMEI { get; set; }

        public Purchase(string c_Purchase, string status, string datePurchase,
            string c_Customer, string iMEI)
        {
            C_Purchase = c_Purchase;
            Status = status;
            DatePurchase = datePurchase;
            C_Customer = c_Customer;
            IMEI = iMEI;
        }
    }
}