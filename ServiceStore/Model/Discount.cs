namespace ServiceStore.Model
{
    public class Discount
    {
        public string C_Discount { get; set; }

        public int Size { get; set; }
        
        public string Name { get; set; }

        public string Starting { get; set; }

        public string Ending { get; set; }

        public Discount(string c_Discount, int size, string name,
            string starting, string ending)
        {
            C_Discount = c_Discount;
            Size = size;
            Name = name;
            Starting = starting;
            Ending = ending;
        }
    }
}