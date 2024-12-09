using System.Security.Cryptography;
using System.Text;

namespace EvaluationBackend.Helpers
{
    public class UniqueIdentifierGenerator
    {
        private static readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();
        private static object lockObject = new object();

        public static string GenerateUniqueIdentifier(string userId)
        {
            lock (lockObject)
            {
                string prefix = userId.ToCharArray()[0].ToString().ToUpper() + userId.ToCharArray()[1].ToString().ToUpper() + "-";

                string dateTimePart = DateTimeOffset.UtcNow.ToString("yyMM");


                string randomDigits = GenerateRandomDigits(2);

                string combinedString = prefix + dateTimePart + randomDigits;

                string checkDigit = CalculateCheckDigit(combinedString);

                return combinedString + checkDigit;
            }
        }

        private static string GenerateRandomDigits(int length)
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);
            StringBuilder sb = new StringBuilder(length);
            foreach (byte b in randomBytes)
            {
                sb.Append(b % 10);
            }
            return sb.ToString();
        }

        private static string CalculateCheckDigit(string number)
        {
            int sum = 0;
            bool doubleDigit = false;

            for (int i = number.Length - 1; i >= 0; i--)
            {
                int digit = number[i] - '0';

                if (doubleDigit)
                {
                    digit *= 2;
                    if (digit > 9)
                        digit -= 9;
                }

                sum += digit;
                doubleDigit = !doubleDigit;
            }

            int remainder = sum % 10;

            return remainder == 0 ? "0" : (10 - remainder).ToString();
        }
    }
}