using AutoMapper;
using emarketo.Models.Forms;
using emarketo.Models.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace emarketo.Services
{
    public class AutoMapperServices : Profile
    {
        public AutoMapperServices() => _ = _ = CreateMap<RegisterForm, AppUser>();

        private object CreateMap<T1, T2>()
        {
            throw new NotImplementedException();
        }
    }
}