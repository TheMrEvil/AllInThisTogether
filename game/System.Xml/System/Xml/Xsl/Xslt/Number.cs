using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200040B RID: 1035
	internal class Number : XslNode
	{
		// Token: 0x060028C9 RID: 10441 RVA: 0x000F5274 File Offset: 0x000F3474
		public Number(NumberLevel level, string count, string from, string value, string format, string lang, string letterValue, string groupingSeparator, string groupingSize, XslVersion xslVer) : base(XslNodeType.Number, null, null, xslVer)
		{
			this.Level = level;
			this.Count = count;
			this.From = from;
			this.Value = value;
			this.Format = format;
			this.Lang = lang;
			this.LetterValue = letterValue;
			this.GroupingSeparator = groupingSeparator;
			this.GroupingSize = groupingSize;
		}

		// Token: 0x04002065 RID: 8293
		public readonly NumberLevel Level;

		// Token: 0x04002066 RID: 8294
		public readonly string Count;

		// Token: 0x04002067 RID: 8295
		public readonly string From;

		// Token: 0x04002068 RID: 8296
		public readonly string Value;

		// Token: 0x04002069 RID: 8297
		public readonly string Format;

		// Token: 0x0400206A RID: 8298
		public readonly string Lang;

		// Token: 0x0400206B RID: 8299
		public readonly string LetterValue;

		// Token: 0x0400206C RID: 8300
		public readonly string GroupingSeparator;

		// Token: 0x0400206D RID: 8301
		public readonly string GroupingSize;
	}
}
