namespace App_Biblioteca1.Models
{
    public class BookStore
    {
        public int Id { get; set; } //Calculado
        public DateTime DateStored { get; set; }
        public int QuantityTotal { get; set; } = 1; //hace un count de la lista de libros agregados 
        public string isbnBook { get; set; }

        //sames Books       
        public HashSet<Books> Books { get; set; }  //campo que se trae los libros con el mismo Isbn correspondiente a la variable anterior
    }
}
