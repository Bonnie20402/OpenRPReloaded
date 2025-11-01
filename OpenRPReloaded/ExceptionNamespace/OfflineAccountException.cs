using System;

namespace OpenRPReloaded.ExceptionNamespace
{
    /// <summary>
    /// Representa um erro a uma ação que necessita que a conta esteja online.
    /// </summary>
    public class OfflineAccountException : Exception
    {
        public OfflineAccountException() : base() { }

        public OfflineAccountException(string message) : base(message) { }


    }
}
