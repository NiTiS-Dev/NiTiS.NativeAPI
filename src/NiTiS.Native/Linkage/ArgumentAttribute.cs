using System;

namespace NiTiS.Native.Linkage;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
public sealed class ArgumentAttribute : Attribute
{
    public required ushort Index { get; init; }
	public required Type Type { get; init; }
	public Flow Flow { get; init; }
}
public enum Flow
{
	None = 0,
	In = 1,
	Out = 2,
}