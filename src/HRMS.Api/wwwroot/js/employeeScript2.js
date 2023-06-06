async function getEmployeeInfo() {
    const employeeInfo = await fetch(`/api/Employees/GetAllEmployee`, {
        method: 'GET',
        mode: 'cors'
    });

    return employeeInfo.json();
}

function createEmployeeInfo(employeeInfo) {
    const model = `
    <tr id="employee-${employeeInfo.id}">
      <td>${employeeInfo.name}</td>
      <td>${employeeInfo.phoneNumber}</td>
      <td>${employeeInfo.position.name}</td>
      <td class="actions" data-th="">
          <div class="text-right">
              <button onclick="deleteEmployee('${employeeInfo.id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Delete
              </button>
          </div>
      </td>
    </tr>`;

    return model;
}

async function populatePositions() {
    const response = await fetch(`/api/Positions/GetAllPosition`);
    const positions = await response.json();

    const positionDropdown = document.getElementById('PositionId');
    positionDropdown.innerHTML = '';

    positions.forEach((position) => {
        const option = document.createElement('option');
        option.value = position.position_id;
        option.textContent = position.name;

        positionDropdown.appendChild(option);
    });
}

async function deleteEmployee(id) {
    const response = await fetch(`/api/Employees/DeleteEmployee?EmployeeId=${id}`, {
        method: 'DELETE',
        mode: 'cors'
    });

    if (response.ok) {
        const employeeRow = document.getElementById(`employee-${id}`);
        employeeRow.style.display = "none";
    }
}

async function displayEmployeeInfo() {
    const employees = await getEmployeeInfo();

    const employeeTable = document.getElementById("EmployeeTable");
    employeeTable.innerHTML = '';

    employees.forEach(element => {
        const model = createEmployeeInfo(element);
        employeeTable.innerHTML += model;
    });
}

async function createEmployee() {
    let nameInput = document.getElementById("EmployeeName");
    let phoneNumberInput = document.getElementById("PhoneNumber");
    let positionIdInput = document.getElementById("PositionId");

    const newEmployee = {
        name: nameInput.value,
        phoneNumber: phoneNumberInput.value,
        position_id: positionIdInput.value
    };


    const response = await fetch(`/api/Employees/PostEmployee`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newEmployee)
    });

    if (response.ok) {
        // Employee created successfully, update the employee table
        displayEmployeeInfo();

        nameInput.value = '';
        phoneNumberInput.value = '';
        positionIdInput.value = '';

        // Hide the create employee form
        const createEmployeeForm = document.getElementById("CreateEmployeeForm");
        createEmployeeForm.style.display = "none";

        // Show the "Add New Employee" button
        const addNewEmployeeButton = document.getElementById("AddNewEmployee");
        addNewEmployeeButton.style.display = "block";
    }
}

// Function to handle the button click event for creating an employee
function handleCreateEmployee() {
    createEmployee();
}

// Function to handle the button click event for adding a new employee
function handleAddNewEmployee() {
    // Show the create employee form
    const createEmployeeForm = document.getElementById("CreateEmployeeForm");
    createEmployeeForm.style.display = "block";

    // Hide the "Add New Employee" button
    const addNewEmployeeButton = document.getElementById("AddNewEmployee");
    addNewEmployeeButton.style.display = "none";
}

// Add an event listener to the create button
const createButton = document.getElementById("CreateEmployee");
createButton.addEventListener("click", handleCreateEmployee);

// Add an event listener to the "Add New Employee" button
const addNewEmployeeButton = document.getElementById("AddNewEmployee");
addNewEmployeeButton.addEventListener("click", handleAddNewEmployee);

// Populate the positions dropdown on page load
populatePositions();

// Display the existing employee information on page load
displayEmployeeInfo();

