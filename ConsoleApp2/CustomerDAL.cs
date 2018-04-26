using System;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class CustomerDAL
    {
        public static Customer GetCustomer(SqlDataReader r)
        {
            Customer c = new Customer();
            try
            {
                c.CPnr = r.GetString(0);
                c.CName = r.GetString(1);
                c.CAddress = r.GetString(2);
                c.CPhone = r.GetString(3);
                c.CMail = r.GetString(4);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return c;
        }

        public static string CreateCustomer(Customer c)
        {
            return "INSERT INTO Customer(cPnr,cName,cAddress,cPhone,cMail) VALUES" +
                "('" + c.CPnr + "','" + c.CName + "','" + c.CAddress + "','" + c.CPhone
                + "','" + c.CMail + "');";
        }

        public static string DeleteCustomer(Customer c)
        {
            return "DELETE FROM Customer WHERE cPnr = '" + c.CPnr + "';";
        }

        public static string UpdateCustomer(Customer c)
        {
            return "UPDATE Customer SET cName='" + c.CName + "', cAddress='"
                + c.CAddress + "', cPhone ='" + c.CPhone + "', cMail= '"
                + c.CMail + "' WHERE cPnr ='" + c.CPnr + "';";
        }
    }
}
