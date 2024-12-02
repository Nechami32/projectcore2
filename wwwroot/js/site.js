const uri = 'http://localhost:5071/Glasses';
let glasses = [];

// function getItems() {
//     fetch(uri)
//         .then(response => response.json())
//         .then(data => _displayItems(data))
//         .catch(error => console.error('Unable to get items.', error));
// }
function getItems() {
    fetch(uri, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${localStorage.getItem("token")}`
        },
    })
        .then(response => response.json())
        .then(data => _displayItems(data))
        // .catch(error => console.error('Unable to get items.', error));
        .catch(error => {
            console.log(error);
            //location.href = "./login.html"

        });

}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');

    const item = {
        isInSale: false,
        name: addNameTextbox.value//.trim()
    };

    fetch(uri, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}`
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
    fetch(`${uri}/${id}`, {
            method: 'DELETE',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}`
            },
        })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = glasses.find(item => item.id === id);

    document.getElementById('edit-name').value = item.name;
    document.getElementById('edit-id').value = item.id;
    document.getElementById('edit-IsInSale').checked = item.isInSale;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-id').value;
    const item = {
        id: parseInt(itemId, 10),
        IsInSale: document.getElementById('edit-IsInSale').checked,
        name: document.getElementById('edit-name').value.trim()
    };
    fetch(`${uri}/${itemId}`, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${localStorage.getItem("token")}`
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
    const name = (itemCount === 1) ? 'glasses' : 'glasses kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    
    console.log(data);
    const tBody = document.getElementById('glasses');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let IsInSaleCheckbox = document.createElement('input');
        IsInSaleCheckbox.type = 'checkbox';
        IsInSaleCheckbox.disabled = true;
        IsInSaleCheckbox.checked = item.IsInSale;

        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(IsInSaleCheckbox);

        let td2 = tr.insertCell(1);
        let textNode = document.createTextNode(item.name);
        td2.appendChild(textNode);

        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });
    
    glasses = data;
}
    if (localStorage.getItem("token") == null) {
        console.log("login");
        sessionStorage.setItem("not", "not exist token")

       location.href = "./login.html"

    }

function createLink() {
    if (localStorage.getItem("link") == "true") {

        let link = document.createElement("a");
        link.href = "./userList.html";
        link.innerHTML = "users";
        console.log(sessionStorage.getItem("link"));
        document.body.appendChild(link);
    }
}
console.log(localStorage.getItem("token"));
const token=localStorage.getItem("token")
