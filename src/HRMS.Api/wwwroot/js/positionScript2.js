async function getPositionInfo() {
    const positionInfo = await fetch(`/api/Positions/GetAllPosition`, {
        method: 'GET',
        mode: 'cors'
    });

    return positionInfo.json();
}

function createPositionInfo(positionInfo) {
    const model =
        `
    <tr id="position-${positionInfo.position_id}">
       <td>${positionInfo.name}</td>
       <td>${positionInfo.salary}</td>
       <td>${positionInfo.monthlyWorkingHours}</td>
       <td class="actions" data-th="">
          <div class="text-right">
              <button onclick="editPosition('${positionInfo.position_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Edit
              </button>
              <button onclick="deletePosition('${positionInfo.position_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Delete
              </button>
          </div>
      </td>
    </tr>`;

    return model;
}


async function populateDepartments() {
    const response = await fetch(`/api/Departments/GetAllDepartment`);
    const departments = await response.json();

    const departmentDropdown = document.getElementById('DepartmentId');
    departmentDropdown.innerHTML = '';

    const EditeddepartmentDropdown = document.getElementById('EditDepartmentId');
    EditeddepartmentDropdown.innerHTML = '';

    departments.forEach((department) => {
        const option = document.createElement('option');
        option.value = department.department_id;
        option.textContent = department.name;
        departmentDropdown.appendChild(option);
    });

    departments.forEach((department) => {
        const option = document.createElement('option');
        option.value = department.department_id;
        option.textContent = department.name;
        EditeddepartmentDropdown.appendChild(option);
    });
}


async function deletePosition(position_id) {
    const response = await fetch(`/api/Positions/DeletePosition?PositionId=${position_id}`, {
        method: 'DELETE',
        mode: 'cors'
    });

    if (response.ok) {
        const positionRow = document.getElementById(`position-${position_id}`);
        positionRow.style.display = "none";
    }
}


async function displayPositionInfo() {
    const positions = await getPositionInfo();

    const positionTable = document.getElementById("PositionTable");
    positionTable.innerHTML = '';

    positions.forEach(element => {
        const model = createPositionInfo(element);
        positionTable.innerHTML += model;
    });
}


async function createPosition() {
    let nameInput = document.getElementById("PositionName");
    let salaryInput = document.getElementById("Salary");
    let monthlyHoursInput = document.getElementById("MonthlyWorkingHours");
    let departmentIdInput = document.getElementById("DepartmentId");

    const newPosition = {
        name: nameInput.value,
        salary: parseFloat(salaryInput.value),
        monthlyWorkingHours: parseInt(monthlyHoursInput.value),
        departmentId: departmentIdInput.value
    };

    const response = await fetch(`/api/Positions/PostPosition`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newPosition)
    });


    if (response.ok) {
        // Position created successfully, update the position table
        displayPositionInfo();

        // Reset input fields
        nameInput.value = '';
        salaryInput.value = '';
        monthlyHoursInput.value = '';
        departmentIdInput.value = '';

        // Hide the create position form
        const createPositionForm = document.getElementById("CreatePositionForm");
        createPositionForm.style.display = "none";
    }
}


function handleCreatePosition() {
    createPosition();
}


function editPosition(position_id) {
    document.getElementById("InputEditPositionId").value = `${position_id}`;

    const createPositionForm = document.getElementById("EditPositionForm");
    createPositionForm.style.display = "block";
}


function handleAddNewPosition() {
    const createPositionForm = document.getElementById("CreatePositionForm");
    createPositionForm.style.display = "block";

    const addNewPositionButton = document.getElementById("AddNewPosition");
    addNewPositionButton.style.display = "none";
}


const createButton = document.getElementById("CreatePosition");
createButton.addEventListener("click", handleCreatePosition);

const addNewPositionButton = document.getElementById("AddNewPosition");
addNewPositionButton.addEventListener("click", handleAddNewPosition);

populateDepartments();

displayPositionInfo();
