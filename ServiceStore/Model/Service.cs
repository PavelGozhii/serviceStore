namespace ServiceStore.Model
{
    public class Service
    {
        public string C_Service { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        public string Detail { get; set; }

        public Service(string c_Service, string name, string category,
            double pRice, string detail)
        {
            C_Service = c_Service;
            Name = name;
            Category = category;
            Price = pRice;
            Detail = detail;
        }
    }
}