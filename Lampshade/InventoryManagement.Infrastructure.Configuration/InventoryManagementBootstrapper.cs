﻿using InventoryManagement.Application;
using InventoryManagement.Application.Contracts.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Configuration
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IInventoryApplication, InventoryApplication>();

            services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
        }
    }
}
