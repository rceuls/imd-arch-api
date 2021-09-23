using System;
using RandalsVideoStore.API.Domain;

namespace RandalsVideoStore.API
{
    public static class MovieProvider
    {
        public static Movie[] StaticMovieList = new[] {
        new Movie(Guid.Parse("6f905dea-e152-45e3-b001-d7d753e11fa4"), "2001: A Space Odyssey", 1968, new[] { Genre.Adventure, Genre.SciFi }),
        new Movie(Guid.Parse("c018db1b-6b86-45a1-b39d-48bc5c6e52b5"), "The Last Picture Show", 1968, new[] { Genre.Drama, Genre.Romance }),
        new Movie(Guid.Parse("bcc41859-3dd7-482e-b414-8ceff1dfe6b8"), "Chinatown", 1974, new[] { Genre.Drama, Genre.Mystery, Genre.Thriller }),
        new Movie(Guid.Parse("26b6ff89-3ca6-4e67-a014-baec15e8d723"), "The Rocky Horror Picture Show", 1975, new [] { Genre.Comedy, Genre.Horror, Genre.Musical })
        };
    }
}
