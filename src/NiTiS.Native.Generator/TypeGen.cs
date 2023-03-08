﻿using NiTiS.Core;
using NiTiS.Core.Annotations;
using NiTiS.Native.Linkage;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NiTiS.Native.Generator;

internal static class TypeGen
{
	private const string IgnoreNextTime = "[NiTiS.Core.Attributes.NativePropertyAttribute(Type=\"Api/Ignore\", Value=null)]";
	private const string GeneratedCode = "[System.Runtime.CompilerServices.CompilerGeneratedAttribute()]";
	public static void Analyze(Options options, Type type)
	{
		if (options.Tasks.HasFlag(Task.GenFile))
			TaskGenFile(options, type);
	}
	private static void TaskGenFile(Options options, Type type)
	{

		string getName;
		{
			NativePropertyAttribute attr = type.GetCustomAttributes<NativePropertyAttribute>().FirstOrDefault(att => att.Type == "Api/NameProvider");

			if (attr != null)
			{
				getName = attr.Value;
			}
			else
			{
				getName = "_getname";
			}
		}

		CodeBuilder cb = new();

		cb.AppendLine($"namespace {type.Namespace};");

		cb.AppendLine($"unsafe partial {(type.IsValueType ? "struct" : "class")} {type.Name}");

		cb.BeginBlock(); // Type
		{
			cb.AppendLine(GeneratedCode);
			cb.AppendLine($"static {type.Name}()");
			cb.BeginBlock(); // cctor
			{
				// Get std loader
				cb.AppendLine("var x0 = NiTiS.Native.Loaders.NativeLibraryLoader.DefaultPlatformLoader;");
				cb.AppendLine("");
				cb.AppendLine($"var x1 = x0.LoadLibrary({getName}());");

				FieldInfo[] fields = type.GetFields();

				foreach (FieldInfo field in fields)
				{
					NativePropertyAttribute apiMethodName = field.GetCustomAttributes<NativePropertyAttribute>().FirstOrDefault(x => x.Type == NativePropertyAttribute.NativeName);
					if (apiMethodName is not null)
					{
						string delegateT;
						{
							delegateT = "delegate *unmanaged";
							string callConv = field.GetCustomAttributes<NativePropertyAttribute>().FirstOrDefault(x => x.Type == "Native/CallConv").Value.ToLower() switch
							{
								"__cdecl" => "[Cdecl]",
								"__fastcall" => "[Fastcall]",
								"__thiscall" => "[Thiscall]",
								"__stdcall" => "[Stdcall]",
								_ => null
							};
							delegateT += callConv;
							delegateT += "<";

							foreach (ArgumentAttribute arg in field.GetCustomAttributes<ArgumentAttribute>())
							{
								delegateT += $"{(arg.Flow switch
								{
									Flow.In => "in ",
									Flow.Out => "out ",
									_ => ""
								})} {arg.Type.FullName},";
							}
							ReturnAttribute ret = field.GetCustomAttribute<ReturnAttribute>();
							if (ret is not null)
							{
								delegateT += ret.Type.NormalizedFullName();
							}
							else
							{
								delegateT += "void";
							}

							delegateT += ">";
						}

						cb.AppendLine($"{field.Name} = ({delegateT})x0.GetProcAddress(x1, \"{apiMethodName.Value}\").Callee;");
					}
					else
					{
						cb.AppendLine($"//Field {field.Name} is ignored!");
					}
				}
			}
			cb.EndBlock(); // cctor
		}
		cb.EndBlock(); // Type

		using FileStream fs = File.Create(Path.Combine("../src", type.Assembly.GetName().Name, options.OutputFile));
		cb.Write(fs);
    }
}