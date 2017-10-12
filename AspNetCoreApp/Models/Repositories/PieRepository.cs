using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApp.Models.Repositories
{
    public class PieRepository : IPieRepository
    {
        private readonly AppDBContext _db;

        public PieRepository(AppDBContext dBContext)
        {
            _db = dBContext;
        }

        public IEnumerable<Pie> Pies => _db.Pies.Include(c => c.Category);

        public IEnumerable<Pie> PiesOftheWeek => _db.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);

        public Pie GetPieById(int PieId) =>
            _db.Pies.Include(p => p.PieReviews).FirstOrDefault(p => p.PieId == PieId);

        public void UpdatePie(Pie pie)
        {
            _db.Pies.Update(pie);
            _db.SaveChanges();
        }

        public void CreatePie(Pie pie)
        {
            _db.Pies.Add(pie);
            _db.SaveChanges();
        }
    }
}
