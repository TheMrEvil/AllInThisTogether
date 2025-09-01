using System;
using System.Globalization;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[VisibleToOtherModules]
	internal sealed class UnityString
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000020FC File Offset: 0x000002FC
		public static string Format(string fmt, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture.NumberFormat, fmt, args);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000211F File Offset: 0x0000031F
		public UnityString()
		{
		}
	}
}
