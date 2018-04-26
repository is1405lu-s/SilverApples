using System;
using System.Data.SqlClient;
using Model;
using System.Collections.Generic;

namespace DAL
{
    public class AttendedDAL
    {

        public static Attended GetAttended(SqlDataReader r)
        {
            Attended a = new Attended();
            try
            {
                a.CPnr = r.GetString(0);
                a.EId = r.GetString(1);
                a.Result = r.GetBoolean(2);
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

        public static string CreateAttended(Attended a)
        {
            return "INSERT INTO Attended (cPnr, eId, result) VALUES('" + a.CPnr + "','" + a.EId + "','" + a.Result + "');";
        }

        public static string DeleteAttendedCust(Attended a)
        {
            return "DELETE FROM Attended WHERE cPnr = '" + a.CPnr + "'; ";
        }

        public static string DeleteAttendedEventt(Attended a)
        {
            return "DELETE FROM Attended WHERE eId = '" + a.EId + "'; ";
        }

        public static string UpdateAttended(Attended a)
        {
            return "UPDATE Attended SET result= '" + a.Result + "' WHERE cPnr'" + a.CPnr + "' AND eId ='" + a.EId + "';";
        }

        public static object[] TableCustomerAttd(Attended a, ref string[] headers)
        {
            string eName = "";
            string eId = "";
            bool eResult = false;
            object[] attdList = { };
            if (a != null)
            {
                List<Eventt> eventtList = UtilsDAL.GetAnything("SELECT * From Eventt", EventtDAL.GetEventt);
                foreach (Eventt e in eventtList)
                {
                    if (e.EId == a.EId)
                    {
                        eName = e.EName;
                        eId = e.EId;
                        eResult = a.Result;
                    }
                }
            }
            if (eResult == true)
            {
                attdList = new object[] { eName, eId, "Acheived" };
            }
            else
            {
                attdList = new object[] { eName, eId, "Unacheived" };
                headers = new string[] { "Event Name", "Event Id", "Result" };
            }
            return attdList;
        }

        public static object[] TableEventtAttd(Attended a, ref string[] headers)
        {
            string cName = "";
            string cPnr = "";
            bool cResult = false;
            object[] attdList = { };
            if (a != null)
            {
                List<Customer> custlist = UtilsDAL.GetAnything("SELECT * From Customer", CustomerDAL.GetCustomer);
                foreach (Customer c in custlist)
                {
                    if (a.CPnr == c.CPnr)
                    {
                        cName = c.CName;
                        cPnr = c.CPnr;
                        cResult = a.Result;
                    }
                }
            }
            if (cResult == true)
            {
                attdList = new object[] { cName, cPnr, "Acheived" };
            }
            else
            {
                attdList = new object[] { cName, cPnr, "Unacheived" };
                headers = new string[] { "Name", "Personal Nr.", "Result" };
            }
            return attdList;
        }
    }
}
