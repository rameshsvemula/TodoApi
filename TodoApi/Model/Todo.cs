using System;
using System.Collections.Generic;

namespace TodoApi.Model
{
    public partial class Todo
    {
        public int Id { get; set; }
        public int SerialNo { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
