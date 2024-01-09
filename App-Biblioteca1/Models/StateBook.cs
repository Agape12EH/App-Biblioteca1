using App_Biblioteca1.Models.Enums;

namespace App_Biblioteca1.Models
{
    public class StateBook
    {
        public int Id { get; set; }
        public StatesOfBooks State { get; set; } = new StatesOfBooks(); //valor calculado cuando sea agregado al inventario empiza como disponible si cambian 
        public DateTime Registrationdate { get; set; } = DateTime.Now;
        public string TakenActions { get; set; }
        //User Resposability
        public User UserActor { get; set; } = new User();
        //Book Affected
        public Books Book { get; set; } = new Books();
    }
}
