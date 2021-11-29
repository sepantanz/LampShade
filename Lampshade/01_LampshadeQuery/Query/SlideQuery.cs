using _01_LampshadeQuery.Contracts;
using _01_LampshadeQuery.Contracts.Slide;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_LampshadeQuery.Query
{
    public class SlideQuery : ISlideQeury
    {
        private readonly ShopContext _shopContext;

        public SlideQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public List<SlideQueryModel> GetSlides()
        {
            return _shopContext.Slides.Where(p => p.IsRemoved == false).Select(p => new SlideQueryModel
            {
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                BtnText = p.BtnText,
                Heading = p.Heading,
                Link = p.Link,
                Text = p.Text,
                Title = p.Title
            }).ToList();
        }
    }
}
