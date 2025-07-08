using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PawStay.Models
{

    public enum CancellationReasonType
    {
        None,
        CustomerRequest,
        Emergency,
        SchedulingConflict,
        Weather,
        Other
    }
    public enum BookingStatus
    {
        Scheduled = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4
    }

    public class BookingModel
	{
        [Key]
        public Guid BookingID { get; set; }

        [Required]
        public DateTime BookingStartTime { get; set; }

        [Required]
        public DateTime BookingEndTime { get; set; }

        public DateTime? ActualBookingStartTime { get; set; }

        public DateTime? ActualBookingEndTime { get; set; }

        public Guid? EmployeeCheckInID { get; set; }

        public Guid? EmployeeCheckOutID { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? CancellationDate { get; set; }

        public CancellationReasonType? CancellationReason { get; set; }

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Scheduled;

        [Required]
        public Guid PetID { get; set; }

        [ForeignKey("PetID")]
        public PetModel Pet { get; set; }

        public BookingModel()
        {
            BookingID = Guid.NewGuid();
        }

    }
}