﻿async function getTimeSheetInfo() {
    const timeSheetInfo = await fetch(`/api/TimeSheets/GetAllTimeSheet`, {
        method: 'GET',
        mode: 'cors',
    });

    return timeSheetInfo.json();
}


function createTimeSheetInfo(timeSheetInfo) {
    const model = `
    <tr id="timeSheet-${timeSheetInfo.timeSheet_id}">
        <td>${timeSheetInfo.employee.name}</td>
        <td>${timeSheetInfo.workingDay}</td>
        <td>${timeSheetInfo.workedHours}</td>
        <td class="actions" data-th="">
            <div class="text-right">
                <button onclick="editTimeSheet('${timeSheetInfo.timeSheet_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                    Edit
                </button>
                <button onclick="deleteTimeSheet('${timeSheetInfo.timeSheet_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                    Delete
                </button>
            </div>
        </td>
    </tr>`;

    return model;
}


async function populateEmployees() {
    const response = await fetch(`/api/Employees/GetAllEmployee`);
    const employees = await response.json();

    const employeeDropdown = document.getElementById('EmployeeId');
    employeeDropdown.innerHTML = '';

    const editedEmployeeDropdown = document.getElementById('EditEmployeeId');
    editedEmployeeDropdown.innerHTML = '';

    employees.forEach((employee) => {
        const option = document.createElement('option');
        option.value = employee.id;
        option.textContent = `${employee.name} - ${employee.phoneNumber}`;
        employeeDropdown.appendChild(option);
    });

    employees.forEach((employee) => {
        const option = document.createElement('option');
        option.value = employee.id;
        option.textContent = `${employee.name} - ${employee.phoneNumber}`;
        editedEmployeeDropdown.appendChild(option);
    });
}

async function deleteTimeSheet(timeSheet_id) {
    const response = await fetch(`/api/TimeSheets/DeleteTimeSheet?TimeSheetId=${timeSheet_id}`, {
        method: 'DELETE',
        mode: 'cors',
    });

    if (response.ok) {
        const timeSheetRow = document.getElementById(`timeSheet-${timeSheet_id}`);
        timeSheetRow.style.display = 'none';
    }
}


async function displayTimeSheetInfo() {
    const timeSheets = await getTimeSheetInfo();

    const timeSheetTable = document.getElementById("TimeSheetTable");
    timeSheetTable.innerHTML = '';

    timeSheets.forEach((element) => {
        const model = createTimeSheetInfo(element);
        timeSheetTable.innerHTML += model;
    });
}

async function createTimeSheet() {
    const employeeIdInput = document.getElementById('EmployeeId');
    const workingDayInput = document.getElementById('WorkingDay');
    const workedHoursInput = document.getElementById('WorkedHours');

    const workingDay = new Date(workingDayInput.value);
    const workingDayString = workingDay.toISOString();

    const newTimeSheet = {
        employeeId: employeeIdInput.value,
        workingDay: workingDayString,
        workedHours: parseFloat(workedHoursInput.value),
    };

    const response = await fetch(`/api/TimeSheets/PostTimeSheet`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(newTimeSheet),
    });

    if (response.ok) {
        // Time sheet created successfully, update the time sheet table
        displayTimeSheetInfo();

        // Reset input fields
        employeeIdInput.value = '';
        workingDayInput.value = '';
        workedHoursInput.value = '';

        // Hide the create time sheet form
        const createTimeSheetForm = document.getElementById('CreateTimeSheetForm');
        createTimeSheetForm.style.display = 'none';

        // Show the "Add New TimeSheet" button
        const addNewTimeSheetButton = document.getElementById('AddNewTimeSheet');
        addNewTimeSheetButton.style.display = 'block';
    }
}


function handleCreateTimeSheet() {
    createTimeSheet();
}

function editTimeSheet(timeSheet_id) {

    document.getElementById("InputEditTimeSheetId").value = `${timeSheet_id}`;

    const createTimeSheetForm = document.getElementById("EditTimeSheetForm");
    createTimeSheetForm.style.display = "block";

}

function handleAddNewTimeSheet() {
    const createTimeSheetForm = document.getElementById("CreateTimeSheetForm");
    createTimeSheetForm.style.display = "block";

    const addNewTimeSheetButton = document.getElementById("AddNewTimeSheet");
    addNewTimeSheetButton.style.display = "none";
}

const createButton = document.getElementById("CreateTimeSheet");
createButton.addEventListener("click", handleCreateTimeSheet);

const addNewTimeSheetButton = document.getElementById("AddNewTimeSheet");
addNewTimeSheetButton.addEventListener("click", handleAddNewTimeSheet);

populateEmployees();

displayTimeSheetInfo();
