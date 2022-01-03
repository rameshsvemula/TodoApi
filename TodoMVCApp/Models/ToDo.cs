using System.ComponentModel.DataAnnotations;

namespace TodoMVCApp.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public int SerialNo { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
