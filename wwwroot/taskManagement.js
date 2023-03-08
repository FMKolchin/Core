// const uri = 'C:\\Users\\Dell Vostro\\Documents\\לימודים\\שנה ב\'\\Core\\4\\Data\\TODOlist.json';
const uri = "/TaskManagement";
let tasks = [];

function getTasks() {
    fetch(uri)
        .then(response => response.json())
        .then(data => {_displayTasks(data); })
        .catch(error => console.error('Unable to get items.', error));
}

function addTasks() {
    const addDescriptionNameTextbox = document.getElementById('add-description');

    const item = {
        Status: false,
        Description: addDescriptionNameTextbox.value.trim(),
        Id:tasks.length+1
    };

    fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        .then(response => response.json())
        .then(() => {
            getTasks();
            addDescriptionNameTextbox.value = '';
        })
         .catch(error => console.error('Unable to add item.', error));
}

function deleteTasks(id) {
    fetch(`${uri}/${id}`, {
            method: 'DELETE'
        })
        .then(() => getTasks())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = tasks.find(item => item.id === id);

    document.getElementById('edit-description').value = item.description;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-status').checked = item.status;
    document.getElementById('editForm').style.display = 'block';
}

function updateTask() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        Id: parseInt(itemId, 10),
        Status: document.getElementById('edit-status').checked,
        Description: document.getElementById('edit-description').value.trim()
    };

    fetch(`${uri}/${itemId}`, {
            method: 'PUT',
            headers: {
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

