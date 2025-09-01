using System;
using System.Reflection;
using UnityEngine.Bindings;

namespace UnityEngine.TestTools
{
	// Token: 0x0200048E RID: 1166
	[NativeType(CodegenOptions.Custom, "ManagedCoveredSequencePoint", Header = "Runtime/Scripting/ScriptingCoverage.bindings.h")]
	public struct CoveredSequencePoint
	{
		// Token: 0x04000FA7 RID: 4007
		public MethodBase method;

		// Token: 0x04000FA8 RID: 4008
		public uint ilOffset;

		// Token: 0x04000FA9 RID: 4009
		public uint hitCount;

		// Token: 0x04000FAA RID: 4010
		public string filename;

		// Token: 0x04000FAB RID: 4011
		public uint line;

		// Token: 0x04000FAC RID: 4012
		public uint column;
	}
}
