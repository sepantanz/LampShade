using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.Slide;
using ShopManagement.Domain.SlideAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditSlide GetDetails(long id)
        {
            return _context.Slides.Select(p => new EditSlide
            {
                Id = p.Id,
                BtnText = p.BtnText,
                Heading = p.Heading,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Text = p.Text,
                Link = p.Link,
                Title = p.Title
            }).FirstOrDefault(p => p.Id == id);
        }

        public List<SlideViewModel> GetList()
        {
            return _context.Slides.Select(p => new SlideViewModel
            {
                Id = p.Id,
                Heading = p.Heading,
                Picture = p.Picture,
                Title = p.Title,
                IsRemoved = p.IsRemoved,
                CreationDate = p.CreationDate.ToFarsi()
            }).OrderByDescending(p => p.Id).ToList();
        }
    }
}
