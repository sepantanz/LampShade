using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {
        private readonly ShopContext _context;

        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures.Select(p => new EditProductPicture
            {
                Id = p.Id,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                ProductId = p.ProductId
            }).FirstOrDefault(p => p.Id == id);
        }

        public ProductPicture GetWithProductAndCategory(long id)
        {
            return _context.ProductPictures.Include(x => x.Product).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures.Include(p => p.Product).Select(p => new ProductPictureViewModel
            {
                Id = p.Id,
                CreationDate = p.CreationDate.ToFarsi(),
                Picture = p.Picture,
                Product = p.Product.Name,
                ProductId = p.ProductId,
                IsRemoved = p.IsRemoved
            });

            if (searchModel.ProductId != 0)
                query = query.Where(p => p.ProductId == searchModel.ProductId);

            return query.OrderByDescending(p => p.Id).ToList();
        }
    }
}
