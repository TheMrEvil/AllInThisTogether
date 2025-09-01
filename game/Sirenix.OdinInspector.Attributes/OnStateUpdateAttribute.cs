using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200004B RID: 75
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	[IncludeMyAttributes]
	[HideInTables]
	public sealed class OnStateUpdateAttribute : Attribute
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00002EBF File Offset: 0x000010BF
		public OnStateUpdateAttribute(string action)
		{
			this.Action = action;
		}

		// Token: 0x040000C1 RID: 193
		public string Action;
	}
}
