namespace App_Biblioteca1.DTOs
{
    public class InventoryDTO
    {
        public int Id { get; set; }
        public int QuantityAvailable { get; set; }
        public BooksDTO Book { get; set; }
    }
}
