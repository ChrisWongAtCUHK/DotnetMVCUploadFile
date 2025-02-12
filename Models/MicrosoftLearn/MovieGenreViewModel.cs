using Microsoft.AspNetCore.Mvc.Rendering;

namespace DotnetMVC.Models.MicrosoftLearn;

public class MovieGenreViewModel
{
  public List<Movie>? Movies { get; set; }
  public SelectList? Genres { get; set; }
  public string? MovieGenre { get; set; }
  public string? SearchString { get; set; }
  public Movie? Movie { get; set; }
}