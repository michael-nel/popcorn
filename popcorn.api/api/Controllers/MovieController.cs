using Domain.Commands;
using Domain.Commands.Movie.AddMovie;
using Domain.Commands.Movie.DeleteMovie;
using Domain.Commands.Movie.GetMovie;
using Domain.Commands.Movie.GetMovies;
using Domain.Commands.Movie.UpdateMovie;
using Infra.Repositories.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using UseCases.Interfaces;

namespace api.Controllers
{
    public class MovieController : Base.ControllerBase
    {
       
        private readonly IMovieUseCase _movieUseCase;

        public MovieController(IMovieUseCase movieUseCase, IUnitOfWork unitOfwork) : base(unitOfwork)
        {
            _movieUseCase = movieUseCase;
        }

        [Authorize]
        [HttpPost]
        [Route("api/movie")]
        public async Task<IActionResult> AddMovie([FromBody] AddMovieRequest request)
        {
            try
            {
                var response = await _movieUseCase.Add(request);
                return await ResponseAsync(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/movie")]
        public async Task<IActionResult> UpdateMovie([FromBody] UpdateMovieRequest request)
        {
            try
            {
               var response = await _movieUseCase.Update(request);
                return await ResponseAsync(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/movie/{id:Guid}")]
        public async Task<IActionResult> GetMovie(Guid id)
        {
            try
            {
                var request = new GetMovieRequest(id);
                var response = await _movieUseCase.Get(request);
                return await ResponseAsync(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/movie")]
        [Route("api/movie/{pages:int}")]
        public async Task<IActionResult> GetMovieAll(int pages)
        {
            try
            {
                var request = new GetMoviesRequest(pages);
                var response = await _movieUseCase.GetList(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("api/movie/{id:Guid}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            try
            {
                 var request = new DeleteMovieRequest(id);
                 var response = await _movieUseCase.Delete(request);
                 return await ResponseAsync(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


      
    }
}
