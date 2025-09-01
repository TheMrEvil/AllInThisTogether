using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200001A RID: 26
	[Conditional("UNITY_EDITOR")]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	public sealed class DisallowModificationsInAttribute : Attribute
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000025B3 File Offset: 0x000007B3
		public DisallowModificationsInAttribute(PrefabKind kind)
		{
			this.PrefabKind = kind;
		}

		// Token: 0x04000045 RID: 69
		public PrefabKind PrefabKind;
	}
}
