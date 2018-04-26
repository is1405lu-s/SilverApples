using System.Data.SqlClient;
using Model;
using DAL;

namespace Controller
{
    public class CustomerController
    {

        public static Customer GetCustomer(SqlDataReader r)
        {
            return CustomerDAL.GetCustomer(r);
        }

        public static string CreateCustomer(Customer c)
        {
            return CustomerDAL.CreateCustomer(c);
        }

        public static string DeleteCustomer(Customer c)
        {
            return CustomerDAL.DeleteCustomer(c);
        }

        public static string UpdateCustomer(Customer c)
        {
            return CustomerDAL.UpdateCustomer(c);
        }
    }
}
