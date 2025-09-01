using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000048 RID: 72
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Conditional("UNITY_EDITOR")]
	[DontApplyToListElements]
	[IncludeMyAttributes]
	[HideInTables]
	public class OnInspectorDisposeAttribute : ShowInInspectorAttribute
	{
		// Token: 0x060000DF RID: 223 RVA: 0x00002E69 File Offset: 0x00001069
		public OnInspectorDisposeAttribute()
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002E71 File Offset: 0x00001071
		public OnInspectorDisposeAttribute(string action)
		{
			this.Action = action;
		}

		// Token: 0x040000BB RID: 187
		public string Action;
	}
}
