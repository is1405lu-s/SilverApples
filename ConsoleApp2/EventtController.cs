using System.Data.SqlClient;
using Model;
using DAL;

namespace Controller
{
    public class EventtController
    {
        public static Eventt GetEventt(SqlDataReader r)
        {
            return EventtDAL.GetEventt(r);
        }

        public static string CreateEventt(Eventt e)
        {
            return EventtDAL.CreateËventt(e);
        }

        public static string DeleteEventt(Eventt e)
        {
            return EventtDAL.DeleteEventt(e);
        }

        public static string UpdateEventt(Eventt e)
        {
            return EventtDAL.UpdateEventt(e);
        }
    }
}
