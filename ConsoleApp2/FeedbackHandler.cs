namespace Utilities
{
    public class FeedbackHandler
    {
        public static string GetMessage(string s)
        {
            switch(s) {

                case "CustCreate":
                    return "Customer was successfully created.";
                case "CustFound":
                    return "Customer was successfully found.";
                case "EventCreate":
                    return "Event was successfully created.";
                case "CustUpdate":
                    return "Customer was successfully updated.";
                case "EventUpdate":
                    return "Event was successfully updated.";
                case "CustDelete":
                    return "Customer was successfully deleted.";
                case "EventDelete":
                    return "Event was successfully deleted.";
                case "CustPaid":
                    return "Customer has now paid.";
                case "CustAttgEvent":
                    return "Customer was successfully added to event.";
                case "CustAttdEvent":
                    return "Customer successfully attended event.";
                case "FillAllFields":
                    return "Fill in all the fields.";
                case "MissingPnr":
                    return "This Personal Nr. does not exists.";
                case "NoInputPnr":
                    return "Please enter Personal Nr.";
                case "ExistPnr":
                    return "This Personal Nr. already exists.";
                case "CustAlrPaid":
                    return "Customer has already paid.";
                case "NoEventSele":
                    return "No event has been selected.";
                case "EIdExist":
                    return "Event ID already exists.";
                case "EIDNotExist":
                    return "Event ID does not exist.";
                case "NoEventYet":
                    return "Event has not taken place yet.";
                case "CustNoPaid":
                    return "Customer has not paid.";
                case "CustUnPaid":
                    return "Customer's payment status was successfully changed.";
                case "FileOpen":
                    return "File opened: ";
                case "FileNotFound":
                    return "File could not be found.";
                case "AllCustFound":
                    return "All customers were successfully found.";
                case "EmplFound":
                    return "Employee was successfully found.";
                case "EmplContractFound":
                    return "Employment contract was successfully found.";
                case "EmplContractCreate":
                    return "Employment contract was successfully created.";
                case "EmplContractExist":
                    return "This Employment Contract Code already exists.";
                case "EmplContractUpdate":
                    return "Employment contract was successfully updated.";
                case "EmplContractDelete":
                    return "Employment contract was successfully deleted.";
                case "TableFound":
                    return "Table was successfully found.";
                case "TableDisplay":
                    return "Table was successfully displayed.";
                default:
                    return "Unknown error.";
            }
        }
    }
}
