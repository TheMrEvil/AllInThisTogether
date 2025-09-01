using System;
using System.Diagnostics;
using UnityEngine;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class DisplayAsStringAttribute : Attribute
	{
		// Token: 0x06000053 RID: 83 RVA: 0x000025C2 File Offset: 0x000007C2
		public DisplayAsStringAttribute()
		{
			this.Overflow = true;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000025D1 File Offset: 0x000007D1
		public DisplayAsStringAttribute(bool overflow)
		{
			this.Overflow = overflow;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000025E0 File Offset: 0x000007E0
		public DisplayAsStringAttribute(TextAlignment alignment)
		{
			this.Alignment = alignment;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000025EF File Offset: 0x000007EF
		public DisplayAsStringAttribute(int fontSize)
		{
			this.FontSize = fontSize;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000025FE File Offset: 0x000007FE
		public DisplayAsStringAttribute(bool overflow, TextAlignment alignment)
		{
			this.Overflow = overflow;
			this.Alignment = alignment;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002614 File Offset: 0x00000814
		public DisplayAsStringAttribute(bool overflow, int fontSize)
		{
			this.Overflow = overflow;
			this.FontSize = fontSize;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000262A File Offset: 0x0000082A
		public DisplayAsStringAttribute(int fontSize, TextAlignment alignment)
		{
			this.FontSize = fontSize;
			this.Alignment = alignment;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002640 File Offset: 0x00000840
		public DisplayAsStringAttribute(bool overflow, int fontSize, TextAlignment alignment)
		{
			this.Overflow = overflow;
			this.FontSize = fontSize;
			this.Alignment = alignment;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000265D File Offset: 0x0000085D
		public DisplayAsStringAttribute(TextAlignment alignment, bool enableRichText)
		{
			this.Alignment = alignment;
			this.EnableRichText = enableRichText;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002673 File Offset: 0x00000873
		public DisplayAsStringAttribute(int fontSize, bool enableRichText)
		{
			this.FontSize = fontSize;
			this.EnableRichText = enableRichText;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002689 File Offset: 0x00000889
		public DisplayAsStringAttribute(bool overflow, TextAlignment alignment, bool enableRichText)
		{
			this.Overflow = overflow;
			this.Alignment = alignment;
			this.EnableRichText = enableRichText;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000026A6 File Offset: 0x000008A6
		public DisplayAsStringAttribute(bool overflow, int fontSize, bool enableRichText)
		{
			this.Overflow = overflow;
			this.FontSize = fontSize;
			this.EnableRichText = enableRichText;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000026C3 File Offset: 0x000008C3
		public DisplayAsStringAttribute(int fontSize, TextAlignment alignment, bool enableRichText)
		{
			this.FontSize = fontSize;
			this.Alignment = alignment;
			this.EnableRichText = enableRichText;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000026E0 File Offset: 0x000008E0
		public DisplayAsStringAttribute(bool overflow, int fontSize, TextAlignment alignment, bool enableRichText)
		{
			this.Overflow = overflow;
			this.FontSize = fontSize;
			this.Alignment = alignment;
			this.EnableRichText = enableRichText;
		}

		// Token: 0x04000046 RID: 70
		public bool Overflow;

		// Token: 0x04000047 RID: 71
		public TextAlignment Alignment;

		// Token: 0x04000048 RID: 72
		public int FontSize;

		// Token: 0x04000049 RID: 73
		public bool EnableRichText;

		// Token: 0x0400004A RID: 74
		public string Format;
	}
}
