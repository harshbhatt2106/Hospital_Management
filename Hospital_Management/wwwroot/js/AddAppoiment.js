let searchbox = document.getElementById('PatientNameSearchBox');
const patientListDD = document.getElementById('PatientList');
const DoctorDropdown = document.getElementById('DoctorDropdown');
const DepartmentDropdown = document.getElementById('DepartmentDropdown');
const appoimentdate = document.getElementById('AppointmentDate');

let _phone = document.getElementById('PatientPhone');
const _address = document.getElementById('PatientAddress');
const _city = document.getElementById('PatientCity');
const _state = document.getElementById('PatientState');
const _dateofBirth = document.getElementById('dateofbirth');

let patientid;


DepartmentDropdown.disabled = true;


searchbox.addEventListener('input', () => {
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
                patientNamesList.forEach(data => {
                    const newElement = document.createElement('li');
                    newElement.classList = "list-group-item list-group-item-action";
                    newElement.textContent = "Click to use: " + data.name;
                    newElement.style.color = "red"
                    newElement.setAttribute("data-id", data.patientID)
                    patientListDD.appendChild(newElement);
                })
                patientListDD.style.display = "block";
            }
        })
})



patientListDD.addEventListener('click', (e) => {
    console.log("data");
    if (e.target.tagName === 'LI') {
        const selectedPatientID = e.target.getAttribute('data-id');
        const selectedPatientName = e.target.textContent.replace("Click to use: ", "");
        console.log("Selected Patient ID:", selectedPatientID);
        console.log("Selected Patient Name:", selectedPatientName);

        fetch(`/Appointment/getPatientByID?id=${selectedPatientID}`)
            .then(res => res.json())
            .then(data => {
                console.log(data)
                searchbox.value = data.name;
                _phone.value = data.phone;
                _phone.readOnly = true;
                _phone.style.backgroundColor = "#d3d3d3"; // light green
                _phone.style.color = "#000";

                _address.value = data.address
                _address.style.backgroundColor = "#d3d3d3"
                _address.style.color = "#000";

                _city.value = data.city;
                _city.style.backgroundColor = "#d3d3d3"
                _city.style.color = "#000";


                    //dateOfBirth
                    //gender
                    //email
                    //state
                    patientListDD.style.display = "none"
            });

    }
});

DoctorDropdown.addEventListener('change', () => {
    const SelectedDoctorID = DoctorDropdown.value;

    // select Department BasedOnDoctorID 
    fetch(`/Appointment/GetAllDepartmentByDoctor?DoctorID=${SelectedDoctorID}`)
        .then(res => res.json())
        .then(departmentList => {
            DepartmentDropdown.innerHTML = "";
            const DefaultElememt = document.createElement('option');
            DefaultElememt.text = "----Select DepartmentName-----"
            DepartmentDropdown.appendChild(DefaultElememt);
            DepartmentDropdown.disabled = false;

            console.log(departmentList);
            departmentList.forEach((d) => {
                const element = document.createElement('option');
                element.text = d.departmentName;
                element.value = d.departmentId;
                DepartmentDropdown.appendChild(element);
            })
        })

})
