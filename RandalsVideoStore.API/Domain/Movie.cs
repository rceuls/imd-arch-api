using System;
using System.Text.Json.Serialization;

namespace RandalsVideoStore.API.Domain
{
    public class Movie
    {
        public Guid Id { get; }
        public int Year { get; }
        public string Title { get; }
        public Genre[] Genres { get; }

        public Movie(Guid id, string title, int year, Genre[] genres)
        {
            Id = id;
            Title = title;
            Year = year;
            Genres = genres;
        }
    }
}
