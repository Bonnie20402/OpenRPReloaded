using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRPReloaded.Enums
{
    public enum AccountCreationResult
    {
        Success,
        Fail_Username_Too_Long,
        Fail_Account_Already_Exists,
        Fail_Password_Too_Shorts,
        Fail_Password_Too_Long,
        Fail_Username_Too_Short,
        Fail
    }
}
