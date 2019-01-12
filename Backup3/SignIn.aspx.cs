using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CostHistory
{
    public partial class SignIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnLogIn.Text = Resources.Resource.signin;
        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                var ds = CostHistory.Models.Connection.GetData("select code,user_name from mis_user where code = '" + txtUsername.Text.Trim() + "' and password = '" + txtPassword.Text.Trim() + "'");
                if (ds.Rows.Count > 0)
                {
                    FormsAuthentication.SetAuthCookie(ds.Rows[0]["user_name"].ToString(), false);
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                }
                else
                {
                    Label1.Text = "User or password is wrong!";
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
           
        }
    }
}