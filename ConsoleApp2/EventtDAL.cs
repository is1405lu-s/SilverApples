using System;
using Model;
using System.Data.SqlClient;

namespace DAL
{
    public class EventtDAL
    {
        public static Eventt GetEventt(SqlDataReader r)
        {
            Eventt e = new Eventt();
            try
            {
                e.EId = r.GetString(0);
                e.EName = r.GetString(1);
                e.EPrice = r.GetInt32(2);
                e.EDate = r.GetDateTime(3);
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
            return e;
        }

        public static string CreateËventt(Eventt e)
        {
            return "INSERT INTO Eventt(eId, eName, ePrice, eDate) VALUES" +
                "('" + e.EId + "','" + e.EName + "','" + e.EPrice + "','" + e.EDate + "');";
        }

        public static string DeleteEventt(Eventt e)
        {
            return "DELETE FROM Eventt WHERE eId = '" + e.EId + "';";
        }

        public static string UpdateEventt(Eventt e)
        {
            return "UPDATE Eventt SET  eId= '" + e.EId + "', eName='" + e.EName + "', " +
                "ePrice='" + e.EPrice + "', eDate='" + e.EDate + "' WHERE eId = '" + e.EId + "';";
        }
    }
}
