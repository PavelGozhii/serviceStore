namespace ServiceStore.Model
{
    public class Customer
    {
        public string C_Customer { get; set; }

        public string FullName { get; set; }

        public string DateOfBirth { get; set; }
        
        public string Address { get; set; }

        public Customer(string c_Customer, string fullName,
            string dateOfBirth, string address)
        {
            C_Customer = c_Customer;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Address = address;
        }
    }
}