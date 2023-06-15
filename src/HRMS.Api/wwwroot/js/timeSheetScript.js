
async function getTimeSheetInfo() {
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
                <button onclick="editTimeSheet('${timeSheetInfo.timeSheet_id}')" class="btn btn-white border-secondary bg-warning text-dark btn-md mb-2">
                    Edit
                </button>
                <button onclick="deleteTimeSheet('${timeSheetInfo.timeSheet_id}')" class="btn btn-white border-secondary bg-danger btn-md mb-2">
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
        displayTimeSheetInfo();

        employeeIdInput.value = '';
        workingDayInput.value = '';
        workedHoursInput.value = '';

        const createTimeSheetForm = document.getElementById('CreateTimeSheetForm');
        createTimeSheetForm.style.display = 'none';

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

function sortTimeSheetByEmployees() {
    var table = document.querySelector('.table');
    var rows = Array.from(table.tBodies[0].rows);
    var sortIcon = document.getElementById('timeSheetEmployeeSortIcon');
    var isAscending = sortIcon.classList.contains('asc');

    rows.sort(function (a, b) {
        var nameA = a.cells[0].textContent.trim().toUpperCase();
        var nameB = b.cells[0].textContent.trim().toUpperCase();

        if (nameA < nameB) {
            return -1;
        } else if (nameA > nameB) {
            return 1;
        } else {
            return 0;
        }
    });

    if (!isAscending) {
        rows.reverse();
        sortIcon.classList.remove('desc');
        sortIcon.classList.add('asc');
    } else {
        sortIcon.classList.remove('asc');
        sortIcon.classList.add('desc');
    }

    while (table.tBodies[0].firstChild) {
        table.tBodies[0].removeChild(table.tBodies[0].firstChild);
    }

    rows.forEach(function (row) {
        table.tBodies[0].appendChild(row);
    });
}
function sortTimeSheetByDays() {
    var table = document.querySelector('.table');
    var rows = Array.from(table.tBodies[0].rows);
    var sortIcon = document.getElementById('timeSheetDaySortIcon');
    var isAscending = sortIcon.classList.contains('asc');

    rows.sort(function (a, b) {
        var dateA = new Date(a.cells[1].textContent.trim());
        var dateB = new Date(b.cells[1].textContent.trim());

        if (dateA < dateB) {
            return -1;
        } else if (dateA > dateB) {
            return 1;
        } else {
            return 0;
        }
    });

    if (!isAscending) {
        rows.reverse();
        sortIcon.classList.remove('desc');
        sortIcon.classList.add('asc');
    } else {
        sortIcon.classList.remove('asc');
        sortIcon.classList.add('desc');
    }

    while (table.tBodies[0].firstChild) {
        table.tBodies[0].removeChild(table.tBodies[0].firstChild);
    }

    rows.forEach(function (row) {
        table.tBodies[0].appendChild(row);
    });
}

function sortTimeSheetByHours() {
    var table = document.querySelector('.table');
    var rows = Array.from(table.tBodies[0].rows);
    var sortIcon = document.getElementById('timeSheetHoursSortIcon');
    var isAscending = sortIcon.classList.contains('asc');

    rows.sort(function (a, b) {
        var hoursA = parseInt(a.cells[2].textContent.trim());
        var hoursB = parseInt(b.cells[2].textContent.trim());

        if (hoursA < hoursB) {
            return -1;
        } else if (hoursA > hoursB) {
            return 1;
        } else {
            return 0;
        }
    });

    if (!isAscending) {
        rows.reverse();
        sortIcon.classList.remove('desc');
        sortIcon.classList.add('asc');
    } else {
        sortIcon.classList.remove('asc');
        sortIcon.classList.add('desc');
    }

    while (table.tBodies[0].firstChild) {
        table.tBodies[0].removeChild(table.tBodies[0].firstChild);
    }

    rows.forEach(function (row) {
        table.tBodies[0].appendChild(row);
    });
}

function searchTable() {
    var searchInput = document.getElementById('searchInput').value.toLowerCase();
    var table = document.querySelector('.table');
    var rows = Array.from(table.tBodies[0].rows);

    rows.forEach(function (row) {
        var matchFound = false;
        Array.from(row.cells).forEach(function (cell) {
            var cellText = cell.textContent.toLowerCase();
            if (cellText.includes(searchInput)) {
                matchFound = true;
                return;
            }
        });

        if (matchFound) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}


populateEmployees();

displayTimeSheetInfo();
