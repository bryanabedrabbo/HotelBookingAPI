using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Models;
using HotelBookingAPI.Data;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly APIContext _context;

        public HotelBookingController(APIContext context)
        {
            _context = context;
        }

        // Create/Edit
        [HttpPost]
        public JsonResult CreateEdit(HotelBooking booking) 
        {
            if (booking.Id == 0)
            {
                _context.Bookings.Add(booking);
            }
            else
            {
                var bookingInDb = _context.Bookings.Find(booking.Id);

                if (bookingInDb == null) return new JsonResult(NotFound());

                booking = bookingInDb;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(booking));

        }

        // Get 1
        [HttpGet]
        public JsonResult Get(int id)
        {
            var booking = _context.Bookings.Find(id);

            if (booking == null) return new JsonResult(NotFound());

            return new JsonResult(Ok(booking));
        }

        // Get All
        [HttpGet("/GetAll")]
        public JsonResult GetAll()
        {
            var booking = _context.Bookings.ToList();

            return new JsonResult(Ok(booking));
        }

        // Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var booking = _context.Bookings.Find(id);

            if (booking == null) return new JsonResult(NotFound());

            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

    }
}
