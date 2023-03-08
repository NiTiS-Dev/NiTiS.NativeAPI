using System;
using System.Runtime.CompilerServices;

namespace NiTiS.Native;

/// <summary>
/// Reference to unmanaged function.
/// </summary>
public readonly unsafe struct FunctionReference
{
	/// <summary>
	/// Reference to unmanaged function.
	/// </summary>
	public readonly UIntPtr Callee;

	/// <summary>
	/// Creates a function reference.
	/// </summary>
	/// <param name="pFunction">Pointer to the function.</param>
	public FunctionReference(void* pFunction)
    {
		this.Callee = (UIntPtr)pFunction;
    }

	public delegate* managed<void> ToManaged()
		=> (delegate* managed<void>)(void*)Callee;
	public delegate* managed<Arg1, void> ToManaged<Arg1>()
		=> (delegate* managed<Arg1, void>)(void*)Callee;
	public delegate* managed<Arg1, Arg2, void> ToManaged<Arg1, Arg2>()
		=> (delegate* managed<Arg1, Arg2, void>)(void*)Callee;
	public delegate* managed<Arg1, Arg2, Arg3, void> ToManaged<Arg1, Arg2, Arg3>()
		=> (delegate* managed<Arg1, Arg2, Arg3, void>)(void*)Callee;
	public delegate* managed<Arg1, Arg2, Arg3, Arg4, void> ToManaged<Arg1, Arg2, Arg3, Arg4>()
		=> (delegate* managed<Arg1, Arg2, Arg3, Arg4, void>)(void*)Callee;
	public delegate* managed<Arg1, Arg2, Arg3, Arg4, Arg5, void> ToManaged<Arg1, Arg2, Arg3, Arg4, Arg5>()
		=> (delegate* managed<Arg1, Arg2, Arg3, Arg4, Arg5, void>)(void*)Callee;
}
