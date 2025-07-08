using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PawStay.Models;
using PawStay.ViewModels;

namespace PawStay.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Send Us A Message!";

            return View();
        }

        public ActionResult GitBasics()
        {
            return Content("Git basics");
        }

        // /Home/Create?name=Lucky&age=5&breed=GermanShephard&medication=N/A&emergencyContactNumber=123456789&emergencyContactName=Jake&dietaryRestrictions=N/A&specialInstructions=N/A
        public ActionResult CreatePet(string name, int age, string breed,
            string medication, string emergencyContactNumber, string emergencyContactName,
            string dietaryRestrictions, string specialInstructions)
        {
            if (string.IsNullOrEmpty(name))
            {
                return Content("name is required!");
            }


            // step 1 - connect to database
            ApplicationDbContext dbContext = new ApplicationDbContext();

            // step 2 - create object
            PetModel petModel = new PetModel();
            petModel.Name = name;
            petModel.Age = age;
            petModel.Breed = breed;
            petModel.Medication = medication;
            petModel.EmergencyContactNumber = emergencyContactNumber;
            petModel.EmergencyContactName = emergencyContactName;
            petModel.DietaryRestrictions = dietaryRestrictions;
            petModel.SpecialInstructions = specialInstructions;

            // step 3 - add our object to oue Dbset
            dbContext.PetModels.Add(petModel);

            // step 4 - finalize (save it)
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }

            return Content("Created: " + petModel.PetID);
        }

        public ActionResult ReadPet(Guid petId)
        {
            // step 1 - connect to database
            ApplicationDbContext dbContext = new ApplicationDbContext();

            // step 2 - query and save
            PetModel petModel = dbContext.PetModels.FirstOrDefault(x => x.PetID == petId);

            // step 3 - validate and return
            if (petModel == null)
            {
                return Content("no pet found for that id");
            }
            return Content("Pet - ID" + petModel.PetID + "Name: " + petModel.Name + "Age: " + petModel.Age + "Breed: " + petModel.Breed +
                "Medication: " + petModel.Medication + "EmergencyContactNumber: " + petModel.EmergencyContactNumber +
                "EmergencyContactName: " + petModel.EmergencyContactName + "DietaryRestrictions: " + petModel.DietaryRestrictions +
                "SpecialInstructions: " + petModel.SpecialInstructions);
        }

        public ActionResult UpdatePet(Guid petId, int age)
        {
            // step 1 - connect to database
            ApplicationDbContext dbContext = new ApplicationDbContext();

            // step 2 - query and save
            PetModel petModel = dbContext.PetModels.FirstOrDefault(x => x.PetID == petId);

            if (petModel == null)
            {
                return Content("Error: Pet not found.");
            }

            petModel.Age = age;

            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }

            return Content("Update");
        }

        public ActionResult DeletePet(Guid petId)
        {
            // step 1 - connect to database
            ApplicationDbContext dbContext = new ApplicationDbContext();

            // step 2 - query and save
            PetModel petModel = dbContext.PetModels.FirstOrDefault(x => x.PetID == petId);

            // step 3 - if exists, delte them
            if (petModel != null)
            {
                var relatedBookings = dbContext.BookingModels.ToList();
                foreach (var booking in relatedBookings)
                {
                    if (booking.PetID == petId)
                    {
                        dbContext.BookingModels.Remove(booking);
                    }
                }
                dbContext.PetModels.Remove(petModel);

                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Content("Error: " + ex.Message);
                }
            }

            return Content("Deleted");
        }

        // /Home/CreateBooking?petId=NUM&startTime=2025-04-01T10:00:00&endTime=2025-04-01T11:00:00
        
        public ActionResult CreateBooking(Guid petId, DateTime startTime, DateTime endTime, Guid? employeeCheckInId)
        {
            if (startTime >= endTime)
            {
                return Content("Error: start time must be before end time.");
            }

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                // check if pet exists
                PetModel pet = dbContext.PetModels.FirstOrDefault(x => x.PetID == petId);
                if (pet == null)
                {
                    return Content("Error: Pet not found. A booking must be linked to valid pet.");
                }

                // ceate new booking
                BookingModel booking = new BookingModel
                {
                    PetID = petId,
                    BookingStartTime = startTime,
                    BookingEndTime = endTime,
                    EmployeeCheckInID = employeeCheckInId,
                    Status = BookingStatus.Scheduled
                };

                dbContext.BookingModels.Add(booking);

                // step 4 - save changes
                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Content("Error: " + ex.Message);
                }

                return Content("Booking created successfully! Booking ID: " + booking.BookingID);
            }
        }
        public ActionResult CheckInBooking(Guid bookingId, Guid employeeId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                BookingModel booking = dbContext.BookingModels.FirstOrDefault(x => x.BookingID == bookingId);

                if (booking == null)
                {
                    return Content("Error: Booking not found.");
                }

                if (booking.EmployeeCheckInID != null)
                {
                    return Content("Error: Booking has already been checked in.");
                }

                booking.EmployeeCheckInID = employeeId;
                booking.ActualBookingStartTime = DateTime.UtcNow;
                booking.Status = BookingStatus.InProgress;

                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Content("Error: " + ex.Message);
                }

                return Content("Check-in successful.");
            }
        }

        public ActionResult CheckOutBooking(Guid bookingId, Guid employeeId)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                BookingModel booking = dbContext.BookingModels.FirstOrDefault(x => x.BookingID == bookingId);

                if (booking == null)
                {
                    return Content("Error: Booking not found.");
                }

                if (booking.EmployeeCheckOutID != null)
                {
                    return Content("Error: Booking has already been checked out.");
                }

                booking.EmployeeCheckOutID = employeeId;
                booking.ActualBookingEndTime = DateTime.UtcNow;
                booking.Status = BookingStatus.Completed;

                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Content("Error: " + ex.Message);
                }
                return Content("Check-out successful.");
            }
        }

        public ActionResult ReadBooking(Guid bookingId)
        {
            // step 1 - connect to database
            ApplicationDbContext dbContext = new ApplicationDbContext();

            // step 2 - find the booking
            BookingModel booking = dbContext.BookingModels.FirstOrDefault(x => x.BookingID == bookingId);

            // step 3 - validate and return result
            if (booking == null)
            {
                return Content("No booking found for that ID.");
            }
            return Content("Booking - ID: " + booking.BookingID +
                  " Pet ID: " + booking.PetID +
                  " Start Time: " + booking.BookingStartTime +
                  " End Time: " + booking.BookingEndTime +
                  " Employee Check-In ID: " + (booking.EmployeeCheckInID ?? Guid.Empty) +
                  " Employee Check-Out ID: " + (booking.EmployeeCheckOutID ?? Guid.Empty) +
                  " Status: " + booking.Status);
        }

        public ActionResult UpdateBooking(Guid bookingId, DateTime newStartTime, DateTime newEndTime)
        {
            if (newStartTime >= newEndTime)
            {
                return Content("Error: start time must be before end time.");
            }

            // step 1 - connect to database
            ApplicationDbContext dbContext = new ApplicationDbContext();

            // step 2 - find the booking
            BookingModel booking = dbContext.BookingModels.FirstOrDefault(x => x.BookingID == bookingId);

            if (booking == null)
            {
                return Content("No booking found for that ID.");
            }

            // step 3 - update details
            booking.BookingStartTime = newStartTime;
            booking.BookingEndTime = newEndTime;

            // step 4 - save changes
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }

            return Content("Booking updated successfully!");
        }

        public ActionResult DeleteBooking(Guid bookingId, CancellationReasonType reason)
        {
            // step 1 - connect to database
            ApplicationDbContext dbContext = new ApplicationDbContext();

            // step 2 - find the booking
            BookingModel booking = dbContext.BookingModels.FirstOrDefault(x => x.BookingID == bookingId);

            if (booking == null)
            {
                return Content("No booking found for that id.");
            }

            // step 3 - update cancellation details
            booking.Status = BookingStatus.Cancelled;
            booking.CancellationDate = DateTime.UtcNow;
            booking.CancellationReason = reason;

            // step 4 - save changes
            try
            {
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
            return Content("Booking detered successfully!");
        }

        public ActionResult DeleteBookingAll(Guid bookingId)
        {
            // step 1 - connect to the database
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                // step 2 - find the booking using the bookingId
                BookingModel booking = dbContext.BookingModels.FirstOrDefault(x => x.BookingID == bookingId);

                if (booking == null)
                {
                    return Content("No booking found for that ID.");
                }

                // step 3 - remove the booking from the context
                dbContext.BookingModels.Remove(booking);

                // step 4 - save the changes to the database
                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Content("Error: " + ex.Message);
                }
                // return confirmation of successful deletion
                return Content("Booking successfully deleted.");
            }
        }
        public ActionResult ContactUs()
        {
            ContactUsSubmissionVM ContactUsSubmissionVM = new ContactUsSubmissionVM();
            ContactUsSubmissionVM.FirstName = string.Empty;
            ContactUsSubmissionVM.LastName = string.Empty;
            ContactUsSubmissionVM.Email = string.Empty;
            ContactUsSubmissionVM.Message = string.Empty;
            return View(ContactUsSubmissionVM);
        }

        [HttpPost]
        public ActionResult ContactUs(ContactUsSubmissionVM contactUsSubmissionVM)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            ContactUsModel contactUsModel = new ContactUsModel();
            contactUsModel.FirstName = contactUsSubmissionVM.FirstName;
            contactUsModel.LastName = contactUsSubmissionVM.LastName;
            contactUsModel.Email = contactUsSubmissionVM.Email;
            contactUsModel.Message = contactUsSubmissionVM.Message;
            dbContext.ContactUsModels.Add(contactUsModel);
            dbContext.SaveChanges();
            return RedirectToAction("ContactUsSubmission");
        }
        public ActionResult ContactUsSubmission()
        {
            return View();
        }

    }
}

