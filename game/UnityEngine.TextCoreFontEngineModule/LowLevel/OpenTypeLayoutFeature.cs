using System;
using System.Diagnostics;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200001B RID: 27
	[DebuggerDisplay("Feature = {tag},  Lookup Count = {lookupIndexes.Length}")]
	[Serializable]
	internal struct OpenTypeLayoutFeature
	{
		// Token: 0x040000B6 RID: 182
		public string tag;

		// Token: 0x040000B7 RID: 183
		public uint[] lookupIndexes;
	}
}
