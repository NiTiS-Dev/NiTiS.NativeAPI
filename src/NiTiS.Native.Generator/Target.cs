using System;
using System.Linq;
using System.Reflection;

namespace NiTiS.Native.Generator;

public record class Target(string ProjectPath, string AssemblyName)
{
	public Assembly GetAssembly()
	{
		Assembly asm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(asm =>
		{
			if (asm.GetName().Name == AssemblyName)
				return true;

			return false;
        });

		return asm ?? Assembly.LoadFrom(AssemblyName + ".dll");
	}
} 