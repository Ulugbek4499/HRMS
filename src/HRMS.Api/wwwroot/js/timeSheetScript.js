/*async function getTimeSheetInfo() {
    const timeSheetInfo = await fetch(`/api/TimeSheets/GetAllTimeSheet`,
        {
            method: 'GET',
            mode: 'cors'
        });

    return timeSheetInfo.json();
}

function CreateTimeSheetInfo(timeSheetInfo) {
    const model =
        `<trid="position-${timeSheetInfo.timeSheet_id}">
          <td>${timeSheetInfo.employee.name}</td>
          <td>${timeSheetInfo.workingDay}</td>
          <td>${timeSheetInfo.workedHours}</td>
          <td class="actions" data-th="">
          <div class="text-right">
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

    employees.forEach((employee) => {
        const option = document.createElement('option');
        option.value = employee.id;
        option.textContent = `${employee.name} - ${employee.phoneNumber}`;
        employeeDropdown.appendChild(option);
    });
}


async function deleteTimeSheet(timeSheet_id) {
    const response = await fetch(`/api/TimeSheets/DeleteTimeSheet?TimeSheetId=${timeSheet_id}`, {
        method: 'DELETE',
        mode: 'cors'
    });

    if (response.ok) {
        const timeSheetRow = document.getElementById(`timeSheet-${timeSheet_id}`);
        timeSheetRow.style.display = "none";
    }
}

async function DisplayTimeSheetInfo() {
    const positions = await getTimeSheetInfo();

    document.getElementById("TimeSheetTable").innerHTML = null;

    positions.forEach(element => {
        const model = CreateTimeSheetInfo(element);
        document.getElementById("TimeSheetTable").innerHTML += model;
    });
}


async function createTimeSheet() {
    let employeeIdInput = document.getElementById("EmployeeId");
    let workingDayInput = document.getElementById("WorkingDay");
    let workedHoursInput = document.getElementById("WorkedHours");

    const newTimeSheet = {
        employeeId: employeeIdInput.value,
        workingDay: workingDayInput.value,
        workedHours: workedHoursInput.value,
    };

    const response = await fetch(`/api/TimeSheets/PostTimeSheet`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newTimeSheet)
    });


    if (response.ok) {
        // Position created successfully, update the position table
        DisplayTimeSheetInfo();

        // Reset input fields
        employeeIdInput.value = '';
        workingDayInput.value = '';
        workedHoursInput.value = '';

        // Hide the create position form
        const createTimeSheetForm = document.getElementById("CreateTimeSheetForm");
        createTimeSheetForm.style.display = "none";

        // Show the "Add New Position" button
        const addNewTimeSheetButton = document.getElementById("AddNewTimeSheet");
        addNewTimeSheetButton.style.display = "block";
    }
}


// Function to handle the button click event for creating a position
function handleCreateTimeSheet() {
    createTimeSheet();
}

// Function to handle the button click event for adding a new position
function handleAddNewTimeSheet() {
    // Show the create position form
    const createTimeSheetForm = document.getElementById("CreateTimeSheetForm");
    createTimeSheetForm.style.display = "block";

    // Hide the "Add New Position" button
    const addNewTimeSheetButton = document.getElementById("AddNewTimeSheet");
    addNewTimeSheetButton.style.display = "none";
}

// Add an event listener to the create button
const createButton = document.getElementById("CreateTimeSheet");
createButton.addEventListener("click", handleCreateTimeSheet);

// Add an event listener to the "Add New Position" button
const addNewTimeSheetButton = document.getElementById("AddNewTimeSheet");
addNewTimeSheetButton.addEventListener("click", handleAddNewTimeSheet);

// Populate the departments dropdown on page load
populateEmployees();

DisplayTimeSheetInfo();*/

async function getTimeSheetInfo() {
    const timeSheetInfo = await fetch(`/api/TimeSheets/GetAllTimeSheet`, {
        method: 'GET',
        mode: 'cors',
    });

    return timeSheetInfo.json();
}

function createTimesheetRow(timeSheetInfo) {
    const row = document.createElement('tr');
    row.id = `timeSheet-${timeSheetInfo.timeSheet_id}`;

    const employeeNameCell = document.createElement('td');
    employeeNameCell.textContent = timeSheetInfo.employee.name;

    const workingDayCell = document.createElement('td');
    workingDayCell.textContent = timeSheetInfo.workingDay;

    const workedHoursCell = document.createElement('td');
    workedHoursCell.textContent = timeSheetInfo.workedHours;

    const actionsCell = document.createElement('td');
    const deleteButton = document.createElement('button');
    deleteButton.className = 'btn btn-white border-secondary bg-white btn-md mb-2';
    deleteButton.textContent = 'Delete';
    deleteButton.addEventListener('click', () => deleteTimeSheet(timeSheetInfo.timeSheet_id));
    actionsCell.appendChild(deleteButton);

    row.appendChild(employeeNameCell);
    row.appendChild(workingDayCell);
    row.appendChild(workedHoursCell);
    row.appendChild(actionsCell);

    return row;
}

async function populateEmployees() {
    const response = await fetch(`/api/Employees/GetAllEmployee`);
    const employees = await response.json();

    const employeeDropdown = document.getElementById('EmployeeId');
    employeeDropdown.innerHTML = '';

    employees.forEach((employee) => {
        const option = document.createElement('option');
        option.value = employee.id;
        option.textContent = `${employee.name} - ${employee.phoneNumber}`;
        employeeDropdown.appendChild(option);
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

    const timeSheetTable = document.getElementById('TimeSheetTable');
    timeSheetTable.innerHTML = '';

    timeSheets.forEach((timeSheet) => {
        const row = createTimesheetRow(timeSheet);
        timeSheetTable.appendChild(row);
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


function handleCreateTimeSheet(event) {
    event.preventDefault();
    createTimeSheet();
}

function handleAddNewTimeSheet(event) {
    event.preventDefault();
    // Show the create time sheet form
    const createTimeSheetForm = document.getElementById('CreateTimeSheetForm');
    createTimeSheetForm.style.display = 'block';

    // Hide the "Add New TimeSheet" button
    const addNewTimeSheetButton = document.getElementById('AddNewTimeSheet');
    addNewTimeSheetButton.style.display = 'none';
}

// Add an event listener to the create button
const createButton = document.getElementById('CreateTimeSheet');
createButton.addEventListener('click', handleCreateTimeSheet);

// Add an event listener to the "Add New TimeSheet" button
const addNewTimeSheetButton = document.getElementById('AddNewTimeSheet');
addNewTimeSheetButton.addEventListener('click', handleAddNewTimeSheet);

// Populate the employees dropdown on page load
populateEmployees();

// Display existing time sheet information
displayTimeSheetInfo();
