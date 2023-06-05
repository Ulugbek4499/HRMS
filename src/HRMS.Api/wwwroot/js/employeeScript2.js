

async function getEmployeeInfo() {
    const employeeInfo = await fetch(`/api/Employees/GetAllEmployee`, {
        method: 'GET',
        mode: 'cors'
    });

    return employeeInfo.json();
}

function createEmployeeInfo(employeeInfo) {
    const model =
    `
    <tr id="employee-${employeeInfo.employee_id}">
      <td>${employeeInfo.name}</td>
      <td>${employeeInfo.phoneNumber}</td>
      <td>${employeeInfo.position.name}</td>
      <td class="actions" data-th="">
          <div class="text-right">
              <button onclick="deleteEmployee('${employeeInfo.employee_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Delete
              </button>
          </div>
      </td>
    </tr>`;

    return model;
}
async function deleteEmployee(employee_id) {
    const response = await fetch(`/api/Employees/DeleteEmployee?employeeId=${employee_id}`, {
        method: 'DELETE',
        mode: 'cors'
    });


    // Check if the request was successful and remove the employee from the table
    if (response.ok) {
        const employeeRow = document.getElementById(`employee-${employee_id}`);
        employeeRow.style.display = "none";

    }
}

async function displayEmployeeInfo() {
    const employees = await getemployeeInfo();

    const employeeTable = document.getElementById("EmployeeTable");
    employeeTable.innerHTML = '';

    employees.forEach(element => {
        const model = createEmployeeInfo(element);
        employeeTable.innerHTML += model;
    });
}

displayEmployeeInfo();
