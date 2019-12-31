using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BroomService_App.Repository
{
    public class Common
    {
        /// <summary>
        /// Checking the internet connection.
        /// </summary>
        /// <returns></returns>
        public static bool CheckConnection()
        {
            var con = CrossConnectivity.Current.IsConnected;
            return con == true ? true : false;
        }

        /// <summary>
        /// Checks the valid email.
        /// </summary>
        /// <returns><c>true</c>, if valid email was checked, <c>false</c> otherwise.</returns>
        /// <param name="Email">Email.</param>
        public static bool CheckValidEmail(string Email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (match.Success)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Make first char of input to upper case
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}
