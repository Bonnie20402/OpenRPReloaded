using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRPReloaded.Enums.Account
{
    public enum AccountCreationResult
    {
        Success,
        Fail_Account_Already_Exists,
        Fail_Invalid_Username,
        Fail_Invalid_Password,
        Fail
    }
}
