using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000046 RID: 70
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class MultiLinePropertyAttribute : Attribute
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00002E2F File Offset: 0x0000102F
		public MultiLinePropertyAttribute(int lines = 3)
		{
			this.Lines = Math.Max(1, lines);
		}

		// Token: 0x040000B8 RID: 184
		public int Lines;
	}
}
