using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elinic.Classes;
using System.Drawing;
using System.Collections.Specialized;

namespace Elinic
{
    public partial class OrderComplete : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Order Complete";
            if (Request.QueryString["OrderId"] != null)
            {
                this.Page.Title += " - Design Number : " + Convert.ToString(Request.QueryString["OrderId"]);
            }
        }

        protected void btnGoHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://www.elinic.com/index.htm");

        }


        protected void btnConfigure_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Project.aspx", false);

        }

    }
}