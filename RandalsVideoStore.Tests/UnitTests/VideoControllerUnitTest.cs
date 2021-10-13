using System;
using Xunit;
using RandalsVideoStore.API.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using RandalsVideoStore.API.Ports;
using RandalsVideoStore.API.Domain;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RandalsVideoStore.Tests.UnitTests
{
    // Only partially tests the controller; the other endpoints are more of a reader's exercise. PR's welcome.
    // Don't pull this on me with your own project ;-).
    public class VideoControllerUnitTest
    {
        // Mock the logger as STDOUT/STDIN is *** slow.
        private Mock<ILogger<MovieController>> _mockedLogger = new Mock<ILogger<MovieController>>();
        // Notice that we don't care what our database implementation looks like, since we "mock" (i.e. fake) it.
        // We are testing the behaviour of our controller in this class, not of the database. 
        private Mock<IDatabase> _mockedDatabase = new Mock<IDatabase>();

        public VideoControllerUnitTest()
        {
            _mockedDatabase.Reset();
            _mockedLogger.Reset();
        }

        [Fact]
        public async Task TestGetById_HappyPath()
        {
            // arrange
            // this is our happy flow: we ask for the id of an existing application
            var ourId = Guid.NewGuid();
            var ourMovie = new Movie { Genres = API.Genre.Adventure | API.Genre.Drama, Id = ourId, Title = "yes", Year = 1234 };
            // set up the mock so that when we call the 'GetMovieById' method we return a predefined task
            // No database calls are happening here.
            _mockedDatabase.Setup(x => x.GetMovieById(ourId)).Returns(Task.FromResult(ourMovie));

            // act
            var controller = new MovieController(_mockedLogger.Object, _mockedDatabase.Object);
            var actualResult = await controller.GetById(ourId.ToString()) as OkObjectResult;

            // assert
            Assert.Equal(200, actualResult.StatusCode);
            var viewModel = actualResult.Value as ViewMovie;
            Assert.Equal(ourMovie.Genres, viewModel.Genres);
            Assert.Equal(ourMovie.Id.ToString(), viewModel.Id);
            Assert.Equal(ourMovie.Title, viewModel.Title);
            Assert.Equal(ourMovie.Year, viewModel.Year);

            _mockedLogger.VerifyAll();
            _mockedDatabase.VerifyAll();
        }

        [Fact]
        public async Task TestGetById_DoesntExist()
        {
            // arrange
            var ourId = Guid.NewGuid();
            var ourMovie = new Movie { Genres = API.Genre.Adventure | API.Genre.Drama, Id = ourId, Title = "yes", Year = 1234 };
            _mockedDatabase.Setup(x => x.GetMovieById(ourId)).Returns(Task.FromResult(null as Movie));

            // act
            var controller = new MovieController(_mockedLogger.Object, _mockedDatabase.Object);

            // assert
            var result = await new MovieController(_mockedLogger.Object, _mockedDatabase.Object).GetById(ourId.ToString());
            Assert.IsType<NotFoundResult>(result);

            _mockedLogger.VerifyAll();
            _mockedDatabase.VerifyAll();
        }

        [Fact]
        public async Task TestGetById_ErrorOnRetrievalAsync()
        {
            // arrange
            var ourId = Guid.NewGuid();
            var ourMovie = new Movie { Genres = API.Genre.Adventure | API.Genre.Drama, Id = ourId, Title = "yes", Year = 1234 };
            _mockedDatabase.Setup(x => x.GetMovieById(ourId)).ThrowsAsync(new Exception("drama"));

            // act
            var result = await new MovieController(_mockedLogger.Object, _mockedDatabase.Object).GetById(ourId.ToString());

            // assert
            Assert.IsType<BadRequestObjectResult>(result);
            _mockedLogger.VerifyAll();
            _mockedDatabase.VerifyAll();
        }
    }
}