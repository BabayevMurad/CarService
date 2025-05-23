﻿using CarService.Entities;

namespace CarService.DataAccess.Abstract
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<bool> AdminExists(string username);
        Task<bool> MechanicExists(string username);
        Task<Admin> AdminLogin(string username, string password);
        Task<Admin> AdminRegister(Admin admin, string password);
        Task<Mechanic> MechanicLogin(string username, string password);
        Task<Mechanic> MexhanicRegister(Mechanic mechanic, string password);
        Task<Admin> AdminLogout(string username, string password);
        Task<Mechanic> AcceptedMechanicLogin(string username, string password);
    }
}
