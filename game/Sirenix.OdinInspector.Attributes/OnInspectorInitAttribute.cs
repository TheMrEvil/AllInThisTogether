using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200004A RID: 74
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
	[Conditional("UNITY_EDITOR")]
	[DontApplyToListElements]
	[IncludeMyAttributes]
	[HideInTables]
	public class OnInspectorInitAttribute : ShowInInspectorAttribute
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00002E69 File Offset: 0x00001069
		public OnInspectorInitAttribute()
		{
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002EB0 File Offset: 0x000010B0
		public OnInspectorInitAttribute(string action)
		{
			this.Action = action;
		}

		// Token: 0x040000C0 RID: 192
		public string Action;
	}
}
