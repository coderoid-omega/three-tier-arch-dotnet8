﻿using ThreeTierArch.Entities;

namespace ThreeTierArch.UI.ViewModels.Student
{
    public class StudentCreateVm
    {
        public string StudentName { get; set; }
        public Address PhysicalAddress { get; set; } = new Address();
        public List<StudentSkillCheckbox> Skills { get; set; } = new List<StudentSkillCheckbox>();
    }

    public class StudentSkillCheckbox
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public bool IsChecked { get; set; }
    }
}
