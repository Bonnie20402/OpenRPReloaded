using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRPReloaded.Enums.Account
{
    public enum AccountUsernameUpdateResult
    {
        Success,
        Fail_Due_To_Username_In_Use,
        Fail_Due_To_Missing_Account,
        Fail_Due_To_Invalid_Username,
        Fail_Due_To_Account_Disabled,
        Fail_Due_To_Database_Error
    }
}
