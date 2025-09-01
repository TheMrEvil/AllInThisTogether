using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200000B RID: 11
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class CustomContextMenuAttribute : Attribute
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000024C3 File Offset: 0x000006C3
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000024CB File Offset: 0x000006CB
		[Obsolete("Use the Action member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MethodName
		{
			get
			{
				return this.Action;
			}
			set
			{
				this.Action = value;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000024D4 File Offset: 0x000006D4
		public CustomContextMenuAttribute(string menuItem, string action)
		{
			this.MenuItem = menuItem;
			this.Action = action;
		}

		// Token: 0x04000034 RID: 52
		public string MenuItem;

		// Token: 0x04000035 RID: 53
		public string Action;
	}
}
