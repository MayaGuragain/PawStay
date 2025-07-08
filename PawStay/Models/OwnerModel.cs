using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PawStay.Models
{
	public class OwnerModel : ApplicationUser
    {

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Range(0, 120)]
        public int Age { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        
        [Phone]
        public override string PhoneNumber { get; set; }

        public List<PetModel> Pets { get; set; }
        
    }
}