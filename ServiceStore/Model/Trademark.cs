namespace ServiceStore.Model
{
    public class Trademark
    {
        public string C_Trademark { get; set; }

        public string Name { get; set; }

        public string Review { get; set; }

        public string C_Producer { get; set; }

        public Trademark(string C_Trademark, string Name, 
            string Review, string C_Producer)
        {
            this.C_Trademark = C_Trademark;
            this.Name = Name;
            this.Review = Review;
            this.C_Producer = C_Producer;
        }
    }
}