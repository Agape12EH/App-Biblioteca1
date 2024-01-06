namespace App_Biblioteca1.DTOs
{
    public class LoanDTO
    {
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public string LoanState { get; set; }
        public BooksDTO Books { get; set; }
        public UserDTO User { get; set; }
    }
}
