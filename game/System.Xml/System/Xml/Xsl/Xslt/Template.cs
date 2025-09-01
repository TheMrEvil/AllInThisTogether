using System;
using System.Globalization;
using System.Text;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000405 RID: 1029
	internal class Template : ProtoTemplate
	{
		// Token: 0x060028C1 RID: 10433 RVA: 0x000F5045 File Offset: 0x000F3245
		public Template(QilName name, string match, QilName mode, double priority, XslVersion xslVer) : base(XslNodeType.Template, name, xslVer)
		{
			this.Match = match;
			this.Mode = mode;
			this.Priority = priority;
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x000F5068 File Offset: 0x000F3268
		public override string GetDebugName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<xsl:template");
			if (this.Match != null)
			{
				stringBuilder.Append(" match=\"");
				stringBuilder.Append(this.Match);
				stringBuilder.Append('"');
			}
			if (this.Name != null)
			{
				stringBuilder.Append(" name=\"");
				stringBuilder.Append(this.Name.QualifiedName);
				stringBuilder.Append('"');
			}
			if (!double.IsNaN(this.Priority))
			{
				stringBuilder.Append(" priority=\"");
				stringBuilder.Append(this.Priority.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append('"');
			}
			if (this.Mode.LocalName.Length != 0)
			{
				stringBuilder.Append(" mode=\"");
				stringBuilder.Append(this.Mode.QualifiedName);
				stringBuilder.Append('"');
			}
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x04002053 RID: 8275
		public readonly string Match;

		// Token: 0x04002054 RID: 8276
		public readonly QilName Mode;

		// Token: 0x04002055 RID: 8277
		public readonly double Priority;

		// Token: 0x04002056 RID: 8278
		public int ImportPrecedence;

		// Token: 0x04002057 RID: 8279
		public int OrderNumber;
	}
}
