using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(15).IsRequired();
            builder.Property(p => p.ShortDescription).HasMaxLength(500).IsRequired();
            builder.Property(p => p.Picture).HasMaxLength(1000);
            builder.Property(p => p.PictureAlt).HasMaxLength(255).IsRequired();
            builder.Property(p => p.PictureTitle).HasMaxLength(500).IsRequired();
            builder.Property(p => p.Keywords).HasMaxLength(100).IsRequired();
            builder.Property(p => p.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Slug).HasMaxLength(500).IsRequired();

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);

            builder.HasMany(p => p.ProductPictures)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
