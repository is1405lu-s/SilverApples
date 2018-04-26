using System.Data.SqlClient;
using DAL;
using Model;

namespace Controller
{
    public class AttendedController
    {
        public static Attended GetAttended(SqlDataReader r)
        {
            return AttendedDAL.GetAttended(r);
        }

        public static string CreateAttended(Attended a)
        {
            return AttendedDAL.CreateAttended(a);
        }

        public static string DeleteAttendedCust(Attended a)
        {
            return AttendedDAL.DeleteAttendedCust(a);
        }

        public static string DeleteAttendedEventt(Attended a)
        {
            return AttendedDAL.DeleteAttendedEventt(a);
        }

        public static string UpdateAttended(Attended a)
        {
            return AttendedDAL.UpdateAttended(a);
        }

        public static object[] TableCustomerAttd(Attended a, ref string[] headers)
        {
            return AttendedDAL.TableCustomerAttd(a, ref headers);
        }

        public static object[] TableEventtAttd(Attended a, ref string[] headers)
        {
            return AttendedDAL.TableEventtAttd(a, ref headers);
        }
    }
}
