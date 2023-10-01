using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private static readonly List<TaskItem> _tasks = new List<TaskItem>();

        public TasksController()
        {
            // Sample tasks for testing
            if (!_tasks.Any())
            {
                _tasks.Add(new TaskItem { Id = 1, Title = "Task 1", IsCompleted = false });
                _tasks.Add(new TaskItem { Id = 2, Title = "Task 2", IsCompleted = true });
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetTasks()
        {
            return _tasks.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetTask(long id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return task;
        }

        [HttpPost]
        public ActionResult<TaskItem> CreateTask(TaskItem task)
        {
            task.Title = $"task {task.Title}";
            task.Id = _tasks.Max(t => t.Id) + 1;
            _tasks.Add(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(long id, TaskItem task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == id);
            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.Title = task.Title;
            existingTask.IsCompleted = task.IsCompleted;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(long id)
        {
            var taskToRemove = _tasks.FirstOrDefault(t => t.Id == id);
            if (taskToRemove == null)
            {
                return NotFound();
            }

            _tasks.Remove(taskToRemove);
            return NoContent();
        }
    }
}
