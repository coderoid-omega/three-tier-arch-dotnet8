using Microsoft.AspNetCore.Mvc;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Interfaces;
using ThreeTierArch.UI.ViewModels.Student;

namespace ThreeTierArch.UI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studentRepo;
        private readonly ISkillRepo _skillRepo;

        public StudentController(IStudentRepo studentRepo, ISkillRepo skillRepo)
        {
            _studentRepo = studentRepo;
            _skillRepo = skillRepo;
        }

        public async Task<IActionResult> Index()
        {
            var student = await _studentRepo.GetAllStudent();
            var vm = student.Select(m => new StudentVm { Id = m.Id, Name = m.Name }).ToList();

            return View(vm);
        }

        public async Task<IActionResult> Create()
        {
            StudentCreateVm model = new StudentCreateVm();
            var skills = await _skillRepo.GetAllSkill();
            foreach (var item in skills)
            {
                model.Skills.Add(new StudentSkillCheckbox { SkillId = item.Id, SkillName = item.Title, IsChecked = false });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateVm model)
        {
            var selectedSkills = model.Skills.Where(m => m.IsChecked).ToList();
            var student = new Student();
            student.Name = model.StudentName;
            student.StudentSkills = new List<StudentSkill>();
            foreach(var item in selectedSkills)
            {
                student.StudentSkills.Add(new StudentSkill { SkillId = item.SkillId });
            }
            await _studentRepo.SaveStudent(student);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepo.GetStudentById(id);
            if(student == null)
            {
                return NotFound();
            }
            var existingSkills = student.StudentSkills.Select(m => m.SkillId).ToList();
            var model = new StudentEditVm();
            model.Id = student.Id;
            model.StudentName = student.Name;
            var skills = await _skillRepo.GetAllSkill();
            foreach (var skill in skills)
            {
                model.Skills.Add(new StudentSkillCheckbox
                {
                    SkillId = skill.Id,
                    SkillName = skill.Title,
                    IsChecked = existingSkills.Contains(skill.Id)
                });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentEditVm model)
        {
            var student = await _studentRepo.GetStudentById(model.Id);
            var existingSkills = student.StudentSkills.Select(m => m.SkillId).ToList();
            student.Name = model.StudentName;
            var selectedSkills = model.Skills.Where(m => m.IsChecked).Select(m => m.SkillId).ToList();

            var toRemove = existingSkills.Except(selectedSkills).ToList();
            var newSkills = selectedSkills.Except(existingSkills).ToList();

            foreach(var skillId in toRemove)
            {
                var studentSkill = student.StudentSkills.Where(m => m.SkillId == skillId).FirstOrDefault();
                student.StudentSkills.Remove(studentSkill);
            }

            foreach (var skillId in newSkills)
            {
                student.StudentSkills.Add(new StudentSkill { SkillId = skillId, StudentId = student.Id });
            }
            await _studentRepo.UpdateStudent(student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(StudentVm student)
        {
            var _student = await _studentRepo.GetStudentById(student.Id);
            if(_student != null)
            {
                await _studentRepo.DeleteStudent(_student);
            }
            return RedirectToAction("Index");
        }

    }
}
