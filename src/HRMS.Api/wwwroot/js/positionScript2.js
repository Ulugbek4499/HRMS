

async function getPositionInfo() {
    const positionInfo = await fetch(`/api/Positions/GetAllPosition`, {
        method: 'GET',
        mode: 'cors'
    });

    return positionInfo.json();
}

function createPositionInfo(positionInfo) {
    const model =
    `
    <tr id="position-${positionInfo.position_id}">
       <td>${positionInfo.name}</td>
       <td>${positionInfo.salary}</td>
       <td>${positionInfo.monthlyWorkingHours}</td>
       <td class="actions" data-th="">
          <div class="text-right">
              <button onclick="deletePosition('${positionInfo.position_id}')" class="btn btn-white border-secondary bg-white btn-md mb-2">
                  Delete
              </button>
          </div>
      </td>
    </tr>`;

    return model;
}
async function deletePosition(position_id) {
    const response = await fetch(`/api/Positions/DeletePosition?PositionId=${position_id}`, {
        method: 'DELETE',
        mode: 'cors'
    });

    if (response.ok) {
        const positionRow = document.getElementById(`position-${position_id}`);
        positionRow.style.display = "none";

    }
}

async function displayPositionInfo() {
    const positions = await getPositionInfo();

    const positionTable = document.getElementById("PositionTable");
    positionTable.innerHTML = '';

    positions.forEach(element => {
        const model = createPositionInfo(element);
        positionTable.innerHTML += model;
    });
}

displayPositionInfo();
