namespace ServiceStore.Model
{
    public class Detail
    {
        public string C_Detail { get; set; }
        
        public string Type { get; set; }

        public Detail(string c_Detail, string type)
        {
            C_Detail = c_Detail;
            Type = type;
        }
    }
}