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

        private readonly ILogger<MovieController> _logger;

        public MovieController(ILogger<MovieController> logger) => _logger = logger;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(string titleStartsWith) => Ok(MovieProvider.StaticMovieList.FirstOrDefault(x => x.Title.StartsWith(titleStartsWith ?? string.Empty, true, CultureInfo.InvariantCulture)));

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
                    return Ok(movie);
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
                var createdMovie = new Movie(Guid.NewGuid(), movie.Title, movie.Year, movie.Genres);
                return CreatedAtAction(nameof(GetById), new { id = createdMovie.Id.ToString() }, createdMovie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
