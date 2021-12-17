namespace Dn6Poc.DocuMgmtPortal.Services
{
    public class JwtService
    {

        public JwtService(IConfiguration configuration, ILogger<JwtService> logger)
        {
            //System.Data.DBConcurrencyException
            
            //var client = new MongoClient(_configuration["mongoDb:safeTravel"]);
        }

        public void StoreToken(string username, string token)
        {

        }

        public string GetToken(string username)
        {
            return "";
        }
    }
}
