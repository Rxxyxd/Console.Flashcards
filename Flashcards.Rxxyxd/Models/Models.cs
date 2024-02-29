namespace Flashcards.Rxxyxd.Models
{
    public class Flashcards
    {
        public int ID { get; set; }
        public string? Question { get; set; }
        public string? Answer { get; set; }
        public string? Stack { get; set; }
    } 

    public class Stacks
    {
        public int ID { get; set; }
        public string? Name { get; set; }
    }

}
