using System;
using System.Text;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000409 RID: 1033
	internal class Key : XslNode
	{
		// Token: 0x060028C7 RID: 10439 RVA: 0x000F51B6 File Offset: 0x000F33B6
		public Key(QilName name, string match, string use, XslVersion xslVer) : base(XslNodeType.Key, name, null, xslVer)
		{
			this.Match = match;
			this.Use = use;
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000F51D4 File Offset: 0x000F33D4
		public string GetDebugName()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<xsl:key name=\"");
			stringBuilder.Append(this.Name.QualifiedName);
			stringBuilder.Append('"');
			if (this.Match != null)
			{
				stringBuilder.Append(" match=\"");
				stringBuilder.Append(this.Match);
				stringBuilder.Append('"');
			}
			if (this.Use != null)
			{
				stringBuilder.Append(" use=\"");
				stringBuilder.Append(this.Use);
				stringBuilder.Append('"');
			}
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		// Token: 0x0400205E RID: 8286
		public readonly string Match;

		// Token: 0x0400205F RID: 8287
		public readonly string Use;

		// Token: 0x04002060 RID: 8288
		public QilFunction Function;
	}
}
