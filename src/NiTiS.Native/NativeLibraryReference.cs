using System;
using System.Runtime.CompilerServices;

namespace NiTiS.Native;

/// <summary>
/// Wrapper for native libraries.
/// </summary>
public readonly struct NativeLibraryReference
{
	/// <summary>
	/// Native pointer to loaded library.
	/// </summary>
	public readonly IntPtr Handle;

    public unsafe NativeLibraryReference(void* handle)
    {
		Handle = (nint)handle;
    }

    /// <inheritdoc/>
    public override string ToString()
		=> Handle.ToString();

	/// <inheritdoc/>
	public override int GetHashCode()
		=> Handle.ToInt32();

	/// <inheritdoc/>
	public override unsafe bool Equals(object? obj)
		=> obj is NativeLibraryReference @ref
		&& @ref.Handle == this.Handle;

	public static unsafe implicit operator void*(NativeLibraryReference self)
		=> self.Handle.ToPointer();

	public static unsafe implicit operator NativeLibraryReference(nint @ref)
		=> Unsafe.As<nint, NativeLibraryReference>(ref @ref);

	public static unsafe implicit operator NativeLibraryReference(nuint @ref)
		=> Unsafe.As<nuint, NativeLibraryReference>(ref @ref);

	public static bool operator ==(NativeLibraryReference left, NativeLibraryReference right)
		=> left.Handle == right.Handle;

	public static bool operator !=(NativeLibraryReference left, NativeLibraryReference right)
		=> left.Handle != right.Handle;

}
