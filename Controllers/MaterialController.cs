using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eve_API.Queries;
using Eve_API.Models;
using System.Data.SqlClient;

namespace Eve_API.Controllers
{
    public class MaterialController : ApiController
    {
        MaterialQueries query = new MaterialQueries();

        private SqlCommand DataConnection(string query)
        {
            SqlConnection db = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Eve-db"].ConnectionString);
            SqlCommand cmd = new SqlCommand(query, db);
            return cmd;
        }

        [HttpGet]
        [Route("api/materials/{groupID}")]
        public IHttpActionResult GetMaterials(int groupID)
        {
            List<IndustryMaterial> result = new List<IndustryMaterial>();

            string sql = query.GetProductionMaterials(groupID);
            SqlCommand cmd = DataConnection(sql);

            try
            {
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return NotFound();
                }

                while (reader.Read())
                {
                    IndustryMaterial mat = new IndustryMaterial();

                    mat.typeID = reader.IsDBNull(reader.GetOrdinal("typeID")) ? 0 : (int)reader["typeID"];
                    mat.activityID = reader.IsDBNull(reader.GetOrdinal("activityID")) ? 0 : (int)reader["activityID"];
                    mat.materialTypeID = reader.IsDBNull(reader.GetOrdinal("materialTypeID")) ? 0 : (int)reader["materialTypeID"];
                    mat.quantity = reader.IsDBNull(reader.GetOrdinal("quantity")) ? 0 : (int)reader["quantity"];

                    result.Add(mat);
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
