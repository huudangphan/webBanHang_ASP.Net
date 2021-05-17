using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using API.Global;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace API.Query
{
    public class AccountQuery
    {
        

        public string getAccount()
        {
            string querry = "select * from Admin";
            return Execute.ExcuteQuery(querry);
        }
       
    }
}
