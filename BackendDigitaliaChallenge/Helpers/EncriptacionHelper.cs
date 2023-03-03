using System;
using System.Linq;

namespace BackendDigitaliaChallenge.Helpers
{
    public class EncriptacionHelper
    {
        public static string Encriptar(string cadena)
        {
            string result = string.Empty;
            byte[] encryted =
            System.Text.Encoding.Unicode.GetBytes(cadena);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static string Desencriptar(string cadena)
        {
            string result = string.Empty;
            byte[] decryted =
            Convert.FromBase64String(cadena);
            //result = 
            System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
