using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000012 RID: 18
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class DisableInAttribute : Attribute
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000025A4 File Offset: 0x000007A4
		public DisableInAttribute(PrefabKind prefabKind)
		{
			this.PrefabKind = prefabKind;
		}

		// Token: 0x04000044 RID: 68
		public PrefabKind PrefabKind;
	}
}
