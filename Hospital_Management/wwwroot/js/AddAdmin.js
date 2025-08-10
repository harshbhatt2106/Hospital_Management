import { SendOTP } from './Forgetpassword.js';

const emailid = document.getElementById('email');
const mobileNumber = document.getElementById('mobilenumber');
const otp = document.getElementById('otp');
const sendOTPButton = document.getElementById('SendOtpBtn');
const Addadminbutton= document.getElementById('AddAdminBtn');


window.addEventListener('load', () => {
    sendOTPButton.disabled = true;
})

// check EmailIsAlreadyISExitsOrNot

SendOTP()

// SendOTP

// VeryFyOTP


