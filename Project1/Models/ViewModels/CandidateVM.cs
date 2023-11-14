using System.ComponentModel.DataAnnotations;

namespace Project1.Models.ViewModels
{
    public class CandidateVM
    {
        public int CandidateId { get; set; }
        [Required, StringLength(50), Display(Name = "Candidate Name")]
        public string CandidateName { get; set; } = default!;
        [Required, Display(Name = "Date of Birth"), DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Picture { get; set; } = default!;
        public IFormFile PictureFile { get; set; } = default!;
        public bool Fresher { get; set; }
        public string SkillsStringfy { get; set; } = default!;
        public Skill[]? SkillList { get; set; }
    }
}
