using AspNetCoreApp.Models;
using System.Collections.Generic;

namespace AspNetCoreApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfTheWeek { get; set; }
    }
}
