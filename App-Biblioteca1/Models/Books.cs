namespace App_Biblioteca1.Models
{
    public class Books
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Gender { get; set; }
        public DateOnly AgePublication { get; set; }

        //Inventories
        public int StoreId { get; set; }
        public BookStore Store { get; set; }

        //registeres and Loans
        public List<StateBook> StateBook { get; set; }
        public List<Loan> Loans { get; set; }
    }
}
