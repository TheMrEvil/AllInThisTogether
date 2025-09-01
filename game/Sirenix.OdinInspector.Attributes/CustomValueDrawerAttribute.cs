using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class CustomValueDrawerAttribute : Attribute
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000024EA File Offset: 0x000006EA
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000024F2 File Offset: 0x000006F2
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

		// Token: 0x06000041 RID: 65 RVA: 0x000024FB File Offset: 0x000006FB
		public CustomValueDrawerAttribute(string action)
		{
			this.Action = action;
		}

		// Token: 0x04000036 RID: 54
		public string Action;
	}
}
