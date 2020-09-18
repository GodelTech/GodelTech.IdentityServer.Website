using AutoMapper;
using GodelTech.IdentityServer.Data.Models;
using GodelTech.IdentityServer.Web.Models.User;
using Microsoft.AspNetCore.Identity;

namespace GodelTech.IdentityServer.Web.Mappings
{
    public class IdentityUserProfile : Profile
    {
        public IdentityUserProfile()
        {
            CreateMap<RegistrationViewModel, User>()
                .ForMember(user => user.UserName, map => map.MapFrom(viewModel => viewModel.Email));
        }
    }
}
