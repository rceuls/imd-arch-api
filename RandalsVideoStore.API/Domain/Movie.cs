using System;
using System.Text.Json.Serialization;

namespace RandalsVideoStore.API.Domain
{
    // this is a domain model. It contains the full representation of an entity within our domain.
    public class Movie
    {
        // A Guid is guaranteed to be unique.
        public Guid Id { get; }
        public int Year { get; }
        public string Title { get; }
        public Genre[] Genres { get; }

        // by exposing only this constructor we will get into trouble later (when we work with our ORM)
        //  but for now this is an accurate representation that all the fields are mandatory.

        // notice that if you want to validate data you also want to do this here (or in the builder, setter, ... )
        // it's the responsibility of your code to contain 
        public Movie(Guid id, string title, int year, Genre[] genres)
        {
            Id = id;
            Title = title;
            Year = year;
            Genres = genres;
        }
    }
}
