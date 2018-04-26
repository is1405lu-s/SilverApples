namespace Model
{ 
    public class Customer
    {
        private string cPnr;
        private string cName;
        private string cAddress;
        private string cPhone;
        private string cMail;

        public Customer()
        { }

        public Customer(string pnr, string name, string address, string phone, string mail)
        {
            CPnr = pnr;
            CName = name;
            CAddress = address;
            CPhone = phone;
            CMail = mail;
        }

        public string CPnr
        {
            get { return cPnr; }
            set { cPnr = value; }
        }
        public string CName
        {
            get { return cName; }
            set { cName = value; }
        }
        public string CAddress
        {
            get { return cAddress; }
            set { cAddress = value; }
        }
        public string CPhone
        {
            get { return cPhone; }
            set { cPhone = value; }
        }
        public string CMail
        {
            get { return cMail; }
            set { cMail = value; }
        }
    }
}