import { VeryfiOTP } from './AllReletedOTP.js';
import { SendOTP } from './AllReletedOTP.js';
import { VerificationGmail } from './AllReletedOTP.js';

const gmailSection = document.getElementById('gmailSection');
const otpSection = document.getElementById('otpSection');
const sendOtpBtn = document.getElementById('sendOtpBtn');
const resendOtpBtn = document.getElementById('resendOtpBtn');
const verifyOtpBtn = document.getElementById('verifyOtpBtn');
const errorMsg = document.getElementById('errorMsg');
const gmail = document.getElementById('gmailInput');
const otp = document.getElementById('otpInput')
const loading = document.getElementById('loadingOverlay');

sendOtpBtn.addEventListener('click', async function () {

    errorMsg.innerText = "";

    try
    {
        if (gmail.value == '')
        {
            errorMsg.innerText = "Enter GmailID"
            return;
        }

        loading.classList.remove('hidden');
        const IsGmailDone = await VerificationGmail(gmail.value);

        if (IsGmailDone)
        {
            // show OTP Page              
            errorMsg.innerText = "";

            // send otp
            const result = await SendOTP(gmail.value);

            if (result)
            {
                loading.classList.add('hidden');
                gmailSection.classList.add('hidden');
                otpSection.classList.remove('hidden');
                sendOtpBtn.disabled = true;
            }
            else
            {
                sendOtpBtn.disabled = false;
                errorMsg.innerText = `otp has been not generated resend otp`;
            }
        }
        else
        {
            loading.classList.add('hidden');
            errorMsg.innerText = `${gmail.value}:This GmailID is Not Register`;
        }
    }
    catch
    {
        loading.classList.add('hidden');
        errorMsg.innerText = "Network error. Please try again.";
        sendOtpBtn.disabled = false;
    }
    finally
    {
        loading.classList.add('hidden');
    }
});

resendOtpBtn.addEventListener('click',async function ()
{
    const result = await SendOTP(gmail.value);

    if (result)
    {
        loading.classList.add('hidden');
        gmailSection.classList.add('hidden');
        otpSection.classList.remove('hidden');
        errorMsg.innerText = "OTP Resend in Your RegisterGmailID...";
        sendOtpBtn.disabled = true;
    }   
    else
    {
        sendOtpBtn.disabled = false;
        errorMsg.innerText = `Resend OTP has been Not Generated`;
    }

})

verifyOtpBtn.addEventListener('click', async function ()
{

    const _otp = otp.value.trim();

    if (_otp === "") {
        errorMsg.innerText = "🔐 Please enter the OTP.";
        return;
    }

    const ResultOfVerifyOTP = await VeryfiOTP(_otp);
    if (ResultOfVerifyOTP._VerifyOTP)
    {
        // otp success
        errorMsg.style.color = '#00FF00';
        errorMsg.innerText = "OTP Verify SuccessFully.....";
        setTimeout(() => {
            window.location.href = '/Password/ResetPassword'
        }, 2000)
    }
    else
    {
        errorMsg.style.color = 'red';
        errorMsg.innerText = ResultOfVerifyOTP.reason
    }

});


