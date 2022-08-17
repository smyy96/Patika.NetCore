using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Command.RefreshToken
{
     public class RefreshTokenCommand
    {
        public string RefreshToken {get;set;}
        private readonly IBookStoreDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IBookStoreDbContext dBContext, IConfiguration configuration)
        {
            _dbContext = dBContext;
            _configuration = configuration;
        }

        public Token Handle()
        {
           
           var user=_dbContext.Users.FirstOrDefault(x=>x.RefreshToken==RefreshToken && x.RefreshTokenExpireDate>DateTime.Now);
           if(user is not null)
           {
               //token yarat
               TokenHandler handler = new TokenHandler(_configuration);
               Token token = handler.CreateAccessToken(user);

               user.RefreshToken = token.RefreshToken;
               user.RefreshTokenExpireDate=token.Expiration.AddMinutes(5); 
               _dbContext.SaveChanges();
               return token;
           }
            else
                throw new InvalidOperationException("Valid bir Refresh Token Bulunamadı.");
        }
    }
}