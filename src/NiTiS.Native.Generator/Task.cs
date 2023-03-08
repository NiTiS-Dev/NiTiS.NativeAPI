using System;

namespace NiTiS.Native.Generator;

[Flags]
public enum Task : byte
{
	AnalyzeH = 1,
	GenFile = 2,
}
