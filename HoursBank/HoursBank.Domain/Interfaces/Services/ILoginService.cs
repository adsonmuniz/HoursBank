﻿using HoursBank.Domain.Dtos;
using System.Threading.Tasks;

namespace HoursBank.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task<object> Login(LoginDto user);
    }
}
