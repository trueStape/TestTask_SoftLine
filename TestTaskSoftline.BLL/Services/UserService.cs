using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TestTaskSoftline.BLL.Interfaces;
using TestTaskSoftline.BLL.Models;
using TestTaskSoftline.DAL.Entities;
using TestTaskSoftline.DAL.Interfaces;

namespace TestTaskSoftline.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(UserModel user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);
            userEntity.Id = Guid.NewGuid();

            await _userRepository.CreateAsync(userEntity);
            await _userRepository.SaveAsync();
        }

        public async Task<UserModel> GetUserAsync(Guid userId)
        {
            var userEntity = await _userRepository.GetUserAsync(userId);
            var userModel = _mapper.Map<UserModel>(userEntity);

            return userModel;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
           var users = await _userRepository.GetAllUsersAsync();
           var usersModels = _mapper.Map<List<UserEntity>, List<UserModel>>(users);

           return usersModels;
        }

        public async Task<string> DeleteUserAsync(Guid userId)
        {
            var user = await _userRepository.GetUserAsync(userId);
            if (user == null) return "User is not found";

            user.IsDeleted = true;
            await _userRepository.SaveAsync();

            return $"User: {user.LastName} {user.Name} deleted";
        }

        public async Task<string> UpdateUserAsync(Guid userId, UserModel userModel)
        {
            var user = await _userRepository.GetUserAsync(userId);
            if (user == null) return "User is not found";

            user.LastName = userModel.LastName;
            user.Name = userModel.Name;
            user.Patronymic = userModel.Patronymic;
            user.DateOfBirth = userModel.DateOfBirth;
            user.About = userModel.About;
            user.Decree = userModel.Decree;

            await _userRepository.SaveAsync();

            return "User Information Updated";
        }
    }
}