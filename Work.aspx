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
        <img width="24" height="24" id="down-click"  onclick="show()" style="margin-top: 8px; cursor: pointer;display:none; float: right; margin-left: 10px" src="Content/svg/si-glyph-arrow-down.svg" />
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

    </div>

    <br />

    <div id="ControlRegion" style="width: 100%">
        <div class="frame " style="width: 100%">
            <ej:Grid ID="Grid1" AllowSorting="true"
                EnableResponsiveRow="true"
                IsResponsive="true"
                AllowScrolling="True" AllowResizing="True"
                EnableRowHover="true" AllowTextWrap="True" AllowCellMerging="false"
                AllowReordering="false" Locale="en-US" AllowMultiSorting="false"
                OnServerWordExporting="FlatGrid_ServerWordExporting" OnServerPdfExporting="FlatGrid_ServerPdfExporting"
                OnServerExcelExporting="FlatGrid_ServerExcelExporting"
                AllowSelection="True" Selectiontype="Single"
                runat="server">
                <Columns>
                    <ej:Column Field="item_code" HeaderText="item_code" IsPrimaryKey="true" IsFrozen="True" TextAlign="Right" Width="100" />
                    <ej:Column Field="item_name" HeaderText="item_name" IsFrozen="True" Width="150" />
                    <ej:Column Field="unit_name" HeaderText="unit_name" TextAlign="Right" Width="110" />
                    <ej:Column Field="qty" HeaderText="qty" TextAlign="Right" Width="90" Format="{0:C}" />
                    <ej:Column Field="price" HeaderText="price" Width="100" TextAlign="Right" Format="{0:C}"   />
                    <ej:Column Field="sum_amount" HeaderText="sum_amount" Width="100" TextAlign="Right" Format="{0:C}"   />
                </Columns>
                <ClientSideEvents ToolbarClick="onToolBarClick" />
                <ToolbarSettings ShowToolbar="True" ToolbarItems="update">
                </ToolbarSettings>
                <ScrollSettings Height="100%" Width="100%" FrozenRows="1"></ScrollSettings>
                <EditSettings AllowEditing="True" AllowAdding="True" AllowDeleting="True"></EditSettings>
                <SelectionSettings EnableToggle="true" />
            </ej:Grid>
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
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
