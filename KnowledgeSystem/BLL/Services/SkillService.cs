using System.Collections.Generic;
using System.Linq;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;
using System;

namespace BLL
{
    public class SkillService : ISkillService
    {
        private readonly IUnitOfWork uow;
        private readonly ISkillRepository skillRepository;

        public SkillService(IUnitOfWork uow, ISkillRepository repository)
        {
            this.uow = uow;
            this.skillRepository = repository;
        }

        public void Create(BllSkill skill)
        {
            skillRepository.Create(SkillMapper.Map(skill));
        }

        public void Delete(int id)
        {
            skillRepository.Delete(id);
        }

        public IEnumerable<BllSkill> GetAll()
        {
            return skillRepository.GetAll().Select(u => SkillMapper.Map(u));
        }

        public BllSkill GetById(int id)
        {
            return SkillMapper.Map(skillRepository.GetById(id));
        }

        public void Update(BllSkill skill)
        {
            skillRepository.Update(SkillMapper.Map(skill));
        }
    }
}
