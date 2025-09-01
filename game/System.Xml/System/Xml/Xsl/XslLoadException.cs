using System;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Xml.Xsl
{
	// Token: 0x02000340 RID: 832
	[Serializable]
	internal class XslLoadException : XslTransformException
	{
		// Token: 0x06002258 RID: 8792 RVA: 0x000D9310 File Offset: 0x000D7510
		protected XslLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if ((bool)info.GetValue("hasLineInfo", typeof(bool)))
			{
				string uriString = (string)info.GetValue("Uri", typeof(string));
				int startLine = (int)info.GetValue("StartLine", typeof(int));
				int startPos = (int)info.GetValue("StartPos", typeof(int));
				int endLine = (int)info.GetValue("EndLine", typeof(int));
				int endPos = (int)info.GetValue("EndPos", typeof(int));
				this.lineInfo = new SourceLineInfo(uriString, startLine, startPos, endLine, endPos);
			}
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000D93E0 File Offset: 0x000D75E0
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("hasLineInfo", this.lineInfo != null);
			if (this.lineInfo != null)
			{
				info.AddValue("Uri", this.lineInfo.Uri);
				info.AddValue("StartLine", this.lineInfo.Start.Line);
				info.AddValue("StartPos", this.lineInfo.Start.Pos);
				info.AddValue("EndLine", this.lineInfo.End.Line);
				info.AddValue("EndPos", this.lineInfo.End.Pos);
			}
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x000D91D1 File Offset: 0x000D73D1
		internal XslLoadException(string res, params string[] args) : base(null, res, args)
		{
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x000D94A2 File Offset: 0x000D76A2
		internal XslLoadException(Exception inner, ISourceLineInfo lineInfo) : base(inner, "XSLT compile error.", null)
		{
			this.SetSourceLineInfo(lineInfo);
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x000D94B8 File Offset: 0x000D76B8
		internal XslLoadException(CompilerError error) : base("{0}", new string[]
		{
			error.ErrorText
		})
		{
			int line = error.Line;
			int num = error.Column;
			if (line == 0)
			{
				num = 0;
			}
			else if (num == 0)
			{
				num = 1;
			}
			this.SetSourceLineInfo(new SourceLineInfo(error.FileName, line, num, line, num));
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x000D950E File Offset: 0x000D770E
		internal void SetSourceLineInfo(ISourceLineInfo lineInfo)
		{
			this.lineInfo = lineInfo;
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x000D9517 File Offset: 0x000D7717
		public override string SourceUri
		{
			get
			{
				if (this.lineInfo == null)
				{
					return null;
				}
				return this.lineInfo.Uri;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x000D9530 File Offset: 0x000D7730
		public override int LineNumber
		{
			get
			{
				if (this.lineInfo == null)
				{
					return 0;
				}
				return this.lineInfo.Start.Line;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x000D955C File Offset: 0x000D775C
		public override int LinePosition
		{
			get
			{
				if (this.lineInfo == null)
				{
					return 0;
				}
				return this.lineInfo.Start.Pos;
			}
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x000D9588 File Offset: 0x000D7788
		private static string AppendLineInfoMessage(string message, ISourceLineInfo lineInfo)
		{
			if (lineInfo != null)
			{
				string fileName = SourceLineInfo.GetFileName(lineInfo.Uri);
				string text = XslTransformException.CreateMessage("An error occurred at {0}({1},{2}).", new string[]
				{
					fileName,
					lineInfo.Start.Line.ToString(CultureInfo.InvariantCulture),
					lineInfo.Start.Pos.ToString(CultureInfo.InvariantCulture)
				});
				if (text != null && text.Length > 0)
				{
					if (message.Length > 0 && !XmlCharType.Instance.IsWhiteSpace(message[message.Length - 1]))
					{
						message += " ";
					}
					message += text;
				}
			}
			return message;
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x000D9642 File Offset: 0x000D7842
		internal static string CreateMessage(ISourceLineInfo lineInfo, string res, params string[] args)
		{
			return XslLoadException.AppendLineInfoMessage(XslTransformException.CreateMessage(res, args), lineInfo);
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x000D9651 File Offset: 0x000D7851
		internal override string FormatDetailedMessage()
		{
			return XslLoadException.AppendLineInfoMessage(this.Message, this.lineInfo);
		}

		// Token: 0x04001C28 RID: 7208
		private ISourceLineInfo lineInfo;
	}
}
