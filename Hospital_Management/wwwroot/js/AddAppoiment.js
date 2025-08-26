const searchbox = document.getElementById('PatientNameSearchBox');
const patientListDD = document.getElementById('PatientList');
const DoctorDropdown = document.getElementById('DoctorDropdown');
const DepartmentDropdown = document.getElementById('DepartmentDropdown');




searchbox.addEventListener('focusout', () => {
    patientListDD.style.display = "none"
})
searchbox.addEventListener('input', () =>
{
    patientListDD.innerHTML = "";
    const patientName = searchbox.value;

    // ajaxCallForSearchPatient
    fetch(`/Appointment/searchPatient?patientName=${patientName}`)
        .then(result => result.json())
        .then(patientNamesList => {
            if (patientNamesList.length == 0) {
                const li = document.createElement('li');
                li.classList = "list-group-item list-group-item-action";
                li.textContent = `No patient Found Name Like: ${patientName}`;
                patientListDD.appendChild(li);
                patientListDD.style.display = "block";
                // No PatientFound
            }
            else {
                // patient Found 
                patientNamesList.forEach(data =>
                {
                    const newElement = document.createElement('li');
                    newElement.classList = "list-group-item list-group-item-action";
                    newElement.textContent = data.name;
                    newElement.setAttribute("data-id", data.patientID)
                    patientListDD.appendChild(newElement);

                    // DropDown Select Fill UserValue

                })
                patientListDD.style.display = "block";
            }
        })
})


