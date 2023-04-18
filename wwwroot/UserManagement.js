
const uri = "/Users";
const token = sessionStorage.getItem('auth');
const user = parseJwt(token.split(' ')[1]);
//const currentUser = sessionStorage.getItem('user');
let users = [];

function getUsers() {
    fetch(uri,{headers: {'Authorization':token}})
        .then(response => {
       return response.json();
    })
        .then(data => {_displayUsers(data); })
        .catch(error => console.error('Unable to get items.', error));
}

function addUsers() {
    const addUserNameTextbox = document.getElementById('add-username');
    const addClassificationOpthinner = document.getElementById('add-classification');
    const addPasswordTextbox = document.getElementById('add-password');

    const item = {
        id: "",
        username: addUserNameTextbox.value.trim(),
        password: addPasswordTextbox.value.trim(),
        classification:addClassificationOpthinner.value
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
      // .then(res=>res.text())
        // .then(response => {alert(response.body.toString());return response.json()})
        .then(() => {
            getUsers();
            addUserNameTextbox.value = '';
            addPasswordTextbox.value = '';
            addClassificationOpthinner.value = "agent";
            
        })
         .catch(error => console.error('Unable to add item.', error));
}

function deleteUsers(id) {
    fetch(`${uri}/${id}`, {
            method: 'DELETE',
            headers: {'Authorization': token,}
        })
        .then(() => getUsers())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = users.find(i => i.id === id.toString());
    document.getElementById('edit-username').value = item.userName;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-password').value = item.password;
    document.getElementById('edit-classification').value = item.classification;
    document.getElementById('editForm').style.display = 'block';
}

function updateUser() {
    const itemId = document.getElementById('edit-id').value;

    const item = {
        Id: itemId,
        password: document.getElementById('edit-password').value.trim(),
        username: document.getElementById('edit-username').value.trim(),
        classification:document.getElementById('edit-classification').value
    };
    alert(`${item.Id},${item.username},${item.classification}`);
    fetch(`${uri}/${itemId}`, {
            method: 'PUT',
            headers: {
                'Authorization': token,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(item)
        })
        .then(() => getUsers())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'user' : 'users';

    document.getElementById('counter').innerText = `${itemCount} ${name} in your system`;
}

function _displayUsers(data) {
    
    // tasks = data;
   
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        // let statusCheckbox = document.createElement('input');
        // statusCheckbox.type = 'checkbox';
        // statusCheckbox.disabled = true;
        // statusCheckbox.checked = item.status;
        let agentOption = document.createElement('option');
        agentOption.value = "agent";
        agentOption.innerHTML = "agent";

        let adminOption = document.createElement('option');
        adminOption.value = "admin";
        adminOption.innerHTML = "admin";

        let classificationSelector = document.createElement('select');
        classificationSelector.appendChild(agentOption);
        classificationSelector.appendChild(adminOption);
        classificationSelector.disabled = true;
        classificationSelector.value = item.classification;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteUsers(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textIdNode = document.createTextNode(item.id);
        td1.appendChild(textIdNode);

        let td2 = tr.insertCell(1);
        let textUserNameNode = document.createTextNode(item.userName);
        td2.appendChild(textUserNameNode);

        let td3 = tr.insertCell(2);
        let textPasswordNode = document.createTextNode(item.password);
        td3.appendChild(textPasswordNode);

        let td4 = tr.insertCell(3);
        td4.appendChild(classificationSelector);



        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
        
    });

    users = data;

    
   
}

getUsers();

function parseJwt (token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}


