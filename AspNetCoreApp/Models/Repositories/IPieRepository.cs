using System.Collections.Generic;

namespace AspNetCoreApp.Models.Repositories
{
    public interface IPieRepository
    {
        IEnumerable<Pie> Pies { get; }

        IEnumerable<Pie> PiesOftheWeek { get; }


        Pie GetPieById(int PieId);
    }
}
