using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace API.Query
{
    public class AccountQuery
    {
        

        public string getAccount()
        {
            string querry = "select userAdmin,tenAdmin from Admin where Loai <> 1";
            return Execute.ExcuteQuery(querry);
        }
       
    }
}
