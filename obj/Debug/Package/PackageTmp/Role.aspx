<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Role.aspx.cs" Inherits="CostHistory.Role" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StyleSection" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table>  
                    <tr>  
                        <td>  
                            <asp:Label ID="lblEmpId" runat="server" Text="Employee Code"></asp:Label>  
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>  
                        </td>  
                         <td>  
                             <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Text="Save" /> 
                         </td>  
                    </tr>  
                </table>  
    <br />

     <asp:DataGrid ID="Grid" runat="server" Width="500px"   OnRowCreated = "GridView1_RowCreated"  DataKeyField="user_code" AutoGenerateColumns="False" CellPadding="10"  ForeColor="White" GridLines="None"    OnDeleteCommand="Grid_DeleteCommand"   >  
                    <Columns>  
                        <asp:BoundColumn HeaderText="Code" HeaderStyle-BackColor="#6c7ae0" DataField="user_code"> </asp:BoundColumn>  
                        <asp:BoundColumn HeaderText="Name" HeaderStyle-BackColor="#6c7ae0"   DataField="user_name"> </asp:BoundColumn> 
                        <asp:ButtonColumn CommandName="Delete"  HeaderStyle-BackColor="#6c7ae0"   ButtonType="PushButton"  HeaderText="#" Text="Delete">
                        </asp:ButtonColumn>  
                    </Columns>  
                    
                   
                    <AlternatingItemStyle BackColor="#f8f6ff" />  
                    <ItemStyle BackColor="White" ForeColor="#333333" />  
                     

     </asp:DataGrid>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ScriptSection" runat="server">
</asp:Content>
