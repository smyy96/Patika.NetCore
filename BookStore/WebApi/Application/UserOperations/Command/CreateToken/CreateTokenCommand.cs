using System;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Command.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model {get;set;}
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IBookStoreDbContext dBContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
           
           var user=_dbContext.Users.FirstOrDefault(x=>x.Email==Model.Email && x.Password == Model.Password);
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
                throw new InvalidOperationException("Kullanıcı adı / şifre hatalı.");
        }
    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}