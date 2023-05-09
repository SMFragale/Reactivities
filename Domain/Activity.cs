namespace Domain
{
    public class Activity
    {
        //Must be of name Id for Entity framework to recognize it as the primary key
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description  { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue  { get; set; }
    }
}