using Syncfusion.EJ.Export;
using Syncfusion.XlsIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostHistory
{
    public partial class Work : System.Web.UI.Page
    {
        public static List<Orders> order = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            dtStartDate.Value = DateTime.Now;
            dtEndDate.Value = DateTime.Now;
            this.AutoComplete.DataSource = new LocalData().GetDataItems().ToList();
            btnFind.Text = Resources.Resource.signin;
            BindDataSource();
        }
        private void BindDataSource()
        {
            order = new List<Orders>();
            int orderId = 1000;
            int empId = 0;
            for (int i = 1; i < 20; i++)
            {
                order.Add(new Orders(orderId + 1, "VINET", empId + 1, 32.38, new DateTime(2014, 12, 25), "Reims"));
                order.Add(new Orders(orderId + 2, "TOMSP", empId + 2, 11.61, new DateTime(2014, 12, 21), "Munster"));
                order.Add(new Orders(orderId + 3, "ANATER", empId + 3, 45.34, new DateTime(2014, 10, 18), "Berlin"));
                order.Add(new Orders(orderId + 4, "ALFKI", empId + 4, 37.28, new DateTime(2014, 11, 23), "Mexico"));
                order.Add(new Orders(orderId + 5, "FRGYE", empId + 5, 67.00, new DateTime(2014, 05, 05), "Colchester"));
                order.Add(new Orders(orderId + 6, "JGERT", empId + 6, 23.32, new DateTime(2014, 10, 18), "Newyork"));
                orderId += 1;
                empId += 1;
            }
            this.Grid1.DataSource = order;
            this.Grid1.DataBind();
        }
        [Serializable]
        public class Orders
        {
            public Orders()
            {
            }
            public Orders(int orderId, string customerId, int empId, double freight, DateTime orderDate, string shipCity)
            {
                this.OrderID = orderId;
                this.CustomerID = customerId;
                this.EmployeeID = empId;
                this.Freight = freight;
                this.OrderDate = orderDate;
                this.ShipCity = shipCity;
            }
            public int OrderID { get; set; }
            public string CustomerID { get; set; }
            public int EmployeeID { get; set; }
            public double Freight { get; set; }
            public DateTime OrderDate { get; set; }
            public string ShipCity { get; set; }
        }
        protected void FlatGrid_ServerExcelExporting(object sender, Syncfusion.JavaScript.Web.GridEventArgs e)
        {
            ExcelExport exp = new ExcelExport();
            exp.Export(Grid1.Model, (IEnumerable)Grid1.DataSource, "Export.xlsx", ExcelVersion.Excel2010, true, true, "flat-lime");
        }
        protected void FlatGrid_ServerWordExporting(object sender, Syncfusion.JavaScript.Web.GridEventArgs e)
        {
            WordExport exp = new WordExport();
            exp.Export(Grid1.Model, (IEnumerable)Grid1.DataSource, "Export.docx", true, true, "flat-lime");
        }
        protected void FlatGrid_ServerPdfExporting(object sender, Syncfusion.JavaScript.Web.GridEventArgs e)
        {
            PdfExport exp = new PdfExport();
            exp.Export(Grid1.Model, (IEnumerable)Grid1.DataSource, "Export.pdf", true, true, "flat-lime");
        }
    }


    public class LocalData
    {

        public LocalData(int _id, string _text)

        {

            this.ID = _id;

            this.Text = _text;

        }

        public LocalData() { }

        public int ID

        {

            get;

            set;

        }

        public string Text

        {

            get;

            set;

        }

        public List<LocalData> GetDataItems()
        {

            List<LocalData> data = new List<LocalData>();

            data.Add(new LocalData(1, "Audi S6"));

            data.Add(new LocalData(2, "Austin-Healey"));

            data.Add(new LocalData(3, "Aston Martin"));

            data.Add(new LocalData(4, "BMW 7"));

            data.Add(new LocalData(5, "Bentley Mulsanne"));

            data.Add(new LocalData(6, "Bugatti Veyron"));

            data.Add(new LocalData(7, "Chevrolet Camaro"));

            data.Add(new LocalData(8, "Cadillac "));

            data.Add(new LocalData(9, "Honda S2000"));

            data.Add(new LocalData(10, "Hyundai Santro"));

            data.Add(new LocalData(11, "Mercedes-Benz "));

            data.Add(new LocalData(12, "Mercury Coupe"));

            data.Add(new LocalData(13, "Maruti Alto 800"));

            data.Add(new LocalData(14, "Volkswagen Shirako"));

            data.Add(new LocalData(15, "Lotus Esprit "));

            data.Add(new LocalData(16, "Lamborghini Diablo"));

            data.Add(new LocalData(17, "Nissan Qashqai "));

            data.Add(new LocalData(18, "Oldsmobile S98 "));

            data.Add(new LocalData(19, "Opel Superboss "));

            data.Add(new LocalData(20, "Scion SRS/SC/SD "));

            data.Add(new LocalData(21, "Saab Sportcombi "));

            data.Add(new LocalData(22, "Subaru Sambar "));

            data.Add(new LocalData(23, "Suzuki Swift "));

            data.Add(new LocalData(24, "Volvo P1800 "));

            data.Add(new LocalData(25, "Kia Sedona EX "));

            data.Add(new LocalData(26, "Koenigsegg Agera "));

            data.Add(new LocalData(27, "Ford Boss 302 "));

            data.Add(new LocalData(28, "Ferrari 360 "));

            data.Add(new LocalData(29, "Ford Thunderbird "));

            data.Add(new LocalData(30, "Alfa Romeo"));

            return data;
        }

    }
}