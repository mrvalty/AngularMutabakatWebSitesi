﻿using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Core.Extensions;
using eReconciliationProject.Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eReconciliationProject.Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        public TokenOptions _tokenOptions;

        DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims, int companyId,string companyName)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.Securitykey);
            var singingcredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, singingcredentials, operationClaims, companyId, companyName);
            var jwtsecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtsecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                CompanyId = companyId
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user, SigningCredentials signingCredentials, List<OperationClaim> operationClaims, int companyId,string companyName)
        {
            var jwt = new JwtSecurityToken(
                 issuer: tokenOptions.Issuer,
                 audience: tokenOptions.Audience,
                 expires: _accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: SetClaims(user, operationClaims, companyId, companyName),
                 signingCredentials: signingCredentials
                      );

            return jwt;
        }
        private IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims,int companyId,string companyName)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.Name}");
            claims.AddRoles(operationClaims.Select(x=>x.Name).ToArray());
            claims.AddCompany(companyId.ToString());
            claims.AddCompanyName(companyName);

            return claims; 
        }
    }
}
