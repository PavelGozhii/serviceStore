namespace ServiceStore.Model
{
    public class Telephone
    {
        public string IMEI { get; set; }

        public string C_TelephoneModel { get; set; }

        public Telephone(string iMEI, string c_TelephoneModel)
        {
            IMEI = iMEI;
            C_TelephoneModel = c_TelephoneModel;
        }
    }
}