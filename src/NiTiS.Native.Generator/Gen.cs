using NiTiS.Core.Annotations;
using NiTiS.Native.Linkage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NiTiS.Native.Generator;

public static class Gen
{
	private static List<Target> targets;

	public static void Main(string[] _)
	{
		Console.WriteLine(Environment.CurrentDirectory);
        foreach (Target target in targets)
		{
			AnalyzeTarget(target);
		}
	}
	private static void AnalyzeTarget(Target target)
	{
		Assembly asm = target.GetAssembly();

        Console.WriteLine(asm);

		Type[] types = asm.GetTypes();

		foreach (Type type in types)
		{
            AnalyzeType(target, type);
        }
	}
	private static void AnalyzeType(Target target, Type type)
	{
		if (!type.FullName.StartsWith("NiTiS."))
			return;

		ApiAttribute attribute = (ApiAttribute)type.GetCustomAttribute(typeof(ApiAttribute));
		if (attribute is null)
			return;

		IEnumerable<NativePropertyAttribute> props = type.GetCustomAttributes<NativePropertyAttribute>();

		Dictionary<string, string> values = new(4);
		foreach (NativePropertyAttribute prop in props)
		{
			values[prop.Type] = prop.Value;
		}

		bool _hideGeneratedMembers = values.Any(x
			=> x.Key is "Api/HideGeneratedMembers"
			&& x.Value is "true" or "True"
			);

		string _outputPath = values.First(x => x.Key is "Api/OutputPath").Value;

		Task _tasks = GetTasks(values.GetValueOrDefault("Api/Tasks"));

		Options opts = new(_tasks, _outputPath, _hideGeneratedMembers);

		TypeGen.Analyze(opts, type);
	}
	private static Task GetTasks(string tasks)
	{
		if (string.IsNullOrWhiteSpace(tasks))
			return 0;

		Task result = 0;

		foreach (string task in tasks.Split(';'))
		{
			if (Enum.TryParse<Task>(task, out Task res))
			{
				result |= res;
			}
			else
				throw new Exception();
		}

		return result;
	}

	static Gen()
	{
		targets = new()
		{
			new("src/NiTiS.Native.GLFW", "NiTiS.Native.GLFW")
		};
	}
}
public record class Options(Task Tasks, string OutputFile, bool HideGeneratedMembers);