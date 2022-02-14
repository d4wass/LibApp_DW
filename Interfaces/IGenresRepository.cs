using System.Collections.Generic;
using LibApp.Models;

namespace LibApp.Interfaces;

public interface IGenresRepository
{
    IEnumerable<Genre> GetGenres();
    Genre GetGenreById(byte genreId);
    void AddGenre(Genre genre);
    void UpdateGenre(Genre genre);
    void DeleteGenre(Genre genre);
    void Save();
}