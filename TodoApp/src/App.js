import React, { useState, useEffect } from "react";
import "./App.css";

function App() {
  const [tasks, setTasks] = useState([]);
  const [newTask, setNewTask] = useState("");

  useEffect(() => {
    fetch("http://localhost:8080/api/tasks")
      .then((response) => response.json())
      .then((data) => setTasks(data));
  }, []);

  const addTask = () => {
    if (newTask.trim() !== "") {
      fetch("http://localhost:8080/api/tasks", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ title: newTask, isCompleted: false }),
      })
        .then((response) => response.json())
        .then((data) => {
          setTasks([...tasks, data]);
          setNewTask("");
        });
    }
  };

  const toggleTaskCompletion = (taskId) => {
    const updatedTasks = tasks.map((task) =>
      task.id === taskId ? { ...task, isCompleted: !task.isCompleted } : task
    );

    fetch(`http://localhost:8080/api/tasks/${taskId}`, {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(updatedTasks.find((task) => task.id === taskId)),
    }).then(() => setTasks(updatedTasks));
  };

  const deleteTask = (taskId) => {
    fetch(`http://localhost:8080/api/tasks/${taskId}`, {
      method: "DELETE",
    }).then(() => {
      debugger;
      setTasks(tasks.filter((task) => task.id !== taskId));
    });
  };

  return (
    <div className="App">
      <h1>To-Do List</h1>
      <div className="task-form">
        <input
          type="text"
          placeholder="Add a new task"
          value={newTask}
          onChange={(e) => setNewTask(e.target.value)}
        />
        <button onClick={addTask}>Add</button>
      </div>
      <ul className="task-list">
        {tasks.map((task) => (
          <li
            key={task.id}
            onClick={() => toggleTaskCompletion(task.id)}
            className={task.isCompleted ? "completed" : ""}
          >
            {task.title}
            <button onClick={() => deleteTask(task.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
