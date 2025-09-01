using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x02000429 RID: 1065
	[Serializable]
	internal class XPathCompileException : XslLoadException
	{
		// Token: 0x06002A65 RID: 10853 RVA: 0x001001A4 File Offset: 0x000FE3A4
		protected XPathCompileException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.queryString = (string)info.GetValue("QueryString", typeof(string));
			this.startChar = (int)info.GetValue("StartChar", typeof(int));
			this.endChar = (int)info.GetValue("EndChar", typeof(int));
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x00100219 File Offset: 0x000FE419
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("QueryString", this.queryString);
			info.AddValue("StartChar", this.startChar);
			info.AddValue("EndChar", this.endChar);
		}

		// Token: 0x06002A67 RID: 10855 RVA: 0x00100256 File Offset: 0x000FE456
		internal XPathCompileException(string queryString, int startChar, int endChar, string resId, params string[] args) : base(resId, args)
		{
			this.queryString = queryString;
			this.startChar = startChar;
			this.endChar = endChar;
		}

		// Token: 0x06002A68 RID: 10856 RVA: 0x00100277 File Offset: 0x000FE477
		internal XPathCompileException(string resId, params string[] args) : base(resId, args)
		{
		}

		// Token: 0x06002A69 RID: 10857 RVA: 0x00100284 File Offset: 0x000FE484
		private static void AppendTrimmed(StringBuilder sb, string value, int startIndex, int count, XPathCompileException.TrimType trimType)
		{
			if (count <= 32)
			{
				sb.Append(value, startIndex, count);
				return;
			}
			switch (trimType)
			{
			case XPathCompileException.TrimType.Left:
				sb.Append("...");
				sb.Append(value, startIndex + count - 32, 32);
				return;
			case XPathCompileException.TrimType.Right:
				sb.Append(value, startIndex, 32);
				sb.Append("...");
				return;
			case XPathCompileException.TrimType.Middle:
				sb.Append(value, startIndex, 16);
				sb.Append("...");
				sb.Append(value, startIndex + count - 16, 16);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002A6A RID: 10858 RVA: 0x00100314 File Offset: 0x000FE514
		internal string MarkOutError()
		{
			if (this.queryString == null || this.queryString.Trim(' ').Length == 0)
			{
				return null;
			}
			int num = this.endChar - this.startChar;
			StringBuilder stringBuilder = new StringBuilder();
			XPathCompileException.AppendTrimmed(stringBuilder, this.queryString, 0, this.startChar, XPathCompileException.TrimType.Left);
			if (num > 0)
			{
				stringBuilder.Append(" -->");
				XPathCompileException.AppendTrimmed(stringBuilder, this.queryString, this.startChar, num, XPathCompileException.TrimType.Middle);
			}
			stringBuilder.Append("<-- ");
			XPathCompileException.AppendTrimmed(stringBuilder, this.queryString, this.endChar, this.queryString.Length - this.endChar, XPathCompileException.TrimType.Right);
			return stringBuilder.ToString();
		}

		// Token: 0x06002A6B RID: 10859 RVA: 0x001003C4 File Offset: 0x000FE5C4
		internal override string FormatDetailedMessage()
		{
			string text = this.Message;
			string text2 = this.MarkOutError();
			if (text2 != null && text2.Length > 0)
			{
				if (text.Length > 0)
				{
					text += Environment.NewLine;
				}
				text += text2;
			}
			return text;
		}

		// Token: 0x04002149 RID: 8521
		public string queryString;

		// Token: 0x0400214A RID: 8522
		public int startChar;

		// Token: 0x0400214B RID: 8523
		public int endChar;

		// Token: 0x0200042A RID: 1066
		private enum TrimType
		{
			// Token: 0x0400214D RID: 8525
			Left,
			// Token: 0x0400214E RID: 8526
			Right,
			// Token: 0x0400214F RID: 8527
			Middle
		}
	}
}
