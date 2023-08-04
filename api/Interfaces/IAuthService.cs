using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces;

public interface IAuthService
{
    Task<bool> UserExistsAsync(string email);

}