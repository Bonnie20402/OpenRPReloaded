using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRPReloaded.Enums.Account
{
    public enum AccountPasswordUpdateResult
    {
        Success,
        Fail_Due_To_Password_Mismatch,
        Fail_Due_To_Same_Password,
        Fail_Due_To_Missing_Account,
        Fail_Due_To_Account_Disabled,
        Fail_Due_To_Database_Error
    }
}
