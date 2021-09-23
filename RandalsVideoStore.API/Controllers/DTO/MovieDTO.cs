namespace RandalsVideoStore.API.Controllers
{
    public class CreateMovie
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public Genre[] Genres { get; set; }
    }
}