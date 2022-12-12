const uriTodoItems = 'api/TodoItems';
const uriPets = 'api/Pets';
let todos = [];
let pets = [];

function getItems() {
    fetch(uriTodoItems)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');

    const item = {
        isComplete: false,
        name: addNameTextbox.value.trim()
    };

    fetch(uriTodoItems, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${uriTodoItems}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = todos.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-isComplete').checked = item.isComplete;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        isComplete: document.getElementById('edit-isComplete').checked,
        name: document.getElementById('edit-name').value.trim()
    };

    fetch(`${uriTodoItems}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'to-do' : 'to-dos';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    const tBody = document.getElementById('todos');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let isCompleteCheckbox = document.createElement('input');
        isCompleteCheckbox.type = 'checkbox';
        isCompleteCheckbox.disabled = true;
        isCompleteCheckbox.checked = item.isComplete;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(isCompleteCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    todos = data;
}

function getPets() {
    fetch(uriPets)
        .then(response => response.json())
        .then(data => _displayPets(data))
        .catch(error => console.error("Unable to get pets.", error));
}

function addPet() {
    const addNameTextbox = document.getElementById('add-petName');
    const addAlterTextbox = document.getElementById('add-Alter');
    const addArtTextbox = document.getElementById('add-Art');
    const addRasseTextbox = document.getElementById('add-Rasse');
    const addGeimpftCheckbox = document.getElementById('add-Geimpft');
    const addGeschlechtCheckbox = document.getElementById('add-Geschlecht');

    const pet = {
        name: addNameTextbox.value.trim(),
        Alter: addAlterTextbox.value.trim(),
        Art: addArtTextbox.value.trim(),
        rasse: addRasseTextbox.value.trim(),
        geimpft: addGeimpftCheckbox.checked,
        geschlecht: addGeschlechtCheckbox.value.trim()

    };

    fetch(uriPets, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(pet)
    })
        .then(response => response.json())
        .then(data => {
            if (data !== "") {
                console.log("Error");
                console.log(data);
            }
            else {
                console.log("After Insert: getPets()");
                getPets();

                addNameTextbox.value = '';
                addAlterTextbox.value = '';
                addArtTextbox.value = '';
                addRasseTextbox.value = '';
                addGeimpftCheckbox.value = false;
                addGeschlechtCheckbox.value = '';
            }
        })
        .catch(error => console.error('Unable to add pet.', error));
}

function deletePet(id) {
    fetch(`${uriPets}/${id}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
        .then(response => response.json())
        .then(data => {
            if (data !== "") {
                console.log("Error");
                console.log(data);
            }
            else {
                console.log("After Delete: getPets()");
                getPets();
            }
        })
        .catch(error => console.error('Unable to delete pet.', error));
}

function displayEditPetForm(id) {
    const pet = pets.find(pet => pet.id === id);

    document.getElementById('edit-petId').value = pet.id;
    document.getElementById('edit-petName').value = pet.name;
    document.getElementById('edit-Alter').value = pet.alter;
    document.getElementById('edit-Art').value = pet.art;
    document.getElementById('edit-Rasse').value = pet.rasse;
    document.getElementById('edit-Geimpft').checked = pet.geimpft;
    document.getElementById('edit-Geschlecht').value = pet.geschlecht;
    document.getElementById('editPetForm').style.display = 'block';
}

function updatePet() {
    //id NaN
    const petId = document.getElementById('edit-petId').value;

    const pet = {
        id: parseInt(petId, 10),
        name: document.getElementById('edit-petName').value.trim(),
        alter: document.getElementById('edit-Alter').value.trim(),
        art: document.getElementById('edit-Art').value.trim(),
        rasse: document.getElementById('edit-Rasse').value.trim(),
        geimpft: document.getElementById('edit-Geimpft').checked,
        geschlecht: document.getElementById('edit-Geschlecht').value.trim()

    };

    fetch(`${uriPets}/${petId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(pet)
    })
        .then(response => response.json())
        .then(data => {
            if (data !== "") {
                console.log("Error");
                console.log(data);
            }
            else {
                console.log("After Update: getPets()");
                getPets();
            }
        })
        .catch(error => console.error('Unable to update pet.', error));

    closePetInput();

    return false;
}

function closePetInput() {
    document.getElementById('editPetForm').style.display = 'none';
}

function _displayPetCount(petCount) {
    const name = (petCount === 1) ? 'pet' : 'pets';

    document.getElementById('petCounter').innerText = `${petCount} ${name}`;
}

function _displayPets(data) {
    const tBody = document.getElementById('pets');
    tBody.innerHTML = '';

    _displayPetCount(data.length);

    const button = document.createElement('button');

    data.forEach(pet => {
        let geimpftCheckbox = document.createElement('input');
        geimpftCheckbox.type = 'checkbox';
        geimpftCheckbox.disabled = true;
        geimpftCheckbox.checked = pet.geimpft;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditPetForm(${pet.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deletePet(${pet.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(pet.name);
        td1.appendChild(textNode);

        let td2 = tr.insertCell(1);
        textNode = document.createTextNode(pet.alter);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        textNode = document.createTextNode(pet.art);
        td3.appendChild(textNode);

        let td4 = tr.insertCell(3);
        textNode = document.createTextNode(pet.rasse);
        td4.appendChild(textNode);

        let td5 = tr.insertCell(4);
        td5.appendChild(geimpftCheckbox);

        let td6 = tr.insertCell(5);
        textNode = document.createTextNode(pet.geschlecht);
        td6.appendChild(textNode);

        let td7 = tr.insertCell(6);
        td7.appendChild(editButton);

        let td8 = tr.insertCell(7);
        td8.appendChild(deleteButton);
    });

    pets = data;
}