using Microsoft.AspNetCore.Authorization;

namespace MyStore.Web.Framework
{
    public class AdminAuthAttribute : AuthorizeAttribute
    {
        public AdminAuthAttribute()
        {
            Policy = "admin";
        }
    }
}