namespace TaskBoard.ViewModels.Board
{
    using System.Collections.Generic;
    using TaskBoard.ViewModels.Task;

    public class BoardAllViewModel
    {
        public string Name { get; set; } = null!;

        public ICollection<TaskViewModel> Tasks { get; set; } = null!;
    }
}
