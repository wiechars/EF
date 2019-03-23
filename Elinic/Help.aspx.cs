using System;
using System.Web.UI;
using System.Web.Services;
using Elinic.Classes;
using System.Drawing;
using System.Web.Script.Serialization;

namespace Elinic
{
    public partial class Help : Page
    {
        Elinic.Classes.Logger log = new Elinic.Classes.Logger();
        protected void Page_Load(object sender, EventArgs e)
        {
            helpText.InnerHtml = GetHelpText().Replace("\n", "<br>");
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
    }
}