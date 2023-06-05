

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
    <tr id="employee-${employeeInfo.id}">
      <td>${employeeInfo.name}</td>
      <td>${employeeInfo.phoneNumber}</td>
      <td>${employeeInfo.position.name}</td>
      <td class="actions" data-th="">
          <div class="text-right">
              <button onclick="deleteEmployee('${employeeInfo.id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Delete
              </button>
          </div>
      </td>
    </tr>`;

    return model;
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
    const employees = await getemployeeInfo();

    const employeeTable = document.getElementById("EmployeeTable");
    employeeTable.innerHTML = '';

    employees.forEach(element => {
        const model = createEmployeeInfo(element);
        employeeTable.innerHTML += model;
    });
}

displayEmployeeInfo();
