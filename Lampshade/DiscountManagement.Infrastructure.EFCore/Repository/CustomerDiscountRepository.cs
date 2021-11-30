using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository(DiscountContext discountContext,
            ShopContext shopContext) : base(discountContext)
        {
            _discountContext = discountContext;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _discountContext.CustomerDiscounts.Select(p => new EditCustomerDiscount
            {
                Id = p.Id,
                ProductId = p.ProductId,
                DiscountRate = p.DiscountRate,
                StartDate = p.StartDate.ToString(),
                EndDate = p.EndDate.ToString(),
                Reason = p.Reason
            }).FirstOrDefault(p => p.Id == id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel)
        {
            var products = _shopContext.Products.Select(p => new { p.Id, p.Name }).ToList();
            var query = _discountContext.CustomerDiscounts.Select(x => new CustomerDiscountViewModel
            {
                Id = x.Id,
                DiscountRate = x.DiscountRate,
                EndDate = x.EndDate.ToFarsi(),
                EndDateGr = x.EndDate,
                StartDate = x.StartDate.ToFarsi(),
                StartDateGr = x.StartDate,
                ProductId = x.ProductId,
                Reason = x.Reason,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (searchModel.ProductId > 0)
                query = query.Where(x => x.ProductId == searchModel.ProductId);

            if (!string.IsNullOrWhiteSpace(searchModel.StartDate))
            {
                query = query.Where(x => x.StartDateGr > searchModel.StartDate.ToGregorianDateTime());
            }

            if (!string.IsNullOrWhiteSpace(searchModel.EndDate))
            {
                query = query.Where(x => x.EndDateGr < searchModel.EndDate.ToGregorianDateTime());
            }

            var discounts = query.OrderByDescending(p => p.Id).ToList();

            discounts.ForEach(discount => discount.Product = products
            .FirstOrDefault(p => p.Id == discount.ProductId)?.Name);

            return discounts;
        }
    }
}
