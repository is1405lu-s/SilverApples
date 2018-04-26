namespace Model
{
    public class Attended
    {
        private string cPnr;
        private string eId;
        private bool result;

        public Attended()
        { }

        public Attended(string cpnr, string eid, bool result)
        {
            CPnr = cpnr;
            EId = eid;
            Result = result;
        }

        public string CPnr
        {
            get { return cPnr; }
            set { cPnr = value; }
        }
        public string EId
        {
            get { return eId; }
            set { eId = value; }
        }
        public bool Result
        {
            get { return result; }
            set { result = value; }
        }
    }
}