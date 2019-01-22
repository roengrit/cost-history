using CostHistory.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using Syncfusion.EJ.Export;
using Syncfusion.JavaScript.Web;
using Syncfusion.XlsIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
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
    public partial class WorkReport : System.Web.UI.Page
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

                Session["inventory"] = new LocalData().GetInventoryItems().ToList();
                this.AutoCompleteItem.DataSource = (List<LocalData>)Session["inventory"];

            }
            else
            {
                if (Session["Supplier"] != null)
                    this.AutoComplete.DataSource = (List<LocalData>)Session["Supplier"];
                else
                {
                    Session["Supplier"] = new LocalData().GetDataItems().ToList();
                    this.AutoComplete.DataSource = (List<LocalData>)Session["Supplier"];
                }

                if (Session["inventory"] != null)
                    this.AutoCompleteItem.DataSource = (List<LocalData>)Session["inventory"];
                else
                {
                    Session["inventory"] = new LocalData().GetInventoryItems().ToList();
                    this.AutoCompleteItem.DataSource = (List<LocalData>)Session["inventory"];
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
            Label1.Text = Resources.Resource.item_name;
            if (dtStartDate.Value.HasValue)
            {
                if (dtStartDate.Value.Value.Year == 2099)
                    dtStartDate.Value = null;
            }
            if (dtEndDate.Value.HasValue)
            {
                if (dtEndDate.Value.Value.Year == 2099)
                    dtEndDate.Value = null;
            }
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
            this.Grid1.Columns[1].Format = "{0:dd/MM/yyyy}";
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

            this.Grid1.Columns[26].HeaderText = "";
            this.Grid1.Columns[27].HeaderText = Resources.Resource.price_normal;
            this.Grid1.Columns[28].HeaderText = Resources.Resource.member_price;
            this.Grid1.Columns[29].HeaderText = Resources.Resource.price_1;
            this.Grid1.Columns[30].HeaderText = Resources.Resource.price_2;
            this.Grid1.Columns[31].HeaderText = Resources.Resource.price_3;
            this.Grid1.Columns[32].HeaderText = Resources.Resource.price_4;
            this.Grid1.Columns[33].HeaderText = Resources.Resource.price_5;
            this.Grid1.Columns[34].HeaderText = Resources.Resource.profit;

        }

        protected void FlatGrid_ServerExcelExporting(object sender, Syncfusion.JavaScript.Web.GridEventArgs e)
        {
            if (Session["can_export"] != null)
            {
                if (Session["can_export"].ToString() == "1")
                {
                    //ExcelExport exp = new ExcelExport();
                    //exp.Export(Grid1.Model, (DataTable)Session["SqlDataSource"], "Export.xlsx", ExcelVersion.Excel2010, true, true, "flat-lime");
                    ExcelPackage excel = new ExcelPackage();
                    var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                    workSheet.TabColor = System.Drawing.Color.Black;
                    workSheet.DefaultRowHeight = 12;
                    //workSheet.Row(1).Height = 20;
                    //workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    workSheet.Row(1).Style.Font.Bold = true;
                    var coIndex = 1;
                    foreach (var item in this.Grid1.Columns)
                    {
                        if (item.Visible)
                        {
                            workSheet.Cells[1, coIndex++].Value = item.HeaderText;
                        }
                    }

                    int recordIndex = 2;
                    foreach (DataRow item in ((DataTable)Session["SqlDataSource"]).Rows)
                    {
                        var coIndexRec = 1;
                        foreach (var itemColumn in this.Grid1.Columns)
                        {
                            if (itemColumn.Visible)
                            {
                                if (itemColumn.Field == "doc_date") {
                                    workSheet.Cells[recordIndex, coIndexRec] .Style.Numberformat.Format = "dd/mm/yyyy";
                                }
                                workSheet.Cells[recordIndex, coIndexRec++].Value =  item[itemColumn.Field] ;

                            }
                        }
                        recordIndex += 1;
                    }
                    //workSheet.Cells[1, 1].Value = "S.No";
                    //workSheet.Cells[1, 2].Value = "Id";
                    //workSheet.Cells[1, 3].Value = "Name";
                    //workSheet.Cells[1, 4].Value = "Address";
                    //Body of table  
                    //  
                    //int recordIndex = 2;
                    //foreach (var student in students)
                    //{
                    //    workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                    //    workSheet.Cells[recordIndex, 2].Value = student.Id;
                    //    workSheet.Cells[recordIndex, 3].Value = student.Name;
                    //    workSheet.Cells[recordIndex, 4].Value = student.Address;
                    //    recordIndex++;
                    //}
                    //workSheet.Column(1).AutoFit();
                    //workSheet.Column(2).AutoFit();
                    //workSheet.Column(3).AutoFit();
                    //workSheet.Column(4).AutoFit();

                    string excelName = "report";
                    using (var memoryStream = new MemoryStream())
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                        excel.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
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
                                          0.0  as net_cost_price,
                                          0.0  as profit,
                                          coalesce(ic_inventory_barcode.price ,0) as price_normal,
                                          coalesce(ic_inventory_barcode.price_member,0) as price_member,
                                          case when coalesce(ic_inventory_price_formula.price_1,'0') = ''  then 0 else coalesce(ic_inventory_price_formula.price_1,'0') :: numeric end as price_1,
                                          case when coalesce(ic_inventory_price_formula.price_2,'0') = ''  then 0 else coalesce(ic_inventory_price_formula.price_2,'0') :: numeric end as price_2,
                                          case when coalesce(ic_inventory_price_formula.price_3,'0') = ''  then 0 else coalesce(ic_inventory_price_formula.price_3,'0') :: numeric end as price_3,
                                          case when coalesce(ic_inventory_price_formula.price_4,'0') = ''  then 0 else coalesce(ic_inventory_price_formula.price_3,'0') :: numeric end as price_4,
                                          case when coalesce(ic_inventory_price_formula.price_5,'0') = ''  then 0 else coalesce(ic_inventory_price_formula.price_3,'0') :: numeric end as price_5 
                                    from  ic_trans_detail join  
                                          ic_inventory on ic_trans_detail.item_code = ic_inventory.code  join 
                                          ic_trans on ic_trans_detail.doc_no = ic_trans.doc_no left join  
                                          ap_supplier on ic_trans.cust_code = ap_supplier.code left join 
                                          ic_unit on ic_trans_detail.unit_code = ic_unit.code left join
                                          ic_inventory_barcode on ic_trans_detail.item_code = ic_inventory_barcode.ic_code and ic_trans_detail.unit_code = ic_inventory_barcode.unit_code and ic_inventory_barcode.status = 1 left join
                                          ic_inventory_price_formula on ic_trans_detail.item_code = ic_inventory_price_formula.ic_code and ic_trans_detail.unit_code = ic_inventory_price_formula.unit_code left join
                                          cost_history on ic_trans_detail.doc_no = cost_history.doc_no 
                                                       and ic_trans_detail.item_code = cost_history.item_code 
                                                       and ic_trans_detail.unit_code = cost_history.unit_code                                          
                                    where ic_trans.trans_flag = 12 and ic_trans_detail.last_status = 0 and ic_trans_detail.qty <> 0 and ic_trans_detail.price <> 0 ";

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
                sql += " and ic_trans.cust_code = '" + AutoComplete.SelectValueByKey.Replace(",", "") + "'";
            }

            if (!string.IsNullOrEmpty(txtDocNo.Text.Trim()))
            {
                sql += " and ic_trans.doc_no = '" + txtDocNo.Text.Trim() + "'";
            }


            if (!string.IsNullOrEmpty(AutoCompleteItem.SelectValueByKey))
            {
                sql += " and ic_inventory.code = '" + AutoCompleteItem.SelectValueByKey.Replace(",", "") + "'";
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
                else
                {
                    item["price_after_discount"] = Convert.ToDecimal(Convert.ToDecimal(item["receipt_price"]).ToString("N2"));
                    item["net_price_thai"] = Convert.ToDecimal(Convert.ToDecimal(item["receipt_price"]).ToString("N2"));
                    item["net_cost_price"] = Convert.ToDecimal(Convert.ToDecimal(item["receipt_price"]).ToString("N2"));
                }


                var divide = 0.0m;
                if (Convert.ToDecimal(item["price_normal"]) > 0)
                {
                    divide += 1;
                }
                if (Convert.ToDecimal(item["price_member"]) > 0)
                {
                    divide += 1;
                }
                if (Convert.ToDecimal(item["price_1"]) > 0)
                {
                    divide += 1;
                }
                if (Convert.ToDecimal(item["price_2"]) > 0)
                {
                    divide += 1;
                }
                if (Convert.ToDecimal(item["price_3"]) > 0)
                {
                    divide += 1;
                }
                if (Convert.ToDecimal(item["price_4"]) > 0)
                {
                    divide += 1;
                }
                if (Convert.ToDecimal(item["price_5"]) > 0)
                {
                    divide += 1;
                }
                if (divide > 0)
                {
                    var temp = ((Convert.ToDecimal(item["price_normal"]) +
                                             Convert.ToDecimal(item["price_member"]) +
                                             Convert.ToDecimal(item["price_1"]) +
                                             Convert.ToDecimal(item["price_2"]) +
                                             Convert.ToDecimal(item["price_3"]) +
                                             Convert.ToDecimal(item["price_4"]) +
                                             Convert.ToDecimal(item["price_5"])) / divide);
                    item["profit"] = ((((temp) - Convert.ToDecimal(item["net_cost_price"])) / temp )*100).ToString("N2");
                }
                else
                    item["profit"] = 0;

            }
            Session["SqlDataSource"] = dt;
            this.BindDataSource();
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["can_export"] != null)
            {
                if (Session["can_export"].ToString() == "1")
                {
                    //ExcelExport exp = new ExcelExport();
                    //exp.Export(Grid1.Model, (DataTable)Session["SqlDataSource"], "Export.xlsx", ExcelVersion.Excel2010, true, true, "flat-lime");
                    ExcelPackage excel = new ExcelPackage();
                    var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
                    workSheet.TabColor = System.Drawing.Color.Black;
                    workSheet.DefaultRowHeight = 12;
                    //workSheet.Row(1).Height = 20;
                    //workSheet.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    workSheet.Row(1).Style.Font.Bold = true;
                    var coIndex = 1;
                    foreach (var item in this.Grid1.Columns)
                    {
                        if (item.Visible)
                        {
                            workSheet.Cells[1, coIndex++].Value = item.HeaderText;
                        }
                    }

                    int recordIndex = 2;
                    foreach (DataRow item in ((DataTable)Session["SqlDataSource"]).Rows)
                    {
                        var coIndexRec = 1;
                        foreach (var itemColumn in this.Grid1.Columns)
                        {
                            if (itemColumn.Visible)
                            {
                                if (itemColumn.Field == "doc_date")
                                {
                                    workSheet.Cells[recordIndex, coIndexRec].Style.Numberformat.Format = "dd/mm/yyyy";
                                }
                                workSheet.Cells[recordIndex, coIndexRec++].Value = item[itemColumn.Field];

                            }
                        }
                        recordIndex += 1;
                    }
                    //workSheet.Cells[1, 1].Value = "S.No";
                    //workSheet.Cells[1, 2].Value = "Id";
                    //workSheet.Cells[1, 3].Value = "Name";
                    //workSheet.Cells[1, 4].Value = "Address";
                    //Body of table  
                    //  
                    //int recordIndex = 2;
                    //foreach (var student in students)
                    //{
                    //    workSheet.Cells[recordIndex, 1].Value = (recordIndex - 1).ToString();
                    //    workSheet.Cells[recordIndex, 2].Value = student.Id;
                    //    workSheet.Cells[recordIndex, 3].Value = student.Name;
                    //    workSheet.Cells[recordIndex, 4].Value = student.Address;
                    //    recordIndex++;
                    //}
                    //workSheet.Column(1).AutoFit();
                    //workSheet.Column(2).AutoFit();
                    //workSheet.Column(3).AutoFit();
                    //workSheet.Column(4).AutoFit();

                    string excelName = "report";
                    using (var memoryStream = new MemoryStream())
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment; filename=" + excelName + ".xlsx");
                        excel.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }
        }
    }

}