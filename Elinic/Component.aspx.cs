﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using Elinic.Classes;
using System.Text.RegularExpressions;
using System.Drawing;


namespace Elinic
{
    public partial class Component : System.Web.UI.Page
    {

        Elinic.Classes.Logger log = new Elinic.Classes.Logger();
        int? ideas = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
            lblDescription.Text = "";
            if (Request.QueryString["Ideas"] != null)
            {
                ideas = Convert.ToInt32(Request.QueryString["Ideas"].ToString());

            }
            orderForm.Visible = false;
            notes.Visible = true;
            btnOrder.Visible = true;
            btnConfigure.Visible = false;
            btnGoBack.Visible = false;
            if (Request.QueryString["Type"] != null)
            {
                this.Page.Title = Convert.ToString(Request.QueryString["Title"]);
                LoadComponents(null, ideas);
            }
            else if (Request.QueryString["CompId"] != null)
            {
                if (Request.QueryString["Comp"] != null)
                {
                    notes.Visible = false;
                    btnOrder.Visible = false;
                    btnConfigure.Visible = true;
                    btnGoBack.Visible = true;
                }

                orderForm.Visible = true;
                LoadRender();
                PopulateDropDowns();

            }
            else
            {
                LoadComponentTypes();
            }

        }

        /// <summary>
        /// Initial load of the screen - loads the differnet component types.
        /// </summary>
        private void LoadComponentTypes()
        {
            Database obj = new Database();
            try
            {
                obj.Connect();
                obj.Query("SELECT * FROM ComponentTypes ORDER BY Seq");

                if (obj.rdr.HasRows == true)
                {
                    while (obj.rdr.Read())
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        tiles.Controls.Add(li);
                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        anchor.Attributes.Add("href", "Component.aspx?Type=" + Convert.ToString(obj.rdr["CompTypeID"].ToString()) + "&Title=" + Convert.ToString(obj.rdr["CompTypeName"].ToString()));
                        anchor.InnerHtml = "<p>" + Convert.ToString(obj.rdr["CompTypeName"]) + "</p><img src=\"../Images/CompTypeThumbs/"
                            + Convert.ToString(obj.rdr["CompTypeThumbImage"].ToString()) + "\">";
                        li.Controls.Add(anchor);
                    }
                }
            }

