using Microsoft.AspNetCore.Mvc;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Implementations;
using ThreeTierArch.Repositories.Interfaces;
using ThreeTierArch.UI.ViewModels.Skill;

namespace ThreeTierArch.UI.Controllers
{
    public class SkillController : Controller
    {
        private ISkillRepo _skillRepo;
        public SkillController(ISkillRepo skillRepo)
        {
            _skillRepo = skillRepo;
        }
        public async Task<IActionResult> Index()
        {
            var skills = await _skillRepo.GetAllSkill();
            var skillVm = skills.Select(m => new SkillViewModel { Id = m.Id, Title = m.Title}).ToList();
            return View(skillVm);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SkillViewModel model)
        {
            Skill skill = new Skill()
            {
                Title = model.Title
            };
            await _skillRepo.SaveSkill(skill);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var skill = await _skillRepo.GetSkillById(id);
            var skillVm = new SkillViewModel { Id = skill.Id, Title = skill.Title };
            return View(skillVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SkillViewModel model)
        {
            var skill = await _skillRepo.GetSkillById(model.Id);
            skill.Title = model.Title;
            await _skillRepo.UpdateSkill(skill);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(SkillViewModel model)
        {
            var skill = await _skillRepo.GetSkillById(model.Id);
            await _skillRepo.DeleteSkill(skill);
            return RedirectToAction("Index");
        }
    }
}
