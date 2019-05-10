namespace angu.models
{
    public class Book
    {
        public long Id { get; set; }
        public string BookName { get; set; }
        public string Publisher { get; set; }
        public string Writer { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }  

        public User User { get; set; }
        public int UserId { get; set; }
    }
}