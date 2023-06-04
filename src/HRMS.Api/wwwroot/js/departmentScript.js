async function getDepartmentInfo()
{
    const departmentInfo = await fetch(`/api/Departments/GetAllDepartment`,
        {
            method: 'GET',
            mode: 'cors'
        });

    return departmentInfo.json();
}

function CreateDepartmentInfo(departmentInfo)
{
    const model =
        `<tr>
          <td>${departmentInfo.name}</td>
         </tr>`;

    return model;
}

async function DisplaySalaryInfo()
{
    const departments = await getDepartmentInfo();

    document.getElementById("DepartmentTable").innerHTML = null;

    departments.forEach(element => {
        const model = CreateDepartmentInfo(element);
        document.getElementById("DepartmentTable").innerHTML += model;
    });
}

DisplaySalaryInfo();