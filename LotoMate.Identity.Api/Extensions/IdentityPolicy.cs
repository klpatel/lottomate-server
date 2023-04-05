using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotoMate.Identity.API.Extensions
{
    public class IdentityPolicy
    {
        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// </summary>
        /// <returns>A random password</returns>
        public static string GenerateRandomPassword()
        {
            var options = BuildPasswordOptions();

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();
            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "!@$?_-"                        // non-alphanumeric
            };

            if (options.Password.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (options.Password.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (options.Password.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (options.Password.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < options.Password.RequiredLength
                || chars.Distinct().Count() < options.Password.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

        public static IdentityOptions BuildPasswordOptions()
        {
            return new IdentityOptions()
            {
                Password = new PasswordOptions()
                {
                    // Password settings.
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase = true,
                    RequiredLength = 8,
                    RequiredUniqueChars = 1
                },
                // Lockout settings.
                Lockout = new LockoutOptions()
                {
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
                    MaxFailedAccessAttempts = 5,
                    AllowedForNewUsers = true
                },
                // User settings.
                User = new UserOptions()
                {
                    AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+",
                    RequireUniqueEmail = true
                }
            };
        }

        /// <summary>
        /// Generates a Random Numbers
        /// </summary>
        /// <returns>A random numbers</returns>
        public static string GenerateOTP()
        {
            string _numbers = "0123456789";
            Random random = new Random();
            StringBuilder otp = new StringBuilder(6);
       
            for (int i = 0; i< 6; i++)
            {
                otp.Append(_numbers[random.Next(0, _numbers.Length)]);
            }
            return otp.ToString(); 
        }
    }
}
