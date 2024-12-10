namespace flashcard_app
{
    public class Flashcard
    {
        public required int Id { get; set; }
        public required string FrontText { get; set; }
        public required string BackText { get; set; }

    }

    public class Stack
    {
        public required int Id { get; set; }
        public required string Name { get; set; }

    }
}