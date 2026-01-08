namespace BusinessLogicLayer.Dtos
{
    public class BookCteateDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public int CategoryId { get; set; }
    }

    public class BookUptadeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int PublishedYear { get; set; }
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; }


    }
}
