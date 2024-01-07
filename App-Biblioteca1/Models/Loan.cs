using App_Biblioteca1.Models.Enums;

namespace App_Biblioteca1.Models
{
    public class Loan
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime LoanDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? CurrentReturnDate { get; set; }
        public StatesOfLoans LoanState { get; set; }

        //Books
        public Guid guidBooks { get; set; }
        public List<Books> Books { get; set; }

        //User
        public Guid guidUser { get; set; }
        public User User { get; set; }
    }
}
