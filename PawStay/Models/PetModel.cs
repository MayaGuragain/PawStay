using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PawStay.Models
{
	public class PetModel
	{
        [Key]
        public Guid PetID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Range(0, 100)]
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }

        [MaxLength(500)]
        public string DietaryRestrictions { get; set; }

        [MaxLength(500)]
        public string Medication { get; set; }

        [MaxLength(1000)]
        public string SpecialInstructions { get; set; }

        [MaxLength(100)]
        public string EmergencyContactName { get; set; }

        [Phone]
        [MaxLength(20)]
        public string EmergencyContactNumber { get; set; }

        public List<OwnerModel> Owners { get; set; }

        public PetModel()
        {
            PetID = Guid.NewGuid();
        }

    }
}