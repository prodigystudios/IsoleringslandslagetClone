using System.ComponentModel.DataAnnotations;

namespace IsoleringslandslagetClone.Models
{
    public class Contact
    {
        public string name { get; set; }
        public int phoneNumber { get; set; }
        public string city { get; set; }
        [Required(ErrorMessage = "Field can't be empty")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }
        public string message { get; set; }
    }
}
