/*async function getDepartmentInfo() {
    const departmentInfo = await fetch(`/api/Departments/GetAllDepartment`, {
        method: 'GET',
        mode: 'cors'
    });

    return departmentInfo.json();
}

function createDepartmentInfo(departmentInfo) {
    const model =
        `
    <tr id="department-${departmentInfo.department_id}">
      <td>${departmentInfo.name}</td>
      <td class="actions" data-th="">
          <div class="text-right">
              <button onclick="deleteDepartment('${departmentInfo.department_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Delete
              </button>
          </div>
      </td>
    </tr>`;

    return model;
}

async function deleteDepartment(department_id) {
    const response = await fetch(`/api/Departments/DeleteDepartment?departmentId=${department_id}`, {
        method: 'DELETE',
        mode: 'cors'
    });

    // Check if the request was successful and remove the department from the table
    if (response.ok) {
        const departmentRow = document.getElementById(`department-${department_id}`);
        departmentRow.style.display = "none";
    }
}

async function postDepartment() {
    let department = document.getElementById("PostDepartment");

    console.log(department.value);

    const jsoncha = {
        name: department.value
    };

    const response = await fetch(`/api/Departments/PostDepartment`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(jsoncha)
    });

    if (response.ok) {
        const createdDepartment = await response.json();
        const model = createDepartmentInfo(createdDepartment);
        const departmentTable = document.getElementById("DepartmentTable");
        departmentTable.innerHTML += model;
    }
}

async function displayDepartmentInfo() {
    const departments = await getDepartmentInfo();

    const departmentTable = document.getElementById("DepartmentTable");
    departmentTable.innerHTML = '';

    departments.forEach(element => {
        const model = createDepartmentInfo(element);
        departmentTable.innerHTML += model;
    });
}

displayDepartmentInfo();
*//*


async function getDepartmentInfo() {
    const departmentInfo = await fetch(`/api/Departments/GetAllDepartment`, {
        method: 'GET',
        mode: 'cors'
    });

    return departmentInfo.json();
}

function createDepartmentInfo(departmentInfo) {
    const model =
        `
    <tr id="department-${departmentInfo.department_id}">
      <td>${departmentInfo.name}</td>
      <td class="actions" data-th="">
          <div class="text-right">
              <button onclick="deleteDepartment('${departmentInfo.department_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Delete
              </button>
          </div>
      </td>
    </tr>`;

    return model;
}

async function deleteDepartment(department_id) {
    const response = await fetch(`/api/Departments/DeleteDepartment?departmentId=${department_id}`, {
        method: 'DELETE',
        mode: 'cors'
    });

    // Check if the request was successful and remove the department from the table
    if (response.ok) {
        const departmentRow = document.getElementById(`department-${department_id}`);
        departmentRow.style.display = "none";
    }
}

async function postDepartment() {
    let department = document.getElementById("PostDepartment");

    console.log(department.value);

    const jsoncha = {
        name: department.value
    };

    const response = await fetch(`/api/Departments/PostDepartment`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(jsoncha)
    });

    if (response.ok) {
        const createdDepartment = await response.json();
        const model = createDepartmentInfo(createdDepartment);
        const departmentTable = document.getElementById("DepartmentTable");
        departmentTable.innerHTML += model;
    }
}

async function displayDepartmentInfo() {
    const departments = await getDepartmentInfo();

    const departmentTable = document.getElementById("DepartmentTable");
    departmentTable.innerHTML = '';

    departments.forEach(element => {
        const model = createDepartmentInfo(element);
        departmentTable.innerHTML += model;
    });
}

displayDepartmentInfo();
*/

async function getDepartmentInfo() {
    const departmentInfo = await fetch(`/api/Departments/GetAllDepartment`, {
        method: 'GET',
        mode: 'cors'
    });

    return departmentInfo.json();
}

function createDepartmentInfo(departmentInfo) {
    const model =
        `
    <tr id="department-${departmentInfo.department_id}">
      <td id="name-${departmentInfo.department_id}">${departmentInfo.name}</td>
      <td class="actions" data-th="">
          <div class="text-right">
              <button onclick="editDepartment('${departmentInfo.department_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Edit
              </button>
              <button onclick="deleteDepartment('${departmentInfo.department_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Delete
              </button>
              <button id="update-${departmentInfo.department_id}" style="display: none;" onclick="updateDepartment('${departmentInfo.department_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Save
              </button>
          </div>
      </td>
    </tr>`;

    return model;
}

async function deleteDepartment(department_id) {
    const response = await fetch(`/api/Departments/DeleteDepartment?departmentId=${department_id}`, {
        method: 'DELETE',
        mode: 'cors'
    });

    // Check if the request was successful and remove the department from the table
    if (response.ok) {
        const departmentRow = document.getElementById(`department-${department_id}`);
        departmentRow.style.display = "none";
    }
}

async function postDepartment() {
    let departmentInput = document.getElementById("PostDepartment");
    let departmentName = departmentInput.value.trim();

    if (departmentName === '') {
        // Display an error message or handle the empty department name case as needed
        console.log('Department name cannot be empty');
        return;
    }

    const jsoncha = {
        name: departmentName
    };

    const response = await fetch(`/api/Departments/PostDepartment`, {
        method: 'POST',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(jsoncha)
    });

    if (response.ok) {
        const createdDepartment = await response.json();
        const model = createDepartmentInfo(createdDepartment);
        const departmentTable = document.getElementById("DepartmentTable");
        departmentTable.innerHTML += model;

        // Clear the input field after successfully creating the department
        departmentInput.value = '';
    }
}


async function displayDepartmentInfo() {
    const departments = await getDepartmentInfo();

    const departmentTable = document.getElementById("DepartmentTable");
    departmentTable.innerHTML = '';

    departments.forEach(element => {
        const model = createDepartmentInfo(element);
        departmentTable.innerHTML += model;
    });
}

async function editDepartment(department_id) {
    const nameElement = document.getElementById(`name-${department_id}`);
    const updateButton = document.getElementById(`update-${department_id}`);

    // Get the current department name
    const currentName = nameElement.innerText;

    // Create an input field to edit the name
    const inputField = document.createElement('input');
    inputField.value = currentName;

    // Replace the name with the input field
    nameElement.innerHTML = '';
    nameElement.appendChild(inputField);

    // Show the update button and hide the edit button
    updateButton.style.display = 'inline-block';
}

async function updateDepartment(department_id) {
    const nameElement = document.getElementById(`name-${department_id}`);
    const updateButton = document.getElementById(`update-${department_id}`);

    // Get the new department name from the input field
    const newName = nameElement.firstChild.value;

    // Send a request to update the department name
    const response = await fetch(`/api/Departments/UpdateDepartment?departmentId=${department_id}&name=${newName}`, {
        method: 'PUT',
        mode: 'cors'
    });

    if (response.ok) {
        // Update the department name in the table
        nameElement.innerHTML = newName;

        // Hide the update button and show the edit button
        updateButton.style.display = 'none';
    }
}




displayDepartmentInfo();
