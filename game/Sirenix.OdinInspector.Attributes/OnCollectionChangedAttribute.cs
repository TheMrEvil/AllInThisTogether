using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000047 RID: 71
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class OnCollectionChangedAttribute : Attribute
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00002102 File Offset: 0x00000302
		public OnCollectionChangedAttribute()
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002E44 File Offset: 0x00001044
		public OnCollectionChangedAttribute(string after)
		{
			this.After = after;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002E53 File Offset: 0x00001053
		public OnCollectionChangedAttribute(string before, string after)
		{
			this.Before = before;
			this.After = after;
		}

		// Token: 0x040000B9 RID: 185
		public string Before;

		// Token: 0x040000BA RID: 186
		public string After;
	}
}
