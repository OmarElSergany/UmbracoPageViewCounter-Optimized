# Umbraco 7 Page View Counter Optimized 
Umbraco Page View Counter (Optimized)


this package is a statistics counter for individual pages

it's a simplest way to shows how many times the page has been visited.

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
     
Please Note this it's not a full umbraco package so if you will uninstall the package you need to delete the below from thee database:

1. Table : ReflectionsUmbracoUtilitiesPageViewCounter
2. Stored Procedure : ReflectionsUmbracoUtilitiesGetPageViewCount
3. Stored Procedure : ReflectionsUmbracoUtilitiesSetPageViewCount
