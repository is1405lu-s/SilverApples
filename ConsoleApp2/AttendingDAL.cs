using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class AttendingDAL
    {
        public static Attending GetAttending(SqlDataReader r)
        {
            Attending a = new Attending();
            try
            {
                a.CPnr = r.GetString(0);
                a.EId = r.GetString(1);
                a.HasPaid = r.GetBoolean(2);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return a;
        }

        public static string CreateAttending(Attending a)
        {
            return "INSERT INTO Attending(cPnr,eId,hasPaid) VALUES('" + a.CPnr + "','" + a.EId + "','" + a.HasPaid + "');";
        }

        public static string DeleteAttending(Attending a)
        {
            return "DELETE FROM Attending WHERE cPnr = '" + a.CPnr + "' AND eId = '" + a.EId + "'; ";
        }

        public static string DeleteAttendingCust(Attending a)
        {
            return "DELETE FROM Attending WHERE cPnr = '" + a.CPnr + "'; ";
        }

        public static string DeleteAttendingEventt(Attending a)
        {
            return "DELETE FROM Attending WHERE eId = '" + a.EId + "'; ";
        }

        public static string UpdateAttending(Attending a)
        {
            return "UPDATE Attending SET hasPaid='" + a.HasPaid + "' WHERE cPnr = '" + a.CPnr + "' AND eId = '" + a.EId + "'; ";
        }

        public static object[] TableCustomerAttg(Attending a, ref string[] headers)
        {
            string eName = "";
            string eId = "";
            bool hasPaid = false;
            object[] attgList = { };
            if (a != null)
            {
                List<Eventt> eventtList = UtilsDAL.GetAnything("SELECT * From Eventt", EventtDAL.GetEventt);
                foreach (Eventt e in eventtList)
                {
                    if (a.EId == e.EId)
                    {
                        eName = e.EName;
                        eId = e.EId;
                        hasPaid = a.HasPaid;
                    }
                }
            }
            if (hasPaid == true)
            {
                attgList = new object[] { eName, eId, "Paid" };
            }
            else
            {
                attgList = new object[] { eName, eId, "Not paid" };
                headers = new string[] { "Event Name", "Event Id", "Paid" };
            }
            return attgList;
        }

        public static object[] TableEventtAttg(Attending a, ref string[] headers)
        {
            string cName = "";
            string cPnr = "";
            bool hasPaid = false;
            object[] attgList = { };
            if (a != null)
            {
                List<Customer> custList = UtilsDAL.GetAnything("SELECT * From Customer", CustomerDAL.GetCustomer);
                foreach (Customer c in custList)
                {
                    if (a.CPnr == c.CPnr)
                    {
                        cName = c.CName;
                        cPnr = c.CPnr;
                        hasPaid = a.HasPaid;
                    }
                }
            }
            if (hasPaid == true)
            {
                attgList = new object[] { cName, cPnr, "Paid" };
            }
            else
            {
                attgList = new object[] { cName, cPnr, "Not paid" };
                headers = new string[] { "Name", "Personal Nr.", "Paid" };
            }
            return attgList;
        }  
    }
}
