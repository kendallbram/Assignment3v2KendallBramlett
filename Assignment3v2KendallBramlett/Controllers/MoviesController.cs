using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment3v2KendallBramlett.Data;
using Assignment3v2KendallBramlett.Models;
using Tweetinvi;
using VaderSharp2;

namespace Assignment3v2KendallBramlett.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetMoviePoster(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            var imageData = movie.MoviePoster;
            return File(imageData, "image/jpg");
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
              return View(await _context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            MovieDetailsVM movieDetailsVM = new MovieDetailsVM();
            movieDetailsVM.Movie = movies;
            movieDetailsVM.Tweets = new List<MovieTweet>();

            var userClient = new TwitterClient("AAx9UfdCemph0Pg0t8Moq5c6L", "LbhoERpFGjBESYSNjTHuRvE0R80cGxZBx5lJWanM5lFpO2Hs63", "1455230009153503238-WTxQgoYUAQ3D9PTSsUu8stHkmJvuVe", "2ZVnM9tWbCSNAhyJcyC4WPIgiIbUWZ77MTLSx2Qb8TkW3");
            var searchResponse = await userClient.SearchV2.SearchTweetsAsync(movies.Title);
            var tweets = searchResponse.Tweets;
            var analyzer = new SentimentIntensityAnalyzer();
            foreach (var tweetText in tweets)
            {
                var tweet = new MovieTweet();
                tweet.TweetText = tweetText.Text;
                var results = analyzer.PolarityScores(tweet.TweetText);
                tweet.Sentiment = results.Compound;
                movieDetailsVM.Tweets.Add(tweet);
            }

            return View(movieDetailsVM);
        }


        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Create([Bind("Id,Title,IMBDLink,Genre,ReleaseYear,MoviePoster")] Movies movies, IFormFile MoviePoster)
        {
            if (ModelState.IsValid)
            {
                /*if (MoviePoster != null && MoviePoster.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await MoviePoster.CopyToAsync(memoryStream);
                    movies.MoviePoster = memoryStream.ToArray();
                }*/

                _context.Add(movies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }
            return View(movies);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,IMBDLink,Genre,ReleaseYear,MoviePoster")] Movies movies, IFormFile MoviePoster)
        {
            if (MoviePoster != null && MoviePoster.Length > 0)
            {
                var memoryStream = new MemoryStream();
                await MoviePoster.CopyToAsync(memoryStream);
                movies.MoviePoster = memoryStream.ToArray();
            }
           
            if (id != movies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviesExists(movies.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movies);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
            }
            var movies = await _context.Movies.FindAsync(id);
            if (movies != null)
            {
                _context.Movies.Remove(movies);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviesExists(int id)
        {
          return _context.Movies.Any(e => e.Id == id);
        }
    }
}
