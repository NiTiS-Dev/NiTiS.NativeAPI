using System;

namespace NiTiS.Native.Linkage;

public sealed class ReturnAttribute : Attribute
{
	public required Type Type { get; init; }
}