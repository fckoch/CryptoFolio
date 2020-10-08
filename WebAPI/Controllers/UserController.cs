using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CryptoFolioAPI.Data;
using CryptoFolioAPI.Models.Entities;
using CryptoFolioAPI.Services;
using CryptoFolioAPI.Models;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CryptoFolioAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;


        public UsersController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        //GET (all users)
        [HttpGet]
        public async Task<ActionResult<OutputUserModel[]>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return _mapper.Map<OutputUserModel[]>(users);
            }
            catch (Exception ex)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }

        //GET (user by id)

        [HttpGet("{id:int}")]
        public async Task<ActionResult<OutputUserModel>> GetUser(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null) return NotFound();

                return _mapper.Map<OutputUserModel>(user);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }


        //POST (user)

        public async Task<ActionResult<OutputUserModel>> Post(InputUserModel model)
        {
            try
            {
                var user = _mapper.Map<User>(model);
                // Check if email already exists
                Console.WriteLine(user.Email);
                var usercheck = await _userService.GetUserByEmailAsync(user.Email);

                if (usercheck == null)
                {
                    // Create new User
                    user.Wallet = new Wallet();
                    user.Role = "User";
                    byte[] passwordHash, passwordSalt;
                    UserService.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;

                    _userService.Add(user);

                    if (await _userService.SaveChangesAsync())
                    {
                        return Created($"api/user/{user.UserId}", _mapper.Map<OutputUserModel>(user));
                    }

                }

                else
                {
                    return this.StatusCode(StatusCodes.Status400BadRequest, $"E-mail already in use");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }

            return BadRequest();
        }

        //Authenticate User

        [HttpPost("authenticate")]
        public async Task<ActionResult<OutputAuthenticateModel>> AuthenticateAsync (InputAuthenticateModel model)
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(model.Email);

                if (user == null || (!UserService.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt)))
                    return BadRequest("Wrong user or password");
                
                var token = _userService.Authenticate(model.Email, model.Password, user.Role, user.UserId, user.FirstName, user.Wallet.WalletId);

                if (token == null)
                    return BadRequest("Authentication error");

                return _mapper.Map<OutputAuthenticateModel>(token);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }
    }
}