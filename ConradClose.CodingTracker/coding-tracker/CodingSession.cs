namespace coding_tracker
{
    public class CodingSession
    {
        public required int Id { get; set; }
        public required DateTime Date { get; set; }
        public required int Quantity { get; set; }
        public required string Unit { get; set; }
    }
}