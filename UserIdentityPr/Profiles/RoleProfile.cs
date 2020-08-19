using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentityPr.ViewModels;

namespace UserIdentityPr.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleAddViewModel,IdentityRole >();
            CreateMap<IdentityRole, RoleEditViewModel>();
        }
    }
}
