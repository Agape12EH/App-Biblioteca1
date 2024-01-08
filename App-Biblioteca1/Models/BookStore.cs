namespace App_Biblioteca1.Models
{
    public class BookStore
    {
        public int Id { get; set; } //Calculado
        public DateTime DateStored { get; set; }
        public int QuantityTotal { get; set; } = 1; //hace un count de la lista de libros agregados 

        //sames Books       
        public string isbnBook { get; set; } //campo que almacena el Isbn de libros repetidos y se nombra como tal
        public Guid BookId { get; set; }
        public List<Books> Books { get; set; } //campo que se trae los libros con el mismo Isbn correspondiente a la variable anterior
    }
}
