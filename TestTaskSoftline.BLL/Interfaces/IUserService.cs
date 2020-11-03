using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskSoftline.BLL.Models;

namespace TestTaskSoftline.BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUserAsync(UserModel user);
        Task<UserModel> GetUserAsync(Guid userId);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<string> DeleteUserAsync(Guid userId);
        Task<string> UpdateUserAsync(Guid userId, UserModel userModel);
    }
}