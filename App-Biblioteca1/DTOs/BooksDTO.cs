﻿namespace App_Biblioteca1.DTOs
{
    public class BooksDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public string Gender { get; set; }
        public DateOnly AgePublication { get; set; }
        public int InventoryId { get; set; }
        public List<StateBookDTO> StateBook { get; set; }
        public List<LoanDTO> Loans { get; set; }
        public InventoryDTO Inventory { get; set; }
    }
}