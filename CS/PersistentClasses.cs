using System;
using DevExpress.Xpo;

namespace DXSample {
    public class Order :XPObject {
        public Order(Session session) : base(session) { }

        private string fOrderName;
        public string OrderName {
            get { return fOrderName; }
            set { SetPropertyValue<string>("OrderName", ref fOrderName, value); }
        }

        private DateTime fOrderDate;
        public DateTime OrderDate {
            get { return fOrderDate; }
            set { SetPropertyValue<DateTime>("OrderDate", ref fOrderDate, value); }
        }
    }
}