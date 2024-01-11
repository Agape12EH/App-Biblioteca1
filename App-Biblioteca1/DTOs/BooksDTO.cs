using App_Biblioteca1.Models;

namespace App_Biblioteca1.DTOs
{
    public class BooksDTO
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Gender { get; set; }
        public DateOnly? AgePublication { get; set; }
        public BookStore Store { get; set; }
        public List<StateBookDTO> StateBook { get; set; }
    }
}
