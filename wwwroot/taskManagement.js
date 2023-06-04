// const uri = 'C:\\Users\\Dell Vostro\\Documents\\לימודים\\שנה ב\'\\Core\\4\\Data\\TODOlist.json';
const uri = "/TaskManagement";
const token = sessionStorage.getItem('auth');
const currentUser = parseJwt(token.split(' ')[1]);
const btnTask = document.getElementById("to-user");
if(currentUser.Classification === "admin")
    btnTask.innerHTML = "לניהול המשתמשים";
else
    btnTask.innerHTML = "לאזור האישי";
let tasks = [];


function getTasks() {
    fetch(uri,{headers: {'Authorization':token}})
        .then(response => {
       return response.json();
    })
        .then(data => {_displayTasks(data); })
        .catch(error => console.error('Unable to get items.', error));
}

function addTasks() {
    const addDescriptionNameTextbox = document.getElementById('add-description');

    const item = {
        id:(tasks.length+1).toString(),
        description: addDescriptionNameTextbox.value.trim(),
        status: false,
        user:currentUser.Id
    };

    fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': token,
            },
            body: JSON.stringify(item)
        })
       .then(res=>res.text())
        // .then(response => {alert(response.body.toString());return response.json()})
        .then((res) => {
            getTasks();
            addDescriptionNameTextbox.value = '';
        })
         .catch(error => console.error('Unable to add item.', error));
}

function deleteTasks(id) {
    fetch(`${uri}/${id}`, {
            method: 'DELETE',
            headers: {'Authorization': token,}
        })
        .then(() => getTasks())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = tasks.find(i => i.id === id.toString());
    document.getElementById('edit-description').value = item.description;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-status').checked = item.status;
    document.getElementById('editForm').style.display = 'block';
}

function updateTask() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        Id: itemId,
        Status: document.getElementById('edit-status').checked,
        Description: document.getElementById('edit-description').value.trim()
    };

    fetch(`${uri}/${itemId}`, {
            method: 'PUT',
            headers: {
                'Authorization': token,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        .then(() => getTasks())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'task' : 'tasks';

    document.getElementById('counter').innerText = `${itemCount} ${name} on your list`;
}

function _displayTasks(data) {
    
    // tasks = data;
   
    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let statusCheckbox = document.createElement('input');
        statusCheckbox.type = 'checkbox';
        statusCheckbox.disabled = true;
        statusCheckbox.checked = item.status;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteTasks(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(statusCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.description);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
        
    });

    tasks = data;

    
   
}

getTasks();

function parseJwt (token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}

