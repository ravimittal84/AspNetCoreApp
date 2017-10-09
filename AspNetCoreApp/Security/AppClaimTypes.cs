using System.Collections.Generic;

namespace AspNetCoreApp.Security
{
    public class AppClaimTypes
    {
        public static List<string> ClaimsList { get; set; } = new List<string> { "Delete Pie", "Add Pie" };
    }
}
