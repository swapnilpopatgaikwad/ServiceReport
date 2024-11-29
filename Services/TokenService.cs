using Microsoft.EntityFrameworkCore;
using ServiceReport.Data;
using ServiceReport.Interface;


namespace ServiceReport.Services
{
    public class TokenService : ITokenService
    {
        private readonly ServiceReportContext _context;


        public TokenService(ServiceReportContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateTokenAsync()
        {
            var currentYear = DateTime.Now.Year;

            // Check if a record already exists for the current year
            var tokenRecord = await _context.ReportService.LastOrDefaultAsync();

            // Determine the next serial number
            int nextSerialNumber = tokenRecord == null ? 000001 : GetSerialnumber(tokenRecord.TokenNumber)+1;

            // Generate the token
            string token = $"SIL{nextSerialNumber:D6}/{currentYear}";

           return token ;
        }

        public int GetSerialnumber(string tokennumber)
        {
            string result = string.Empty;

            foreach (char c in tokennumber)
            {
                if (char.IsDigit(c))
                {
                    result += c;
                }
            }

            return int.Parse(result);

        }

        }
}
