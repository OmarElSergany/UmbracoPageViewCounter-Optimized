//https://our.umbraco.com/Documentation/Reference/Routing/WebApi/

// The UmbracoApiController derives from the ASP.NET WebAPI controller:
// https://www.asp.net/web-api

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

using Umbraco.Web.WebApi;


namespace Reflections.UmbracoUtilities
{
    //Route: ~/Umbraco/Api/[YourControllerName]
    public class FileDownloadCounterApiController : UmbracoApiController
    {
        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage Download(int nodeId, int mediaId,string FileName)
        {

            FileDownloadCounter.SetFileDownloadCount(nodeId, mediaId);

            var MediaFile = Umbraco.Media(mediaId);
            var FileUrl = MediaFile.Url;
            
            //Create HTTP Response.
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            //Set the File Path.
            //string filePath = HttpContext.Current.Server.MapPath("~/Files/") + fileName;
            string filePath = HttpContext.Current.Server.MapPath(FileUrl);

            //Check whether File exists.
            if (!File.Exists(filePath))
            {
                //Throw 404 (Not Found) exception if File not found.
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", FileName);
                throw new HttpResponseException(response);
            }

            //Read the File into a Byte Array.
            byte[] bytes = File.ReadAllBytes(filePath);

            //Set the Response Content.
            response.Content = new ByteArrayContent(bytes);

            //Set the Response Content Length.
            response.Content.Headers.ContentLength = bytes.LongLength;

            //Set the Content Disposition Header Value and FileName.
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = FileName;

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(FileName));
            return response;

            //string contentType = "application/pdf";
            //return Controller.File(FileUrl, contentType, fileName);
        }
    }
}