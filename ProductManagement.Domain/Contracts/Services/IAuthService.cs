﻿using ProductManagement.Domain.Contracts.Services.Base;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Dtos.Auth;
using ProductManagement.Domain.Dtos.Responses;
using ProductManagement.Domain.Dtos;
using ProductManagement.Domain.Dtos.CRUD;

namespace ProductManagement.Domain.Contracts.Services
{
    public interface IAuthService 
    {
        Task<ServiceResponse<UserReadDto?>> GetAsync(int id);
        Task<ServiceResponse<string?>> Login(LoginDto user);
        Task<ServiceResponse<int?>> Register(RegisterDto user);
    }
}
