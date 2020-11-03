// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
var path = "https://localhost:44319";

$(document).ready(function () {
    $.ajax({
        url: `${path}/api/user/`,
        type: 'GET',
        error: function (e) {
            console.log('Error' + e);
        }
    }).done(function (data) {
        var table = document.getElementById("userTable");
        var count = 0;

        for (let i = 0; i < data.length; i++) {
            var userRow = table.insertRow();
            AddUserRow(data, userRow, i);
            count++;
        }
    });
    $.ajax({
        url: `${path}/api/department/`,
        type: 'GET',
        error: function (e) {
            console.log('Error' + e);
        }
    }).done(function (data) {
        AddDepartmentToList("DropdownListUserAdd", data);
        AddDepartmentToList("DropdownListUserEdit", data);
    });
});


function AddUserRow(data, row, count) {
    row.id = `${data[count].id}`;

    const cellCount = row.insertCell();
    const cellName = row.insertCell();
    const cellLastName = row.insertCell();
    const cellPatronymic = row.insertCell();
    const cellDate = row.insertCell();
    const cellAddress = row.insertCell();
    const cellAbout = row.insertCell();
    const cellDepart = row.insertCell();
    const cellEdit = row.insertCell();
    const cellDelete = row.insertCell();

    cellCount.innerHTML = count;
    cellName.innerHTML = data[count].name;
    cellLastName.innerHTML = data[count].lastName;
    cellPatronymic.innerHTML = data[count].patronymic;
    cellDate.innerHTML = data[count].userInformation.dateOfBirth.toString().slice(0, -9);
    cellAddress.innerHTML = data[count].userInformation.addres;
    cellAbout.innerHTML = data[count].userInformation.about;
    cellDepart.innerHTML = data[count].userInformation.department.name;

    cellEdit.innerHTML = `<button id=\"Edit${count}\" class=\"btn-primary btn-xs\" onclick=\"EditUser('${row.id}')\">Edit</button>`;
    cellDelete.innerHTML = `<button id=\"Delete${count}\" class=\"btn-danger btn-xs\" onclick=\"DeleteUser('${row.id}')\">Delete</button>`;
}

function AddDepartmentToList(elementId, data) {
    var department = document.getElementById(elementId);

    for (var i = 0; i < data.length; i++) {
        department.innerHTML += `<option value=\"${data[i].id}\"> ${data[i].name}</option>`;
    }
}

function createNewUser() {
    const data = serialize("userForm");
    const jsonData = JSON.stringify(data);
    const URL = `${path}/api/user`;

    window.$.ajax({
        url: URL,
        type: 'POST',
        contentType: 'application/json',
        data: jsonData,
        error: function (e) {
            console.log('Error' + e);
        }
    }).done(function () {
        location.reload();
    });
}

function DeleteUser(userId) {
    const URL = `${path}/api/user/${userId}`;
    window.$.ajax({
        url: URL,
        type: 'DELETE',
        contentType: 'application/json',
        error: function (e) {
            console.log('Error' + e);
        }
    }).done(function (data) {
        location.reload();
        alert(data);
    });
}

function EditUser(userId) {
    var editButton = document.getElementById("editButton");

    editButton.onclick = function () { SendEditForm(userId) }

    var userInfo = document.getElementById(userId).cells;
    var userEditForm = document.getElementById("userEditForm");
    var dataLength = userInfo.length - 4;

    for (var i = 1; i <= dataLength; i++) {
        var cell = userInfo[i].innerText;
        userEditForm[i].value = cell;
    }
}

function SendEditForm(userId) {
    const dataToSend = serialize("userEditForm");
    const jsonData = JSON.stringify(dataToSend);
    const URL = `${path}/api/user/${userId}`;

    window.$.ajax({
        url: URL,
        type: 'PUT',
        contentType: 'application/json',
        data: jsonData,
        error: function (e) {
            console.log('Error' + e);
        }
    }).done(function (data) {
        location.reload();
        alert(data);
    });
}

function CreateDepartment() {
    const data = serialize("AddDepartment");
    const jsonData = JSON.stringify(data);
    const URL = `${path}/api/department`;

    window.$.ajax({
        url: URL,
        type: 'POST',
        contentType: 'application/json',
        data: jsonData,
        error: function (e) {
            console.log('Error' + e);
        }
    }).done(function () {
        location.reload();
    });
}

function serialize(id) {
    var elements = document.getElementById(id);
    var data = {};
    for (var i = 0; i < elements.length; i++) {
        var el = elements[i];
        var val = el.value;
        if (!val) val = "";
        var fullName = el.getAttribute("name");
        if (!fullName) continue;
        var fullNameParts = fullName.split('.');
        var prefix = '';
        var stack = data;
        for (var k = 0; k < fullNameParts.length - 1; k++) {
            prefix = fullNameParts[k];
            if (!stack[prefix]) {
                stack[prefix] = {};
            }
            stack = stack[prefix];
        }
        prefix = fullNameParts[fullNameParts.length - 1];
        if (stack[prefix]) {

            var newVal = stack[prefix] + ',' + val;
            stack[prefix] += newVal;
        } else {
            stack[prefix] = val;
        }
    }
    return data;
}