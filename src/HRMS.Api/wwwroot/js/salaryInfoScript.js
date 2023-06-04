async function getSalaryInfo()
{
    const salaryInfo = await fetch(`/api/SalaryInfo/GetAllSalaryInfo`,
    {
        method: 'GET', 
        mode: 'cors'
  });

  return salaryInfo.json();
}

function CreateSalaryInfo(salaryInfo)
{
  const model= 
  `<tr>
          <td>${salaryInfo.name}</td>
          <td>${salaryInfo.positionName}</td>
          <td>${salaryInfo.fixedWorkingHours}</td>
          <td>${salaryInfo.actualWorkingHours}</td>
          <td>${salaryInfo.fixedSalary}</td>
          <td>${salaryInfo.actualSalary}</td>
    </tr>
  `;

  return model;
}

async function DisplaySalaryInfo()
{
    const salaries = await getSalaryInfo();

    document.getElementById("SalaryTable").innerHTML = null;

    salaries.forEach(element =>
    {
        const model = CreateSalaryInfo(element);

        document.getElementById("SalaryTable").innerHTML += model;

    });
} 

DisplaySalaryInfo();