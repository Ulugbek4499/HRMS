async function getDepartmentInfo() {
    const departmentInfo = await fetch('/api/Departments/GetAllDepartment', {
        method: 'GET',
        mode: 'cors'
    });

    return departmentInfo.json();
}

function createDepartmentInfo(departmentInfo) {
    const model = `
        <tr id="department-${departmentInfo.department_id}">
            <td id="name-${departmentInfo.department_id}">${departmentInfo.name}</td>
            <td class="actions" data-th="">
               <div class="text-right">
    <button onclick="editDepartment('${departmentInfo.department_id}')" class="btn btn-white border-secondary bg-warning text-dark btn-md mb-2">
        Edit
    </button>
    <button onclick="deleteDepartment('${departmentInfo.department_id}')" class="btn btn-white border-secondary bg-danger btn-md mb-2">
        Delete
    </button>
    <button id="update-${departmentInfo.department_id}" style="display: none;" onclick="updateDepartment('${departmentInfo.department_id}')" class="btn btn-white border-secondary bg-success btn-md mb-2">
        Update
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
        departmentRow.style.display = 'none';
    }
}

async function postDepartment() {
    let departmentInput = document.getElementById('PostDepartment');
    let departmentName = departmentInput.value.trim();

    if (departmentName === '') {
        // Display an error message or handle the empty department name case as needed
        console.log('Department name cannot be empty');
        return;
    }

    const jsoncha = {
        name: departmentName
    };

    const response = await fetch('/api/Departments/PostDepartment', {
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
        const departmentTable = document.getElementById('DepartmentTable');
        departmentTable.innerHTML += model;

        // Clear the input field after successfully creating the department
        departmentInput.value = '';
    }
}

/*async function displayDepartmentInfo() {
    const departments = await getDepartmentInfo();

    const departmentTable = document.getElementById('DepartmentTable');
    departmentTable.innerHTML = '';

    departments.forEach(element => {
        const model = createDepartmentInfo(element);
        departmentTable.innerHTML += model;
    });
}*/

async function displayDepartmentInfo() {
    const departments = await getDepartmentInfo();

    // Sort the departments by name
    departments.sort((a, b) => {
        const nameA = a.name.toUpperCase();
        const nameB = b.name.toUpperCase();

        if (nameA < nameB) return isAscending ? -1 : 1;
        if (nameA > nameB) return isAscending ? 1 : -1;
        return 0;
    });

    const departmentTable = document.getElementById('DepartmentTable');
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

    const requestBody = {
        id: department_id,
        name: newName
    };

    // Send a request to update the department name
    const response = await fetch(`/api/Departments/UpdateDepartment`, {
        method: 'PUT',
        mode: 'cors',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(requestBody)
    });

    if (response.ok) {
        // Update the department name in the table
        nameElement.innerHTML = newName;

        // Hide the update button and show the edit button
        updateButton.style.display = 'none';
    }
}

let isAscending = true;

const departmentNameHeader = document.querySelector('.table th:first-child');

departmentNameHeader.addEventListener('click', () => {
    isAscending = !isAscending;
    displayDepartmentInfo();
});

displayDepartmentInfo();
