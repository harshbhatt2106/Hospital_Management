namespace Hospital_Management.Interfaces
{
    public interface IotpService
    {
        public Task<bool> SendOTP(string gmail);
        public (bool isExpiry, bool isValid) verify_OTP(int otp,int UserEnterdOTP);
    }
}
