using Ships.Application.Interfaces;

namespace Ships.Application.Services
{
    public class CodeGeneratorService : ICodeGeneratorService
    {
        public string GenerateShipCode()
        {
            const string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";

            var codeChars = new char[12];
            var random = new Random();
            short i = 0;

            while (i < codeChars.Length)
            {
                if (i < 4 || i == 10)
                    codeChars[i] = alphabets[random.Next(alphabets.Length)];
                else if (i == 4 || i == 9)
                    codeChars[i] = '-';
                else
                    codeChars[i] = numbers[random.Next(numbers.Length)];

                i++;
            }

            return new string(codeChars);
        }
    }
}
