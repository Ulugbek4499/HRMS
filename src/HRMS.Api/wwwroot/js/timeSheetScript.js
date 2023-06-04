async function getTimeSheetInfo() {
    const timeSheetInfo = await fetch(`/api/TimeSheets/GetAllTimeSheet`,
        {
            method: 'GET',
            mode: 'cors'
        });

    return timeSheetInfo.json();
}

function CreateTimeSheetInfo(timeSheetInfo) {
    const model =
        `<tr>
          <td>${timeSheetInfo.employee.name}</td>
          <td>${timeSheetInfo.workingDay}</td>
          <td>${timeSheetInfo.workedHours}</td>
         </tr>`;

    return model;
}

async function DisplayTimeSheetInfo() {
    const positions = await getTimeSheetInfo();

    document.getElementById("TimeSheetTable").innerHTML = null;

    positions.forEach(element => {
        const model = CreateTimeSheetInfo(element);
        document.getElementById("TimeSheetTable").innerHTML += model;
    });
}

DisplayTimeSheetInfo();