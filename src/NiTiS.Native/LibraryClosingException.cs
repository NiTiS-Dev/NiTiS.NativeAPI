using System;

namespace NiTiS.Native;

public sealed class LibraryUnloadException : ApiException
{
	public LibraryUnloadException(string message, Exception? exception = null) : base(message, exception)
	{

	}
}