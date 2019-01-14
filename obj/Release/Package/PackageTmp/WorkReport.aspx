<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkReport.aspx.cs" Inherits="CostHistory.WorkReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script src='<%= ResolveUrl("~/Scripts/ej/i18n/ej.culture.en-US.min.js")%>' type="text/javascript"></script>
    <script src='<%= ResolveUrl("~/Scripts/ej/l10n/ej.localetexts.en-US.min.js")%>' type="text/javascript"></script>
    <script>
        if (localStorage.getItem("hide") == "1") {
            hide();
        } else {
            show();
        }
        function hide() {
            $("#down-click").show();
            $("#find").hide();
            localStorage.setItem("hide", "1")
        }

        function show() {
            $("#find").show();
            $("#down-click").hide();
            localStorage.setItem("hide", "0")

        }
    </script>
    <br />
    
    <div class="form-row col-md-12">
        <h4 style="margin-bottom: 15px; float: left">
            <asp:Label ID="lblSearch" runat="server" Text="Search"></asp:Label> 
        </h4>
        <img width="24" height="24" id="down-click" onclick="show()" style="margin-top: 8px; cursor: pointer; display: none; float: right; margin-left: 10px" src="Content/svg/si-glyph-arrow-down.svg" />
    </div>

    <div class="form-row col-md-6" id="find">
        <div class="form-group col-md-12">
            <label>
                <asp:Label ID="lblSupplier" runat="server" Text="Search"></asp:Label> 
            </label>
            <ej:Autocomplete ID="AutoComplete"   FilterType="Contains" Width="100%" runat="server" DataTextField="Text"  MultiSelectMode="None"  DataUniqueKeyField="ID" />
        </div>

        <div class="form-group col-md-6">
            <label> <asp:Label ID="lblForm" runat="server" Text="Search"></asp:Label> </label>
            <ej:DatePicker ID="dtStartDate" DayHeaderFormat="Long" DateFormat="dd/MM/yyyy" runat="server"></ej:DatePicker>
        </div>
        <div class="form-group col-md-6">
            <label> <asp:Label ID="lblTo" runat="server" Text="Search"></asp:Label> </label>
            <ej:DatePicker ID="dtEndDate" DayHeaderFormat="Long" DateFormat="dd/MM/yyyy" runat="server"></ej:DatePicker>
        </div>
        <div class="form-group col-md-12">
            <label> <asp:Label ID="lblDocNo" runat="server" Text="Search"></asp:Label> </label>
            <asp:TextBox runat="server" ID="txtDocNo" class="form-control small" type="text" name='username'></asp:TextBox>
        </div>
        <div class="form-group col-md-12">
            <label>
                <asp:Label ID="Label1" runat="server" Text="Search"></asp:Label> 
            </label>
            <ej:Autocomplete ID="AutoCompleteItem" Width="100%" runat="server" DataTextField="Text"   FilterType="Contains"  MultiSelectMode="None"  DataUniqueKeyField="ID" />
        </div>
        <div class="form-group col-md-5">
            <asp:Button runat="server" class="btn btn-primary btn-block" ID="btnFind" OnClick="btnFind_Click"></asp:Button>
        </div>
        <div class="form-group col-md-3">
            <img width="24" height="24" onclick="hide()" style="margin-top: 8px; cursor: pointer;" src="Content/svg/si-glyph-arrow-up.svg" />
        </div>
         <asp:Label ID="lblSave" Visible="false" runat="server" Text="Search"></asp:Label>

    </div>

    <div id="ControlRegion" style="width: 100%">
        <div class="frame " style="width: 100%">

            <ej:Grid ID="Grid1" AllowSorting="true"
                AllowResizeToFit="True"
                AllowScrolling="True" AllowResizing="True"
                EnableRowHover="true" AllowTextWrap="True" AllowCellMerging="false"
                AllowReordering="false" Locale="en-US" AllowMultiSorting="false"
                OnServerWordExporting="FlatGrid_ServerWordExporting" 
                OnServerPdfExporting="FlatGrid_ServerPdfExporting"
                OnServerExcelExporting="FlatGrid_ServerExcelExporting"
                AllowSelection="True" Selectiontype="Single"
                ShowColumnChooser="true" 
                runat="server">
                 
             

                <Columns>
                    <ej:Column Field="key" IsPrimaryKey="true" Visible="false" HeaderText="" AllowEditing="false" TextAlign="Left" />
                    <ej:Column Field="doc_date" HeaderText="doc_date" AllowEditing="false" TextAlign="Left" Format="{0:dd/MM/yyyy}" />
                    <ej:Column Field="doc_no" HeaderText="doc_no" AllowEditing="false" TextAlign="Left" />
                    <ej:Column Field="supplier_name" HeaderText="supplier_name" AllowEditing="false" TextAlign="Left" />
                    <ej:Column Field="item_code" HeaderText="item_code" AllowEditing="false" TextAlign="Left" />
                    <ej:Column Field="item_name" HeaderText="item_name" AllowEditing="false" />
                    <ej:Column Field="unit_name" HeaderText="unit_name" AllowEditing="false" TextAlign="Right" />
                    <ej:Column Field="qty" HeaderText="qty" TextAlign="Right" AllowEditing="false"   />
                    <ej:Column Field="price" HeaderText="price" AllowEditing="false" TextAlign="Right"  />
                    <ej:Column Field="discount" HeaderText="discount" AllowEditing="false" TextAlign="Right"  />
                    <ej:Column Field="price_after_discount" HeaderText="price_after_discount" AllowEditing="false" TextAlign="Right"  />
                    <ej:Column Field="total_vat_value" HeaderText="total_vat_value" AllowEditing="false" TextAlign="Right"  />
                    <ej:Column Field="receipt_price" HeaderText="receipt_price" AllowEditing="false" TextAlign="Right"  />
                    <ej:Column Field="free_value" HeaderText="free_value" AllowEditing="false" TextAlign="Right"  />
                    <ej:Column Field="other_discount" HeaderText="other_discount"  AllowEditing="false" TextAlign="Right"  />
                    <ej:Column Field="rebate" HeaderText="rebate"  />
                    <ej:Column Field="price_after_pro" HeaderText="price_after_pro" TextAlign="Right"  AllowEditing="false"   > 
                    </ej:Column> 
                    <ej:Column Field="vat_add" HeaderText="vat_add"  TextAlign="Right" EditType="NumericEdit" AllowEditing="false"    > 
                        <NumericEditOptions DecimalPlaces="2"></NumericEditOptions>
                    </ej:Column> 
                    <ej:Column Field="transport_bkk_nk" HeaderText="transport_bkk_nk" TextAlign="Right" EditType="NumericEdit" AllowEditing="false"    > 
                        <NumericEditOptions DecimalPlaces="2"></NumericEditOptions>
                    </ej:Column> 
                    <ej:Column Field="net_price_thai" HeaderText="net_price_thai" AllowEditing="false"   />
                    <ej:Column Field="transport_nk_vt" HeaderText="transport_nk_vt" TextAlign="Right" EditType="NumericEdit" AllowEditing="false"   > 
                        <NumericEditOptions DecimalPlaces="2"></NumericEditOptions>
                    </ej:Column> 
                    <ej:Column Field="transport_bkk_vt" HeaderText="transport_bkk_vt"  TextAlign="Right" EditType="NumericEdit" AllowEditing="false"    > 
                        <NumericEditOptions DecimalPlaces="2"></NumericEditOptions>
                    </ej:Column>                  
                    <ej:Column Field="import_value" HeaderText="import_value"  TextAlign="Right" EditType="NumericEdit" AllowEditing="false"    > 
                        <NumericEditOptions DecimalPlaces="2"></NumericEditOptions>
                    </ej:Column> 
                    <ej:Column Field="other_value" HeaderText="other_value"  TextAlign="Right" EditType="NumericEdit" AllowEditing="false"    > 
                        <NumericEditOptions DecimalPlaces="2"></NumericEditOptions>
                    </ej:Column> 
                    <ej:Column Field="remark" HeaderText="remark"  />

                    <ej:Column Field="net_cost_price" HeaderText="net_cost_price" AllowEditing="false" TextAlign="Right"   />

                    <ej:Column Field="rebate_number"  Visible="false" HeaderText="" AllowEditing="false" TextAlign="Left" />
                    <ej:Column Field="price_normal" HeaderText="price_normal" AllowEditing="false" TextAlign="Right"   />
                    <ej:Column Field="price_member" HeaderText="price_member" AllowEditing="false" TextAlign="Right"   />
                    <ej:Column Field="price_1" HeaderText="price_1" AllowEditing="false" TextAlign="Right"   />
                    <ej:Column Field="price_2" HeaderText="price_2" AllowEditing="false" TextAlign="Right"   />
                    <ej:Column Field="price_3" HeaderText="price_3" AllowEditing="false" TextAlign="Right"   />
                    <ej:Column Field="profit" HeaderText="profit" AllowEditing="false" TextAlign="Right"   />
                     
                </Columns>

                <ClientSideEvents CellSave="cellSave"  ActionComplete="complete" CellEdit="cellEdit" EndAdd="endAdd"  BeforeBatchSave="endEdit" EndDelete="endDelete" EndEdit="endEdit" DataBound="onDataBound" />
                <ToolbarSettings ShowToolbar="True"  ToolbarItems="excelExport"  >
                </ToolbarSettings>
                <ScrollSettings Height="100%" Width="100%"></ScrollSettings>
              <%--  <EditSettings  ></EditSettings>--%>
            </ej:Grid>
        </div>
        <script type="text/javascript">

            function saveAll() {
                var data = $("#" + "<%=Grid1.ClientID%>").ejGrid("getBatchChanges")["changed"];
                $.ajax({
                    type: "POST",
                    url: "<%=ResolveUrl("~/Work.aspx/Update")%>",
                    data: "{items:" + JSON.stringify(data) +",user:'" + '<%=HttpContext.Current.User.Identity.IsAuthenticated? HttpContext.Current.User.Identity.Name : "001" %>' + "'  }" ,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data)
                        if(JSON.parse(data.d).success)
                        alert("บันทึกสำเร็จ");
                    },
                    failure: function(errMsg) {
                       // alert(errMsg);
                    }
                });
            }

            

            function cellSave(args) {

                var gridObj = $("#" + this._id).data("ejGrid");
                var rowIndex = gridObj.selectedRowsIndexes;
                var objRow = gridObj.getSelectedRecords()[0];
                if (!args.value) return;
                if (args.value == "") return;

                if (args.columnName == "rebate") {
                    var rebate_number = 0.0;
                    if (args.value)
                        if (args.value.trim() != '') {
                            if (args.value.indexOf("%") >= 0) {
                                rebate_number = (objRow.price_after_discount * -1) * (parseFloat(args.value.replace("%", "")) / 100)
                                rebate_number = Math.round(rebate_number * 100) / 100
                            } else if (!isNaN(args.value)) {
                                rebate_number = (parseFloat(args.value)) * -1
                            } else {
                                rebate_number = 0
                            }
                        }
                    objRow.rebate_number = rebate_number
                }

                switch (args.columnName) {
                    case 'vat_add': objRow.vat_add = isNaN(args.value) ? 0 : parseFloat(args.value); break;
                    case 'transport_bkk_nk': objRow.transport_bkk_nk = isNaN(args.value) ? 0 : parseFloat(args.value); break;
                    case 'transport_nk_vt': objRow.transport_nk_vt = isNaN(args.value) ? 0 : parseFloat(args.value); break;
                    case 'transport_bkk_vt': objRow.transport_bkk_vt = isNaN(args.value) ? 0 : parseFloat(args.value); break;
                    case 'import_value' : objRow.import_value = isNaN(args.value) ? 0 : parseFloat(args.value); break;
                    default:
                }

                console.log(args.columnName);
                console.log(objRow.vat_add);
                console.log(args.value);

                objRow.price_after_pro = (objRow.receipt_price + objRow.rebate_number) - parseFloat(objRow.other_discount);
                objRow.price_after_pro = Math.round(objRow.price_after_pro * 100) / 100;

                objRow.vat_add = objRow.vat_add ? objRow.vat_add : 0;
                objRow.transport_bkk_nk = objRow.transport_bkk_nk ? objRow.transport_bkk_nk : 0;
                objRow.transport_nk_vt = objRow.transport_nk_vt ? objRow.transport_nk_vt : 0;
                objRow.transport_bkk_vt = objRow.transport_bkk_vt ? objRow.transport_bkk_vt : 0;
                objRow.import_value = objRow.import_value ? objRow.import_value : 0;
                
                console.log(objRow)

                objRow.net_price_thai = parseFloat(objRow.receipt_price) + parseFloat(objRow.vat_add) + parseFloat(objRow.transport_bkk_nk) + parseFloat(objRow.rebate_number);
                objRow.net_cost_price = parseFloat(objRow.transport_nk_vt) + parseFloat(objRow.transport_bkk_vt) + parseFloat(objRow.import_value) + parseFloat(objRow.net_price_thai);
               
                gridObj.setCellValue(rowIndex, "rebate_number", objRow.rebate_number);


                 var column = this.getColumnByField("price_after_pro");
                column.allowEditing = true;   
                gridObj.setCellValue(rowIndex, "price_after_pro", objRow.price_after_pro);
                column.allowEditing = false;

                column = this.getColumnByField("net_price_thai");
                column.allowEditing = true;                 
                gridObj.setCellValue(rowIndex, "net_price_thai", objRow.net_price_thai);
                column.allowEditing = false;  
                
                column = this.getColumnByField("net_cost_price");
                column.allowEditing = true;   
                gridObj.setCellValue(rowIndex, "net_cost_price", objRow.net_cost_price);
                column.allowEditing = false;

            }

            function cellEdit(args) {
                 
                
            }

            function endAdd(args) {

            }

            function endDelete(args) {

            }

            function endEdit(args) {
               
            }

            function onDataBound(args) {
                //var column = this.getColumnByField("doc_no");
                //column.IsFrozen = true;
            }

            function complete(args) {
                 
            }
        </script>
        <style type="text/css">
            .headericon {
                background-image: url(../Content/Images/Grid/icons-gray.png);
                padding-left: 20px;
            }

            .date {
                background-position: -3px -86px;
            }

            .employee {
                background-position: -82px -62px;
            }
        </style>
        <style type="text/css" class="cssStyles">
            .refresh {
                background-position: -76px 3px;
                background-image: url("../Content/Images/Grid/icons-gray.png");
            }
        </style>

    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
