using NiTiS.Core;
using NiTiS.Core.Annotations;
using NiTiS.Native.Linkage;
using NiTiS.Native.Loaders;
using System.ComponentModel;
using System.Diagnostics;
using static NiTiS.Core.Annotations.NativePropertyAttribute;

namespace NiTiS.Native.GLFW;

[Api]
[NativeProperty(Type = "Api/HideGeneratedMembers", Value = "true")]
[NativeProperty(Type = "Api/OutputPath", Value = "Glfw.g.cs")]
[NativeProperty(Type = "Api/Tasks", Value = "AnalyzeH;GenFile")]
[NativeProperty(Type = "Api/NameProvider", Value = nameof(_nameprov))]
public unsafe partial class Glfw
{
	private static string _nameprov()
	{
		if (MachineInfo.IsWindows)
			return "glfw3.dll";
		else if (MachineInfo.IsLinux || MachineInfo.IsAnroid)
			return "libglfw.so.3.3";
		else if (MachineInfo.IsMacos || MachineInfo.IsIos)
			return "libglfw.3.dylib";
		else
			throw new System.PlatformNotSupportedException();
	}

	[NativeProperty(Type = NativeName, Value = "glfwInit")]
	[NativeProperty(Type = "Native/CallConv", Value = "__stdcall")]
	[Return(Type = typeof(int))]
	[Browsable(false)]
	public static readonly delegate* unmanaged[Stdcall]<int> _init;

	[NativeProperty(Type = NativeName, Value = "glfwInit")]
	[NativeProperty(Type = "Native/CallConv", Value = "__stdcall")]
	[Argument(Index = 0, Type = typeof(void*))]
	[Argument(Index = 1, Type = typeof(int))]
	[Browsable(false)]
	public static readonly delegate* unmanaged[Stdcall]<void*, int, void> _setWindowShouldClose;
}