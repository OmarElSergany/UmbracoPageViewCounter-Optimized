using System;
using System.Web.Mvc;
using Umbraco.Web.Composing;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

/// <summary>
/// If you need complete control over how your pages are rendered then Hijacking Umbraco Routes is for you.
/// https://our.umbraco.com/documentation/Reference/Routing/custom-controllers
/// </summary>
namespace Reflections.UmbracoUtilities
{

    public class FileDownloadCounterController : RenderMvcController
    {
        public override ActionResult Index(ContentModel model)
        {
            // Do some stuff here, then return the base method
            return base.Index(model);
        }

        public FileResult Download(int nodeId, int mediaId, String fileName)
        {
            var FileId = Umbraco.Media(mediaId);
            var FileUrl = FileId.Url;
            string contentType = "application/pdf";
            return File(FileUrl, contentType, fileName);
        }
    }
}