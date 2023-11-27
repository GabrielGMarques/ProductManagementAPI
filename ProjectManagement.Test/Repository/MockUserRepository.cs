using ProductManagement.Domain.Contracts.Repository;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Test.Repository.Base;
using System.Xml.Linq;

namespace ProductManagement.Infra.Data.Repository
{
    public class MockUserRepository : MockRepository<User>, IUserRepository
    {
        public Task<User?> GetUserByName(string username)
        {
            User? user = _data.FirstOrDefault(x => x.Username.ToLower().Equals(username.ToLower()));

            return Task.FromResult(user);
        }

        public Task<bool> UserExists(string username)
        {
            var exists = _data.Any(x => x.Username.ToLower().Equals(username.ToLower()));

            return Task.FromResult(exists);
        }
    }
}
