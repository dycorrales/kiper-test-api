using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Kiper.Condominio.CrossCutting.Identity.Authorization
{
    public class SigningCredentialsConfiguration
    {
        private const string SecretKey = "kiperBF389F54-FE47-4957-B808-8DF08B7431B6";
        public static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public SigningCredentials SigningCredentials { get; }

        public SigningCredentialsConfiguration()
        {
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}
