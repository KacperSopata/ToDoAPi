using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ToDoApi.Models
{
    public class ToDo
    {
        public DateTime ExpiryDate { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Range(0, 100)]
        public int PercentComplete { get; set; } = 0;
        public bool IsDone { get; set; } = false;
    }
}
