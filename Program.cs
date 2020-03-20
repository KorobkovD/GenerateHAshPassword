using System;
using System.Text;

namespace GenerateHashPassword
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var imei = "869493026608130";
            Console.WriteLine(CreateMD5(imei));
            Console.ReadLine();
        }

        private static string CreateMD5(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                foreach (var hByte in hashBytes)
                {
                    sb.Append(hByte.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}