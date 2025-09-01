using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000012 RID: 18
	[UsedByNativeCode]
	[DebuggerDisplay("Feature = {tag},  Lookup Count = {lookupIndexes.Length}")]
	internal struct OTL_Feature
	{
		// Token: 0x04000087 RID: 135
		public string tag;

		// Token: 0x04000088 RID: 136
		public uint[] lookupIndexes;
	}
}
