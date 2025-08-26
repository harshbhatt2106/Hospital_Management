import { SendOTP } from './Forgetpassword.js';
import { VeryfiOTP } from './AllReletedOTP.js';
import { VerificationGmail } from './AllReletedOTP.js';

const emailid = document.getElementById('email');
const mobileNumber = document.getElementById('mobilenumber');
const otp = document.getElementById('otp');
const sendOTPButton = document.getElementById('SendOtpBtn');
const Addadminbutton= document.getElementById('AddAdminBtn');
const otpSection = document.getElementById("otpSection");


//document.getElementById("SendOtpBtn").addEventListener("click", function () {
//    document.getElementById("otpSection").classList.remove("d-none");
//});



window.addEventListener('load', () =>
{
    sendOTPButton.disabled = true;
})

// check EmailIsAlreadyISExitsOrNot



SendOTP()

// SendOTP

// VeryFyOTP


