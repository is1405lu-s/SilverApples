using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class UtilsDAL
    {

        public delegate object[] Delegate<Temp>(Temp t, ref string[] headers);

        public static SqlConnection Connect()
        {
            string connString = "user id=sa;password=password;server=localhost;Trusted_Connection=yes;database=SilverApples;";
            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
            }
            catch
            {
                throw;
            }
            return conn;
        }
  
        public static List<Temp> GetAnything<Temp>(string sql, Func<SqlDataReader, Temp> method)
        {
            SqlDataReader r = null;
            List<Temp> resultList = new List<Temp>();
            SqlConnection conn = Connect();
            if (conn == null)
                return null;
            SqlCommand c = new SqlCommand(sql, conn);
            try
            {
                r = c.ExecuteReader();
                while (r.Read())
                {
                    if (r.GetValue(0) != DBNull.Value)
                    {
                        Temp t = method(r);
                        resultList.Add(t);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (r != null)
                    r.Close();
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return resultList;
        }

        public static int CreateUpdateDeleteAnything<Temp>(Temp t, Func<Temp, string> method) //method tar in ett object (= Temp) och returnerar en string (=SQL-sats)
        {
            SqlConnection conn = Connect();
            int i = 0;
            if (conn == null)
                return 0;
            try
            {
                string sql = method(t);
                SqlCommand c = new SqlCommand(sql, conn);
                i = c.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return i;
        }

        public static DataTable ShowTable<Temp>(List<Temp> list, Delegate<Temp> method)
        {
            DataTable table = new DataTable();
            string[] headers = new string[0];
            int columns = 0;
            Temp t = default(Temp);
            try
            {
                if (list.Count() != 0)
                {
                    //Gets header
                    columns = method(t, ref headers).Length;

                    for (int i = 0; i < columns; i++)
                    {
                        table.Columns.Add(headers[i]);
                    }
                    foreach (Temp te in list)
                    {
                        //Gets a row from table
                        object[] objectList = method(te, ref headers);
                        table.Rows.Add(objectList);
                    }
                }
                else
                {
                    method(t, ref headers);
                    for (int i = 0; i < headers.Length; i++)
                        table.Columns.Add(headers[i]);
                }
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return table;
        }
    }
}
