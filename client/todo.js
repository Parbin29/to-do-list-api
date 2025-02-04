const callToDoAPI = async () => {
    document.getElementById("todo-list").innerHTML = "";
    // let searchTerm = document.getElementById("todo-name");
    let apiUrl = `http://localhost:5293/api/Task`;

    let options = {
        method: 'GET', // or POST, PUT, DELETE, etc.
        headers: {
            'Content-Type': 'application/json',
        },
        mode: 'no-cors', // Ensure mode is set to 'cors'
    };
    
    const response = await fetch(apiUrl, options);
    const data = await response.then(res => res.json());
       console.log(data);
}

function addTask() {
    let taskInput = document.getElementById("taskInput");
    let taskText = taskInput.value.trim();
    if (taskText === "") return;
    
    let li = document.createElement("li");
    li.innerHTML = `<span onclick="toggleTask(this)">${taskText}</span> <button onclick="removeTask(this)">X</button>`;
    document.getElementById("taskList").appendChild(li);
    
    taskInput.value = "";
}

function removeTask(button) {
    button.parentElement.remove();
}

function toggleTask(span) {
    span.classList.toggle("completed");
}