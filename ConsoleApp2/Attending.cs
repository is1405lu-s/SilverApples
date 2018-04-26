namespace Model
{
    public class Attending
    {
        private string cPnr;
        private string eId;
        private bool hasPaid;

        public Attending()
        { }

        public Attending(string cpnr, string eid, bool haspaid)
        {
            CPnr = cpnr;
            EId = eid;
            HasPaid = haspaid;
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
        public bool HasPaid
        {
            get { return hasPaid; }
            set { hasPaid = value; }
        }
    }
}