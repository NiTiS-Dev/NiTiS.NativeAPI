using NiTiS.Core;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace NiTiS.Native.Generator;

public class CodeBuilder : IDisposable
{
	private readonly StringBuilder sb = new();
	private int indent;
    public CodeBuilder()
    {
		indent = 0;
    }
	public void BeginBlock()
	{
		AppendLine("{");
		IndentPlus();
	}
	public void EndBlock()
	{
		IndentMinus();
		AppendLine("}");
	}
	public void AppendLine(string line)
	{
		sb.AppendLine(Indent() + line);
	}
	public void AppendLines(params string[] lines)
	{
		for (int i = 0; i < lines.Length; i++)
		{
			AppendLine(lines[i]);
		}
	}
	private string Indent()
		=> Strings.Multiply("\t", indent);
	public void Dispose()
	{
		sb.Clear();
	}
	public void IndentPlus()
	{
		this.indent += 1;
	}
	public void IndentMinus()
	{
		this.indent -= 1;
	}

	public void Write(FileStream fs)
	{
		using TextWriter tw = new StreamWriter(fs);

		tw.Write(sb.ToString());
	}
}