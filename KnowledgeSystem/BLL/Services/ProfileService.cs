using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;

namespace BLL
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork uow;
        private readonly IProfileRepository profileRepository;

        public ProfileService(IUnitOfWork uow, IProfileRepository repository)
        {
            this.uow = uow;
            this.profileRepository = repository;
        }

        public BllProfile GetProfile(int id)
        {
            return ProfileMapper.MapProfile(profileRepository.Get(id));
        }

        public void EditProfile(BllProfile profile)
        {
            profileRepository.Update(ProfileMapper.MapProfile(profile));
        }
    }
}
