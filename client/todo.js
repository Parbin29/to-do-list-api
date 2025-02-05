const callToDoAPI = async () => {
  document.getElementById("todo-list").innerHTML = "";
  // let searchTerm = document.getElementById("todo-name");
  let apiUrl = `http://localhost:5293/api/Task`;

  let options = {
    method: "GET", // or POST, PUT, DELETE, etc.
    headers: {
      "Content-Type": "application/json",
    },
    mode: "no-cors", // Ensure mode is set to 'cors'
  };

  const response = await fetch(apiUrl, options);
  const data = await response.then((res) => res.json());
  console.log(data);
};

async function fetchTasks() {
    try {
        let response = await fetch("http://localhost:5293/api/Task");
        if (!response.ok) {
            throw new Error("Failed to fetch tasks");
        }
        let tasks = await response.json();
        let taskList = document.getElementById("taskList");
        taskList.innerHTML = "";
        tasks.forEach(task => {
            let li = document.createElement("li");
            li.innerHTML = `<span onclick="toggleTask(this)">${task.name}</span> | <span>Project ${task.projectId}</span> <button onclick="removeTask(this)">X</button>`;
            taskList.appendChild(li);
        });
    } catch (error) {
        console.error("Error fetching tasks:", error);
    }
}

const callCreateToDoAPI = async (taskName, projectId) => {
  // let searchTerm = document.getElementById("todo-name");
  let apiUrl = `http://localhost:5293/api/Task`;

  let task = {
    name: taskName,
    projectId: projectId,
  };

  let options = {
    method: "POST",
    headers: { 
        "Content-Type": "application/json",
        "Accept": "application/json"
    },
    body: JSON.stringify(task),
    // mode: "no-cors", // Ensure mode is set to 'cors'
  };

  try {
    let response = await fetch(apiUrl, options);

    if (!response.ok) {
      throw new Error("Failed to save task");
    }

    // const data = await response.then((res) => res.json());
    // console.log(data);
    let result = await response.json(); // Ensure you properly parse the response if needed
        console.log("Task saved:", result);

     return result;
  } catch (error) {
    console.error(error);
  }

};

const addTask = async () => {
  // Collect all inputs
  let taskInput = document.getElementById("taskInput");
  let taskText = taskInput.value.trim();
  if (taskText === "") return;

  let projectId = document.getElementById("projectId");
  let pId = projectId.value.trim() || 1;
  if (pId == 0) return;

  // Call the API to save the task
  newToDo = await callCreateToDoAPI(taskText, pId);

  // Add the task to the list
  if (newToDo == null) return;

  let li = document.createElement("li");
  li.innerHTML = `
  <span onclick="toggleTask(this)">${newToDo.name}</span> | <span>Project ${newToDo.projectId}</span>
  <button onclick="removeTask(this)">X</button>
  `;
//   li.innerHTML = `
//     <span onclick="toggleTask(this)">${taskText}</span>
//     <span> | Project ${pId}</span> 
//     <button onclick="removeTask(this)">X</button>
//     `;
  document.getElementById("taskList").appendChild(li);

  taskInput.value = "";
};

function removeTask(button) {
  button.parentElement.remove();
}

function toggleTask(span) {
  span.classList.toggle("completed");
}
