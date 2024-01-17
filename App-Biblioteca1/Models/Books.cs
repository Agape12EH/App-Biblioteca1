using System.ComponentModel.DataAnnotations;

namespace App_Biblioteca1.Models
{
    public class Books
    {
        
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Author { get; set; }

        public string ISBN { get; set; } //consulta tabla inventories y crea un invenotires y se agrega
        public string Gender { get; set; }
        public DateOnly? AgePublication { get; set; }

        //Inventories
        public int StoreId { get; set; } //calculado con el valor retornado cuando crea el inventory
                                         //proceso logico de agregar libro
        public Byte Delete {  get; set; } 
        public BookStore Store { get; set; }   //registro store traido

        //registeres and Loans
        public HashSet<StateBook> StateBooks { get; set; }
    }
}
