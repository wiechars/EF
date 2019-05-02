using System;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using Elinic.Classes;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace Elinic
{
    public partial class Material : Page
    {
        Elinic.Classes.Logger log = new Elinic.Classes.Logger();
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadMaterials();
            LoadHandle();
            if (Request.QueryString["LayoutID"] != null)
            {
                Session["projectLayout"] = Request.QueryString["LayoutID"];
            }
            if (Request.QueryString["Ideas"] != null)
            {
                Session["projectIdeas"] = Request.QueryString["Ideas"];
            }
            backToProject.HRef = getProjectLink();
            if (Request.RequestType == "POST")
            {
                MaterialSessionStore();
            } 
        }

        /// <summary>
        /// Retrieve Materials and bind to controls.
        /// </summary>
        public void LoadMaterials()
        {
            if (compMaterial.Items.Count < 1)
            {
                try
                {

                    MaterialObject material = new MaterialObject();
                    List<MaterialObject> materials = material.GetMaterials();
                    int index = 0;
                    foreach (MaterialObject mat in materials)
                    {
                        var json = new JavaScriptSerializer().Serialize(mat);
                        Session["materials"] = null;
                        Session["materials"] = json;
                        compMaterial.Items.Insert(index, new ListItem(mat.Name, json));
                        if (index == 0)
                        {
                            imgMaterial.Src = "../Images/" + mat.ImagePath;
                            imgMaterial.Alt = mat.ImagePath;
                        }
                        index++;
                    }
                    
                    //Finish
                    compFinish.Items.Insert(0, new ListItem("Gloss", "Gloss"));
                    compFinish.Items.Insert(1, new ListItem("Semi-Gloss", "Semi-Gloss"));
                    compFinish.Items.Insert(2, new ListItem("Satin(matted, flat)", "Satin(matted, flat)"));
                    compMaterial.SelectedValue = Session["materialIndex"] != null ? Convert.ToString(Session["materialIndex"]) : "";
                    compFinish.SelectedValue = Session["materialFinish"] != null ? Convert.ToString(Session["materialFinish"]) : "";
                }
                catch (Exception ex)
                {

                    log.LogErrorMessage("Load Material Types : " + ex);

                }
            }

        }
        
        /// <summary>
        /// Retrieve Handles and bind to control
        /// </summary>
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

        protected String getProjectLink()
        {
            String link = "Project.aspx?";
            if (Session["projectLayout"] != null)
            {
                link += "LayoutID=" + Session["projectLayout"].ToString() + "&";
            }
            if (Session["projectIdeas"] != null)
            {
                link += "Ideas=" + Session["projectIdeas"].ToString() + "&";
            }
            return link;
        }
        /// <summary>
        /// Stores selected components in session store to be retrieved on Project scren.
        /// </summary>
        protected void MaterialSessionStore()
        {
            String link = getProjectLink();
            Session["material"] = compMaterial.SelectedItem;
            Session["materialFinish"] = compFinish.SelectedValue;
            Session["handleIndex"] = compHandle.SelectedItem;
            Response.Redirect(link);
        }


    }
}