using System;
using System.Collections.Generic;
using System.Text;

namespace GenerateHashPassword
{
    internal class Program
    {
        private const string IsDigitsFlag = "-d";

        private static readonly Dictionary<char, char> replacingRules = new Dictionary<char, char>
        {
            { 'A','0' },
            { 'B','1' },
            { 'C','2' },
            { 'D','3' },
            { 'E','4' },
            { 'F','5' }
        };

        // Первым параметром строка
        // Вторым параметром длина пароля
        // Третьим параметром указание флага наличия только цифр в пароле
        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Неверное количество аргументов");
                return;
            }

            var imei = args[0];
            if (imei.Length < 15 || imei.Length > 17)
            {
                Console.WriteLine("Указан некорректный IMEI");
                return;
            }

            if (!byte.TryParse(args[1], out var passwordLength))
            {
                Console.WriteLine("Указана неверная длина пароля");
                return;
            }

            var isDigits = false;

            if (args.Length >= 3)
            {
                isDigits = args[2].ToLower() == IsDigitsFlag;
            }

            var hash = CreateMD5(imei);
            var password = hash.Substring(0, passwordLength);

            if (isDigits)
            {
                password = ReplaceDigits(password);
            }

            Console.WriteLine($"Пароль для устройства {imei}: {password}");
            Console.ReadLine();
        }

        /// <summary>
        /// Создание хеш для строки
        /// </summary>
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

        /// <summary>
        /// Замена символов на цифры в пароле
        /// </summary>
        /// <param name="password">Пароль</param>
        private static string ReplaceDigits(string password)
        {
            var builder = new StringBuilder(password.Length);
            foreach (var symbol in password)
            {
                var addingChar = symbol;
                if (replacingRules.ContainsKey(symbol))
                {
                    addingChar = replacingRules[symbol];
                }

                builder.Append(addingChar);
            }

            return builder.ToString();
        }
    }
}