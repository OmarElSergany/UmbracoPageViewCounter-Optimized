using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Reflections.UmbracoUtilities
{
    public static class PageViewCounter 
    {
        public static void SetPageViewCount(int nodeId)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["umbracoDbDSN"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ReflectionsUmbracoUtilitiesSetPageViewCount", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("NodeId", System.Data.SqlDbType.Int).Value = nodeId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static int GetPageViewCount(int nodeId)
        {
            int viewCount = 0;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["umbracoDbDSN"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ReflectionsUmbracoUtilitiesGetPageViewCount", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("NodeId", System.Data.SqlDbType.Int).Value = nodeId;
                    conn.Open();
                    viewCount = (int)cmd.ExecuteScalar();
                }
            }
            return viewCount;
        }

    }

   
}
