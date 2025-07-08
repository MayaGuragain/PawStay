using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PawStay.Models
{
	public class ContactUsModel
	{
        [Key]
        public Guid ContactUsID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Message { get; set; }

        public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;

        public string OwnerID { get; set; }

        [ForeignKey("OwnerID")]
        public OwnerModel Owner { get; set; }

        public ContactUsModel()
        {
            ContactUsID = Guid.NewGuid();
        }
    }
}