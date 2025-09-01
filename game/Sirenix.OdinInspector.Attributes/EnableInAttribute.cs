using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000023 RID: 35
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class EnableInAttribute : Attribute
	{
		// Token: 0x0600006B RID: 107 RVA: 0x0000273B File Offset: 0x0000093B
		public EnableInAttribute(PrefabKind prefabKind)
		{
			this.PrefabKind = prefabKind;
		}

		// Token: 0x0400004F RID: 79
		public PrefabKind PrefabKind;
	}
}
