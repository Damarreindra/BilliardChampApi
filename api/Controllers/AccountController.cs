using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Dtos.UserDto;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{   
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        readonly private UserManager<AppUser> _userManager;
        readonly private ITokenService _tokenService;

        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
         _userManager = userManager;   
         _tokenService = tokenService;
         _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto){
            
            try{
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }
                var appUser = new AppUser{
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if(createdUser.Succeeded){
                   var addRoles = await _userManager.AddToRoleAsync(appUser, "Admin");
                   if(addRoles.Succeeded){
                    return Ok(
                        new NewUserDto{
                            Email = appUser.Email,
                            UserName = appUser.UserName,
                            Token = _tokenService.CreateToken(appUser)
                        }
                      
                    );
                   } 
                   else{
                    return StatusCode(500, addRoles.Errors);
                   }
                }else{
                    return StatusCode(500, createdUser.Errors);
                }
            }catch(Exception e){
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginDto loginDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Username);
            if(user == null){
                return BadRequest(new{message = "Invalid Username"});
            }
            
           var result =  await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded){
                return BadRequest(new{message = "Invalid Password"});
            }

            return Ok(
                new NewUserDto{
                    Token = _tokenService.CreateToken(user)
                }
            );


        }
    }
}