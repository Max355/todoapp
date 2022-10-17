using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Models
{
    public class ToDoItem
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Назва не може бути пустим")]
        [MinLength(5, ErrorMessage = "Введіть мініму 5 символів")]
        [Display(Name = "Назва задачі")]
        public string Text { get; set; }

        [Display(Name = "Статус")]
        public bool IsComplete { get; set; }

        [Display(Name = "Дата виконання")]
        public DateTime CompleteDate { get; set; }
    }
}
