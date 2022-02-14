using System.Collections.Generic;
using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;

namespace LibApp.Repositories;

public class GenresRepository : IGenresRepository
{
    private readonly ApplicationDbContext _context;
    public GenresRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void AddGenre(Genre genre)
    {
        _context.Genre.Add(genre);
    }

    public void DeleteGenre(Genre genre)
    {
        _context.Genre.Remove(genre);
    }

    public Genre GetGenreById(byte genreId)
    {
        return _context.Genre.Find(genreId);
    }

    public IEnumerable<Genre> GetGenres()
    {
        return _context.Genre;
    }

    public void UpdateGenre(Genre genre)
    {
        _context.Genre.Update(genre);
    }
    public void Save()
    {
        _context.SaveChanges();
    }
}