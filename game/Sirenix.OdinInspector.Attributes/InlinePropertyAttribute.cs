using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200003F RID: 63
	[AttributeUsage(AttributeTargets.All, Inherited = false)]
	[Conditional("UNITY_EDITOR")]
	public class InlinePropertyAttribute : Attribute
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00002102 File Offset: 0x00000302
		public InlinePropertyAttribute()
		{
		}

		// Token: 0x0400008D RID: 141
		public int LabelWidth;
	}
}
