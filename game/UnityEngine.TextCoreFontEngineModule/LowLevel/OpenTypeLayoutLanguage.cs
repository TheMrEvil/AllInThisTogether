using System;
using System.Diagnostics;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200001A RID: 26
	[DebuggerDisplay("Language = {tag},  Feature Count = {featureIndexes.Length}")]
	[Serializable]
	internal struct OpenTypeLayoutLanguage
	{
		// Token: 0x040000B4 RID: 180
		public string tag;

		// Token: 0x040000B5 RID: 181
		public uint[] featureIndexes;
	}
}
