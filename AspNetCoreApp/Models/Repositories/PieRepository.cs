using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApp.Models.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDBContext _dBContext;

        public PieRepository(AppDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IEnumerable<Pie> Pies => _dBContext.Pies.Include(c => c.Category);

        public IEnumerable<Pie> PiesOftheWeek => _dBContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);

        public Pie GetPieById(int PieId) =>
            _dBContext.Pies.FirstOrDefault(p => p.PieId == PieId);
    }
}