            catch (Exception ex)
            {
                log.LogErrorMessage("Load Component Types : " + ex);
            }
            finally
            {
                obj.Close();
            }
        }

        /// <summary>
        /// Initial load of the screen - loads the differnet component types.
        /// </summary>
        private void LoadComponents(int? compID, int? ideas)
        {
            String queryString = "";
            Database obj = new Database();
            try
            {
                obj.Connect();
                if (compID != null)
                {
                    obj.Query("SELECT Components.Description, Components.CompID,Components.CompThumbImage,ComponentTypes.CompTypeName, ComponentTypes.CompTypeName, Components.CompID  FROM Components " +
                                "INNER JOIN ComponentTypes ON (componentTypes.CompTypeID = Components.CompType)" +
                                "WHERE CompId =" + Request.QueryString["CompId"].ToString() + " ORDER BY Components.CompID;");
                }
                else
                {
                    obj.Query("SELECT * FROM Components Where CompType = " + Request.QueryString["Type"].ToString() + " ORDER BY Components.CompID;");
                }

                if (obj.rdr.HasRows == true)
                {
                    while (obj.rdr.Read())
                    {
                        lblDescription.Text = Convert.ToString(obj.rdr["Description"].ToString());
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        if (compID != null)
                        {
                            comp.Controls.Add(li);
                            this.Page.Title = Convert.ToString(obj.rdr["CompTypeName"]) + " - " + Convert.ToString(obj.rdr["CompID"]);
                            HtmlGenericControl div = new HtmlGenericControl("div");
                            div.InnerHtml = "<p>" + Convert.ToString(obj.rdr["CompTypeName"]) + " - " + Convert.ToString(obj.rdr["CompID"]) + "</p><img src=\"../Images/CompThumbs/" + Convert.ToString(obj.rdr["CompThumbImage"].ToString()) +
                                 "\">";
                            li.Controls.Add(div);


                        }
                        else
                        {

                            queryString = "Component.aspx?CompId=" + Convert.ToString(obj.rdr["CompID"]);
                            if (Request.QueryString["Comp"] != null)
                            {
                                queryString = queryString + "&Comp=" + Request.QueryString["Comp"].ToString() + "&LayoutID=" + Request.QueryString["LayoutID"].ToString();
                            }
                            if (ideas != null)
                            {
                                queryString = queryString + "&Ideas=1";
                            }
                            tiles_small.Controls.Add(li);
                            HtmlGenericControl anchor = new HtmlGenericControl("a");
                            anchor.Attributes.Add("href", queryString);
                            anchor.InnerHtml = "<p>" + Convert.ToString(obj.rdr["CompID"].ToString())
                                + "</p><img src=\"../Images/CompThumbs/" + Convert.ToString(obj.rdr["CompThumbImage"]) + "\">";
                            li.Controls.Add(anchor);
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                log.LogErrorMessage("Load Components : " + ex);
            }
            finally
            {
                obj.Close();
            }
        }

        /// <summary>
        /// Initial load of the screen - loads the differnet component types.
        /// </summary>
        private void LoadRender()
        {
            Database obj = new Database();
            try
            {
                LoadComponents(Convert.ToInt32(Request.QueryString["CompId"].ToString()), null);
                obj.Connect();
                obj.Query("SELECT * FROM Renders Where Component = " + Request.QueryString["CompId"].ToString() + ";");

                if (obj.rdr.HasRows == true)
                {
                    int i = 1;
                    while (obj.rdr.Read())
                    {
                        //Gallery 
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        HtmlGenericControl liLarge = new HtmlGenericControl("li");
                        li.Attributes.Add("class", "item");
                        li.Attributes.Add("id", "gallery_small" + i); //used to give unique id for jquery.
                        i++;
                        liLarge.Attributes.Add("class", "item--big");
                        gallery.Controls.Add(li);
                        gallery_large.Controls.Add(liLarge);
                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        HtmlGenericControl anchorLarge = new HtmlGenericControl("a");
                        anchor.InnerHtml = "<img src=\"../Images/CompRenders/" + Convert.ToString(obj.rdr["Image"].ToString()) + "\">";
                        anchorLarge.InnerHtml = anchor.InnerHtml;
                        li.Controls.Add(anchor);
                        liLarge.Controls.Add(anchorLarge);
                    }

                }
            }

            catch (Exception ex)
            {
                log.LogErrorMessage("Load Render : " + ex);
            }
            finally
            {
                obj.Close();
            }
        }

        /// <summary>
        /// Populates the drop down size selector based on database values
        /// </summary>
        private void PopulateDropDowns()
        {
            int widthMin = 0;
            int widthMax = 0;
            int depthMin = 0;
            int depthMax = 0;
            int heightMin = 0;
            int heightMax = 0;
            int index = 0;

            //Clear Values
            compWidth.Items.Clear();
            compDepth.Items.Clear();
            compHeight.Items.Clear();
            compMaterial.Items.Clear();
            compDoors.Items.Clear();


            Database db = new Database();
            try
            {
                db.Connect();
                db.Query("SELECT * FROM Components Where CompID = " + Request.QueryString["CompId"].ToString() + ";");

                if (db.rdr.HasRows == true)
                {
                    while (db.rdr.Read())
                    {

                        widthMin = (Convert.ToString(db.rdr["Wmin"])) != null ? Convert.ToInt32(Convert.ToString(db.rdr["Wmin"])) : 0;
                        widthMax = (Convert.ToString(db.rdr["Wmax"])) != null ? Convert.ToInt32(Convert.ToString(db.rdr["Wmax"])) : 0;
                        heightMin = (Convert.ToString(db.rdr["Hmin"])) != null ? Convert.ToInt32(Convert.ToString(db.rdr["Hmin"])) : 0;
                        heightMax = (Convert.ToString(db.rdr["Hmax"])) != null ? Convert.ToInt32(Convert.ToString(db.rdr["Hmax"])) : 0;
                        depthMin = !string.IsNullOrEmpty((Convert.ToString(db.rdr["Dmin"]))) ? Convert.ToInt32(Convert.ToString(db.rdr["Dmin"])) : 0;
                        depthMax = !string.IsNullOrEmpty((Convert.ToString(db.rdr["Dmax"]))) ? Convert.ToInt32(Convert.ToString(db.rdr["Dmax"])) : 0;
                        numDoors.Text = (Convert.ToString(db.rdr["Ndoors"]));
                        numShelves.Text = (Convert.ToString(db.rdr["Nshelves"]));
                        numDrawers.Text = (Convert.ToString(db.rdr["Ndrawers"]));
                        numHandles.Text = (Convert.ToString(db.rdr["Nhandles"]));
                        faceDoorCoverage.Text = (Convert.ToString(db.rdr["FaceDoorCoverage"]));
                    }

                    for (int i = widthMin; i <= widthMax; i++)
                    {
                        compWidth.Items.Insert(index, new ListItem(Convert.ToString(i) + " \"", Convert.ToString(i) + " \""));
                        index++;

                    }
                    index = 0;
                    for (int i = depthMin; i <= depthMax; i++)
                    {
                        compDepth.Items.Insert(index, new ListItem(Convert.ToString(i) + " \"", Convert.ToString(i) + " \""));
                        index++;
                    }
                    index = 0;
                    for (int i = heightMin; i <= heightMax; i++)
                    {
                        compHeight.Items.Insert(index, new ListItem(Convert.ToString(i) + " \"", Convert.ToString(i) + " \""));
                        index++;
                    }
                    //Doors
                    if (numDoors.Text == "0" || numDoors.Text =="")
                    {

                        divDoors.Visible = false;
                    }
                    else
                    {
                        divDoors.Visible = true;
                        compDoors.Items.Insert(0, new ListItem("Left", "Left"));
                        compDoors.Items.Insert(1, new ListItem("Right", "Right"));
                    }
                    //Material
                    compMaterial.Items.Insert(0, new ListItem("Veneer Maple", "Veneer Maple"));
                    compMaterial.Items.Insert(1, new ListItem("Veneer Oak", "Veneer Oak"));
                    compMaterial.Items.Insert(2, new ListItem("Veneer Cherry", "Veneer Cherry"));
                    compMaterial.Items.Insert(3, new ListItem("Painted", "Painted"));



                }
            }

            catch (Exception ex)
            {
                log.LogErrorMessage("Populate Drop Downs : " + ex);
            }
            finally
            {
                db.Close();
            }

        }


        protected void btnConfigure_Click(object sender, EventArgs e)
        {
            try
            {
                string doors = compDoors.SelectedIndex == -1 ? "N/A" : compDoors.SelectedItem.Text;  

                Session[Request.QueryString["Comp"].ToString() + "Price"] = compPrice.Value.Substring(1, compPrice.Value.Length - 1);
                Session[Request.QueryString["Comp"].ToString() + "OrderSummary"] = "<b>Component ID:</b> " + Request.QueryString["CompId"].ToString()
                    + " </br><b>W:</b> " + compWidth.SelectedItem.Text + " </br><b>D:</b> " + compDepth.SelectedItem.Text + " </br><b>H:</b> " + compHeight.SelectedItem.Text
                    + " </br><b>Doors:</b> " + doors + " </br><b>Material:</b> " + compMaterial.SelectedItem.Text + " </br><b>Price:</b>" + compPrice.Value + "<hr/>";
                Session[Request.QueryString["Comp"].ToString()] = "&nbsp; <b>Component ID:</b> " + Request.QueryString["CompId"].ToString()
                    + " &nbsp;   <b>W:</b> " + compWidth.SelectedItem.Text + "   <b>D:</b> " + compDepth.SelectedItem.Text + "   <b>H:</b> " + compHeight.SelectedItem.Text
                    + "  <b>Doors:</b> " + doors + "  <b>Material:</b> " + compMaterial.SelectedItem.Text + "  <b>Price:</b>" + compPrice.Value;
                if (ideas != null)
                {
                    Response.Redirect("~/Project.aspx?LayoutID=" + (Request.QueryString["LayoutID"]) + "&Ideas=1", false);
                }
                else
                {
                    Response.Redirect("~/Project.aspx?LayoutID=" + (Request.QueryString["LayoutID"]), false);
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMessage("Error Getting Configuration Values " + ex);

            }
        }

        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (ideas != null)
                {
                    Response.Redirect("~/Project.aspx?LayoutID=" + (Request.QueryString["LayoutID"]) + "&Ideas=1", false);
                }
                else
                {
                    Response.Redirect("~/Project.aspx?LayoutID=" + (Request.QueryString["LayoutID"]), false);
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMessage("Error Getting Configuration Values " + ex);

            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("http://www.elinic.com/CustomizeBy.htm");
        }

        /// <summary>
        /// Populates the database with the order details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSend_Click(object sender, EventArgs e)
        {
            string doors = compDoors.SelectedIndex == -1 ? "N/A" : compDoors.SelectedItem.Text; 

            string orderDetails = "Component ID: " + Request.QueryString["CompId"].ToString()
                    + " W:" + compWidth.SelectedItem.Text + " D:" + compDepth.SelectedItem.Text + " H: " + compHeight.SelectedItem.Text
                    + " Doors:" + doors + " Material:" + compMaterial.SelectedItem.Text;
           
            string price = compPrice.Value;
            string notes = orderNotes.InnerText;

            Database obj = new Database();
            try
            {
                obj.Connect();

                obj.Insert("INSERT INTO Orders (OrderDetails, TotalPrice, Notes) VALUES('" + orderDetails + "','" + price + "','" + notes + "');");
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