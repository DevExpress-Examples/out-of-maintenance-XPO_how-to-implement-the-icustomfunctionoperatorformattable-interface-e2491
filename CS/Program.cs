using System;
using DXSample;
using System.Data;
using System.Linq;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System.Diagnostics;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.Helpers;
using DevExpress.Xpo.Metadata;

namespace CustomCommand {
    static class Program {
        static void Main() {
            ConnectionProviderSql provider = 
                (ConnectionProviderSql)XpoDefault.GetConnectionProvider(AccessConnectionProvider.GetConnectionString(@"..\..\CustomCommand.mdb"),
                AutoCreateOption.DatabaseAndSchema);
            provider.RegisterCustomFunctionOperator(new GetMonthFunction());
            XPDictionary dict = new ReflectionDictionary();
            dict.CustomFunctionOperators.Add(new GetMonthFunction());
            XpoDefault.DataLayer = new SimpleDataLayer(dict, provider);
            CreateData();

            using (Session session = new Session()) {
                XPView view = new XPView(session, typeof(Order));
                view.AddProperty("Month", "custom('GetMonth', OrderDate)");

                foreach (ViewRecord prop in view)
                    Console.WriteLine(prop["Month"]);

                var list = from o in new XPQuery<Order>(session)
                           where o.OrderName == "Chai"
                           select new { Month = GetMonthFunction.GetMonth(o.OrderDate) };

                foreach (var item in list)
                    Console.WriteLine(item.Month);
            }
            Console.WriteLine("done\npress any key to exit ..");
            Console.ReadKey();
        }

        static void CreateData() {
            using (UnitOfWork uow = new UnitOfWork()) {
                if (uow.FindObject<Order>(null) != null) return;
                new Order(uow) { OrderName = "Chai", OrderDate = new DateTime(2011, 2, 10) };
                uow.CommitChanges();
            }
        }
    }
}