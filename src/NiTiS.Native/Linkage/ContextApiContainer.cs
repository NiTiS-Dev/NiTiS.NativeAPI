using System.Collections.Generic;

namespace NiTiS.Native.Linkage;

/// <summary>
/// Container for contextual api getProcAddress function.
/// </summary>
public static class ContextApiContainer
{
	private static readonly Dictionary<string, nuint> context;

	/// <summary>
	/// Registry getProcAddress function for specific contextual api.
	/// </summary>
	/// <param name="apiName">Name of the contextual api.</param>
	/// <param name="getProcAddress">Function reference.</param>
	public static void RegistryContextualApiGetProcAddress(string apiName, FunctionReference getProcAddress)
	{
		context[apiName] = getProcAddress.Callee;
	}

	/// <summary>
	/// Reads context of the contextual api.
	/// </summary>
	/// <param name="apiName">Name of the contextual api.</param>
	/// <returns>Reference to getProcAddress (context) function.</returns>
	public static unsafe FunctionReference ContextualApiGetProcAddress(string apiName)
	{
		if (context.TryGetValue(apiName, out nuint @ref)) {
			return new FunctionReference((void*)@ref);
		}

		return default;
	}

	static ContextApiContainer()
	{
		context = new(4)
		{
			["OpenGL"] = 0,
			["Vulkan"] = 0,
		};
	}
}