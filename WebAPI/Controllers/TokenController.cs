using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TokenController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generate Token
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="role">角色("Client","System","Admin")</param>
        /// <param name="expiresMinutes">过期时间</param>
        /// <returns></returns>
        [HttpGet]
        public string Get(int id = 1, string role = "Client", int expiresMinutes = 30)
        {
            if (true)//用户验证
            {
                // push the user’s name into a claim, so we can identify the user later on.
                var claims = new[]
                {
                   new Claim(ClaimTypes.Sid,id.ToString()),
                   new Claim(ClaimTypes.Role,role),
               };
                //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
                /**
                 * Claims (Payload)
                    Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                    iss(Issuser)：代表这个JWT的签发主体；
                    sub(Subject)：代表这个JWT的主体，即它的所有人；
                    aud(Audience)：代表这个JWT的接收对象；
                    exp(Expiration time)：是一个时间戳，代表这个JWT的过期时间；
                    nbf(Not Before)：是一个时间戳，代表这个JWT生效的开始时间，意味着在这个时间之前验证JWT是会失败的；
                    iat(Issued at)：是一个时间戳，代表这个JWT的签发时间；
                    jti(JWT ID)：是JWT的唯一标识。
                    除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
                 * */
                var token = new JwtSecurityToken(
                    issuer: "api." + _configuration["Domain"],
                    audience: _configuration["Domain"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(expiresMinutes),
                    signingCredentials: creds);

                return "Bearer " + new JwtSecurityTokenHandler().WriteToken(token);

            }
        }
    }
}