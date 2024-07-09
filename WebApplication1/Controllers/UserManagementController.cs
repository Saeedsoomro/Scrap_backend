using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using WebApplication1.Models;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
        
    public class UserMangementController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public UserMangementController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [Route("add_Address")]
        [HttpPost]
        public async Task<IActionResult> add_Address([FromBody] AddAdressDto addressDto)
        {
            if (addressDto == null)
            {
                return BadRequest("Address data is null.");
            }

            var address = new Address
            {
                UserId = addressDto.UserId,
                StreetName = addressDto.StreetName,
                City = addressDto.City,
                PostalCode = addressDto.PostalCode,
                State = addressDto.State,
                Number = addressDto.Number,
                FloorUnit = addressDto.FloorUnit
            };

            await _addressService.AddAddressAsync(address);
            return Ok(address);
        }

        [Route("set_Address")]
        [HttpPut()]
        public async Task<IActionResult> UpsertAddress(Guid userId, [FromBody] AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return BadRequest("Address data is null.");
            }
                
            var address = new Address
            {
                UserId = userId,
                StreetName = addressDto.StreetName,
                City = addressDto.City,
                PostalCode = addressDto.PostalCode,
                State = addressDto.State,
                Number = addressDto.Number,
                FloorUnit = addressDto.FloorUnit
            };

            await _addressService.UpsertAddressAsync(address);
            return Ok(address);
        }

        [Route("get_Address")]
        [HttpGet()]
        public async Task<IActionResult> get_Address(Guid userId)
        {
            if (userId == null)
            {
                return BadRequest("userId is null.");
            }
            var address = await _addressService.GetAddressByUserId(userId);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }

        [Route("update_User")]
        [HttpPut()]
        public async Task<IActionResult> update_User([FromBody] UpdateUserDto user)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponse
                    {
                        StatusCode = 400,
                        Message = "Model state is not validated",
                        Detailed = "none",
                        Success = false

                    });

                }

                var updatedUser = await _addressService.UpdateUser(user);
                if (updatedUser == null)
                {
                    return NotFound(new ErrorResponse
                    {
                        StatusCode = 400,
                        Message = "User not found",
                        Detailed = "none",
                        Success = false

                    });
                }
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating user details.");


            }

        }
    }
}
