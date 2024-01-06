namespace App_Biblioteca1.DTOs
{
    public class StateBookDTO
    {
        public string State { get; set; }
        public DateTime Registrationdate { get; set; }
        public Guid guidBook { get; set; }
        public BooksDTO Book { get; set; }
    }
}
