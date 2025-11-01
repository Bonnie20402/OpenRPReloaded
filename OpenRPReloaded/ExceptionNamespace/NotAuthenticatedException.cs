using System;

namespace OpenRPReloaded.ExceptionNamespace
{
    /// <summary>
    /// Representa um erro a uma ação que necessita que a conta esteja autenticada.
    /// </summary>
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException() : base() { }

        public NotAuthenticatedException(string message) : base(message) { }



    }
}
