namespace App_Biblioteca1.Models
{
    public class BookStore
    {
        public int Id { get; set; }
        public DateTime DateStored { get; set; }
        public int QuantityTotal { get; set; }

        //sames Books       
        public Guid IdBook { get; set; }
        public List<Books> Books { get; set; } 
    }
}
