using System;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using Elinic.Classes;
using System.Drawing;
using System.Web.Script.Serialization;

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
                            MaterialObject material = new MaterialObject();

                            material.Price = Convert.ToInt32(Convert.ToString(obj.rdr["MaterialPrice"]));
                            material.ImagePath = Convert.ToString(obj.rdr["MaterialImage"]);
                            material.Name = Convert.ToString(obj.rdr["MaterialName"]);
                            var json = new JavaScriptSerializer().Serialize(material);

                            compMaterial.Items.Insert(index, new ListItem(Convert.ToString(obj.rdr["MaterialName"]), json));
                            if (index == 0)
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
        protected void MaterialSessionStore()
        {
            String link = getProjectLink();
            Session["material"] = compMaterial.SelectedItem;
            Session["materialStain"] = compStain.SelectedValue;
            Session["materialFinish"] = compFinish.SelectedValue;
            Session["handleIndex"] = compHandle.SelectedItem;
            Response.Redirect(link);
        }


    }
}