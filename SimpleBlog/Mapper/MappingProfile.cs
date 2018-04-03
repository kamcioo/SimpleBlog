using AutoMapper;
using SimpleBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog.Mapper
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserPostAddViewModel, Post>();
                cfg.CreateMap<Post, UserPostDetailsViewModel>();
                cfg.CreateMap<Post, UserPostEditViewModel>();
                cfg.CreateMap<UserPostEditViewModel, Post>();
                cfg.CreateMap<Post, UserPostAddViewModel>();
                cfg.CreateMap<Post, CategoriesWithLastPosts>();
                cfg.CreateMap<User, UserViewModel>();
                cfg.CreateMap<User, ChangePersonalData>();
            });
            return config;
        }
    }
}