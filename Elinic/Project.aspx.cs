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
using System.Web.Script.Serialization;

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
            lblLayoutDescription.Text = "";
            btnBackToProjects.Visible = false;
            selectedComponent.Visible = false;
            notes.Visible = false;
            if (Request.QueryString["Request"] != null)
            {
                if (Request.QueryString["Request"] == "true")
                {
                    showMessage("Request successfully submitted!", true);
                }
                else
                {
                    showMessage("Error submitting request! Please call.", false);
                }
            }
            if (Request.QueryString["Type"] != null)
            {
                Session["Type"] = Convert.ToString(Request.QueryString["Type"]);
                Session["Title"] = Convert.ToString(Request.QueryString["Title"]);
                this.Page.Title = Convert.ToString(Request.QueryString["Title"]);
                btnBackToProjects.Visible = true;
                LoadProjectLayouts(null, null);
                index.InnerHtml = "<a href=\"/\" class=\"text-info\"> Home </a> > <a href=\"/Project.aspx\" class=\"text-info\">Projects</a> > " + Convert.ToString(Request.QueryString["Title"]);

            }
            else if (Request.QueryString["LayoutID"] != null)
            {

              //  orderValues.InnerHtml = "";
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
                CustomizeMaterial.HRef = getMaterialsLink();
                loadSessionMaterials();
            }
            else
            {
                Session.Clear();
                LoadProjectTypes();
            }

        }
        private string getMaterialsLink()
        {
            String link = "Material.aspx?";
            if (Request.QueryString["LayoutID"] != null)
            {
                link += "LayoutID=" + Request.QueryString["LayoutID"] + "&";
            }
            if (Request.QueryString["Ideas"] != null)
            {
                link += "Ideas=" + Request.QueryString["Ideas"] + "&";
            }
            return link;
        }
        private void loadSessionMaterials()
        {
            if (Session["material"] == null || Session["materialStain"] == null || Session["materialFinish"] == null || Session["handleIndex"] == null) return;

            MaterialsContainer.InnerHtml = "<h5 class='my-1'>Wood: <span class='text-muted'>" + Session["material"].ToString() + "</span></h5>" +
                    "<h5 class='my-1'>Stain: <span class='text-muted'>" + Session["materialStain"].ToString() + "</span></h5>" +
                    "<h5 class='my-1'>Finish: <span class='text-muted'>" + Session["materialFinish"].ToString() + "</span></h5>" +
                    "<h5 class='my-1'>Handle: <span class='text-muted'>" + Session["handleIndex"].ToString() + "</span></h5>"; 
        }
        private string GetHelpText()
        {
            string helpText = "";
            Database obj = new Database();
            try
            {
                obj.Connect();
                obj.Query("SELECT * FROM Helptext");

                if (obj.rdr.HasRows == true)
                {
                    while (obj.rdr.Read())
                    {
                        helpText = Convert.ToString(obj.rdr["TopicText"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {

                log.LogErrorMessage("Load Help Text : " + ex);

            }
            finally
            {
                obj.Close();
            }

            return helpText;
        }
        private void addCard(HtmlGenericControl container, String title, String imgSrc, String buttonText, String link)
        {
            HtmlGenericControl cardDiv = new HtmlGenericControl("div"),
                                            card = new HtmlGenericControl("div"),
                                            cardImg = new HtmlGenericControl("img"),
                                            cardBody = new HtmlGenericControl("div"),
                                            cardTitle = new HtmlGenericControl("h4"),
                                            cardButton = new HtmlGenericControl("a");

            cardDiv.Attributes["class"] = "col-md-6 col-sm-12 col-lg-4 col-xl-3 mb-4";
            card.Attributes["class"] = "card m-1 m-b-1 h-100";
            cardImg.Attributes["class"] = "card-img-top";
            cardBody.Attributes["class"] = "card-body d-flex flex-column";
            cardTitle.Attributes["class"] = "card-title font-weight-bold mt-auto border-top pt-3 text-center";
            cardButton.Attributes["class"] = "btn btn-primary btn-lg btn-block mt-3";


            cardButton.InnerHtml = buttonText;

            cardButton.Attributes.Add("href", link);
            cardTitle.InnerHtml = title;
            cardImg.Attributes.Add("src", imgSrc);
            cardImg.Style["max-height"] = "300px";
            card.Controls.Add(cardImg);
            cardBody.Controls.Add(cardTitle);
            cardBody.Controls.Add(cardButton);
            card.Controls.Add(cardBody);
            cardDiv.Controls.Add(card);
            container.Controls.Add(cardDiv);
        }
        /// <summary>
        /// Initial load of the screen - loads the differnet component types.
        /// </summary>
        private void LoadProjectTypes()
        {
            index.InnerHtml = "<a href=\"/\" class=\"text-info\"> Home </a> > Projects";
            Database obj = new Database();
            try
            {
                obj.Connect();
                obj.Query("SELECT * FROM Projects WHERE Active = 1 ORDER BY seq");

                if (obj.rdr.HasRows == true)
                {
                    while (obj.rdr.Read())
                    {
                        String link = "";

                        if (Convert.ToString(obj.rdr["ProjectType"].ToString()) == "12")
                        {  //Hardcode link for walking closets
                            link = "ClosetShape.htm";
                        }
                        else
                        {
                            link = "Project.aspx?Type=" + Convert.ToString(obj.rdr["ProjectType"].ToString()) + "&Title=" + Convert.ToString(obj.rdr["ProjectName"].ToString());
                        }

                        String title = obj.rdr["ProjectName"].ToString();
                        String imageSrc = "../Images/ProjectTypeThumbs/"
                            + Convert.ToString(obj.rdr["ProjectThumbImage"].ToString());
                        String description = obj.rdr["Description"].ToString();
                        addCard(tiles, title, imageSrc, "Select", link);
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
                        else
                        {
  lblLayoutDescription.Text = Convert.ToString(obj.rdr["Description2"].ToString());

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
                            index.InnerHtml = "<a href=\"/\" class=\"text-info\"> Home </a> > <a href=\"/Project.aspx\" class=\"text-info\">Projects</a> > " + this.Page.Title;

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
                            index.InnerHtml = "<a href=\"/\" class=\"text-info\"> Home </a> > <a href=\"/Project.aspx\" class=\"text-info\">Projects</a> > " + this.Page.Title;

                        }
                        else
                        {
                            layoutsDiv.Visible = true;
                            String title = Convert.ToString(obj.rdr["LayoutID"].ToString()),
                                    link = "Project.aspx?LayoutID=" + Convert.ToString(obj.rdr["LayoutID"].ToString()),
                                    imageSrc = "../Images/LayoutThumbs/" + Convert.ToString(obj.rdr["LayoutThumbImage"].ToString());

                            addCard(tiles_small, title, imageSrc, "Select", link);
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
        protected void btnGoBack_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Project.aspx?Type=" + Session["Type"] + "&Title=" + Session["Title"]);
            }
            catch (Exception ex)
            {
                //log.LogErrorMessage("Error Getting Configuration Values " + ex);

            }
        }

        protected void btnBackToProjects_click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/Project.aspx");
            }
            catch (Exception ex)
            {
                //log.LogErrorMessage("Error Getting Configuration Values " + ex);

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
                        String title = Convert.ToString(obj.rdr["IdeaID"].ToString()),
                                imageSrc = "../Images/LayoutThumbs/"
                            + Convert.ToString(obj.rdr["IdeaThumbImage"].ToString()),
                                link = "Project.aspx?LayoutID=" + Convert.ToString(obj.rdr["IdeaID"].ToString()) + "&Ideas=1";
                        addCard(tiles_ideas, title, imageSrc, "Select", link);
                        /**
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        tiles_ideas.Controls.Add(li);

                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        anchor.Attributes.Add("href", "Project.aspx?LayoutID=" + Convert.ToString(obj.rdr["IdeaID"].ToString()) + "&Ideas=1");
                        anchor.InnerHtml = "<p>" + Convert.ToString(obj.rdr["IdeaID"].ToString()) + "</p><img src=\"../Images/LayoutThumbs/"
                            + Convert.ToString(obj.rdr["IdeaThumbImage"].ToString()) + "\">";
                        li.Controls.Add(anchor);
    **/
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
                        for (int counter = 1 + (5 * (i - 1)); counter <= 5 * i; counter++)
                        {
                            string detailsDiv = "";
                            string buttonDiv = "";
                            string anchorLink = "";
                            HtmlGenericControl li = new HtmlGenericControl("li");
                            comp_small.Controls.AddAt(0,li); 
                            li.Attributes.Add("id", "liAddComponent" + counter);
                            li.Attributes.Add("style", "width:100%; height:auto!important;");
                            //Hide li Place Holders
                            if (counter != 1 + (5 * (i - 1)) && Session["Comp" + counter] == null)
                            {
                                li.Style.Add("display", "none");
                            }
                            string link = "";
                            if (ideas != null)
                            {
                                link = "Component.aspx?Type=" + Convert.ToString(obj.rdr["CompTypeID"]) + "&Comp=Comp" + counter + "&LayoutID=" + Request.QueryString["LayoutID"].ToString() + "&Title=" + Convert.ToString(obj.rdr["CompTypeName"]) + "&Ideas=1";
                            }
                            else
                            {
                                link = "Component.aspx?Type=" + Convert.ToString(obj.rdr["CompTypeID"]) + "&Comp=Comp" + counter + "&LayoutID=" + Request.QueryString["LayoutID"].ToString() + "&Title=" + Convert.ToString(obj.rdr["CompTypeName"]);
                            }

                            if (Session["Comp" + counter] != null)
                            {
                                detailsDiv = "<div class=\"customized-values configured\"  id=\"Comp"
                                        + counter
                                        + "\"><a style=\"text-align:left; line-height:1.6em\" href=\"" + link + "\"\\><b>Component Type:</b> "
                                        + Convert.ToString(obj.rdr["CompTypeID"].ToString());
                                price = price + Convert.ToDouble(Session["Comp" + counter + "Price"].ToString());
                                detailsDiv = detailsDiv + Session["Comp" + counter].ToString().Replace("&nbsp;", "<br/>") + "</div>";


                            }
                            else
                            {
                                    detailsDiv = "<div class=\"customized-values\"  id=\"Comp"
                                        + counter + "\">Nothing configured yet.<a style=\"text-align:left; line-height:1.6em\" href=\"" + link + "\"\\></div>";
                            }
                            HtmlGenericControl addParentDiv = new HtmlGenericControl("div");
                            if (Session["Comp" + counter] != null)
                            {
                                var compId = Session["Comp" + counter + "CompSelectedId"] != null ? Session["Comp" + counter + "CompSelectedId"].ToString() : "";
                                anchorLink = "<a href=\""+link+"\"><p style=\"padding-top:2px;\">" + Convert.ToString(obj.rdr["CompTypeName"].ToString()) + " - " + compId + "</p><img src=\"" + Session["Comp" + counter + "CompImagePath"].ToString() + "\"></a>";
                                Session["ComponentImagePath"] = "";
                            }
                            else
                            {
                                anchorLink = "<a href=\"" + link + "\"><p  style=\"padding-top:2px;\">" + Convert.ToString(obj.rdr["CompTypeName"].ToString()) + "</p><img src=\"../Images/CompTypeThumbs/"
                           + Convert.ToString(obj.rdr["CompTypeThumbImage"].ToString()) + "\"></a>";
                            }

                            if (counter != 1 + (5 * (i - 1)))
                            {


                                buttonDiv =
                                              "<Button class=\"btn btn-primary\" style=\"width:100%\" id =configure" + counter + "  onclick=\"window.location.href = '" + link + "';return false \"><i class=\"fa fa-gear\"></i>&nbsp;Config</Button>" +
                                                "<Button class=\"btn btn-warning\" style=\"width:100%\" id =AddComponent" + counter + " onclick=\"return false;\"><i class=\"fa fa-plus\"></i> &nbsp;Add</Button>" +
                                                "<Button class=\"btn btn-danger\" style=\"width:100%\" id =RemoveComponent" + counter + " onclick=\"return false;\"><i class=\"fa fa-times\"></i>&nbsp;Remove</Button>" +
                                                "<Button class=\"btn btn-info\" style=\"width:100%\" id =RedoComponent" + counter + " onclick=\"return false;\"><i class=\"fa fa-refresh\"></i>&nbsp;Clear</Button>";
                            }
                            else
                            {
                                buttonDiv  =
                                    "<Button class=\"btn btn-primary\" style=\"width:100%\" id =configure" + counter + "  onclick=\"window.location.href = '" + link + "';return false \"><i class=\"fa fa-gear\"></i>&nbsp;Config</Button>" +
                                    "<Button class=\"btn btn-warning\" style=\"width:100%\" id =AddComponent" + counter + " onclick=\"return false;\"><i class=\"fa fa-plus\"></i> &nbsp;Add</Button>" +
                                     "<Button class=\"btn btn-info\" style=\"width:100%\" id =RedoComponent" + counter + " onclick=\"return false;\"><i class=\"fa fa-refresh\"></i>Clear</Button>";
                            }
                       


                            addParentDiv.InnerHtml = "<div class=\"col-xs-12\"><div class=\"col-lg-2 col-sm-3\">"+anchorLink+"</div><div class=\"col-sm-5 col-lg-6 customized-values \" style=\"height:auto\" >"+detailsDiv+"</div><div class=\"col-sm-4\"  >" + buttonDiv + "</div></div>";
                            li.Controls.Add(addParentDiv);
                       

                        }
                        i++;
                    }
                    lblTotalPrice.Text = "$" + price.ToString();
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
            Response.Redirect("http://www.elinic.com/index.htm");
        }

        /// <summary>
        /// Redirect to the Order Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int itemNumber = 1;
            string orderHTML = "";
            for (int i = 1; i < 50; i++)//Hardcoded limit on how many configured components
            {
                if (Session["Comp" + i] != null)
                {
                    orderHTML = orderHTML + "ITEM #" + itemNumber + ": " + Session["Comp" + i].ToString() + "\n";
                    itemNumber++;
                }
            }

            if (Session["material"] != null)
            {
                orderHTML = orderHTML + "Material : " + Session["material"].ToString() + "\n";
                orderHTML = orderHTML + "Lacquer Finish  : " + Session["materialFinish"].ToString() + "\n";
                orderHTML = orderHTML + "Stain : " + Session["materialStain"].ToString() + "\n";
                orderHTML = orderHTML + "Handle : " + Session["handleIndex"].ToString() + "\n";
            }

            OrderDetails odr = new OrderDetails();
            //odr.Price = lblOrderPrice.Text;
            odr.Details = Regex.Replace(orderHTML, @"<[^>]+>|&nbsp;", "").Trim();

            Session["OrderDetails"] = null;
            Session["OrderDetails"] = odr;

            Response.Redirect("~/SubmitRequest.aspx");
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

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void RemoveSession(String id)
        {

            HttpContext.Current.Session.Remove("Comp" + id);
        }


        //using System.Web.Services;
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string CloneSession(int srcId, int destId)
        {
            HttpContext.Current.Session["Comp" + destId] = HttpContext.Current.Session["Comp" + srcId];
            HttpContext.Current.Session["Comp" + destId + "Price"] = HttpContext.Current.Session["Comp" + srcId + "Price"];
            HttpContext.Current.Session["Comp" + destId + "CompSelectedId"] = HttpContext.Current.Session["Comp" + srcId + "CompSelectedId"];
            HttpContext.Current.Session["Comp" + destId + "CompImagePath"] = HttpContext.Current.Session["Comp" + srcId + "CompImagePath"];
            return HttpContext.Current.Session["Comp" + destId] != null ? HttpContext.Current.Session["Comp" + destId].ToString() : "";
        }

    }
}

class MaterialObject
{

    public String ImagePath { get; set; }
    public float Price { get; set; }
    public String Name { get; set; }
}