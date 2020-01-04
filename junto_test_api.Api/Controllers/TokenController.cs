
using junto_test_api.Api.Models;
using junto_test_api.Domain;
using junto_test_api.Domain.Service;
using junto_test_api.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace junto_test_api.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IConfiguration _config;
        private readonly TokenService<TokenViewModel, Token> _tokenService;

        public TokenController(IConfiguration config, TokenService<TokenViewModel, Token> tokenService)
        {
            _config = config;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Create([FromBody]CreateTokenModel createTokenModel)
        {
            IActionResult response = Unauthorized();
            var newToken = Authenticate(createTokenModel);

            if (newToken != null)
            {
                response = Ok(newToken);
            }

            return response;
        }

        private TokenModel Authenticate(CreateTokenModel createTokenModel)
        {
            var newToken = _tokenService.CreateNewToken(new CreateTokenViewModel() { IntegrationKey = createTokenModel.IntegrationKey });

            if (newToken == null)
                return null;

            return new TokenModel()
            {
                Key = newToken.Key,
                DueDate = newToken.DueDate
            };
        }
    }
}