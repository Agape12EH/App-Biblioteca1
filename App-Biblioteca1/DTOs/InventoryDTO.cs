using App_Biblioteca1.Models;

namespace App_Biblioteca1.DTOs
{
    public class InventoryDTO
    {
        public int Id { get; set; }
        public DateTime DateStored { get; set; }
        public string isbnBook { get; set; }
        public int QuantityTotal { get; set; }
        public List<BooksDTO> Books { get; set; }
    }
}
