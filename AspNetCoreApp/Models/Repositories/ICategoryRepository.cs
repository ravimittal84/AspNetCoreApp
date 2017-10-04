using System.Collections.Generic;

namespace AspNetCoreApp.Models.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
