
using System.ComponentModel.DataAnnotations;

namespace QuizWeb.Models
{
    public class Question_Entity
    {
        
        [StringLength(300)]
        public string Question { get; set; }
        [StringLength(255)]
        public string Option1 { get; set; }
        [StringLength(255)]
        public string Option2 { get; set; }
        [StringLength(255)]
        public string Option3 { get; set; }
        [StringLength(255)]
        public string Option4 { get; set; }
        [StringLength(255)]
        public string Answer { get; set; }
    }
}
