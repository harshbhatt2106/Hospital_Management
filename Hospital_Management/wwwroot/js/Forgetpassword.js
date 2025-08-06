const gmailSection = document.getElementById('gmailSection');
const otpSection = document.getElementById('otpSection');
const sendOtpBtn = document.getElementById('sendOtpBtn');
const verifyOtpBtn = document.getElementById('verifyOtpBtn');
const errorMsg = document.getElementById('errorMsg');
const gmail = document.getElementById('gmailInput');
const otp = document.getElementById('otpInput')
const loading = document.getElementById('loadingOverlay');

sendOtpBtn.addEventListener('click', function ()
{
    const data = gmail.value.trim();

    if (data === "")
    {
        errorMsg.innerText = "📧 Please enter your email.";
        return;
    }

    loading.classList.remove('hidden');// Show loader for user 

    // ajax call for ConformatiomEmail
    fetch("/Admin/VerifyGmail",
        {
            method: "POST",
            headers:
            {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(data)
        })
        .then(res => res.json())
        .then(result =>
        {   

            if (result.success)// verification Gmail Resule
            {
                loading.classList.add('hidden');// show loader
                gmailSection.classList.add('hidden');
                otpSection.classList.remove('hidden');

                fetch("/Admin/SendOTP", // ajax call to Send OTP
                    {
                        method: "POST",
                        headers:
                        {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify(data)
                    })
                    .then(res => res.json())
                    .then(data =>
                    {
                        if (data.success)
                        {
                            loading.classList.add('hidden');// close Loader
                            errorMsg.innerText = "OTP Send SuccessFully";
                            sendOtpBtn.disabled = true;
                        }
                        else
                        {
                            errorMsg.innerText = "OTP not Send try Again or Chech Your Internet";
                            sendOtpBtn.disabled = false;
                        }
                    })
            }
            else
            {
                setTimeout(() =>
                {
                    loading.classList.add('hidden');// close Loader
                    errorMsg.innerText = `${data}:This GmailID is Not Register`;
                },1000)
            }
        })
        .catch(() =>
        {
            // loding page close
            loading.classList.add('hidden');
            errorMsg.innerText = "Network error. Please try again.";
            sendOtpBtn.disabled = false;
        })
});

verifyOtpBtn.addEventListener('click', function () {
    const _otp = otp.value.trim();

    if (_otp === "") {
        errorMsg.innerText = "🔐 Please enter the OTP.";
        return;
    }
    // veryfy otp ajax call 
    fetch("/Admin/VerifyOTP",
        {
            method: "Post",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(_otp)
        })
        .then(res => res.json())
        .then(data => {
            if (data._VerifyOTP)
            {
                // otp success
                errorMsg.style.color = '#00FF00';
                errorMsg.innerText = "OTO Verify SuccessFully.....";
                setTimeout(() => {
                    window.location.href = '/Admin/ResetPassword'
                }, 2000)
            }
            else {
                // otp wrong
                errorMsg.style.color = 'red';
                errorMsg.innerText = "OTP is Wrong.....";
            }
        })

});


