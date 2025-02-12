using Microsoft.EntityFrameworkCore;
using DotnetMVC.Models.MicrosoftLearn;

namespace DotnetMVC.Data.MicrosoftLearn;

public class MvcMovieContext(DbContextOptions<MvcMovieContext> options) : DbContext(options)
{
    public DbSet<Movie> Movie { get; set; } = default!;
}

