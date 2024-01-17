namespace App_Biblioteca1.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        //Loans
        public HashSet<Loan> Loans { get; set; }
        public HashSet<StateBook> StateBooks { get; set; }


    }
}
