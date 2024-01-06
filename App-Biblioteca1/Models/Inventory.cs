namespace App_Biblioteca1.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public DateTime dateInventory { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantityTotal { get; set; }

        //sames Books       
        public List<Guid> IdBook { get; set; }
        public List<Books> Books { get; set; } 
    }
}
