using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskSoftline.DAL.Entities;

namespace TestTaskSoftline.DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserEntity> GetUserAsync(Guid id);
        Task<List<UserEntity>> GetAllUsersAsync();
    }
}