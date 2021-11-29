using _01_LampshadeQuery.Contracts.Slide;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents
{
    public class SlideViewComponent : ViewComponent
    {
        private readonly ISlideQeury _slideQeury;

        public SlideViewComponent(ISlideQeury slideQeury)
        {
            _slideQeury = slideQeury;
        }

        public IViewComponentResult Invoke()
        {
            var slides = _slideQeury.GetSlides();
            return View(slides);
        }
    }
}
