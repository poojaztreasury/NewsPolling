using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsPolling.Utility
{
    public static class ConnectionString
    {
        private static string cName = "Data Source=DESKTOP-K7B5EN0\\SQLEXPRESS;Initial Catalog=newspolling;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        public static string CName
        {
            get => cName;
        }
    }
}
