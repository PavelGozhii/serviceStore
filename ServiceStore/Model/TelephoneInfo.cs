using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStore.Model
{
    class TelephoneInfo
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string OS { get; set; }

        public int MemorySize { get; set; }

        public string Processor { get; set; }

        public string Trademark { get; set; }

        public string Producer { get; set; }

        public TelephoneInfo(string name, string category, string oS, int memorySize, string processor, string trademark, string producer)
        {
            Name = name;
            Category = category;
            OS = oS;
            MemorySize = memorySize;
            Processor = processor;
            Trademark = trademark;
            Producer = producer;
        }
    }
}
