using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApp.Models.Repositories
{
    public class PieReviewRepository : IPieReviewRepository
    {
        private readonly AppDBContext _db;

        public PieReviewRepository(AppDBContext appDbContext)
        {
            _db = appDbContext;
        }

        public void AddPieReview(PieReview pieReview)
        {
            _db.PieReviews.Add(pieReview);
            _db.SaveChanges();
        }

        public IEnumerable<PieReview> GetReviewsForPie(int pieId)
        {
            return _db.PieReviews.Where(p => p.Pie.PieId == pieId);
        }
    }
}
