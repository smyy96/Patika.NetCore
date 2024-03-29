
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        public readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_mapper,_context);
            var obj=query.Handle();
            return Ok(obj);
        }

        [HttpGet("id")]
        public ActionResult GetGenresDetail(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_mapper,_context);
            query.GenreId=id;
            GetGenreDetailQueryValidator validations=new GetGenreDetailQueryValidator();
            validations.ValidateAndThrow(query);
            var obj=query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command= new CreateGenreCommand(_context);
            command.Model=newGenre;

            CreateGenreCommandValidator validations=new CreateGenreCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command =new UpdateGenreCommand(_context);
            command.GenreId=id;
            command.Model=updateGenre;

            UpdateGenreCommandValidator validations=new UpdateGenreCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command=new DeleteGenreCommand(_context);
            command.GenreId=id;

            DeleteGenreCommandValidator validations=new DeleteGenreCommandValidator();
            validations.ValidateAndThrow(command);
            
            command.Handle();
            return Ok();
        }
    }
}