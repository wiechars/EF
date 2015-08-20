using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace Elinic.Classes
{
    public class Database : System.Web.UI.Page
    {
        private OleDbConnection conn = null;
        private OleDbCommand cmd = null;
        public OleDbDataReader rdr = null;

        public Database()
        {
            conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0; Persist Security Info = False;" + "Data Source=" + Server.MapPath("~\\database.mdb"));

        }
        /// <summary>
        ///  Connect the Databse Object
        /// </summary>
        public void Connect()
        {
            if (conn != null)
            {
                conn.Open();
            }
        }

        /// <summary>
        /// Close the Database Object
        /// </summary>
        public void Close()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }

        public void Query(String query)
        {
            OleDbCommand cmd = new OleDbCommand(query, conn);
            rdr = cmd.ExecuteReader();
        }

        public void Insert(String query)
        {
            OleDbCommand cmd = new OleDbCommand(query, conn);
            cmd.ExecuteNonQuery();

        }
    }
}