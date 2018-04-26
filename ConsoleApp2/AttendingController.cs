using System.Data.SqlClient;
using Model;
using DAL;

namespace Controller
{
    public class AttendingController
    {

        public static Attending GetAttending(SqlDataReader r)
        {
            return AttendingDAL.GetAttending(r);
        }

        public static string CreateAttending(Attending a)
        {
            return AttendingDAL.CreateAttending(a);
        }

        public static string DeleteAttending(Attending a)
        {
            return AttendingDAL.DeleteAttending(a);
        }

        public static string DeleteAttendingCust(Attending a)
        {
            return AttendingDAL.DeleteAttendingCust(a);
        }

        public static string DeleteAttendingEventt(Attending a)
        {
            return AttendingDAL.DeleteAttendingEventt(a);
        }

        public static string UpdateAttending(Attending a)
        {
            return AttendingDAL.UpdateAttending(a);
        }

        public static object[] TableCustomerAttg(Attending a, ref string[] headers)
        {
            return AttendingDAL.TableCustomerAttg(a, ref headers);
        }

        public static object[] TableEventtAttg(Attending a, ref string[] headers)
        {
            return AttendingDAL.TableEventtAttg(a, ref headers);
        }
    }
}
