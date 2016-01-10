using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elinic.Classes;
using System.Drawing;

namespace Elinic
{
    public partial class SubmitOrder : Page
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
                }
            }

            if (Session["OrderDetails"] != null)
            {
                odr = (OrderDetails)Session["OrderDetails"];
            }
        }

        /// <summary>
        /// Populates the database with the order details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            //string doors = compDoors.SelectedIndex == -1 ? "N/A" : compDoors.SelectedItem.Text;

            //string orderDetails = "Component ID: " + Request.QueryString["CompId"].ToString()
            //        + " W:" + compWidth.SelectedItem.Text + " D:" + compDepth.SelectedItem.Text + " H: " + compHeight.SelectedItem.Text
            //        + " Doors:" + doors + " Material:" + compMaterial.SelectedItem.Text;

            //string price = compPrice.Value;
            string notes = orderNotes.InnerText;

            Database obj = new Database();
            try
            {
                obj.Connect();

                obj.Insert("INSERT INTO Orders (OrderDetails, TotalPrice, Notes, OrderDate) VALUES('" + odr.Details + "','" + odr.Price + "','" + notes + "', '" + DateTime.Now + "');");
                obj.Close();
            }
            catch (Exception ex)
            {
                showMessage("Error Creating Order.", false);
                log.LogErrorMessage("Error creating order: " + ex);
            }
            finally
            {
                obj.Close();
            }
            showMessage("Order Successful", true);

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

        /// <summary>
        /// Status message for events.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="success"></param>
        private void showMessage(string message, bool success)
        {
            lblMsg.Visible = true; // here lblMsg is asp label control on your aspx page.
            if (success)
                lblMsg.ForeColor = Color.Green;
            else
                lblMsg.ForeColor = Color.Red;
            lblMsg.Text = message;
        }
    }
}