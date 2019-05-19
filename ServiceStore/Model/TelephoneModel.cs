namespace ServiceStore.Model
{
    public class TelephoneModel
    {
        public string C_TelephoneModel { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string OperatingSystem { get; set; }

        public int MemorySize { get; set; }

        public string Processor { get; set; }

        public string C_Trademark { get; set; }

        public TelephoneModel(string c_TelephoneModel, string name, string category, 
            string operatingSystem, int memorySize, string processor, string c_Trademark)
        {
            C_TelephoneModel = c_TelephoneModel;
            Name = name;
            Category = category;
            OperatingSystem = operatingSystem;
            MemorySize = memorySize;
            Processor = processor;
            C_Trademark = c_Trademark;
        }
    }
}