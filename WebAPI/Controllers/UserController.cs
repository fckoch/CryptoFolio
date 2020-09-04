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

namespace CryptoFolioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;


        public UserController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        //GET (all users)

        [HttpGet]
        public async Task<ActionResult<UserModel[]>> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return _mapper.Map<UserModel[]>(users);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");  
            }
        }

        //GET (user by id)

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            try
            {

                return await _userService.GetUserAsync2<UserModel>(id);

                /*var user  = await _userService.GetUserAsync(id);

                if (user == null) return NotFound();

                return _mapper.Map<UserModel>(user);*/
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }


        //POST
    }
}