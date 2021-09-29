using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandalsVideoStore.API.Domain;

namespace RandalsVideoStore.API.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MovieController : ControllerBase
    {

        // everything you use on _logger will end up on STDOUT (the terminal where you started your process)
        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger) => _logger = logger;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // when we got a single result
        [ProducesResponseType(StatusCodes.Status204NoContent)] // no results
        public IActionResult Get(string titleStartsWith) =>
            Ok(MovieProvider.StaticMovieList
                .Select(ViewMovie.FromModel)
                .FirstOrDefault(x => x.Title.StartsWith(titleStartsWith ?? string.Empty, true, CultureInfo.InvariantCulture)));

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(string id)
        {
            try
            {
                var movie = MovieProvider.StaticMovieList.FirstOrDefault(x => x.Id == Guid.Parse(id));
                if (movie != null)
                {
                    return Ok(ViewMovie.FromModel(movie));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // This is just good practice; you never want to expose a raw exception message. There are some libraries/services to handle this
                // but it's better to take full control of your code.
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteById(string id)
        {
            try
            {
                var movie = MovieProvider.StaticMovieList.FirstOrDefault(x => x.Id == Guid.Parse(id));
                if (movie != null)
                {
                    _logger.LogInformation($"Cool, going to delete {movie.Title} ({id})");
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateMovie(CreateMovie movie)
        {
            try
            {
                _logger.LogInformation($"Cool, creating a new movie");
                var createdMovie = movie.ToMovie();
                return CreatedAtAction(nameof(GetById), new { id = createdMovie.Id.ToString() }, ViewMovie.FromModel(createdMovie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
