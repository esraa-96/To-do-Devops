namespace TodoApi.Models
{
    public class TaskItem
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
