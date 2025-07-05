using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public class InputValidator : IInputValidator
    {
        public bool isPhoneValidation(string phoneNumber)
        {
            string regex = @"^(?:\+84|84|0)(3|5|7|8|9)\d{8}$";
            return Regex.IsMatch(phoneNumber, regex);
        }
    }
}
