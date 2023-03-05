using emarketo.Contexts;
using emarketo.Models;
using Microsoft.EntityFrameworkCore;

namespace emarketo.Services
{
    public class ShowcaseService
    {
        private readonly DataContext _context;

        public ShowcaseService(DataContext context)
        {
            _context = context;
        }

        public async Task<Showcase> GetLatest()
        {
            var showcaseEntity = await _context.Showcases.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (showcaseEntity == null)
                return null!;

            return new Showcase
            {
                Title_1 = showcaseEntity.Title_1,
                Title_2 = showcaseEntity.Title_2,
                ImageUrl = showcaseEntity.ImageUrl,
                LinkUrl = showcaseEntity.LinkUrl
            };
        }
    }
}
