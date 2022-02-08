using System.Threading.Tasks;
using MarketPlace.Application.Services.Interfaces;

namespace MarketPlace.Application.Services.Implementations
{
    public class SmsService : ISmsService
    {
        private string apiKey = "57414754384673787942643978306D644B6F6B716F324F6277386D6D6F64526362646A4230412F7651634D3D";


        public async Task SendVerificationSms(string mobile, string activationCode)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);
            await api.VerifyLookup(mobile, activationCode, "Verification");
        }

        public async Task SendUserPasswordSms(string mobile, string password)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(apiKey);
            await api.VerifyLookup(mobile, password, "Verification");
        }
    }
}
