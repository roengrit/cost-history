using CostHistory.Models;
using Syncfusion.EJ.Export;
using Syncfusion.XlsIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostHistory
{
    public partial class Work : System.Web.UI.Page
    {
        DataTable dt = new DataTable("Order");
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Grid1.Columns[0].HeaderText = "รหัส";
            this.Grid1.Columns[0].HeaderText = "ชื่อ";
            this.Grid1.Columns[0].HeaderText = "หน่วย";

            dtStartDate.Value = DateTime.Now;
            dtEndDate.Value = DateTime.Now;
            this.AutoComplete.DataSource = new LocalData().GetDataItems().ToList();
            btnFind.Text = Resources.Resource.signin;
        }

        private void BindDataSource()
        {
            this.Grid1.DataSource = (DataTable)Session["SqlDataSource"]; 
            this.Grid1.DataBind();
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

        protected void btnFind_Click(object sender, EventArgs e)
        {
            dt = Connection.GetData(@"select       
                                          ic_trans.doc_date,
                                          ic_trans.doc_no,
                                          ap_supplier.code,
                                          ap_supplier.name_1 as suplier_name,
                                          ic_trans_detail.item_code, 
                                          ic_trans_detail.item_name, 
                                          ic_trans_detail.unit_code, 
                                          ic_unit.name_1 as unit_name,
                                          ic_trans_detail.qty, 
                                          ic_trans_detail.price, 
                                          ic_trans_detail.discount, 
										  ic_trans_detail.discount_amount,
                                          ic_trans_detail.total_vat_value,
                                          ic_trans_detail.sum_of_cost, 
                                          ic_trans_detail.sum_amount		                                     
                                    from  ic_trans_detail join  
                                          ic_trans on ic_trans_detail.doc_no = ic_trans.doc_no left join  
                                          ap_supplier on ic_trans.cust_code = ap_supplier.code left join 
                                          ic_unit on ic_trans_detail.unit_code = ic_unit.code 
                                    where ic_trans.trans_flag = 6 and ic_trans_detail.last_status = 0
                                    limit 100
                                    ");
            Session["SqlDataSource"] = dt;
            this.BindDataSource();
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