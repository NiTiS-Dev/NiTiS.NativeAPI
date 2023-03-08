using System;

namespace NiTiS.Native;

public class ApiException : Exception
{
    public ApiException(string message, Exception? exception = null) : base(message, exception)
    {
        
    }
}
