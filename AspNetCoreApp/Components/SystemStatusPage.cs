using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCoreApp.Components
{
    public class SystemStatusPage : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:48309");

                return View(response.StatusCode == HttpStatusCode.OK);
            }
        }
    }
}
