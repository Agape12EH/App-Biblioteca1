using System.ComponentModel.DataAnnotations;

namespace App_Biblioteca1.Models
{
    public class Books
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; } //consulta tabla inventories y crea un invenotires y se agrega
        public string Gender { get; set; }
        public DateOnly AgePublication { get; set; }

        //Inventories
        public int StoreId { get; set; } //calculado con el valor retornado cuando crea el inventory
                                         //proceso logico de agregar libro
        public BookStore Store { get; set; } //registro store traido
            
        //registeres and Loans
        public int StateBookId {  get; set; } //proceso logico de gestion de libro
        public List<StateBook> StateBook { get; set; } //cuando agrega un libro se crea registro en statebook con el nombre del operario, 
        //y la accion de agregar libro con el estado disponible
        public List<Loan> Loans { get; set; } //puede ser null
    }
}
