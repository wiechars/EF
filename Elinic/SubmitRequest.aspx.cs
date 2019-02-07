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
    public partial class SubmitRequest : Page
    {
        private OrderDetails odr = new OrderDetails();
        Elinic.Classes.Logger log = new Elinic.Classes.Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                    if (Request.UrlReferrer.ToString().Contains("Component")){
                        btnOrder.Text = "Submit Component For Review";
                    }
                    else {
                        btnOrder.Text = "Submit Set For Review";
                    }
                    
                }
            }

            if (Session["OrderDetails"] != null)
            {
                odr = (OrderDetails)Session["OrderDetails"];
                populateConfiguredOrder();
            }
        }

        /// <summary>
        /// Populates the database with the order details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string notes = orderNotes.InnerText;           
            string result = "";
            string orderId = "";
            Database obj = new Database();
            try
            {
                obj.Connect();

                obj.Insert("INSERT INTO Orders (OrderDetails, TotalPrice, Notes, OrderDate, Email) VALUES('" + odr.Details + "','" + odr.Price + "','" + notes.Replace("'", "''") + "', '" + DateTime.Now + "','"+txtEmail.Text+"');");

                obj.Query("SELECT ID FROM Orders ORDER BY ID ASC");

                if (obj.rdr.HasRows == true)
                {
                    
                        while (obj.rdr.Read())
                        {
                            orderId = (Convert.ToString(obj.rdr["ID"]));
                        }
                }

                    obj.Close();
                result = "true";
            }
            catch (Exception ex)
            {
                result = "false";
                log.LogErrorMessage("Error creating order: " + ex);
            }
            finally
            {
                obj.Close();
            }


           

            Response.Redirect("~/OrderComplete.aspx?OrderId="+orderId, false);


        }

        /// <summary>
        /// Goes Back to previous URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            object refUrl = ViewState["RefUrl"];
            if (refUrl != null)
                Response.Redirect((string)refUrl);

        }

        private void populateConfiguredOrder()
        {
            if (odr.Details != null){
                txtAreaComponents.InnerText = odr.Details.ToString();
                txtPrice.Text = odr.Price;
            }
            else {
                txtAreaComponents.InnerText = "No Configured Components Found!";
            }
        }
    }
}