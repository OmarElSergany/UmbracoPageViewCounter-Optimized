using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Reflections.UmbracoUtilities
{
    public static class FileDownloadCounter 
    {
        public static void SetFileDownloadCount(int nodeId, int mediaId)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["umbracoDbDSN"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ReflectionsUmbracoUtilitiesSetFileDownloadCount", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("NodeId", System.Data.SqlDbType.Int).Value = nodeId;
                    cmd.Parameters.Add("MediaId", System.Data.SqlDbType.Int).Value = mediaId;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }



        }

        public static int GetFileDownloadCount(int nodeId, int mediaId)
        {
            int viewCount = 0;
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["umbracoDbDSN"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("ReflectionsUmbracoUtilitiesGetFileDownloadCount", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("NodeId", System.Data.SqlDbType.Int).Value = nodeId;
                    cmd.Parameters.Add("MediaId", System.Data.SqlDbType.Int).Value = mediaId;
                    conn.Open();
                    try
                    {
                        viewCount = (int)cmd.ExecuteScalar();
                    }
                    catch { }
                }
            }
            return viewCount;
        }

        public static string FileDownloadCount(int nodeId, int mediaId)
        {
            return "Umbraco/Api/FileDownload/download?nodeId=" + nodeId + "&mediaId=" + mediaId;
        }
    }

   
}
