using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eve_API.Queries;
using Eve_API.Models;

namespace Eve_API.Controllers
{
    public class CategoriesController : ApiController
    {
        CategoriesQueries query = new CategoriesQueries();

        private SqlCommand DataConnection(string query)
        {
            SqlConnection db = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Eve-db"].ConnectionString);
            SqlCommand cmd = new SqlCommand(query, db);
            return cmd;
        }

        [HttpGet]
        [Route("api/categories")]
        public IHttpActionResult Categories()
        {
            List<Category> result = new List<Category>();

            string sql = query.GetCategories();
            SqlCommand cmd = DataConnection(sql);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Category cat = new Category();

                    cat.categoryID = (int)reader["categoryID"];
                    cat.categoryName = reader.IsDBNull(reader.GetOrdinal("categoryName")) ? "" : reader["categoryName"] as string;
                    cat.iconID = reader.IsDBNull(reader.GetOrdinal("iconID")) ? 0 : (int)reader["iconID"];
                    cat.published = reader.IsDBNull(reader.GetOrdinal("published")) ? false : (Boolean)reader["published"];

                    result.Add(cat);
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        [HttpGet]
        [Route("api/groups")]
        public IHttpActionResult BlueprintGroups()
        {
            List<Group> result = new List<Group>();

            string sql = query.GetBlueprintGroups();
            SqlCommand cmd = DataConnection(sql);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Group gr = new Group();

                    gr.groupID = (int)reader["groupID"];
                    gr.categoryID = reader.IsDBNull(reader.GetOrdinal("categoryID")) ? 0 : (int)reader["categoryID"];
                    gr.groupName = reader.IsDBNull(reader.GetOrdinal("groupName")) ? "" : reader["groupName"] as string;
                    gr.iconID = reader.IsDBNull(reader.GetOrdinal("iconID")) ? 0 : (int)reader["iconID"];
                    gr.useBasePrice = reader.IsDBNull(reader.GetOrdinal("useBasePrice")) ? false : (Boolean)reader["useBasePrice"];
                    gr.anchored = reader.IsDBNull(reader.GetOrdinal("anchored")) ? false : (Boolean)reader["anchored"];
                    gr.anchorable = reader.IsDBNull(reader.GetOrdinal("anchorable")) ? false : (Boolean)reader["anchorable"];
                    gr.fittableNonSingleton = reader.IsDBNull(reader.GetOrdinal("fittableNonSingleton")) ? false : (Boolean)reader["fittableNonSingleton"];
                    gr.published = reader.IsDBNull(reader.GetOrdinal("published")) ? false : (Boolean)reader["published"];

                    result.Add(gr);
                }

                return Ok(result);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }


    }
}
