

const _Email = document.getElementById('Email');
const _phone = document.getElementById('Phone');
const error = document.getElementById('error');
const doctorAddbtn = document.getElementById('addDoctor');

const phoneError = document.getElementById('phoneError');
const emailError = document.getElementById('emailError');


function CheckDoctrExitsOrNot() {
    const DEmail = _Email.value;
    const DPhone = _phone.value;

    if (DEmail.trim() == '' || DPhone == '')
    {
        return;
    }

    fetch(`/Doctor/isDoctorExitsOrNot`,
        {
            method: 'POST',
            headers:
            {
                'Content-Type': "application/json"
            },
            body: JSON.stringify({
                GmailID: DEmail,
                Phone: DPhone
            })
        })
        .then(res => res.json())
        .then(val =>
        {
            let isValid;
            if (val.emailExists)
            {
                emailError.textContent = "Email already exists";
                doctorAddbtn.disabled = true;
                isValid = false;
            }
            if (val.phoneExists)
            {
                phoneError.textContent = "Phone number already exists";
                doctorAddbtn.disabled = true;
                isValid = false;
            }
            if (isValid)
            {
                error.textContent = "You can add ";
                doctorAddbtn.disabled = false;
            }
        })

}

_Email.addEventListener('blur', CheckDoctrExitsOrNot)
_phone.addEventListener('blur', CheckDoctrExitsOrNot)


