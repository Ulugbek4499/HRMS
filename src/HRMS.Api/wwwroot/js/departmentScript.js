﻿async function getDepartmentInfo() {
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

    // Check if the request was successful and update the table
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
