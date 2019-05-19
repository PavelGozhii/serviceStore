namespace ServiceStore.Model
{
    public class Producer
    {
        public string C_Producer { get; set; }

        public string Director { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string Review { get; set; }

        public string PhisicalAdress { get; set; }

        public Producer(string c_Producer, string Director, 
            string tepephone, string email, string Review, string phisicalAdress)
        {
            C_Producer = c_Producer;
            this.Director = Director;
            Telephone = tepephone;
            Email = email;
            this.Review = Review;
            PhisicalAdress = phisicalAdress;
        }
    }
}