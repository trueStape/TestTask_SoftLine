using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskSoftline.DAL.Entities;
using TestTaskSoftline.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TestTaskSoftline.DAL.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
            : base(context)
        {
            _context = context;
        }

        public Task<UserEntity> GetUserAsync(Guid id)
        {
            return Query()
                .Where(x => x.Id == id && x.IsDeleted == false)
                .SingleOrDefaultAsync();
        }

        public Task<List<UserEntity>> GetAllUsersAsync()
        {
            return Query()
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.LastName)
                .ToListAsync();
        }
    }
}