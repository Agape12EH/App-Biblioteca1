using App_Biblioteca1.Models.Enums;

namespace App_Biblioteca1.Models
{
    public class Loan
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public StatesOfLoans LoanState { get; set; }

        //Books
        public List<Guid> guidsBooks { get; set; }
        public List<Books> Books { get; set; }

        //User
        public Guid guidBook { get; set; }
        public User User { get; set; }
    }
}
