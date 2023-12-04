﻿using ProductManagement.Domain.Contracts.Repositories.Base;
using ProductManagement.Domain.Shared.Responses;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Contracts.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
