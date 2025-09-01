using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000059 RID: 89
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class RequiredInAttribute : Attribute
	{
		// Token: 0x0600012F RID: 303 RVA: 0x00003560 File Offset: 0x00001760
		public RequiredInAttribute(PrefabKind kind)
		{
			this.PrefabKind = kind;
		}

		// Token: 0x040000F6 RID: 246
		public string ErrorMessage;

		// Token: 0x040000F7 RID: 247
		public PrefabKind PrefabKind;
	}
}
