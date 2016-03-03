﻿using Elinic.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Elinic
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class OrdersWebService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string GetTableData()
        {
            var echo = int.Parse(HttpContext.Current.Request.Params["sEcho"]);
            var displayLength = int.Parse(HttpContext.Current.Request.Params["iDisplayLength"]);
            var displayStart = int.Parse(HttpContext.Current.Request.Params["iDisplayStart"]);
            var sortOrder = HttpContext.Current.Request.Params["sSortDir_0"].ToString(CultureInfo.CurrentCulture);
            string rawSearch = HttpContext.Current.Request.Params["sSearch"];
            //var roleId = HttpContext.Current.Request.Params["roleId"].ToString(CultureInfo.CurrentCulture);


            Database db = new Database();
            List<OrderDetails> results = new List<OrderDetails>(); db.Connect();

            var sb = new StringBuilder();

            var wrappedSearch = "'%" + rawSearch + "%'";

            sb.Append("SELECT * FROM Orders");
            var whereClause = string.Empty;

            // Raw Search
            if (rawSearch.Length > 0)
            {
                sb.Append(" WHERE ID LIKE ");
                sb.Append(wrappedSearch);
                sb.Append(" OR OrderDetails LIKE ");
                sb.Append(wrappedSearch);
                sb.Append(" OR TotalPrice LIKE ");
                sb.Append(wrappedSearch);
                sb.Append(" OR Notes LIKE ");
                sb.Append(wrappedSearch);
                sb.Append(" OR OrderDate LIKE ");
                sb.Append(wrappedSearch);
                sb.Append(" OR Email LIKE ");
                sb.Append(wrappedSearch);

            }

            sb.Append(whereClause);

            // Ordering
            StringBuilder sbOrder = new StringBuilder();
            sbOrder.Append(HttpContext.Current.Request.Params["iSortCol_0"].ToString());

            sbOrder.Append(" ");

            sbOrder.Append(HttpContext.Current.Request.Params["sSortDir_0"].ToString());

            string orderByClause = sbOrder.ToString();

            if (!String.IsNullOrEmpty(sbOrder.ToString()))
            {

                orderByClause = orderByClause.Replace("0", ", ID ");
                orderByClause = orderByClause.Replace("1", ", Email ");
                orderByClause = orderByClause.Replace("2", ", OrderDate ");
                orderByClause = orderByClause.Replace("3", ", TotalPrice ");
                orderByClause = orderByClause.Replace("4", ", Notes ");
                orderByClause = orderByClause.Replace("5", ", OrderDetails ");
                orderByClause = orderByClause.Remove(0, 1);
            }
            else
            {
                orderByClause = "ID ASC";
            }
            orderByClause = " ORDER BY " + orderByClause + ";";
            sb.Append(orderByClause);

            var records = GetRecordsFromDatabaseWithFilter(sb.ToString()).ToList();

            if (!records.Any())
            {
                return string.Empty;
            }

            //var orderedResults = sortOrder == "asc"
            //                     ? records.OrderBy(o => o.Notes)
            //                     : records.OrderByDescending(o => o.Notes);
            var itemsToSkip = displayStart == 0
                              ? 0
                              : displayStart + 1;
            var pagedResults = records.Skip(itemsToSkip).Take(displayLength).ToList();


            var hasMoreRecords = false;
            sb.Clear();

            sb.Append(@"{" + "\"sEcho\": " + echo + ",");
            sb.Append("\"recordsTotal\": " + records.Count + ",");
            sb.Append("\"recordsFiltered\": " + records.Count + ",");
            sb.Append("\"iTotalRecords\": " + records.Count + ",");
            sb.Append("\"iTotalDisplayRecords\": " + records.Count + ",");
            sb.Append("\"aaData\": [");

            foreach (var result in pagedResults)
            {
                if (hasMoreRecords)
                {
                    sb.Append(",");
                }

                sb.Append("[");
                sb.Append("\"" + result.ID + "\",");
                sb.Append("\"" + result.Email + "\",");
                sb.Append("\"" + result.OrderDate + "\",");
                sb.Append("\"" + result.Price + "\",");
                sb.Append("\"" + result.Notes + "\",");
                sb.Append("\"" + result.Details + "\""); sb.Append("]");
                hasMoreRecords = true;
            }
            sb.Append("]}");
            return sb.ToString();
        }


        /// <summary>
        /// Returns a list of all orders in the system
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<OrderDetails> GetRecordsFromDatabaseWithFilter(string query)
        {
            Database db = new Database();
            List<OrderDetails> results = new List<OrderDetails>(); db.Connect();


            db.Query(query);

            if (db.rdr.HasRows == true)
            {
                while (db.rdr.Read())
                {
                    OrderDetails order = new OrderDetails();
                    order.ID = (Convert.ToString(db.rdr["ID"]));
                    order.Email = (Convert.ToString(db.rdr["Email"]));
                    order.OrderDate = (Convert.ToString(db.rdr["OrderDate"]));
                    order.Details = RemoveSpecialCharacters(Convert.ToString(db.rdr["OrderDetails"]).ToString());
                    order.Price = (Convert.ToString(db.rdr["TotalPrice"]));
                    order.Notes = (Convert.ToString(db.rdr["Notes"]));
                    results.Add(order);
                }
            }
            return results;
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ' || c == ':')
                {
                    sb.Append(c);

                }
            }
            return sb.ToString();
        }



    }


}
