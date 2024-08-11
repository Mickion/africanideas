using Microsoft.AspNetCore.Components;
using System.Collections.Specialized;
using System.Web;

namespace afi.university.ui.Helpers
{
    /// <summary>
    /// The extension methods class adds a couple of simple extension methods 
    /// to the NavigationManager for accessing query string parameters in the URL.
    /// </summary>
    public static class ExtensionMethods
    {
        public static NameValueCollection QueryString(this NavigationManager navigationManager)
        {
            return HttpUtility.ParseQueryString(new Uri(navigationManager.Uri).Query);
        }

        public static string QueryString(this NavigationManager navigationManager, string key)
        {
            return navigationManager.QueryString()[key];
        }
    }
}
