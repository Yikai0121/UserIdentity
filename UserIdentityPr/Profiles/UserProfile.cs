using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentityPr.Models;
using UserIdentityPr.ViewModels;

namespace UserIdentityPr.Profiles
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<UserCreateViewModel, ApplicationUser>();
            CreateMap<UserEditViewModel, ApplicationUser>();
        }
    }
}
