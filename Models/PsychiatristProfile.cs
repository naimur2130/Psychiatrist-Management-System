using System.ComponentModel.DataAnnotations;

namespace Psychiatrist_Management_System.Models
{
    public class PsychiatristProfile
    {
        [Key]
        public int PyschoId { get; set; }
        public string PsychoName { get; set; }
        public string PsychoEmail { get; set; }
        public int PsychoAge  { get; set; }
        public string PsychoGender { get; set; }
        public string PsychoPhone { get; set; }
        public string Specialization { get; set; }

    }
}
