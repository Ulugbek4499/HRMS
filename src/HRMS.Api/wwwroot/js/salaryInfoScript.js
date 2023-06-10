
async function getSalaryInfo() {
    const salaryInfo = await fetch(`/api/SalaryInfo/GetAllSalaryInfo`, {
        method: 'GET',
        mode: 'cors'
    });

    return salaryInfo.json();
}

function createSalaryInfo(salaryInfo) {
    const model = `
        <tr>
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

async function displaySalaryInfo() {
    const salaries = await getSalaryInfo();
    const salaryTable = document.getElementById("SalaryTable");
    salaryTable.innerHTML = '';

    salaries.forEach(element => {
        const model = createSalaryInfo(element);
        salaryTable.innerHTML += model;
    });
}

displaySalaryInfo();
