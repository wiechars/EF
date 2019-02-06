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
            selectedMaterial.Visible = false;
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
                Session.Clear();
                Session["Type"] = Convert.ToString(Request.QueryString["Type"]);
                Session["Title"] = Convert.ToString(Request.QueryString["Title"]);
                this.Page.Title = Convert.ToString(Request.QueryString["Title"]);
                btnBackToProjects.Visible = true;
                LoadProjectLayouts(null, null);

            }
            else if (Request.QueryString["LayoutID"] != null)
            {
                
                orderValues.InnerHtml = "";
                int? ideas = null;

                if (Request.QueryString["Ideas"] != null)
                {
                    ideas = Convert.ToInt32(Request.QueryString["Ideas"].ToString());
                }
                selectedComponent.Visible = true;
                selectedMaterial.Visible = true;
                notes.Visible = true;
                LoadProjectLayouts(Convert.ToInt32(Request.QueryString["LayoutID"].ToString()), ideas);
                LoadComponents(ideas);
                LoadLayout(ideas);
                LoadMaterials();
                LoadHandle();
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
                obj.Query("SELECT * FROM Projects WHERE Active = 1 ORDER BY seq");

                if (obj.rdr.HasRows == true)
                {
                    while (obj.rdr.Read())
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        tiles.Controls.Add(li);
                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        if (Convert.ToString(obj.rdr["ProjectType"].ToString()) == "12")
                        {  //Hardcode link for walking closets
                            anchor.Attributes.Add("href", "ClosetShape.htm");
                        }
                        else
                        {
                            anchor.Attributes.Add("href", "Project.aspx?Type=" + Convert.ToString(obj.rdr["ProjectType"].ToString()) + "&Title=" + Convert.ToString(obj.rdr["ProjectName"].ToString()));
                        }
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

        private void LoadMaterials()
        {
            if (compMaterial.Items.Count < 1)
            {
                Database obj = new Database();
                try
                {
                    obj.Connect();
                    obj.Query("SELECT * FROM Materials");
                    int index = 0;
                    if (obj.rdr.HasRows == true)
                    {
                        while (obj.rdr.Read())
                        {
                            Material material = new Material();

                            material.Price = Convert.ToInt32(Convert.ToString(obj.rdr["MaterialPrice"]));
                            material.ImagePath = Convert.ToString(obj.rdr["MaterialImage"]);
                            material.Name = Convert.ToString(obj.rdr["MaterialName"]);
                            var json = new JavaScriptSerializer().Serialize(material);

                            compMaterial.Items.Insert(index, new ListItem(Convert.ToString(obj.rdr["MaterialName"]), json));
                            if(index == 0)
                            {
                                imgMaterial.Src = "../Images/" + Convert.ToString(obj.rdr["MaterialImage"]);
                                imgMaterial.Alt = Convert.ToString(obj.rdr["MaterialImage"]);
                            }
                            index++;
                        }

                    }
                    //Stain
                    compStain.Items.Insert(0, new ListItem("Not stained", "Not stained"));
                    compStain.Items.Insert(1, new ListItem("Stained", "Stained"));


                    //Finish
                    compFinish.Items.Insert(0, new ListItem("Gloss", "Gloss"));
                    compFinish.Items.Insert(1, new ListItem("Semi-Gloss", "Semi-Gloss"));
                    compFinish.Items.Insert(2, new ListItem("Satin(matted, flat)", "Satin(matted, flat)"));

                    compMaterial.SelectedValue = Session["materialIndex"] != null ? Convert.ToString(Session["materialIndex"]) : "";
                    compStain.SelectedValue = Session["materialStain"] != null ? Convert.ToString(Session["materialStain"]) : "";
                    compFinish.SelectedValue = Session["materialFinish"] != null ? Convert.ToString(Session["materialFinish"]) : "";
                }

               

                catch (Exception ex)
                {

                    log.LogErrorMessage("Load Material Types : " + ex);

                }
                finally
                {
                    obj.Close();
                }
            }

        }

        protected void MaterialChanged(object sender, EventArgs e)
        {

            MaterialSessionStore();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Material material = new Material();
            material = serializer.Deserialize<Material>(compMaterial.SelectedItem.Value);
            var json = new JavaScriptSerializer().Serialize(material);

            imgMaterial.Src = "../Images/" + material.ImagePath;
            imgMaterial.Alt = material.ImagePath;

            compStain.Enabled = compMaterial.SelectedValue.Contains("Melamine") ? false : true;
            if (!compStain.Enabled)
            {
                compStain.SelectedIndex = 0;
            }
        }
        private void LoadHandle()
        {
            if (compHandle.Items.Count < 1)
            {
                Database obj = new Database();
                try
                {
                    obj.Connect();
                    obj.Query("SELECT * FROM Handles");
                    int index = 0;
                    if (obj.rdr.HasRows == true)
                    {
                        while (obj.rdr.Read())
                        {
                        
                            compHandle.Items.Insert(index, new ListItem(Convert.ToString(obj.rdr["HandleID"]), Convert.ToString(obj.rdr["HandleImage"])));
                            if (index == 0)
                            {
                                imgHandle.Src = "../Images/" + Convert.ToString(obj.rdr["HandleImage"]);
                                imgHandle.Alt = Convert.ToString(obj.rdr["HandleImage"]);
                            }
                            index++;
                        }

                        compHandle.SelectedValue = Session["handleIndex"] != null ? Convert.ToString(Session["handleIndex"]) : "";
                    }

                    

                }

                catch (Exception ex)
                {

                    log.LogErrorMessage("Load Material Types : " + ex);

                }
                finally
                {
                    obj.Close();
                }
            }

        }

        protected void HandleChanged(object sender, EventArgs e)
        {
            imgHandle.Src = "../Images/" + compHandle.SelectedItem.Value;
            imgHandle.Alt = compHandle.SelectedItem.Value;
            MaterialSessionStore();
        }

        protected void MaterialSessionStore()
        {
            Session["materialIndex"] = compMaterial.SelectedValue;
            Session["materialStain"] = compStain.SelectedValue;
            Session["materialFinish"] = compFinish.SelectedValue;
            Session["handleIndex"] = compHandle.SelectedValue;
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
                        lblLayoutDescription.Text = Convert.ToString(obj.rdr["Description2"].ToString());

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
                        for (int counter = 1 + (5 * (i - 1)); counter <= 5 * i; counter++)
                        {
                            string detailsDiv = "";
                            HtmlGenericControl li = new HtmlGenericControl("li");
                            comp_small.Controls.Add(li);
                            li.Attributes.Add("id", "liAddComponent" + counter);
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
                                        + "\"><a href=\"" + link + "\"\\><b>Component Type:</b> "
                                        + Convert.ToString(obj.rdr["CompTypeID"].ToString());

                                orderValues.InnerHtml = orderValues.InnerHtml + "<div class=\"customized-values\" style=\"font-size:16px!important;\" id=\"Comp" + counter
                                        + "\"><b>Component Type:</b> "
                                        + Convert.ToString(obj.rdr["CompTypeID"].ToString());
                                price = price + Convert.ToDouble(Session["Comp" + counter + "Price"].ToString());
                                orderValues.InnerHtml = orderValues.InnerHtml + Session["Comp" + counter].ToString() + "</div>";
                                detailsDiv = detailsDiv + Session["Comp" + counter].ToString() + "</div>";


                            }
                            else
                            {
                                //Logic to hide additional place holders
                                if (counter != 1 + (5 * (i - 1)))
                                {
                                    detailsDiv = "<div class=\"customized-values\"  id=\"Comp"
                                        + counter + "\"><a href=\"" + link + "\"\\>";
                                }
                            }
                            HtmlGenericControl add = new HtmlGenericControl("div");
                            HtmlGenericControl remove = new HtmlGenericControl("Button");
                            HtmlGenericControl addDetailsDiv = new HtmlGenericControl("div");
                            HtmlGenericControl anchor = new HtmlGenericControl("a");
                            anchor.Attributes.Add("href", link);
                            if (Session["Comp" + counter] != null)
                            {
                                anchor.InnerHtml = "<p>" + Convert.ToString(obj.rdr["CompTypeName"].ToString()) + "</p><img src=\"" + Session["Comp" + counter + "CompImagePath"].ToString() + "\">";
                                Session["ComponentImagePath"] = "";
                            }
                            else
                            {
                                anchor.InnerHtml = "<p>" + Convert.ToString(obj.rdr["CompTypeName"].ToString()) + "</p><img src=\"../Images/CompTypeThumbs/"
                           + Convert.ToString(obj.rdr["CompTypeThumbImage"].ToString()) + "\"></a>";
                            }
                    
                            if (counter != 1 + (5 * (i - 1)))
                            {


                                add.InnerHtml =
                                              "<Button class=\"btn btn-primary\" id =configure" + counter + "  onclick=\"window.location.href = '" + link + "';return false \"><i class=\"fa fa-gear\"></i></Button>" +
                                                "<Button class=\"btn btn-warning\" id =AddComponent" + counter + " onclick=\"return false;\"><i class=\"fa fa-plus\"></i></Button>" +
                                                "<Button class=\"btn btn-danger\" id =RemoveComponent" + counter + " onclick=\"return false;\"><i class=\"fa fa-times\"></i></Button>" +
                                                "<Button class=\"btn btn-info\" id =RedoComponent" + counter + " onclick=\"return false;\"><i class=\"fa fa-refresh\"></i></Button>";
                            }
                            else
                            {
                                add.InnerHtml =
                                  
                                    "<Button class=\"btn btn-primary\" id =configure" + counter + "  onclick=\"window.location.href = '" + link + "';return false \"><i class=\"fa fa-gear\"></i></Button>" +
                                    "<Button class=\"btn btn-warning\" id =AddComponent" + counter + " onclick=\"return false;\"><i class=\"fa fa-plus\"></i></Button>" +
                                     "<Button class=\"btn btn-info\" id =RedoComponent" + counter + " onclick=\"return false;\"><i class=\"fa fa-refresh\"></i></Button>";
                            }
                            //remove.InnerHtml = "<Button style=\"padding:2 2 2 2px !important; background-color: red;width: 40%%;\"id=RemoveComponent" + counter + " onclick=\"return false;\">-</Button>";
                            remove.InnerHtml = "<Button class=\"btn btn-danger\" id =RemoveComponent" + counter + " onclick=\"return false;\"><i class=\"fa fa-times\"></i></Button>";




                            addDetailsDiv.InnerHtml = detailsDiv;
                            li.Controls.Add(add);
                           // li.Controls.Add(remove);
                            li.Controls.Add(anchor);
                            if (counter % 5 != 0)  //Causing extra anchor tags - don't know why.
                            {
                                li.Controls.Add(addDetailsDiv);
                            }


                        }
                        i++;
                    }
                    lblTotalPrice.Text = "$" + price.ToString();
                    lblOrderPrice.Text = "$" + price.ToString();
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
        protected void btnOrder_Click(object sender, EventArgs e)
        {
            int itemNumber = 1;
            string orderHTML = "";
            for(int i=1; i< 50; i++)//Hardcoded limit on how many configured components
            {
                if (Session["Comp" + i] != null)
                {
                    orderHTML = orderHTML + "ITEM #" + itemNumber + ": " + Session["Comp" + i].ToString() + "\n";
                    itemNumber++;
                }
            }

            orderHTML = orderHTML + "Material : " + compMaterial.SelectedItem.Text + "\n";
            orderHTML = orderHTML + "Lacquer Finish  : " + compFinish.SelectedValue + "\n";
            orderHTML = orderHTML + "Stain : " + compStain.SelectedValue + "\n";
            orderHTML = orderHTML + "Handle : " + compHandle.SelectedItem.Text + "\n";

            OrderDetails odr = new OrderDetails();
            odr.Price = lblOrderPrice.Text;
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
       HttpContext.Current.Session["Comp" + destId + "Price"] = HttpContext.Current.Session["Comp" + srcId+ "Price"];
       HttpContext.Current.Session["Comp" + destId + "CompImagePath"] = HttpContext.Current.Session["Comp" + srcId + "CompImagePath"];
       return HttpContext.Current.Session["Comp" + destId].ToString();
    }

}
}

class Material
{

    public String ImagePath{get;set;}
    public float Price { get; set; }
    public String Name { get; set; }
}