using App_Biblioteca1.Models.Enums;

namespace App_Biblioteca1.Models
{
    public class StateBook
    {
        public int Id { get; set; }
        public StatesOfBooks State { get; set; }
        public DateTime Registrationdate { get; set; }
        public string TakenActions { get; set; }
        //User Resposability
        public Guid IdUser { get; set; }
        public User UserActor { get; set; }
        //Book Affected
        public Guid IdBook { get; set; }
        public Books Book { get; set; }
    }
}
