using System;
using RandalsVideoStore.API.Domain;

namespace RandalsVideoStore.API.Controllers
{
    // DTO stands for Data Transfer Object; these are dumb classes that should only be used
    // for transferring data between layers of the application.
    public class CreateMovie
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public Genre[] Genres { get; set; }

        public Movie ToMovie() => new Movie(Guid.NewGuid(), Title, Year, Genres);
    }

    public class ViewMovie
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }

        public static ViewMovie FromModel(Movie movie) => new ViewMovie
        {
            Id = movie.Id.ToString(),
            Title = movie.Title,
            Year = movie.Year
        };
    }
}