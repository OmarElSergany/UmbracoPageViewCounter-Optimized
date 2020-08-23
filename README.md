# Umbraco 8 Page View Counter and file download Counter (Optimized)
Umbraco Page View Counter (Optimized)


this package is a statistics counter for individual pages and file downloads

it's a simplest way to shows how many times the page has been visited or file has been downloaded.

Note: this is a optimized way to count the page views, it's using custome database tables to store data, and not save and publish every time the page requested, so you can use it without warry abiut the performance in high traffec web sites.

Follow these instructions:

  1.Install the Package (Developer > Packages)
  
  2.add the below code to the page template you need to count page views
  
      @{ 
          int nodeId = Umbraco.AssignedContentItem.Id;
          string cookieName = String.Format("PageView_{0}", nodeId);
          if (Session[cookieName] == null)
          {
              Reflections.UmbracoUtilities.PageViewCounter.SetPageViewCount(nodeId);
              Session[cookieName] = 1;
          }
      }

 3.add the below code in the place you need to display the counter in.
 
     @Reflections.UmbracoUtilities.PageViewCounter.GetPageViewCount(Umbraco.AssignedContentItem.Id)
     
     
For File Download Counter

```
@{
var downloadableBrochure = Model.Value<IPublishedContent>("downloadfile");
int MediaId = downloadableBrochure.Id;
}

<a href="@(Reflections.UmbracoUtilities.FileDownloadCounter.FileDownloadCount(Umbraco.AssignedContentItem.Id,MediaId))" target="_blank" download>Download Brochure</a>
```

To get file download counter

```
@(Reflections.UmbracoUtilities.FileDownloadCounter.GetFileDownloadCount(Umbraco.AssignedContentItem.Id,MediaId))
```

Please Note this it's not a full umbraco package so if you will uninstall the package you need to delete the below from thee database:

1. Table : ReflectionsUmbracoUtilitiesPageViewCounter
2. Stored Procedure : ReflectionsUmbracoUtilitiesGetPageViewCount
3. Stored Procedure : ReflectionsUmbracoUtilitiesSetPageViewCount

4. Table : ReflectionsUmbracoUtilitiesFileDownloadCounter
5. Stored Procedure : ReflectionsUmbracoUtilitiesGetFileDownloadCount 
6. Stored Procedure : ReflectionsUmbracoUtilitiesSetFileDownloadCount
