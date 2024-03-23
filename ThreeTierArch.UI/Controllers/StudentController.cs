using Microsoft.AspNetCore.Mvc;
using ThreeTierArch.Repositories.Interfaces;

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

        public IActionResult Index()
        {
            return View();
        }
    }
}
