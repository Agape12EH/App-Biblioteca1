using App_Biblioteca1.Models.Enums;

namespace App_Biblioteca1.Models
{
    public class Loan
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime ExpectedReturnDate { get; set; } //calculado
        public DateTime? CurrentReturnDate { get; set; }//calculado
        public StatesOfLoans LoanState { get; set; } = new StatesOfLoans(); //calculado

        //Books
        public List<Books> Books { get; set; } = new List<Books>();

        //User
        public User User { get; set; } = new User();
    }
}
