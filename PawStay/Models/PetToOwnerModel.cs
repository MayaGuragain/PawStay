using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PawStay.Models
{
	public class PetToOwnerModel
	{
        [Key]
        public Guid PetOwnerID { get; set; }
        
        [Required]
        public Guid PetID { get; set; }

        [ForeignKey("PetID")]

        public PetModel Pet { get; set; }

        [Required]
        public string OwnerID { get; set; }

        [ForeignKey("OwnerID")]
        public OwnerModel Owner { get; set; }

        public PetToOwnerModel()
        {
            PetOwnerID = Guid.NewGuid();
        }
    }
}