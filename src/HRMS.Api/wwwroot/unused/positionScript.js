async function getPositionInfo()
{
    const positionInfo = await fetch(`/api/Positions/GetAllPosition`,
        {
            method: 'GET',
            mode: 'cors'
        });

    return positionInfo.json();
}

function CreatePositionInfo(positionInfo)
{
    const model =
        `<tr>
          <td>${positionInfo.name }</td>
          <td>${positionInfo.salary}</td>
          <td>${positionInfo.monthlyWorkingHours}</td>
         </tr>`;

    return model;
}

async function DisplayPositionInfo() {
    const positions = await getPositionInfo();

    document.getElementById("PositionTable").innerHTML = null;

    positions.forEach(element => {
        const model = CreatePositionInfo(element);
        document.getElementById("PositionTable").innerHTML += model;
    });
}

DisplayPositionInfo();