using AspNetCoreApp.Models;
using System.Collections.Generic;

namespace AspNetCoreApp.ViewModels
{
    public class PiesListViewModel
    {
        public IEnumerable<Pie> Pies { get; set; }
        public string CurrentCategory { get; set; }
    }
}
