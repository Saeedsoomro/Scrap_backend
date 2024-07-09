using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTOs;
using WebApplication1.Repositries;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;
        private readonly ScrapMangementDbContext dbContext;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, ScrapMangementDbContext dbContext)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var existingUser = await userManager.FindByEmailAsync(registerRequestDto.Username);
            if (existingUser != null)
            {
                return BadRequest(new ErrorResponse{
                    StatusCode = 400,
                    Message ="User already exist!",
                    Detailed = "User already exist!",
                    Success=false

                });
            }
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
                PhoneNumber = registerRequestDto.PhoneNumber
            };

          
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRoleAsync(identityUser, registerRequestDto.Roles);

                    if (!identityResult.Succeeded)
                    {
                       return BadRequest(new ErrorResponse
                        {
                            StatusCode = 400,
                            Message = "Role does not exist",
                            Detailed = "none",
                            Success = false

                        });
                    }
                    if (identityResult.Succeeded)
                    {
                        var userManagement = new UserManagement
                        {
                            UserId = identityUser.Id,
                            FirstName = registerRequestDto.FirstName,
                            LastName = registerRequestDto.LastName,
                            PhoneNumber = registerRequestDto.PhoneNumber,
                            Email = registerRequestDto.Username
                        };
                        if(registerRequestDto.Roles == "User")
                        {
                            userManagement.RoleId = 1;
                        }else if(registerRequestDto.Roles == "Scrapper")
                        {
                            userManagement.RoleId= 3;
                        }
                        else if (registerRequestDto.Roles == "Admin")
                        {
                            userManagement.RoleId = 2;
                        }
                        else if (registerRequestDto.Roles == "SuperAdmin")
                        {
                            userManagement.RoleId = 4;
                        }

                        try
                        {
                            dbContext.UserManagements.Add(userManagement);
                            await dbContext.SaveChangesAsync();
                            return Ok(new ErrorResponse
                            {
                                StatusCode = 200,
                                Message = "User has been registered!, please login.",
                                Detailed = "none",
                                Success = true

                            });
                    
                        }
                        catch (Exception ex)    
                        {
                            // Log the exception here
                            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving user details.");
                        }

                       
                    }
                }
            }

            return BadRequest("Something went wrong");
        }


        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CraeteJwtToken(user, roles.ToList());

                        var response = new LoginReponseDto { JwtToken = jwtToken };

                        return Ok(response);
                    }

                }
            }

            return BadRequest("Invalid username or password");
        }


        [HttpGet]
        [Route("GetUser")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           ;  

            if (userId == null)
            {
                return Unauthorized(new { Message = "User not found" });
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var userDetails = new UserDetailsDto
            {
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = userRoles
            };



            return Ok(userDetails);
        }

    }
}
