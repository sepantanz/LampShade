﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductPictureAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPictures");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PictureAlt).HasMaxLength(500).IsRequired();
            builder.Property(p => p.PictureTitle).HasMaxLength(500).IsRequired();

            builder.HasOne(p => p.Product).WithMany(p => p.ProductPictures).HasForeignKey(p => p.ProductId);
        }
    }
}
