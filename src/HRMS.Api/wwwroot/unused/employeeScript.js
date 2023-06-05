async function getEmployeeInfo()
{
    const employeeInfo = await fetch(`/api/Employees/GetAllEmployee`,
        {
            method: 'GET',
            mode: 'cors'
        });

    return employeeInfo.json();
}

function CreateEmployeeInfo(employeeInfo)
{
    const model =
        `<tr>
          <td>${employeeInfo.name}</td>
          <td>${employeeInfo.phoneNumber}</td>
          <td>${employeeInfo.position.name}</td>
        </tr>`;

    return model;
}

async function DisplayEmployeeInfo()
{
    const employees = await getEmployeeInfo();

    document.getElementById("EmployeeTable").innerHTML = null;

    employees.forEach(element =>
    {
        const model = CreateEmployeeInfo(element);

        document.getElementById("EmployeeTable").innerHTML += model;

    });
}

DisplayEmployeeInfo();