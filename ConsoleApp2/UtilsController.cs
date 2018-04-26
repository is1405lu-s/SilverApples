using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DAL;
using System.Data;
namespace Controller
{
    public class UtilsController
    {

        public static SqlConnection Connect()
        {
            return UtilsDAL.Connect();
        }

        public static List<Temp> GetAnything<Temp>(string sql, Func<SqlDataReader, Temp> method)
        {
            return UtilsDAL.GetAnything(sql, method);
        }

        public static int CreateUpdateDeleteAnything<Temp>(Temp t, Func<Temp, string> method)
        {
            return UtilsDAL.CreateUpdateDeleteAnything(t, method);
        }

        public static DataTable ShowTable<Temp>(List<Temp> list, UtilsDAL.Delegate<Temp> method)
        {
            return UtilsDAL.ShowTable(list, method);
        }
    }
}
