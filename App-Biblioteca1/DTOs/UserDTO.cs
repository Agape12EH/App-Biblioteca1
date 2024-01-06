namespace App_Biblioteca1.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public List<LoanDTO> Loans { get; set; }
    }
}
