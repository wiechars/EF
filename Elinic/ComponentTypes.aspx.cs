using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using Elenic.Classes;


namespace ElinicFurniture
{
    public partial class ComponentTypes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadComponents();
        }

        private void LoadComponents()
        {
            ContentPlaceHolder mpContentPlaceHolder;
            mpContentPlaceHolder =
              (ContentPlaceHolder)Master.FindControl("MainContent");

            Database obj = new Database();
            try
            {
                obj.Connect();
                obj.Query("SELECT * FROM ComponentTypes");

                if (obj.rdr.HasRows == true)
                {
                    while (obj.rdr.Read())
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        if (mpContentPlaceHolder != null)
                        {
                            mpContentPlaceHolder.FindControl("tiles").Controls.Add(li);
                        }
                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        anchor.Attributes.Add("href", "ComponentList.aspx/?Type=" + Convert.ToString(obj.rdr["CompTypeID"].ToString()));
                        anchor.InnerHtml = "<div class=\"nailthumb-container\"><img src=\"../assets/img/CompTypeThumbs/" + Convert.ToString(obj.rdr["CompTypeThumbImage"].ToString()) + "\"></div>";
                        li.Controls.Add(anchor);
                        // li.InnerHtml = "<img src=\"../assets/img/Furniture/" + Convert.ToString(obj.rdr["CompTypeThumbImage"].ToString()) + "\">";
                        li.Attributes.Add("onclick", "");
                    }
                }
            }

            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message.ToString() + " : " + ex.StackTrace.ToString(), "Load Error");
            }
            finally
            {
                obj.Close();
            }
        }


        protected void LoadImages()
        {

        }

    }
}