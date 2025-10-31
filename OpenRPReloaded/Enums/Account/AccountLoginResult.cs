using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRPReloaded.Enums.Account
{
    public enum AccountLoginResult
    {
        Success,
        Fail_Due_To_Ban,
        Fail_Due_To_Missing_Account,
        Fail_Due_To_Wrong_Password,
        Fail_Due_To_Account_Disabled,
        Fail_Due_To_Database_Error
    }
}
