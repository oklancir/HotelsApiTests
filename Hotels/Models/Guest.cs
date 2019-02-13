using System.ComponentModel.DataAnnotations;

namespace Hotels.Models
{
    public class Guest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name"), Display(Name = "First name"), StringLength(50)]
        public string FirstName { get; set; }

        [Required, Display(Name = "Last name"), StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(100)]
        public string Address { get; set; }

        [Required, EmailAddress, StringLength(100), DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required, Phone, Display(Name = "Phone number"), StringLength(50)]
        public string PhoneNumber { get; set; }
    }
}