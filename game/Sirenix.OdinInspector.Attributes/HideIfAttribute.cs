using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200002B RID: 43
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[DontApplyToListElements]
	[Conditional("UNITY_EDITOR")]
	public sealed class HideIfAttribute : Attribute
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000281D File Offset: 0x00000A1D
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002825 File Offset: 0x00000A25
		[Obsolete("Use the Condition member instead.", false)]
		public string MemberName
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = value;
			}
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000282E File Offset: 0x00000A2E
		public HideIfAttribute(string condition, bool animate = true)
		{
			this.Condition = condition;
			this.Animate = animate;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002844 File Offset: 0x00000A44
		public HideIfAttribute(string condition, object optionalValue, bool animate = true)
		{
			this.Condition = condition;
			this.Value = optionalValue;
			this.Animate = animate;
		}

		// Token: 0x04000061 RID: 97
		public string Condition;

		// Token: 0x04000062 RID: 98
		public object Value;

		// Token: 0x04000063 RID: 99
		public bool Animate;
	}
}
