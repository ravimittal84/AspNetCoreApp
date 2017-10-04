using System.Collections.Generic;

namespace AspNetCoreApp.Models.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDBContext _dBContext;

        public CategoryRepository(AppDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IEnumerable<Category> Categories => _dBContext.Categories;
    }
}
