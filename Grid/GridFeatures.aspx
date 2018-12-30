<%@ Page Language="C#" MasterPageFile="~/Site.Master" Title="Grid" AutoEventWireup="true" CodeBehind="GridFeatures.aspx.cs" Inherits="CostHistory.GridFeatures" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Grid Features:</h2>
<br />
<li> Grid Samples - Default</li>
<li> Filtering</li>
<li> Sorting</li>
<li> Paging</li>
<li> Selection - Single</li>
<li> Column Resizing</li>
<li> Frozen Rows and Columns</li>
<li> Custom Toolbar</li>
<li> Keyboard Interaction</li>
<li> Column Chooser</li>
<li> Allow Wrap</li>
<li> Header Template</li>
<li> Scrolling</li>
<li> Export</li>
<li> Summary</li>
<li> Editing Type - Inline Editing</li>
<li> Localization - en-US</li>
<li> Theme - Bootstrap-Theme</li>
<br/>
     <script src='<%= Page.ResolveClientUrl("~/Scripts/ej/i18n/ej.culture.en-US.min.js")%>' type="text/javascript"></script>
			<script src='<%= Page.ResolveClientUrl("~/Scripts/ej/l10n/ej.localetexts.en-US.min.js")%>' type="text/javascript"></script>
<div id = "ControlRegion">
<div class="frame ">
    <div>
        <ej:Grid ID="Grid1"  AllowSorting="true" AllowPaging="True"
             AllowScrolling="True" AllowResizing="True"  
            EnableRowHover="true" AllowFiltering="True" AllowTextWrap="True" AllowCellMerging="false"
            ShowColumnChooser="true" AllowReordering="false" Locale="en-US" AllowMultiSorting="false" 
		 OnServerWordExporting="FlatGrid_ServerWordExporting" 		OnServerPdfExporting="FlatGrid_ServerPdfExporting" 
            OnServerExcelExporting="FlatGrid_ServerExcelExporting" 
              AllowSelection="True" Selectiontype="Single"
            ShowSummary="True"  runat="server">
           <SummaryRows>
                <ej:SummaryRow Title="Sum">
                    <SummaryColumn>
                        <ej:SummaryColumn SummaryType="Sum" Format="{0:C}" DisplayColumn="Freight" DataMember="Freight" />
                    </SummaryColumn>
                </ej:SummaryRow>
                <ej:SummaryRow Title="Average">
                    <SummaryColumn>
                        <ej:SummaryColumn SummaryType="Average" Format="{0:C}" DisplayColumn="Freight" DataMember="Freight" />
                    </SummaryColumn>
                </ej:SummaryRow>
                <ej:SummaryRow Title="Smallest">
                    <SummaryColumn>
                        <ej:SummaryColumn SummaryType="Minimum" Format="{0:C}" DisplayColumn="Freight" DataMember="Freight" />
                    </SummaryColumn>
                </ej:SummaryRow>
                <ej:SummaryRow Title="Largest">
                    <SummaryColumn>
                        <ej:SummaryColumn SummaryType="Maximum" Format="{0:C}" DisplayColumn="Freight" DataMember="Freight" />
                    </SummaryColumn>
                </ej:SummaryRow>
            </SummaryRows> 
            <Columns>
                <ej:Column Field="OrderID" HeaderText="Order ID" IsPrimaryKey="true" IsFrozen="True"  TextAlign="Right" Width="90" />
                <ej:Column Field="CustomerID" HeaderText="Customer ID" IsFrozen="True" Width="100"  />
                <ej:Column Field="EmployeeID" HeaderText="Employee ID" HeaderTemplateID="#employeeTemplate" TextAlign="Right" Width="110"  />
                <ej:Column Field="Freight" HeaderText="Freight"  TextAlign="Right" Width="90" Format="{0:C}" />
                <ej:Column Field="OrderDate" HeaderText="Order Date" Width="100" HeaderTemplateID="#dateTemplate" TextAlign="Right" Format="{0:MM/dd/yyyy}" />
                <ej:Column Field="ShipCity" HeaderText="Ship City" Width="100" />
            </Columns>
            <ClientSideEvents ToolbarClick="onToolBarClick" />
<ToolbarSettings ShowToolbar="True" ToolbarItems="excelExport,wordExport,pdfExport, add,edit,delete,update,cancel">
                <CustomToolbarItem>
                    <ej:CustomToolbarItem TemplateID="#Refresh" />
                </CustomToolbarItem>
</ToolbarSettings>
           <ScrollSettings Height="330" Width="550" FrozenRows="1"></ScrollSettings>
             <PageSettings PageSize="15" />
              <EditSettings AllowEditing="True" AllowAdding="True" AllowDeleting="True"></EditSettings>
            <SelectionSettings EnableToggle="true" />
           <FilterSettings FilterBarMode="Immediate" ShowFilterBarStatus="True" StatusBarWidth="300"></FilterSettings>
        </ej:Grid>
    </div>
        </div>
<script type="text/javascript">
        $(function () {
        });
        $(document).on("keydown", function (e) {
            if (e.altKey && e.keyCode === 74) {
                $("#Grid1").focus();
            }
        });
    </script>
 <script id="dateTemplate" type="text/x-jsrender">
        <span class="date headericon"></span>Order Date
    </script>
    <script id="employeeTemplate" type="text/x-jsrender">
        <span class="headericon employee"></span>Emp ID
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
             <script id="Refresh" type="text/x-jsrender">
        <a class="e-toolbaricons refresh" />
    </script>
    <script type="text/javascript">
        function onToolBarClick(sender, args) {
            var tbarObj = $(sender.target);
            if (tbarObj.hasClass("refresh"))
                this.refreshContent();
        }
    </script>
</div>
</asp:Content>
