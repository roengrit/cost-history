<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="CostHistory.SignIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSection" runat="server">
    <style>
        div.well {
            height: 250px;
        }

        .Absolute-Center {
            margin: auto;
            position: absolute;
            top: 0;
            left: 0;
            bottom: 0;
            right: 0;
        }

            .Absolute-Center.is-Responsive {
                width: 50%;
                height: 50%;
                min-width: 200px;
                max-width: 400px;
                padding: 40px;
            }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="Absolute-Center is-Responsive">
                <div class="col-sm-12 col-md-10 col-md-offset-1">
                    <h6><%=Resources.Resource.signin_text %> </h6>
                    <form id="loginForm"  >
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="txtUsername" class="form-control" type="text" name='username' placeholder="Username"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="txtPassword" class="form-control" type="password" name='password' placeholder="Password"></asp:TextBox>
                        </div>
                         <div class="form-group">
                             <asp:Label ID="Label1" ForeColor="Red" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Button runat="server" class="btn btn-success btn-block" ID="btnLogIn" OnClick="btnLogIn_Click"></asp:Button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
