using System.ComponentModel.DataAnnotations;

namespace Psychiatrist_Management_System.ViewModel
{
    public class PsychiatristViewModel
    {
        [Required]
        public string PsychoName { get; set; }
        [Required]
        public string PsychoEmail { get; set; }
        [Required]
        public int PsychoAge { get; set; }
        [Required]
        public string PsychoGender { get; set; }
        [Required]
        public string HospitalName { get; set; }
        [Required]
        public string PsychoPhone { get; set; }
        [Required]
        public string Specialization { get; set; }
        [Required]
        public IFormFile PsychoImage { get; set; }
    }
}
