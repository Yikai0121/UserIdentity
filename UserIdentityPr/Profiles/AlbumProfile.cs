using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserIdentityPr.Models;
using UserIdentityPr.ViewModels;

namespace UserIdentityPr.Pro
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<AlbumCreateViewModel, Album>();
            CreateMap<AlbumUpdateViewModel, Album>();
            CreateMap<Album, AlbumUpdateViewModel>();
        }
    }
}
