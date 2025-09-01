using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000071 RID: 113
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class ToggleAttribute : Attribute
	{
		// Token: 0x06000178 RID: 376 RVA: 0x00003CA5 File Offset: 0x00001EA5
		public ToggleAttribute(string toggleMemberName)
		{
			this.ToggleMemberName = toggleMemberName;
			this.CollapseOthersOnExpand = true;
		}

		// Token: 0x04000144 RID: 324
		public string ToggleMemberName;

		// Token: 0x04000145 RID: 325
		public bool CollapseOthersOnExpand;
	}
}
