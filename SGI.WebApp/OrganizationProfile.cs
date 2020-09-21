using AutoMapper;
using SGI.ApplicationCore.DTOs;
using SGI.ApplicationCore.Entities;
using SGI.Infrastructure.Entities;
using SGI.WebApp.ApiModels;
using SGI.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGI.WebApp
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Incidencia, IncidenciaApiModel>().ReverseMap();
            CreateMap<Dealer, DealerApiModel>().ReverseMap();
            CreateMap<Role, RoleApiModel>().ReverseMap();
            CreateMap<RolePermission, RolePermissionApiModel>().ReverseMap();
            CreateMap<User, UserApiModel>().ReverseMap();
            CreateMap<UserRole, UserRoleApiModel>().ReverseMap();
            CreateMap(typeof(PagedResult<>), typeof(PagedResultViewModel<>)).ReverseMap();
            CreateMap<PagingInfo, PagingInfoViewModel>().ReverseMap();
            CreateMap<Token, TokenApiModel>().ReverseMap();
        }
    }
}
