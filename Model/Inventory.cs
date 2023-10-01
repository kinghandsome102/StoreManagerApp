using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagerApp.Model
{
    public class Inventory
    {
        public Product product { get; set; }
        public int SST { get; set; }
        public int Count { get; set; }
    }
}
