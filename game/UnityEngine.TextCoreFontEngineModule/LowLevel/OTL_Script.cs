using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000010 RID: 16
	[DebuggerDisplay("Script = {tag},  Language Count = {languages.Length}")]
	[UsedByNativeCode]
	internal struct OTL_Script
	{
		// Token: 0x04000083 RID: 131
		public string tag;

		// Token: 0x04000084 RID: 132
		public OTL_Language[] languages;
	}
}
