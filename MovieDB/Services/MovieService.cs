
using Microsoft.EntityFrameworkCore;
using MovieDB.Data;
using MovieDB.DTOs;
using MovieDB.Entities;

namespace MovieDB.Services
{
    public class MovieService
    {
        private readonly MovieDbContext _context;

        public MovieService(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateMovie(
            int id,
            UpdateMovieDto dto)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return false;
            }

            movie.Title = dto.Title;
            movie.ReleaseYear = dto.ReleaseYear;
            movie.StudioId = dto.StudioId;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return false;
            }

            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
