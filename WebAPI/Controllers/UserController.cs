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
                var user = await _userService.GetUserAsync(id);
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
                // Create new User
                var user = _mapper.Map<User>(model);
                user.Wallet = new Wallet();
                user.Role = "User";
                _userService.Add(user);

                if (await _userService.SaveChangesAsync())
                {
                    return Created($"api/user/{user.UserId}", _mapper.Map<OutputUserModel>(user));
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
                var token = await _userService.AuthenticateAsync(model.UserName, model.Password);

                if (token == null)
                    return BadRequest("Wrong user or password");

                return _mapper.Map<OutputAuthenticateModel>(token);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure - {ex.ToString()}");
            }
        }
    }
}