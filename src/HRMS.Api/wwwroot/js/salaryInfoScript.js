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
             <td>${salaryInfo.departmentName}</td>
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

function searchTable() {
    var searchInput = document.getElementById('searchInput').value.toLowerCase();
    var table = document.querySelector('.table');
    var rows = Array.from(table.tBodies[0].rows);

    rows.forEach(function (row) {
        var matchFound = false;
        Array.from(row.cells).forEach(function (cell) {
            var cellText = cell.textContent.toLowerCase();
            if (cellText.includes(searchInput)) {
                matchFound = true;
                return;
            }
        });

        if (matchFound) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}

displaySalaryInfo();
