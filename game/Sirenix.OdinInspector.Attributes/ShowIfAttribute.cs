using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000061 RID: 97
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class ShowIfAttribute : Attribute
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00003772 File Offset: 0x00001972
		// (set) Token: 0x0600014E RID: 334 RVA: 0x0000377A File Offset: 0x0000197A
		[Obsolete("Use the Condition member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x0600014F RID: 335 RVA: 0x00003783 File Offset: 0x00001983
		public ShowIfAttribute(string condition, bool animate = true)
		{
			this.Condition = condition;
			this.Animate = animate;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003799 File Offset: 0x00001999
		public ShowIfAttribute(string condition, object optionalValue, bool animate = true)
		{
			this.Condition = condition;
			this.Value = optionalValue;
			this.Animate = animate;
		}

		// Token: 0x04000109 RID: 265
		public string Condition;

		// Token: 0x0400010A RID: 266
		public object Value;

		// Token: 0x0400010B RID: 267
		public bool Animate;
	}
}
