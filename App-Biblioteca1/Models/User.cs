namespace App_Biblioteca1.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        //Loans
        public Guid? IdLoan { get; set; } //
        public List<Loan> Loans { get; set; }
    }
}
