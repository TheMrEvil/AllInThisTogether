using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200002D RID: 45
	[AttributeUsage(AttributeTargets.All)]
	[Conditional("UNITY_EDITOR")]
	public class HideInAttribute : Attribute
	{
		// Token: 0x06000089 RID: 137 RVA: 0x000028F8 File Offset: 0x00000AF8
		public HideInAttribute(PrefabKind prefabKind)
		{
			this.PrefabKind = prefabKind;
		}

		// Token: 0x04000065 RID: 101
		public PrefabKind PrefabKind;
	}
}
