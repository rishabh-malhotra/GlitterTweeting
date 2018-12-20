using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlitterTweeting.Business
{
    public class PasswordHasher
    {
        private static string GetRandomSalt() => BCrypt.Net.BCrypt.GenerateSalt(12);

        /// <summary>
        /// Takes password string, adds salts and returns a hashed string
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());

        /// <summary>
        /// Takes the entered password and correctly hashed string to check validty
        /// </summary>
        /// <param name="password"></param>
        /// <param name="correctHash"></param>
        /// <returns></returns>
        public static bool ValidatePassword(string password, string correctHash) => BCrypt.Net.BCrypt.Verify(password, correctHash);

    }
}
