using junto_test_api.Domain;
using junto_test_api.Domain.Service;
using junto_test_api.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace junto_test_api.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService<UserViewModel, User> _userService;
        private readonly TokenService<TokenViewModel, Token> _tokenService;

        public UserController(UserService<UserViewModel, User> userService, TokenService<TokenViewModel, Token> tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult GetAll([FromHeader] string token)
        {
            if (!ValidateToken(token))
                return Unauthorized();

            var items = _userService.GetAll();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id, [FromHeader] string token)
        {
            if (!ValidateToken(token))
                return Unauthorized();

            var item = _userService.GetOne(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserViewModel user, [FromHeader] string token)
        {
            if (user == null)
                return BadRequest();

            if (!ValidateToken(token))
                return Unauthorized();

            user.AccountId = GetAccountIdByToken(token);

            var id = _userService.Add(user);
            return Created($"api/User/{id}", id);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UserViewModel user, [FromHeader] string token)
        {
            if (user == null || user.Id != id)
                return BadRequest();

            if (!ValidateToken(token))
                return Unauthorized();

            user.AccountId = GetAccountIdByToken(token);

            int userUpdated = _userService.Update(user);

            if (userUpdated == 0)
                return StatusCode(304);
            else if (userUpdated == -1)
                return StatusCode(412, "Impossible Update"); 
            else
                return Accepted();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromHeader] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return BadRequest();

            if (!ValidateToken(token))
                return Unauthorized();

            int retVal = _userService.Remove(id);
            if (retVal == 0)
                return NotFound();  
            else if (retVal == -1)
                return StatusCode(412, "Impossible Delete"); 
            else
                return Ok();
        }

        [HttpPut]
        public IActionResult ChangePassword([FromBody] ChangePasswordViewModel newPass, [FromHeader] string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return BadRequest();

            if (!ValidateToken(token))
                return Unauthorized();

            if (newPass == null)
                return BadRequest();

            var userUpdated = _userService.ChangePassword(newPass);

            if (!userUpdated)
                return BadRequest();
            else
                return Ok(new { Message = "Password Changed" });
        }


        private bool ValidateToken(string token)
        {
            return _tokenService.ValidateToken(token);
        }

        private int GetAccountIdByToken(string token)
        {
            return _tokenService.GetAccountIdByToken(token);
        }
    }
}


