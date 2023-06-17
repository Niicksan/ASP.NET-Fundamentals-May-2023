namespace TaskBoard.ViewModels.Board
{
    using System.Collections.Generic;
    using TaskBoard.ViewModels.Task;

    public class BoardAllViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public IEnumerable<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
    }
}
