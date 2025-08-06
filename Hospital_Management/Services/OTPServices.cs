using Hospital_Management.Data;
using Hospital_Management.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Services
{
    public class OTPServices : IotpService
    {
        private readonly HospitalDbContext _context;
        private readonly IHttpContextAccessor _content;
        private readonly IEmailservice emailservices;

        public OTPServices(HospitalDbContext hospitalDbContext,
                          IHttpContextAccessor httpContent,
                          IEmailservice emailservices)
        {
            _content = httpContent;
            _context = hospitalDbContext;
            this.emailservices = emailservices;
        }

        public bool SendOTP(string gmail)
        {
            int OTP = new Random().Next(100000, 999999);
            _content.HttpContext.Session.SetInt32("AdminOTP", OTP);
            _content.HttpContext.Session.SetString("ForgetPasswordProgress", "True");

            string Subject_of_ForgetEmail = "Your Admin Account OTP for Password Reset"; ;
            string body = $@"
                        <h2>Password Reset Request</h2>
                        <p>Dear Admin,</p>
                        <p>We received a request to reset the password for your admin account.</p>
                        <p><strong>Your OTP is: <span style='color:blue; font-size:18px;'>{OTP}</span></strong></p>
                        <p>Please enter this OTP on the verification screen to proceed.</p>
                        <p>If you did not request this, please ignore this email.</p>
                        <p>This OTP valid 1 Time </p> 
                        <br/>
                        <p>Thanks,<br/>Hospital Management Team</p>
                    ";

            return emailservices.SendEmail(gmail, Subject_of_ForgetEmail, body);
        }

        public bool VerifyGmail(string gmailid)
        {
            return true;
        }


        public bool VerifyOTP([FromBody] int otp, int UserEnterdOTP)
        {
            if (otp != UserEnterdOTP)
            {
                return false;
            }
            return true;
        }

        void IotpService.VerifyGmail(string gmailid)
        {
            throw new NotImplementedException();
        }
    }
}
