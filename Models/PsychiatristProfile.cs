using System.ComponentModel.DataAnnotations;

namespace Psychiatrist_Management_System.Models
{
    public class PsychiatristProfile
    {
        [Key]
        public int PsychoId { get; set; }
        public string UserId { get; set; }
        public string PsychoName { get; set; }
        public string PsychoEmail { get; set; }
        public int PsychoAge  { get; set; }
        public string HospitalName { get; set; }
        public string PsychoGender { get; set; }
        public string PsychoPhone { get; set; }
        public string Specialization { get; set; }
        public string PsychoImage { get; set; }
    }
}
