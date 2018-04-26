using System;

namespace Model
{
    public class Eventt
    {
        private string eId;
        private string eName;
        private int ePrice;
        private DateTime eDate;

        public Eventt()
        { }

        public Eventt(string eid, string name, int price, DateTime eDate)
        {
            EId = eid;
            EName = name;
            EPrice = price;
            EDate = eDate;
        }

        public string EId
        {
            get { return eId; }
            set { eId = value; }
        }
        public string EName
        {
            get { return eName; }
            set { eName = value; }
        }
        public int EPrice
        {
            get { return ePrice; }
            set { ePrice = value; }
        }
        public DateTime EDate
        {
            get { return eDate; }
            set { eDate = value; }
        }
    }
}