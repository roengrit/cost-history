using CostHistory.Models;
using Newtonsoft.Json;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript.Web;
using Syncfusion.XlsIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostHistory
{
    public partial class Work : System.Web.UI.Page
    {
        DataTable dt = new DataTable("Order");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect(ResolveUrl("~/Default.aspx"));
            }

            string language = "lo-LA";

            //Detect User's Language.


            if (Session["lang"] != null)
            {
                language = Session["lang"].ToString();
            }


            //Check if PostBack is caused by Language DropDownList.
            if (Request.Form["__EVENTTARGET"] != null)
            {
                //Set the Language.
                if (Request.Form["lang"] != null)
                    if (Request.Form["lang"] != "")
                    {
                        language = Request.Form["__EVENTTARGET"];
                        Session["lang"] = language;
                    }
            }

            //Set the Culture.
            Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

            if (!this.IsPostBack)
            {
                dtStartDate.Value = null;
                dtEndDate.Value = null;
                Session["Supplier"] = new LocalData().GetDataItems().ToList();
                this.AutoComplete.DataSource = (List<LocalData>)Session["Supplier"];
#if DEBUG
               // txtDocNo.Text = "PO1811020004";
#endif
            }
            else {
                if (Session["Supplier"] != null)
                    this.AutoComplete.DataSource = (List<LocalData>)Session["Supplier"];
                else
                {
                    Session["Supplier"] = new LocalData().GetDataItems().ToList();
                    this.AutoComplete.DataSource = (List<LocalData>)Session["Supplier"];
                }
                }
                this.GridHeader();
            btnFind.Text = Resources.Resource.find;
            lblSearch.Text = Resources.Resource.find;
            lblSupplier.Text = Resources.Resource.supplier_name;
            lblForm.Text = Resources.Resource.from;
            lblTo.Text = Resources.Resource.to;
            lblDocNo.Text = Resources.Resource.doc_no;
            lblSave.Text = Resources.Resource.save;
        }

        private void BindDataSource()
        {
            this.Grid1.DataSource = (DataTable)Session["SqlDataSource"];
            this.GridHeader();
            this.Grid1.DataBind();
        }

        private void GridHeader()
        {
            this.Grid1.AllowTextWrap = false;

            this.Grid1.Columns[0].HeaderText = "";
            this.Grid1.Columns[1].HeaderText = Resources.Resource.doc_date;
            this.Grid1.Columns[2].HeaderText = Resources.Resource.doc_no;
            this.Grid1.Columns[3].HeaderText = Resources.Resource.supplier_name;
            this.Grid1.Columns[4].HeaderText = Resources.Resource.item_code;
            this.Grid1.Columns[5].HeaderText = Resources.Resource.item_name;
            this.Grid1.Columns[6].HeaderText = Resources.Resource.unit_name;
            this.Grid1.Columns[7].HeaderText = Resources.Resource.qty;
            this.Grid1.Columns[8].HeaderText = Resources.Resource.price;
            this.Grid1.Columns[9].HeaderText = Resources.Resource.discount;
            this.Grid1.Columns[10].HeaderText = Resources.Resource.price_after_discount;
            this.Grid1.Columns[11].HeaderText = Resources.Resource.total_vat_value;
            this.Grid1.Columns[12].HeaderText = Resources.Resource.receipt_price;
            this.Grid1.Columns[13].HeaderText = Resources.Resource.free_value;
            this.Grid1.Columns[14].HeaderText = Resources.Resource.other_discount;
            this.Grid1.Columns[15].HeaderText = Resources.Resource.rebate;
            this.Grid1.Columns[16].HeaderText = Resources.Resource.price_after_pro;
            this.Grid1.Columns[17].HeaderText = Resources.Resource.vat_add;
            this.Grid1.Columns[18].HeaderText = Resources.Resource.transport_bkk_nk;
            this.Grid1.Columns[19].HeaderText = Resources.Resource.net_price_thai;
            this.Grid1.Columns[20].HeaderText = Resources.Resource.transport_nk_vt;
            this.Grid1.Columns[21].HeaderText = Resources.Resource.transport_bkk_vt;
            //this.Grid1.Columns[22].HeaderText = Resources.Resource.transport_value;
            this.Grid1.Columns[22].HeaderText = Resources.Resource.import_value;
            this.Grid1.Columns[23].HeaderText = Resources.Resource.other_value;
            this.Grid1.Columns[24].HeaderText = Resources.Resource.remark;
            this.Grid1.Columns[25].HeaderText = Resources.Resource.net_cost_price;

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
            var sql = @"select            concat(ic_trans.doc_no,'#', ic_trans_detail.item_code , '#' ,  ic_trans_detail.unit_code ) as key ,              
                                          ic_trans.trans_flag,
                                          ic_trans.doc_date,
                                          ic_trans.doc_no,
                                          ic_trans.cust_code,
                                          ap_supplier.name_1 as supplier_name,
                                          ic_trans_detail.item_code, 
                                          ic_trans_detail.item_name, 
                                          ic_trans_detail.unit_code, 
                                          ic_unit.name_1 as unit_name,
                                          ic_trans_detail.qty, 
                                          ic_trans_detail.price,  
                                          ic_trans_detail.discount, 
                                          ic_trans_detail.price_exclude_vat,
                                          ic_trans_detail.price_base,
										  ic_trans_detail.discount_amount,
										  ic_trans_detail.price - (ic_trans_detail.discount_amount / ic_trans_detail.qty) as price_after_discount,
										  ic_trans_detail.total_vat_value,		
                                          ic_trans_detail.price - (ic_trans_detail.discount_amount / ic_trans_detail.qty) +  ic_trans_detail.total_vat_value as receipt_price,
                                          ic_trans_detail.sum_of_cost, 
                                          coalesce( (select sum(qty) from ic_trans_detail as sub where sub.doc_no = ic_trans_detail.doc_no and sub.item_code = ic_trans_detail.item_code and sub.unit_code = ic_trans_detail.unit_code and sub.price <= 0 and sub.last_status = 0),0) as free_value,
                                          0.0 as other_discount,
                                          cost_history.*,
                                          0.0  as net_price_thai,
                                          0.0  as net_cost_price
                                    from  ic_trans_detail join  
                                          ic_trans on ic_trans_detail.doc_no = ic_trans.doc_no left join  
                                          ap_supplier on ic_trans.cust_code = ap_supplier.code left join 
                                          ic_unit on ic_trans_detail.unit_code = ic_unit.code left join
                                          cost_history on ic_trans_detail.doc_no = cost_history.doc_no 
                                                       and ic_trans_detail.item_code = cost_history.item_code 
                                                       and ic_trans_detail.unit_code = cost_history.unit_code                                          
                                    where ic_trans.trans_flag = 6 and ic_trans_detail.last_status = 0 and ic_trans_detail.qty <> 0 and ic_trans_detail.price <> 0 ";

            if (dtStartDate.Value.HasValue && !dtEndDate.Value.HasValue)
            {
                sql += " and ic_trans.doc_date = '" + (dtStartDate.Value.Value.ToString("yyyy-MM-dd")) + "'";
            }
            else if (!dtStartDate.Value.HasValue && dtEndDate.Value.HasValue)
            {
                sql += " and ic_trans.doc_date = '" + (dtEndDate.Value.Value.ToString("yyyy-MM-dd")) + "'";
            }
            else if (dtStartDate.Value.HasValue && dtEndDate.Value.HasValue)
            {
                sql += " and ic_trans.doc_date between '" + (dtStartDate.Value.Value.ToString("yyyy-MM-dd")) + "' and '" + (dtEndDate.Value.Value.ToString("yyyy-MM-dd")) + "'";
            }

            if (!string.IsNullOrEmpty(AutoComplete.SelectValueByKey))
            {
                sql += " and ic_trans.cust_code = '" + AutoComplete.SelectValueByKey.Replace(",","") + "'";
            }

            if (!string.IsNullOrEmpty(txtDocNo.Text.Trim()))
            {
                sql += " and ic_trans.doc_no = '" + txtDocNo.Text.Trim() + "'";
            }

            dt = Connection.GetData(sql);
            foreach (DataRow item in dt.Rows)
            {
                item["other_discount"] = Convert.ToDecimal((Convert.ToDecimal(item["receipt_price"]) - (
                    (Convert.ToDecimal(item["qty"]) * Convert.ToDecimal(item["receipt_price"])) /
                    (Convert.ToDecimal(item["qty"]) + Convert.ToDecimal(item["free_value"]))
                    )).ToString("N2"));

                item["price_after_discount"] = Convert.ToDecimal(Convert.ToDecimal(item["price_after_discount"]).ToString("N2"));
                item["receipt_price"] = Convert.ToDecimal(Convert.ToDecimal(item["receipt_price"]).ToString("N2"));

                if (item["doc_no1"].ToString() != "")
                {
                    var rebate_number = 0.0m;
                    if (!string.IsNullOrEmpty(item["rebate"].ToString()))
                    {
                        if (item["rebate"].ToString().Contains("%"))
                        {
                            rebate_number = (Convert.ToDecimal(item["price_after_discount"]) * -1) * (Convert.ToDecimal(item["rebate"].ToString().Replace("%", "")) / 100);
                        }
                        else if (!string.IsNullOrEmpty(item["rebate"].ToString()))
                        {
                            rebate_number = Convert.ToDecimal(item["rebate"].ToString().Replace("%", "")) * -1;
                        }
                        else
                        {
                            rebate_number = 0;
                        }
                        item["rebate_number"] = Convert.ToDecimal(rebate_number.ToString("N2"));
                    }

                    item["price_after_pro"] = Convert.ToDecimal(item["receipt_price"]) + Convert.ToDecimal(item["rebate_number"]) + Convert.ToDecimal(item["other_discount"]);
                    item["net_price_thai"] = Convert.ToDecimal(item["receipt_price"]) + Convert.ToDecimal(item["vat_add"]) + Convert.ToDecimal(item["transport_bkk_nk"]) + Convert.ToDecimal(item["rebate_number"]);
                    item["net_cost_price"] = Convert.ToDecimal(item["transport_nk_vt"]) + Convert.ToDecimal(item["transport_bkk_vt"]) + Convert.ToDecimal(item["import_value"]) + Convert.ToDecimal(item["net_price_thai"]);
                }
                else {
                    item["price_after_discount"] = Convert.ToDecimal(Convert.ToDecimal(item["receipt_price"]).ToString("N2"));
                    item["net_price_thai"] = Convert.ToDecimal(Convert.ToDecimal(item["receipt_price"]).ToString("N2"));
                    item["net_cost_price"] = Convert.ToDecimal(Convert.ToDecimal(item["receipt_price"]).ToString("N2"));
                }

            }
            Session["SqlDataSource"] = dt;
            this.BindDataSource();
        }

        protected void EditEvents_ServerEditRow(object sender, GridEventArgs e)
        {
            EditAction(e.EventType, e.Arguments["data"]);
        }

        protected void EditAction(string eventType, object record)
        {

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void Insert(object data)
        {

        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string Update(object items, string user)
        {
            List<ProductSave> lstItems = new JavaScriptSerializer().ConvertToType<List<ProductSave>>(items);
            List<string> commans = new List<string>();
            foreach (var item in lstItems)
            {
                item.vat_add = string.IsNullOrEmpty(item.vat_add) ? "0" : item.vat_add;
                item.transport_bkk_nk = string.IsNullOrEmpty(item.transport_bkk_nk) ? "0" : item.transport_bkk_nk;
                item.transport_nk_vt = string.IsNullOrEmpty(item.transport_nk_vt) ? "0" : item.transport_nk_vt;
                item.transport_bkk_vt = string.IsNullOrEmpty(item.transport_bkk_vt) ? "0" : item.transport_bkk_vt;
                item.import_value = string.IsNullOrEmpty(item.import_value) ? "0" : item.import_value;
                item.other_value = string.IsNullOrEmpty(item.other_value) ? "0" : item.other_value;
                item.remark = string.IsNullOrEmpty(item.remark) ? "0" : item.remark;

                commans.Add(
                            @"INSERT INTO cost_history(doc_no,item_code,unit_code,rebate,vat_add,transport_bkk_nk,transport_nk_vt,transport_bkk_vt,import_value,other_value,remark,creator_code)
                              VALUES('@doc_no@','@item_code@','@unit_code@','@rebate@',@vat_add@,@transport_bkk_nk@,@transport_nk_vt@,@transport_bkk_vt@,@import_value@,@other_value@,'@remark@','@creator_code@')
                              ON CONFLICT ON CONSTRAINT  cost_history_un  DO 
                              UPDATE SET rebate = '@rebate@' ,
                                                      vat_add = @vat_add@,
                                                      transport_bkk_nk = @transport_bkk_nk@ ,
                                                      transport_nk_vt = @transport_nk_vt@ ,
                                                      transport_bkk_vt = @transport_bkk_vt@, 
                                                      import_value = @import_value@ ,
                                                      other_value = @other_value@ ,
                                                      remark = '@remark@' ,
                                                      last_editor = '@creator_code@' , 
                                                      last_edit_date_time = now()"
                                                     .Replace("@doc_no@", item.doc_no)
                                                     .Replace("@item_code@", item.item_code)
                                                     .Replace("@unit_code@", item.unit_code)
                                                     .Replace("@rebate@", item.rebate)
                                                     .Replace("@vat_add@", item.vat_add.ToString())
                                                     .Replace("@transport_bkk_nk@", item.transport_bkk_nk.ToString())
                                                     .Replace("@transport_nk_vt@", item.transport_nk_vt.ToString())
                                                     .Replace("@transport_bkk_vt@", item.transport_bkk_vt.ToString())
                                                     .Replace("@import_value@", item.import_value.ToString())
                                                     .Replace("@other_value@", item.other_value.ToString())
                                                     .Replace("@remark@", item.remark.ToString())
                                                     .Replace("@creator_code@", user)
                           );
            }
            try
            {
                Connection.ExecuteSqlTransaction(commans);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { success = false, message = ex.Message });
            }
            return JsonConvert.SerializeObject(new { success = true, message = "" });
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void Delete(int key)
        {

        }

        protected void Grid1_ServerBatchEditRow(object sender, GridEventArgs e)
        {
            var d = e.Arguments.Values;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }


    public class LocalData
    {

        public LocalData(string _id, string _text)

        {

            this.ID = _id;

            this.Text = _text;

        }

        public LocalData() { }

        public string ID

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

            var dataTable = Connection.GetData("select name_1,code  from ap_supplier");

            foreach (DataRow item in dataTable.Rows)
            {
                data.Add(new LocalData(item["code"].ToString(),  item["name_1"].ToString() ));
            }

            //

            //data.Add(new LocalData(2, "Austin-Healey"));

            //data.Add(new LocalData(3, "Aston Martin"));

            //data.Add(new LocalData(4, "BMW 7"));

            //data.Add(new LocalData(5, "Bentley Mulsanne"));

            //data.Add(new LocalData(6, "Bugatti Veyron"));

            //data.Add(new LocalData(7, "Chevrolet Camaro"));

            //data.Add(new LocalData(8, "Cadillac "));

            //data.Add(new LocalData(9, "Honda S2000"));

            //data.Add(new LocalData(10, "Hyundai Santro"));

            //data.Add(new LocalData(11, "Mercedes-Benz "));

            //data.Add(new LocalData(12, "Mercury Coupe"));

            //data.Add(new LocalData(13, "Maruti Alto 800"));

            //data.Add(new LocalData(14, "Volkswagen Shirako"));

            //data.Add(new LocalData(15, "Lotus Esprit "));

            //data.Add(new LocalData(16, "Lamborghini Diablo"));

            //data.Add(new LocalData(17, "Nissan Qashqai "));

            //data.Add(new LocalData(18, "Oldsmobile S98 "));

            //data.Add(new LocalData(19, "Opel Superboss "));

            //data.Add(new LocalData(20, "Scion SRS/SC/SD "));

            //data.Add(new LocalData(21, "Saab Sportcombi "));

            //data.Add(new LocalData(22, "Subaru Sambar "));

            //data.Add(new LocalData(23, "Suzuki Swift "));

            //data.Add(new LocalData(24, "Volvo P1800 "));

            //data.Add(new LocalData(25, "Kia Sedona EX "));

            //data.Add(new LocalData(26, "Koenigsegg Agera "));

            //data.Add(new LocalData(27, "Ford Boss 302 "));

            //data.Add(new LocalData(28, "Ferrari 360 "));

            //data.Add(new LocalData(29, "Ford Thunderbird "));

            //data.Add(new LocalData(30, "Alfa Romeo"));

            return data;
        }

        public List<LocalData> GetInventoryItems()
        {

            List<LocalData> data = new List<LocalData>();

            var dataTable = Connection.GetData("select name_1,code  from ic_inventory");

            foreach (DataRow item in dataTable.Rows)
            {
                data.Add(new LocalData(item["code"].ToString(), item["name_1"].ToString()));
            }


            return data;
        }

    }
}