using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Common;
using MoviesAPI.Models;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{

    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }       

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GetMoviesOnFilterCriteria([FromBody] MovieRequest movieRequest)
        {
            var result = await _movieService.GetMoviesOnFilterCriteria(movieRequest);
            return (result.MoviesResponses.FirstOrDefault().Errors == null || !result.MoviesResponses.FirstOrDefault().Errors.Any()) ? Ok(result.MoviesResponses) : ResponseStatusCodeParser.Parse(result.MoviesResponses.FirstOrDefault().Errors, result.MoviesResponses);

        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetMoviesBasedonRatings()
        {
            var result = await _movieService.GetMoviesOnTotalUsersRating();
            return (result.MoviesResponses.FirstOrDefault().Errors == null || !result.MoviesResponses.FirstOrDefault().Errors.Any()) ? Ok(result.MoviesResponses) : ResponseStatusCodeParser.Parse(result.MoviesResponses.FirstOrDefault().Errors, result.MoviesResponses);

        }

        [HttpGet]
        [Route("[action]/{user}")]
        public async Task<IActionResult> GetMoviesBasedonSpecificUserRatings(string user)
        {
            var result = await _movieService.GetMoviesOnUserRating(user);
            return (result.MoviesResponses.FirstOrDefault().Errors == null || !result.MoviesResponses.FirstOrDefault().Errors.Any()) ? Ok(result.MoviesResponses) : ResponseStatusCodeParser.Parse(result.MoviesResponses.FirstOrDefault().Errors, result.MoviesResponses);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddorUpdateMovieRating([FromBody] AddMovieRating addMovieRating)
        {
            var result = await _movieService.AddorUpdateMovieRating(addMovieRating);
            return (result.Errors == null || !result.Errors.Any()) ? Ok(result.Success) : ResponseStatusCodeParser.Parse(result.Errors, result);


        }

    }
}