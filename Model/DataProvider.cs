using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagerApp.Model
{
    public class DataProvider
    {
        //StoreManagerEntities use name entity framwork
        // in this demo not use function using() {}
        //StoreManagerEntities DbSupport = new StoreManagerEntities();
        private static DataProvider _ins = null;
        public static DataProvider Ins
        {
            get 
            {
                if (_ins == null)
                    _ins = new DataProvider();
                return _ins;
            }
            set { _ins = value; }
        }

        public StoreManagerEntities DB { get; set; }

        private DataProvider() 
        {
            DB = new StoreManagerEntities();
        }
    }
}
