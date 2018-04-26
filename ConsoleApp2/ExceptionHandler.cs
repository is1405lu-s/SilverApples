using System;
using System.Data.SqlClient;
using System.Web.Services.Protocols;

namespace Utilities
{
    public class ExceptionHandler : Exception
    {
        public static string HandleException(Exception ex)
        {
            string s = null;

            if (ex is InvalidOperationException)
            {
                s = "Error trying to access something that doesn't exist.";
            }
            else if (ex is FormatException)
            {
                s = "Invalid format entered.";
            }
            else if (ex is IndexOutOfRangeException)
            {
                s = "Input does not exist.";
            }
             else if (ex is SoapException)
            {
                    s = "Something went wrong when trying to connect to web server. Try again.";
            }
            else if (ex is SqlException)
            {
                SqlException sqlEx = ex as SqlException;
                int errorNum = sqlEx.Number;

                switch (errorNum)
                {
                    case 8152:
                        s = "The entered input is too long. Please check the fields.";
                        break;
                    case 2627:
                        s = "The Personal Nr. or Event ID you tried to register/create/add already exist.";
                        break;
                    case 547:
                        s = "The entered Personal Nr. doesn't exist.";
                        break;
                    case 4104:
                        s = "An error occured. Unable to process the chosen statements.";
                        break;
                    case 102:
                        s = "An invalid characther has been entered. Please check the fields.";
                        break;
                    case 17: // No connection
                        s = "Connection to database failed, please try again. If the error persists contact support.";
                        break;
                    case 0: // Login time expired
                        s = "Login to the database timed out, please check your connection and try again.";
                        break;
                    default:
                        s = "An unknown error accured. Please contact customer support.";
                        break;
                }
            }
            else
            {
                s = "An unknown error accured. Please contact customer support.";
            }
            return s;
        }
    }
}
