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
                  Update
              </button>
              <button onclick="deletePosition('${positionInfo.position_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Delete
              </button>
          </div>
      </td>
    </tr>`;

    return model;
}


async function editPosition(id) {

    document.getElementById("UpdatePositionForm").style.display = "block";
    
    let nameInput = document.getElementById("PositionName2");
    let salaryInput = document.getElementById("Salary2");
    let monthlyHoursInput = document.getElementById("MonthlyWorkingHours2");
    let departmentIdInput = document.getElementById("DepartmentId2");

    const newPosition = {
        id : id,
        name: nameInput.value,
        salary: parseInt(salaryInput.value),
        monthlyWorkingHours: parseInt(monthlyHoursInput.value),
        departmentId: departmentIdInput.value
    };
    await updateButtnFunc(newPosition);
    
    if (updatePositon.ok) {
        // Position created successfully, update the position table
        displayPositionInfo();

        // Reset input fields
        nameInput.value = '';
        salaryInput.value = '';
        monthlyHoursInput.value = '';
        departmentIdInput.value = '';

        // Hide the create position form
        const createPositionForm = document.getElementById("UpdatePositionForm");
        createPositionForm.style.display = "none";

        // Show the "Add New Position" button
        const addNewPositionButton = document.getElementById("AddNewPosition");
        addNewPositionButton.style.display = "block";
    }



}

async function updateButtnFunc(model) {

    const updatePositon = await fetch(`/api/Positions/UpdatePosition`, {
        method: 'PUT',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(model)
    });
    return updatePositon;


}
async function populateDepartments() {
    const response = await fetch(`/api/Departments/GetAllDepartment`);
    const departments = await response.json();

    const departmentDropdown = document.getElementById('DepartmentId');
    departmentDropdown.innerHTML = '';

    departments.forEach((department) => {
        const option = document.createElement('option');
        option.value = department.department_id;
        option.textContent = department.name;
        departmentDropdown.appendChild(option);
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
        salary: parseInt(salaryInput.value),
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

// Function to handle the button click event for creating a position
function handleCreatePosition() {
    createPosition();
}

// Function to handle the button click event for adding a new position
function handleAddNewPosition() {
    // Show the create position form
    const createPositionForm = document.getElementById("CreatePositionForm");
    createPositionForm.style.display = "block";

    // Hide the "Add New Position" button
    const addNewPositionButton = document.getElementById("AddNewPosition");
    addNewPositionButton.style.display = "none";
}

// Add an event listener to the create button
const createButton = document.getElementById("CreatePosition");
createButton.addEventListener("click", handleCreatePosition);

// Add an event listener to the "Add New Position" button
const addNewPositionButton = document.getElementById("AddNewPosition");
addNewPositionButton.addEventListener("click", handleAddNewPosition);

// Populate the departments dropdown on page load
populateDepartments();

// Display position information
displayPositionInfo();
