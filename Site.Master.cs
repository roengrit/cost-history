#region Copyright Syncfusion Inc. 2001-2018.
// Copyright Syncfusion Inc. 2001-2018. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;

namespace CostHistory
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
                string language = "lo-LA";

                //Detect User's Language.
               

                if (Session["lang"] != null) {
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
            }
    }
}
