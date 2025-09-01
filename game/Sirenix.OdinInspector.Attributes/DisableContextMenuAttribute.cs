using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000010 RID: 16
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class DisableContextMenuAttribute : Attribute
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002558 File Offset: 0x00000758
		public DisableContextMenuAttribute(bool disableForMember = true, bool disableCollectionElements = false)
		{
			this.DisableForMember = disableForMember;
			this.DisableForCollectionElements = disableCollectionElements;
		}

		// Token: 0x04000040 RID: 64
		public bool DisableForMember;

		// Token: 0x04000041 RID: 65
		public bool DisableForCollectionElements;
	}
}
