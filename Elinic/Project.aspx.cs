using System;
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
    public partial class Project : System.Web.UI.Page
    {
        Elinic.Classes.Logger log = new Elinic.Classes.Logger();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Initialize specific Divs.
            layoutsDiv.Visible = false;
            layoutsDivContent.Visible = true;
            ideasDiv.Visible = false;
            lblMsg.Visible = false;
            lblDescription.Text = "";

            selectedComponent.Visible = false;
            notes.Visible = false;
            if (Request.QueryString["Type"] != null)
            {
                Session.Clear();
                this.Page.Title = Convert.ToString(Request.QueryString["Title"]);

                LoadProjectLayouts(null, null);

            }
            else if (Request.QueryString["LayoutID"] != null)
            {
                values.InnerHtml = "";
                orderValues.InnerHtml = "";
                int? ideas = null;

                if (Request.QueryString["Ideas"] != null)
                {
                    ideas = Convert.ToInt32(Request.QueryString["Ideas"].ToString());
                }
                selectedComponent.Visible = true;
                notes.Visible = true;
                LoadProjectLayouts(Convert.ToInt32(Request.QueryString["LayoutID"].ToString()), ideas);
                LoadComponents(ideas);
                LoadLayout(ideas);

            }
            else
            {
                Session.Clear();
                LoadProjectTypes();
            }

        }

        /// <summary>
        /// Initial load of the screen - loads the differnet component types.
        /// </summary>
        private void LoadProjectTypes()
        {
            Database obj = new Database();
            try
            {
                obj.Connect();
                obj.Query("SELECT * FROM Projects ORDER BY seq");

                if (obj.rdr.HasRows == true)
                {
                    while (obj.rdr.Read())
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        tiles.Controls.Add(li);
                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        anchor.Attributes.Add("href", "Project.aspx?Type=" + Convert.ToString(obj.rdr["ProjectType"].ToString()) + "&Title=" + Convert.ToString(obj.rdr["ProjectName"].ToString()));
                        anchor.InnerHtml = "<p>" + Convert.ToString(obj.rdr["ProjectName"].ToString()) + "</p><img src=\"../Images/ProjectTypeThumbs/"
                            + Convert.ToString(obj.rdr["ProjectThumbImage"].ToString()) + "\">";
                        li.Controls.Add(anchor);
                    }
                }
            }

            catch (Exception ex)
            {

                log.LogErrorMessage("Load Project Types : " + ex);

            }
            finally
            {
                obj.Close();
            }
        }

        /// <summary>
        /// Initial load of the screen - loads the differnet component types.
        /// </summary>
        private void LoadProjectLayouts(int? layoutID, int? ideas)
        {
            bool hasDescription = false;
            Database obj = new Database();
            try
            {
                obj.Connect();
                if (layoutID != null && ideas == null)
                {
                    obj.Query("SELECT * FROM Layouts " +
                      "INNER JOIN Projects ON (Projects.ProjectType = Layouts.ProjectType) Where LayoutID = " + layoutID + " ORDER BY Layouts.LayoutID;");
                }
                else if (layoutID != null && ideas != null)
                {
                    obj.Query("SELECT * FROM Ideas " +
                      "INNER JOIN Projects ON (Projects.ProjectType = Ideas.ProjectType) Where IdeaID = " + layoutID + " ORDER BY Ideas.IdeaID;");
                }
                else
                {
                    obj.Query("SELECT * FROM Layouts" + // Projects.Description FROM Layouts " +
                            " INNER JOIN Projects ON (Projects.ProjectType = Layouts.ProjectType)  " +
                            " Where Layouts.ProjectType = " + Request.QueryString["Type"].ToString() + " ORDER BY LayoutID;");
                    hasDescription = true;
                }

                if (obj.rdr.HasRows == true)
                {
                    while (obj.rdr.Read())
                    {
                        if (hasDescription)
                        {
                            lblDescription.Text = Convert.ToString(obj.rdr["Description"].ToString());
                        }
                        if (layoutID != null && ideas == null)
                        {
                            HtmlGenericControl li = new HtmlGenericControl("li");
                            layout.Controls.Add(li);
                            HtmlGenericControl div = new HtmlGenericControl("div");
                            div.InnerHtml = "<p>" + Convert.ToString(obj.rdr["ProjectName"].ToString()) + " - " + Convert.ToString(obj.rdr["LayoutID"].ToString()) + "</p><img src=\"../Images/LayoutThumbs/" +
                                Convert.ToString(obj.rdr["LayoutThumbImage"].ToString()) + "\">";
                            li.Controls.Add(div);
                            this.Page.Title = Convert.ToString(obj.rdr["ProjectName"].ToString()) + " - " + Convert.ToString(obj.rdr["LayoutID"].ToString());

                        }
                        else if (layoutID != null && ideas != null)
                        {
                            HtmlGenericControl li = new HtmlGenericControl("li");
                            layout.Controls.Add(li);
                            HtmlGenericControl div = new HtmlGenericControl("div");
                            div.InnerHtml = "<p>" + Convert.ToString(obj.rdr["ProjectName"].ToString()) + " - " + Convert.ToString(obj.rdr["IdeaID"].ToString()) + "</p><img src=\"../Images/LayoutThumbs/" +
                                Convert.ToString(obj.rdr["IdeaThumbImage"].ToString()) + "\">";
                            li.Controls.Add(div);
                            this.Page.Title = Convert.ToString(obj.rdr["ProjectName"].ToString()) + " - " + Convert.ToString(obj.rdr["IdeaID"].ToString());

                        }
                        else
                        {
                            layoutsDiv.Visible = true;
                            HtmlGenericControl li = new HtmlGenericControl("li");
                            tiles_small.Controls.Add(li);
                            HtmlGenericControl anchor = new HtmlGenericControl("a");
                            anchor.Attributes.Add("href", "Project.aspx?LayoutID=" + Convert.ToString(obj.rdr["LayoutID"].ToString()));
                            anchor.InnerHtml = "<p>" + Convert.ToString(obj.rdr["LayoutID"].ToString()) + "</p><img src=\"../Images/LayoutThumbs/"
                                + Convert.ToString(obj.rdr["LayoutThumbImage"].ToString()) + "\">";
                            li.Controls.Add(anchor);
                        }
                    }

                }
                else
                {
                    //No Layouts - Hide div
                    layoutsDiv.Visible = false;
                    layoutsDivContent.Visible = false;
                }
                //Check to see if there are Ideas to Load
                if (layoutID == null)
                {
                    LoadProjectIdeas(obj);
                }
            }

            catch (Exception ex)
            {
                log.LogErrorMessage("Load Project Layouts : " + ex);
            }
            finally
            {
                obj.Close();
            }
        }

        /// <summary>
        /// Initial load of the screen - loads the differnet component types.
        /// </summary>
        private void LoadProjectIdeas(Database obj)
        {

            try
            {
                obj.Query("SELECT * FROM Ideas INNER JOIN Projects ON Projects.ProjectType = Ideas.ProjectType Where Ideas.ProjectType = " + Request.QueryString["Type"].ToString() + " ORDER BY IdeaID;");

                if (obj.rdr.HasRows == true)
                {
                    ideasDiv.Visible = true;
                    while (obj.rdr.Read())
                    {
                        lblDescription.Text = Convert.ToString(obj.rdr["Description"].ToString());

                        HtmlGenericControl li = new HtmlGenericControl("li");
                        tiles_ideas.Controls.Add(li);

                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        anchor.Attributes.Add("href", "Project.aspx?LayoutID=" + Convert.ToString(obj.rdr["IdeaID"].ToString()) + "&Ideas=1");
                        anchor.InnerHtml = "<p>" + Convert.ToString(obj.rdr["IdeaID"].ToString()) + "</p><img src=\"../Images/LayoutThumbs/"
                            + Convert.ToString(obj.rdr["IdeaThumbImage"].ToString()) + "\">";
                        li.Controls.Add(anchor);
                    }
                }
            }

            catch (Exception ex)
            {
                log.LogErrorMessage("Load Project Layouts : " + ex);
            }
        }

        /// <summary>
        /// Initial load of the screen - loads the differnet component types.
        /// </summary>
        private void LoadLayout(int? ideas)
        {
            Database obj = new Database();

            try
            {
                if (ideas == null)
                {
                    obj.Connect();
                    obj.Query("SELECT Renders.Image,Layouts.LayoutThumbImage FROM Renders " +
                                "INNER JOIN Layouts ON (Layouts.LayoutID = Renders.Layout) " +
                                "WHERE Layout = " + Request.QueryString["LayoutID"].ToString() + ";");
                }
                else
                {
                    obj.Connect();
                    obj.Query("SELECT Renders.Image,Ideas.IdeaThumbImage FROM Renders " +
                                "INNER JOIN Ideas ON (Ideas.IdeaID = Renders.Layout) " +
                                "WHERE Layout = " + Request.QueryString["LayoutID"].ToString() + ";");
                }

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
                        anchor.InnerHtml = "<img src=\"../Images/LayoutRenders/" + Convert.ToString(obj.rdr["Image"].ToString()) + "\">";
                        anchorLarge.InnerHtml = anchor.InnerHtml;
                        li.Controls.Add(anchor);
                        liLarge.Controls.Add(anchorLarge);

                    }
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMessage("Load Layout : " + ex);
            }
            finally
            {
                obj.Close();
            }
        }

        private void LoadIdeas()
        {
            Database obj = new Database();

            try
            {
                obj.Connect();
                obj.Query("SELECT Renders.Image,Layouts.LayoutThumbImage FROM Renders " +
                            "INNER JOIN Layouts ON (Layouts.LayoutID = Renders.Layout) " +
                            "WHERE Layout = " + Request.QueryString["LayoutID"].ToString() + ";");

                if (obj.rdr.HasRows == true)
                {

                    while (obj.rdr.Read())
                    {
                        //Gallery 
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        HtmlGenericControl liLarge = new HtmlGenericControl("li");
                        li.Attributes.Add("class", "item");
                        liLarge.Attributes.Add("class", "item--big");
                        gallery.Controls.Add(li);
                        gallery_large.Controls.Add(liLarge);
                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        HtmlGenericControl anchorLarge = new HtmlGenericControl("a");
                        anchor.InnerHtml = "<img src=\"../Images/LayoutRenders/" + Convert.ToString(obj.rdr["Image"].ToString()) + "\">";
                        anchorLarge.InnerHtml = anchor.InnerHtml;
                        li.Controls.Add(anchor);
                        liLarge.Controls.Add(anchorLarge);

                    }
                }
            }
            catch (Exception ex)
            {
                log.LogErrorMessage("Load Layout : " + ex);
            }
            finally
            {
                obj.Close();
            }
        }

        /// <summary>
        /// Initial load of the screen - loads the differnet component types.
        /// </summary>
        private void LoadComponents(int? ideas)
        {
            double price = 0;
            int i = 1;
            Database obj = new Database();
            try
            {
                obj.Connect();
                obj.Query("SELECT ComponentTypes.CompTypeName, ComponentTypes.CompTypeID, ComponentTypes.CompTypeThumbImage FROM Comp_Layout" +
                                " INNER JOIN ComponentTypes ON (ComponentTypes.CompTypeID = Comp_Layout.ComponentType) " +
                                " Where LayoutID = " + Request.QueryString["LayoutID"].ToString() + " ORDER BY SeqInDrawing;");

                if (obj.rdr.HasRows == true)
                {
                    while (obj.rdr.Read())
                    {
                        //Add Place Holders for additional (up to 5)
                        //This was a late requirement to add components
                        for (int counter = 1 + (5 * (i - 1)); counter < 6 * i; counter++)
                        {
                            HtmlGenericControl li = new HtmlGenericControl("li");
                            comp_small.Controls.Add(li);
                            li.Attributes.Add("id", "liAddComponent" + counter);
                            //Hide li Place Holders
                            if (counter != 1 + (5 * (i - 1)) && Session["Comp" + counter] == null)
                            {
                                li.Style.Add("display", "none");
                            }

                            if (Session["Comp" + counter] != null)
                            {
                                values.InnerHtml = values.InnerHtml + "<div class=\"customized-values\"  id=\"Comp" + counter + "\"><b>Component Type:</b> " + Convert.ToString(obj.rdr["CompTypeID"].ToString());
                                price = price + Convert.ToDouble(Session["Comp" + counter + "Price"].ToString());
                                values.InnerHtml = values.InnerHtml + Session["Comp" + counter].ToString() + "</div>";


                            }
                            else
                            {
                                //Logic to hide additional place holders
                                if (counter != 1 + (5 * (i - 1)))
                                {
                                    values.InnerHtml = values.InnerHtml + "<div class=\"customized-values\" style=\"display:none;\" id=\"Comp" + counter + "\"><b>Component Type:</b> " + Convert.ToString(obj.rdr["CompTypeID"].ToString());

                                }
                                else
                                {
                                    values.InnerHtml = values.InnerHtml + "<div class=\"customized-values\"  id=\"Comp" + counter + "\"><b>Component Type:</b> " + Convert.ToString(obj.rdr["CompTypeID"].ToString());

                                }
                                values.InnerHtml = values.InnerHtml + "&nbsp; <b>Component ID :</b> n/a &nbsp; <b>W:</b> n/a <b>D:</b> n/a <b>H:</b> n/a <b>Doors:</b> n/a <b>Material:</b> n/a <b>Price:</b> n/a</div>";
                                //values.InnerHtml.Style.Add("display", "none");
                            }
                            HtmlGenericControl anchor = new HtmlGenericControl("a");
                            if (ideas != null)
                            {
                                anchor.Attributes.Add("href", "Component.aspx?Type=" + Convert.ToString(obj.rdr["CompTypeID"]) + "&Comp=Comp" + counter + "&LayoutID=" + Request.QueryString["LayoutID"].ToString() + "&Title=" + Convert.ToString(obj.rdr["CompTypeName"]) + "&Ideas=1");
                            }
                            else
                            {
                                anchor.Attributes.Add("href", "Component.aspx?Type=" + Convert.ToString(obj.rdr["CompTypeID"]) + "&Comp=Comp" + counter + "&LayoutID=" + Request.QueryString["LayoutID"].ToString() + "&Title=" + Convert.ToString(obj.rdr["CompTypeName"]));
                            }
                            anchor.InnerHtml = "<p>" + Convert.ToString(obj.rdr["CompTypeName"].ToString()) + "</p><img src=\"../Images/CompTypeThumbs/"
                                + Convert.ToString(obj.rdr["CompTypeThumbImage"].ToString()) + "\"></a><label id=AddComponent" + counter + ">+</label>";
                            li.Controls.Add(anchor);


                        }
                        i++;
                    }
                    lblTotalPrice.Text = "$" + price.ToString();
                    lblOrderPrice.Text = "$" + price.ToString();
                    orderValues.InnerHtml = values.InnerHtml;


                }
            }
            catch (Exception ex)
            {
                log.LogErrorMessage("Load Component : " + ex);
            }
            finally
            {
                obj.Close();
            }
        }

        /// <summary>
        /// Navigates to the Home Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            int i = 1;
            string orderHTML = "";
            while (i > 0)
            {
                if (Session["Comp" + i] != null)
                {
                    orderHTML = orderHTML + "ITEM #" + i + ": " + Session["Comp" + i].ToString() + " ";
                    i++;
                }
                else
                {
                    i = -1;
                }
            }

            string price = lblOrderPrice.Text;
            string notes = orderNotes.InnerText;
            string orderNoHTML = Regex.Replace(orderHTML, @"<[^>]+>|&nbsp;", "").Trim();
            Database obj = new Database();
            try
            {
                obj.Connect();

                obj.Insert("INSERT INTO Orders (OrderDetails, TotalPrice, Notes) VALUES('" + orderNoHTML + "','" + price + "','" + notes + "');");
                obj.Close();
            }
            catch (Exception ex)
            {
                showMessage("Error Creating Order.", false);
                log.LogErrorMessage("Error creating order: " + ex);
            }
            showMessage("Order Successful", true);
        }

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