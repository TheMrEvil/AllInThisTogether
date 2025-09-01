using System;
using System.Collections;
using System.Security;

namespace Mono.Xml
{
	// Token: 0x02000006 RID: 6
	[CLSCompliant(false)]
	public class SecurityParser : MiniParser, MiniParser.IHandler, MiniParser.IReader
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002945 File Offset: 0x00000B45
		public SecurityParser()
		{
			this.stack = new Stack();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002958 File Offset: 0x00000B58
		public void LoadXml(string xml)
		{
			this.root = null;
			this.xmldoc = xml;
			this.pos = 0;
			this.stack.Clear();
			base.Parse(this, this);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002982 File Offset: 0x00000B82
		public SecurityElement ToXml()
		{
			return this.root;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000298C File Offset: 0x00000B8C
		public int Read()
		{
			if (this.pos >= this.xmldoc.Length)
			{
				return -1;
			}
			string text = this.xmldoc;
			int num = this.pos;
			this.pos = num + 1;
			return (int)text[num];
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000029CA File Offset: 0x00000BCA
		public void OnStartParsing(MiniParser parser)
		{
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000029CC File Offset: 0x00000BCC
		public void OnStartElement(string name, MiniParser.IAttrList attrs)
		{
			SecurityElement securityElement = new SecurityElement(name);
			if (this.root == null)
			{
				this.root = securityElement;
				this.current = securityElement;
			}
			else
			{
				((SecurityElement)this.stack.Peek()).AddChild(securityElement);
			}
			this.stack.Push(securityElement);
			this.current = securityElement;
			int length = attrs.Length;
			for (int i = 0; i < length; i++)
			{
				this.current.AddAttribute(attrs.GetName(i), SecurityElement.Escape(attrs.GetValue(i)));
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002A52 File Offset: 0x00000C52
		public void OnEndElement(string name)
		{
			this.current = (SecurityElement)this.stack.Pop();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002A6A File Offset: 0x00000C6A
		public void OnChars(string ch)
		{
			this.current.Text = SecurityElement.Escape(ch);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002A7D File Offset: 0x00000C7D
		public void OnEndParsing(MiniParser parser)
		{
		}

		// Token: 0x0400003F RID: 63
		private SecurityElement root;

		// Token: 0x04000040 RID: 64
		private string xmldoc;

		// Token: 0x04000041 RID: 65
		private int pos;

		// Token: 0x04000042 RID: 66
		private SecurityElement current;

		// Token: 0x04000043 RID: 67
		private Stack stack;
	}
}
