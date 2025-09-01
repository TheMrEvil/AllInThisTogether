using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000040 RID: 64
	[DontApplyToListElements]
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class LabelTextAttribute : Attribute
	{
		// Token: 0x060000AC RID: 172 RVA: 0x00002BA9 File Offset: 0x00000DA9
		public LabelTextAttribute(string text)
		{
			this.Text = text;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public LabelTextAttribute(SdfIconType icon)
		{
			this.Icon = icon;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00002BC7 File Offset: 0x00000DC7
		public LabelTextAttribute(string text, bool nicifyText)
		{
			this.Text = text;
			this.NicifyText = nicifyText;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002BDD File Offset: 0x00000DDD
		public LabelTextAttribute(string text, SdfIconType icon)
		{
			this.Text = text;
			this.Icon = icon;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00002BF3 File Offset: 0x00000DF3
		public LabelTextAttribute(string text, bool nicifyText, SdfIconType icon)
		{
			this.Text = text;
			this.NicifyText = nicifyText;
			this.Icon = icon;
		}

		// Token: 0x0400008E RID: 142
		public string Text;

		// Token: 0x0400008F RID: 143
		public bool NicifyText;

		// Token: 0x04000090 RID: 144
		public SdfIconType Icon;

		// Token: 0x04000091 RID: 145
		public string IconColor;
	}
}
