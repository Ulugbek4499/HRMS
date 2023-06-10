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
         <button onclick="editEmployee('${employeeInfo.id}')" class="btn btn-white border-secondary bg-warning text-dark btn-md mb-2">
    Edit
</button>
<button onclick="deleteEmployee('${employeeInfo.id}')"class="btn btn-white border-secondary bg-danger btn-md mb-2">
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

    const EditedpositionDropdown = document.getElementById('EditPositionId');
    EditedpositionDropdown.innerHTML = '';


    positions.forEach((position) => {
        const option = document.createElement('option');
        option.value = position.position_id;
        option.textContent = position.name;
        positionDropdown.appendChild(option);
    });

    positions.forEach((position) => {
        const option = document.createElement('option');
        option.value = position.position_id;
        option.textContent = position.name;
        EditedpositionDropdown.appendChild(option);

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

    employees.forEach((element) => {
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
        positionId: positionIdInput.value
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
        displayEmployeeInfo();

        nameInput.value = '';
        phoneNumberInput.value = '';
        positionIdInput.value = '';

        const createEmployeeForm = document.getElementById("CreateEmployeeForm");
        createEmployeeForm.style.display = "none";

        const addNewEmployeeButton = document.getElementById("AddNewEmployee");
        addNewEmployeeButton.style.display = "block";
    }
}

function handleCreateEmployee() {
    createEmployee();
}


function editEmployee(id) {

    document.getElementById("InputEditEmployeeId").value = `${id}`;

    const createEmployeeForm = document.getElementById("EditEmployeeForm");
    createEmployeeForm.style.display = "block";

}











function handleAddNewEmployee() {
    const createEmployeeForm = document.getElementById("CreateEmployeeForm");
    createEmployeeForm.style.display = "block";

    const addNewEmployeeButton = document.getElementById("AddNewEmployee");
    addNewEmployeeButton.style.display = "none";
}






const createButton = document.getElementById("CreateEmployee");
createButton.addEventListener("click", handleCreateEmployee);

const addNewEmployeeButton = document.getElementById("AddNewEmployee");
addNewEmployeeButton.addEventListener("click", handleAddNewEmployee);

populatePositions();

displayEmployeeInfo();
