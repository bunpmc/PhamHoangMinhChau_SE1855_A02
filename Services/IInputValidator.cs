using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IInputValidator
    {
        public bool isPhoneValidation (string phoneNumber);
    }
}
