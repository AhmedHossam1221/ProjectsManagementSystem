﻿using AutoMapper;
using ProjectsManagement.CQRS.Users.Commands;
using ProjectsManagement.Models;
using ProjectsManagement.ViewModels.Auth;

namespace ProjectsManagement.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterRequestViewModel, RegisterRequestDTO>().ReverseMap();
            CreateMap<RegisterRequestDTO, User>().ReverseMap();

            CreateMap<LoginRequestViewModel, LoginRequestDTO>().ReverseMap();
            CreateMap<LoginRequestDTO, User>().ReverseMap();
        }
    }
}