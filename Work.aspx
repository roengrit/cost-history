<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Work.aspx.cs" Inherits="CostHistory.Work" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script src='<%= Page.ResolveClientUrl("~/Scripts/ej/i18n/ej.culture.en-US.min.js")%>' type="text/javascript"></script>
    <script src='<%= Page.ResolveClientUrl("~/Scripts/ej/l10n/ej.localetexts.en-US.min.js")%>' type="text/javascript"></script>
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
        <h4 style="margin-bottom: 15px; float: left">Search</h4>
        <img width="24" height="24" id="down-click" onclick="show()" style="margin-top: 8px; cursor: pointer; display: none; float: right; margin-left: 10px" src="Content/svg/si-glyph-arrow-down.svg" />
    </div>

    <div class="form-row col-md-6" id="find">
        <div class="form-group col-md-12">
            <label>Supplier</label>
            <ej:Autocomplete ID="AutoComplete" Width="100%" runat="server" DataTextField="Text" DataUniqueKeyField="ID" />
        </div>

        <div class="form-group col-md-6">
            <label>From</label>
            <ej:DatePicker ID="dtStartDate" DayHeaderFormat="Long" DateFormat="dd/MM/yyyy" runat="server"></ej:DatePicker>
        </div>
        <div class="form-group col-md-6">
            <label>To</label>
            <ej:DatePicker ID="dtEndDate" DayHeaderFormat="Long" DateFormat="dd/MM/yyyy" runat="server"></ej:DatePicker>
        </div>
        <div class="form-group col-md-12">
            <label>Document No</label>
            <asp:TextBox runat="server" ID="txtUsername" class="form-control small" type="text" name='username'></asp:TextBox>
        </div>
        <div class="form-group col-md-5">
            <asp:Button runat="server" class="btn btn-success btn-block" ID="btnFind" OnClick="btnFind_Click"></asp:Button>
        </div>
        <div class="form-group col-md-3">
            <img width="24" height="24" onclick="hide()" style="margin-top: 8px; cursor: pointer;" src="Content/svg/si-glyph-arrow-up.svg" />
        </div>

            <div class="form-group col-md-5">
            <asp:Button runat="server" class="btn btn-success btn-block" ID="btnSave" OnClick="btnSave_Click"></asp:Button>
        </div>

    </div>

    <br />

    <div id="ControlRegion" style="width: 100%">
        <div class="frame " style="width: 100%">
          
            <ej:Grid ID="Grid1" AllowSorting="true"
                AllowScrolling="True" AllowResizing="True"
                EnableRowHover="true" AllowTextWrap="True" AllowCellMerging="false"
                AllowReordering="false" Locale="en-US" AllowMultiSorting="false"
                OnServerWordExporting="FlatGrid_ServerWordExporting" OnServerPdfExporting="FlatGrid_ServerPdfExporting"
                OnServerExcelExporting="FlatGrid_ServerExcelExporting"
                AllowSelection="True" Selectiontype="Single"
                OnServerEditRow="EditEvents_ServerEditRow"
                OnServerBatchEditRow="Grid1_ServerBatchEditRow"
                runat="server">
                <ClientSideEvents ActionComplete="complete" EndAdd="endAdd" EndDelete="endDelete" EndEdit="endEdit" DataBound="onDataBound" />
              
                <Columns>
                    <ej:Column Field="key" IsPrimaryKey="true" Visible="false" HeaderText="doc_no" AllowEditing="false" TextAlign="Left" />
                    <ej:Column Field="doc_date" HeaderText="doc_date" AllowEditing="false" TextAlign="Left" Format="{0:dd/MM/yyyy}" />
                    <ej:Column Field="doc_no" HeaderText="doc_no" AllowEditing="false" TextAlign="Left" />
                    <ej:Column Field="suplier_name" HeaderText="suplier_name" AllowEditing="false" TextAlign="Left" />
                    <ej:Column Field="item_code" HeaderText="item_code" AllowEditing="false" TextAlign="Left" />
                    <ej:Column Field="item_name" HeaderText="item_name" AllowEditing="false" />
                    <ej:Column Field="unit_name" HeaderText="unit_name" AllowEditing="false" TextAlign="Right" />
                    <ej:Column Field="qty" HeaderText="qty" TextAlign="Right" AllowEditing="false" Format="{0:C}" />
                    <ej:Column Field="price" HeaderText="price" AllowEditing="false" TextAlign="Right" Format="{0:C}" />
                    <ej:Column Field="discount" HeaderText="discount" AllowEditing="false" TextAlign="Right" Format="{0:C}" />
                    <ej:Column Field="price_after_discount" HeaderText="price_after_discount" AllowEditing="false" TextAlign="Right" Format="{0:C}" />
                    <ej:Column Field="total_vat_value" HeaderText="total_vat_value" AllowEditing="false" TextAlign="Right" Format="{0:C}" />
                    <ej:Column Field="receipt_price" HeaderText="receipt_price" AllowEditing="false" TextAlign="Right" Format="{0:C}" />
                    <ej:Column Field="free_value" HeaderText="free_value" AllowEditing="true" TextAlign="Right" Format="{0:C}" />
                    <ej:Column Field="other_discount" HeaderText="other_discount" TextAlign="Right" Format="{0:C}" />
                </Columns>

                <ClientSideEvents CellSave="cellSave" ActionComplete="complete" CellEdit="cellEdit" EndAdd="endAdd" EndDelete="endDelete" EndEdit="endEdit" DataBound="onDataBound" />
                <ToolbarSettings ShowToolbar="True"  ToolbarItems="update">
                </ToolbarSettings>
                <ScrollSettings Height="100%" Width="100%"></ScrollSettings>
                <EditSettings AllowEditing="True" AllowAdding="True" EditMode="Batch" AllowDeleting="True"></EditSettings>
            </ej:Grid>
        </div>
        <script type="text/javascript">

            function cellSave(args) {
                 var gridObj = $("#" + this._id).data("ejGrid"); 
                 var rowIndex = gridObj.selectedRowsIndexes; 
                if (args.columnName == "other_discount") {  
                    gridObj.setCellValue(rowIndex, "free_value", 66666); 
                } 
            }

            function cellEdit(args) {
                
            }
            function endAdd(args) {
                console.log('A');
            }
            function endDelete(args) {
                
            }
            function endEdit(args) {
                console.log(args);
               
            }
            function onDataBound(args) {

            }
            function complete(args) {
                console.log("A")
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
