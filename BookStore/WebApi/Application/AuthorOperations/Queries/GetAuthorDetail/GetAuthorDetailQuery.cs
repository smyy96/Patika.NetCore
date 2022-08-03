using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorId { get; set; }
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorsDetailViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Yazar Bulunamadı.");
            return _mapper.Map<AuthorsDetailViewModel>(author);
        }
    }

    public class AuthorsDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        
    }
}