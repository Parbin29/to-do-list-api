const callToDoAPI = async () => {
    document.getElementById("todo-list").innerHTML = "";
    // let searchTerm = document.getElementById("todo-name");
    let apiUrl = `http://localhost:5293/api/tasks`;

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