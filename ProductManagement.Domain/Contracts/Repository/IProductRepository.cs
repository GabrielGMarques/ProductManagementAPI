using ProductManagement.Domain.Contracts.Repository.Base;
using ProductManagement.Domain.Dtos.Responses;
using ProductManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Contracts.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
