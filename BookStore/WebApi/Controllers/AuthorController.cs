using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.AuthorOperation.Commands.CreatAuthor;
using WebApi.Application.AuthorOperation.Commands.DeleteAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.Application.AuthorOperation.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.UpdateOperation.Command.UpdateAuthor;
using WebApi.DBOperations;


namespace WebApi.Controllers
{

    [ApiController]
    [Route("[controller]s")]
    public class AuthorController : ControllerBase
    {
        public readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(BookStoreDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult GetAuthors()
        {
            GetAuthorQuery query = new GetAuthorQuery(_context, _mapper);
            var obj=query.Handle();
            return Ok(obj);
        }


        [HttpGet("{id}")]
        public ActionResult GetAuthorDetail(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context,_mapper);
            query.AuthorId=id;
            GetAuthorDetailQueryValidator validations=new GetAuthorDetailQueryValidator();
            validations.ValidateAndThrow(query);
            var obj = query.Handle();
            return Ok(obj);
        }


        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel newGenre)
        {
            CreateAuthorCommand command= new CreateAuthorCommand(_mapper,_context);
            command.Model = newGenre;

            CreateAuthorCommandValidator validations=new CreateAuthorCommandValidator();
            validations.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }


        [HttpDelete("{id}")] // Silme
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.authorId = id;
            DeleteAuthorCommandValidator cv = new DeleteAuthorCommandValidator(); //validator sınıfını calıştırma
            cv.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

         [HttpPut("{id}")] //güncelleme
        public IActionResult UpdateAuthor(int id,[FromBody] UpdateAuthorModel updatedAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.Authorid = id;
            command.Model = updatedAuthor;

            UpdateAuthorCommandValidator cv = new UpdateAuthorCommandValidator();
            cv.ValidateAndThrow(command);
            command.Handle();


            return Ok();

        }
    }
}