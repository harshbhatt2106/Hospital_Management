using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Hospital_Management.Interfaces
{
    public interface IotpService
    {
        public bool SendOTP(string gmail);
        public bool VerifyOTP([FromBody]int otp,int UserEnterdOTP);

        public void VerifyGmail(string gmailid);
    }
}
