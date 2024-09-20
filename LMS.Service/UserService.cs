﻿using AutoMapper;
using LMS.Contracts;
using LMS.Infrastructure.Dtos;
using LMS.Models.Entities;
using LMS.Service.Contracts;

namespace LMS.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<UserDto?> GetUserAsync(Guid courseId, bool trackChanges = false)
        {
            var user = await _uow.User.GetUserAsync(courseId, trackChanges);
            if (user is null) return null; //ToDo: Fix later

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync(bool trackChanges = false)
        {
            var users = await _uow.User.GetUsersAsync(trackChanges);
            if (users is null) return null!; //ToDo: Fix later

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}