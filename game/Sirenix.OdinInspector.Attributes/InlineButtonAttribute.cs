using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200003D RID: 61
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class InlineButtonAttribute : Attribute
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002A61 File Offset: 0x00000C61
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002A69 File Offset: 0x00000C69
		[Obsolete("Use the Action member instead.", false)]
		public string MemberMethod
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

		// Token: 0x060000A3 RID: 163 RVA: 0x00002A72 File Offset: 0x00000C72
		public InlineButtonAttribute(string action, string label = null)
		{
			this.Action = action;
			this.Label = label;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002A88 File Offset: 0x00000C88
		public InlineButtonAttribute(string action, SdfIconType icon, string label = null)
		{
			this.Action = action;
			this.Icon = icon;
			this.Label = label;
		}

		// Token: 0x0400007A RID: 122
		public string Action;

		// Token: 0x0400007B RID: 123
		public string Label;

		// Token: 0x0400007C RID: 124
		public string ShowIf;

		// Token: 0x0400007D RID: 125
		public string ButtonColor;

		// Token: 0x0400007E RID: 126
		public string TextColor;

		// Token: 0x0400007F RID: 127
		public SdfIconType Icon;

		// Token: 0x04000080 RID: 128
		public IconAlignment IconAlignment;
	}
}
