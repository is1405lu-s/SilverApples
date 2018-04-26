using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DAL
{
    public static class CronusDAL
    {

        public static SqlConnection ConnectNavision()
        {
            string connString = "user id=sa;password=password;server=localhost;Trusted_Connection=yes;database=Demo Database NAV (5-0);";
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

        public static SqlConnection ConnectSA()
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

        public static List<string> GetEmployees()
        {
                string sql = "SELECT No_,[First Name],[Last Name] FROM [CRONUS Sverige AB$Employee];";
            List<string> emplList = new List<string>();
            SqlCommand c = new SqlCommand(sql, ConnectNavision());
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    emplList.Add(r.GetString(0) + ": " + r.GetString(1) + " " + r.GetString(2));
                }
                r.Close();
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
                if (ConnectNavision().State == ConnectionState.Open)
                    ConnectNavision().Close();
            }
            return emplList;
        }

        public static List<string> GetEmplTables()
        {
            string sql = "SELECT * FROM sys.tables WHERE name LIKE 'CRONUS Sverige AB$Empl%'";
            List<string> emplList = new List<string>();
            SqlCommand c = new SqlCommand(sql, ConnectNavision());
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    emplList.Add(r.GetString(0));
                }
                r.Close();
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
                if (ConnectNavision().State == ConnectionState.Open)
                    ConnectNavision().Close();
            }
            return emplList;
        }
    

        public static List<List<string>> GetAllInfo(string table)
        {
            string sql = "SELECT * FROM [" + table + "]";
            int k = 0;
            List<List<string>> outerList = new List<List<string>>();
            SqlCommand c = new SqlCommand(sql, ConnectNavision());
            try
            {
                SqlDataReader r = c.ExecuteReader();
                if (table.Equals("CRONUS Sverige AB$Employee Statistics Group") || table.Equals("CRONUS Sverige AB$Employment Contract"))
                    k = 3;
                else
                    k = 8;
                while (r.Read())
                {
                    List<string> innerList = new List<string>();
                    for (int i = 0; i < k; i++)
                    {
                            innerList.Add(r.GetValue(i).ToString());
                    }
                    innerList.RemoveAt(0);
                    outerList.Add(innerList);
                }
                r.Close();
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
                if (ConnectNavision().State == ConnectionState.Open)
                    ConnectNavision().Close();
            }
            return outerList;
        }

        public static List<string> GetMetaDataColumnName(string table)
        {
            string sql = "SELECT TOP 8 INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + table + "';";
            List<string> list = new List<string>();
            SqlCommand c = new SqlCommand(sql, ConnectNavision());
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    list.Add(r.GetString(0));
                }
                r.Close();
                list.RemoveAt(0);
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
                if (ConnectNavision().State == ConnectionState.Open)
                    ConnectNavision().Close();
            }
            return list;
        }

        public static List<List<string>> GetAllKeys()
        {
            {
                string sql = "SELECT TOP 20 name, object_id, parent_object_id, type, type_desc FROM sys.objects WHERE type = 'PK'  or type = 'FK'";
                List<List<string>> outerList = new List<List<string>>();
                outerList.Add(new List<string> { "select name", "object_id", "parent_object_id", "type", "type_desc" });
                SqlConnection conn = ConnectNavision();
                SqlCommand c = new SqlCommand(sql, conn);
                //SqlCommand c = new SqlCommand(sql, ConnectNavision());
                try
                {
                    SqlDataReader r = c.ExecuteReader();
                    while (r.Read())
                    {
                        string s1 = r.GetString(0);
                        string s2 = r.GetInt32(1).ToString();
                        string s3 = r.GetInt32(2).ToString();
                        string s4 = r.GetString(3);
                        string s5 = r.GetString(4);
                        List<string> innerList = new List<string> { s1, s2, s3, s4, s5 };
                        outerList.Add(innerList);
                    }
                    r.Close();
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
                return outerList;
            }
        }

        public static List<List<string>> GetAllIndexes()
        {
            string sql = "SELECT TOP 20 name, type, type_desc, index_id, object_id FROM sys.indexes";
            List<List<string>> outerList = new List<List<string>>();
            outerList.Add(new List<string> { "select name", "type", "type_desc", "index_id", "object_id" });
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s1 = "NULL";
                    if (r.GetValue(0) != DBNull.Value)
                        s1 = r.GetString(0);
                    string s2 = r.GetByte(1).ToString();
                    string s3 = r.GetString(2);
                    string s4 = r.GetInt32(3).ToString();
                    string s5 = r.GetInt32(4).ToString();
                    List<string> innerList = new List<string> { s1, s2, s3, s4, s5 };
                    outerList.Add(innerList);
                }
                r.Close();
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
                if (conn != null || conn.State == ConnectionState.Open)
                   conn.Close();
            }
                return outerList;
        }

        public static List<List<string>> GetAllTableConstraints()
        {
            string sql = "SELECT top 20 constraint_catalog, constraint_schema, constraint_name, table_catalog, table_name FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS";
            List<List<string>> outerList = new List<List<string>>();
            outerList.Add(new List<string> { "constraint_catalog", "constraint_schema", "constraint_name", "table_catalog", "table_name" });
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s1 = r.GetString(0);
                    string s2 = r.GetString(1);
                    string s3 = r.GetString(2);
                    string s4 = r.GetString(3);
                    string s5 = r.GetString(4);
                    List<string> innerList = new List<string> { s1, s2, s3, s4, s5 };
                    outerList.Add(innerList);
                }
                r.Close();
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
            return outerList;
        }

        public static List<List<string>> GetAllTablesOne()
        {
            string sql = "SELECT TOP 20 name, object_id, type_desc, create_date, modify_date FROM sys.tables;";
            List<List<string>> outerList = new List<List<string>>();
            outerList.Add(new List<string> { "name", "object_id", "type_desc", "create_date", "modify_date" });
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s1 = r.GetString(0);
                    string s2 = r.GetInt32(1).ToString();
                    string s3 = r.GetString(2);
                    string s4 = r.GetDateTime(3).ToString().Substring(0,10);
                    string s5 = r.GetDateTime(4).ToString().Substring(0,10);
                    List<string> innerList = new List<string> { s1, s2, s3, s4, s5 };
                    outerList.Add(innerList);
                }
                r.Close();
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
            return outerList;
        }

        public static List<List<string>> GetAllTablesTwo()
        {
            string sql = "SELECT TOP 20 TABLE_NAME, TABLE_CATALOG, TABLE_SCHEMA, TABLE_TYPE FROM INFORMATION_SCHEMA.TABLES; ";
            List<List<string>> outerList = new List<List<string>>();
            outerList.Add(new List<string> { "TABLE_NAME", "TABLE_CATALOG", "TABLE_SCHEMA", "TABLE_TYPE" });
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s1 = r.GetString(0);
                    string s2 = r.GetString(1);
                    string s3 = r.GetString(2);
                    string s4 = r.GetString(3);
                    List<string> innerList = new List<string> { s1, s2, s3, s4 };
                    outerList.Add(innerList);
                }
                r.Close();
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
            return outerList;
        }

        public static List<List<string>> GetAllEmployeeColumnsOne()
        {
            string sql = "SELECT name, id, length FROM syscolumns WHERE id=OBJECT_ID('[CRONUS Sverige AB$Employee]');";
            List<List<string>> outerList = new List<List<string>>();
            outerList.Add(new List<string> { "name", "id", "length" });
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s1 = r.GetString(0);
                    string s2 = r.GetInt32(1).ToString();
                    string s3 = r.GetInt16(2).ToString(); //GetInt32(2).ToString();
                    List<string> innerList = new List<string> { s1, s2, s3 };
                    outerList.Add(innerList);
                }
                r.Close();
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
            return outerList;
        }

        public static List<List<string>> GetAllEmployeeColumnsTwo()
        {
            string sql = "SELECT COLUMN_NAME,TABLE_SCHEMA,TABLE_NAME,TABLE_CATALOG,ORDINAL_POSITION FROM [Demo Database NAV (5-0)].INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'CRONUS Sverige AB$Employee';";
            List<List<string>> outerList = new List<List<string>>();
            outerList.Add(new List<string> { "TABLE_NAME", "TABLE_SCHEMA", "TABLE_CATALOG", "COLUMN_NAME", "ORDINAL_POSITION" });
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s1 = r.GetString(0);
                    string s2 = r.GetString(1);
                    string s3 = r.GetString(2);
                    string s4 = r.GetString(3);
                    string s5 = r.GetInt32(4).ToString();
                    List<string> innerList = new List<string> { s1, s2, s3, s4, s5 };
                    outerList.Add(innerList);
                }
                r.Close();
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
            return outerList;
        }

        public static List<List<string>> GetEmplRelatives(string no)
        {
            no = no.Substring(0, 2);
            string sql = "SELECT [CRONUS Sverige AB$Employee].[First Name],[CRONUS Sverige AB$Employee].[Last Name],[Relative Code],[CRONUS Sverige AB$Employee Relative].[First Name],[CRONUS Sverige AB$Employee Relative].[Last Name],[CRONUS Sverige AB$Employee Relative].[Birth Date]" +
                " FROM [CRONUS Sverige AB$Employee Relative] INNER JOIN[CRONUS Sverige AB$Employee] ON[CRONUS Sverige AB$Employee].No_ = [CRONUS Sverige AB$Employee Relative].[Employee No_] WHERE[CRONUS Sverige AB$Employee].No_ = '" + no + "'; ";
            List<List<string>> outerList = new List<List<string>>();
            outerList.Add(new List<string> { "Employee", "Relationship", "Relative", "Birthday" });
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s1 = r.GetString(0) + " " + r.GetString(1);
                    string s2 = r.GetString(2);
                    string s3 = r.GetString(3) + " " + r.GetString(4);
                    string s4 = r.GetDateTime(5).ToString();
                    s4 = s4.Substring(0, 10);
                    List<string> innerList = new List<string> { s1, s2, s3, s4 };
                    outerList.Add(innerList);
                }
                r.Close();
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
            return outerList;
        }

        public static List<List<string>> GetSick2004()
        {
            string sql = "SELECT [CRONUS Sverige AB$Employee].[First Name], [CRONUS Sverige AB$Employee].[Last Name], [CRONUS Sverige AB$Employee Absence].[From Date], [CRONUS Sverige AB$Employee Absence].[Cause of Absence Code], [CRONUS Sverige AB$Employee Absence].Quantity" +
                " FROM [CRONUS Sverige AB$Employee Absence] INNER JOIN [CRONUS Sverige AB$Employee] ON[CRONUS Sverige AB$Employee].No_ = [CRONUS Sverige AB$Employee Absence].[Employee No_] WHERE[Cause of Absence Code] = 'SJUK' AND[From Date] LIKE '%2004%'; ";
            List<List<string>> outerList = new List<List<string>>();
            outerList.Add(new List<string> { "Employee", "Date", "Cause", "Hours" });
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s1 = r.GetString(0) + " " + r.GetString(1);
                    string s2 = r.GetDateTime(2).ToString().Substring(0, 10);
                    string s3 = r.GetString(3);
                    string s4 = r.GetDecimal(4).ToString().Substring(0,1);
                    List<string> innerlist = new List<string> { s1, s2, s3, s4 };
                    outerList.Add(innerlist);
                }
                r.Close();
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
            return outerList;
        }

        public static List<List<string>> GetFirstNameTopSick()
        {
            string sql = "SELECT TOP 5 [CRONUS Sverige AB$Employee].[First Name], COUNT([CRONUS Sverige AB$Employee Absence].[Employee No_]) AS TimesSick " +
                "FROM [CRONUS Sverige AB$Employee] LEFT JOIN [CRONUS Sverige AB$Employee Absence] ON [CRONUS Sverige AB$Employee].No_=[CRONUS Sverige AB$Employee Absence].[Employee No_] GROUP BY [First Name] ORDER BY TimesSick DESC;";
            List<List<string>> outerList = new List<List<string>>();
            outerList.Add(new List<string> { "First Name", "Times sick" });
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s1 = r.GetString(0);
                    string s2 = r.GetInt32(1).ToString();

                    List<string> innerlist = new List<string> { s1, s2 };
                    outerList.Add(innerlist);
                }
                r.Close();
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
            return outerList;
        }

        public static void CreateEmplContract(string code, string description)
        {
            string sql = "INSERT INTO[CRONUS Sverige AB$Employment Contract](Code, Description) VALUES(@sqlCode, @sqlDescription);";
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            c.Parameters.AddWithValue("@sqlCode", code);
            c.Parameters.AddWithValue("@sqlDescription", description);
            try
            {
                int i = c.ExecuteNonQuery();
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
        }

        public static List<string> GetEmplContract(string code)
        {
            List<string> result = new List<string>();

            string sql = "SELECT * FROM [CRONUS Sverige AB$Employment Contract] WHERE Code = @sqlCode;";
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            c.Parameters.AddWithValue("@sqlCode", code);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s1 = r.GetString(1);
                    string s2 = r.GetString(2);
                    result = new List<string> {  s1, s2 };
                }
                r.Close();
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
            return result;
        }

        public static void UpdateEmplContract(string code, string description)
        {
            string sql = "UPDATE [CRONUS Sverige AB$Employment Contract] SET Description = @sqlDescription WHERE Code = @sqlCode;";
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            c.Parameters.AddWithValue("@sqlCode", code);
            c.Parameters.AddWithValue("@sqlDescription", description);
            try
            {
                int i = c.ExecuteNonQuery();
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
        }

        public static void DeleteEmplContract(string code)
        {
            string sql = "DELETE FROM [CRONUS Sverige AB$Employment Contract] WHERE Code = @sqlCode;";
            SqlConnection conn = ConnectNavision();
            SqlCommand c = new SqlCommand(sql, conn);
            c.Parameters.AddWithValue("@sqlCode", code);
            try
            {
                int i = c.ExecuteNonQuery();
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
        }

        public static string Search(string fileName)
        {
            string s = null;
            try
            {
                if (fileName.EndsWith(".txt"))
                {
                    string[] lines = File.ReadAllLines(@fileName);
                    foreach (string line in lines)
                    {
                        s += "\n" + lines;
                    }
                    return s;
                }
                else if (fileName.Contains(".doc"))
                {
                    Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                    object miss = System.Reflection.Missing.Value;
                    object path = @fileName;
                    object readOnly = true;
                    Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                    for (int i = 0; i < docs.Paragraphs.Count; i++)
                    {
                        s += " \r\n " + docs.Paragraphs[i + 1].Range.Text.ToString();
                    }
                    docs.Close(ref miss, ref miss, ref miss);
                    word.Quit(ref miss, ref miss, ref miss);
                    return s;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return s;
        }

        public static List<string> GetCustomer(string cPnr)
        {
            string sql = "SELECT * FROM Customer WHERE cPnr = @sqlcPnr;";
            SqlConnection conn = ConnectSA();
            SqlCommand c = new SqlCommand(sql, conn);
            List<string> result = new List<string>();
            c.Parameters.AddWithValue("@sqlcPnr", cPnr);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s0 = r.GetString(0);
                    string s1 = r.GetString(1);
                    string s2 = r.GetString(2);
                    string s3 = r.GetString(3);
                    string s4 = r.GetString(4);
                    result = new List<string> { s0, s1, s2, s3, s4 };
                }
                r.Close();
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
            return result;
        }

        public static List<List<string>> GetCustomers()
        {
            string sql = "SELECT * FROM Customer";
            List<List<string>> outerList = new List<List<string>>();
            SqlConnection conn = ConnectSA();
            SqlCommand c = new SqlCommand(sql, conn);
            try
            {
                SqlDataReader r = c.ExecuteReader();
                while (r.Read())
                {
                    string s0 = r.GetString(0);
                    string s1 = r.GetString(1);
                    string s2 = r.GetString(2);
                    string s3 = r.GetString(3);
                    string s4 = r.GetString(4);
                    List<string> innerList = new List<string> { s0, s1, s2, s3, s4 };
                    outerList.Add(innerList);
                }
                r.Close();
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
            return outerList;
        }
    }
}

