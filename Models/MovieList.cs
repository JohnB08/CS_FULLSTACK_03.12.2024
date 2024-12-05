using System;

namespace CS_FULLSTACK_03._12._2024.Models;

public class MovieList:List<Movie>
{
    public IQueryable<Movie> QueryBuilder(MovieQueryParam queryParams)
    {
        var query = this.AsQueryable();
        if (!string.IsNullOrEmpty(queryParams.Title))
        {
            query = query.Where(movie => movie.Title.Contains(queryParams.Title, StringComparison.InvariantCultureIgnoreCase));
        }
        if (queryParams.Year.HasValue)
        {
            query = query.Where(movie => movie.Year == queryParams.Year);
        }
        if (queryParams.To.HasValue)
        {
            query = query.Where(movie => movie.Year <= queryParams.To);
        }
        if (queryParams.From.HasValue)
        {
            query = query.Where(movie => movie.Year >= queryParams.From);
        }
        if (!string.IsNullOrEmpty(queryParams.CastMember))
        {
            query = query.Where(movie => movie.Cast.Any(castName => castName.Contains(queryParams.CastMember, StringComparison.InvariantCultureIgnoreCase)));
        }
        if (!string.IsNullOrEmpty(queryParams.Extract))
        {
            query = query.Where(movie => movie.Extract.Contains(queryParams.Extract, StringComparison.InvariantCultureIgnoreCase));
        }
        if (!string.IsNullOrEmpty(queryParams.Genre))
        {
            query = query.Where(movie => movie.Genres.Any(genre => genre.Contains(queryParams.Genre, StringComparison.InvariantCultureIgnoreCase)));
        }
        return query;
    }
}
