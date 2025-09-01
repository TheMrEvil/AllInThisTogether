using System;
using System.Collections;
using System.IO;
using System.Security;

namespace Mono.Xml
{
	// Token: 0x0200005F RID: 95
	internal class SecurityParser : SmallXmlParser, SmallXmlParser.IContentHandler
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00004BBD File Offset: 0x00002DBD
		public SecurityParser()
		{
			this.stack = new Stack();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004BD0 File Offset: 0x00002DD0
		public void LoadXml(string xml)
		{
			this.root = null;
			this.stack.Clear();
			base.Parse(new StringReader(xml), this);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004BF1 File Offset: 0x00002DF1
		public SecurityElement ToXml()
		{
			return this.root;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void OnStartParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void OnProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void OnIgnorableWhitespace(string s)
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004BFC File Offset: 0x00002DFC
		public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
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

		// Token: 0x06000138 RID: 312 RVA: 0x00004C82 File Offset: 0x00002E82
		public void OnEndElement(string name)
		{
			this.current = (SecurityElement)this.stack.Pop();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004C9A File Offset: 0x00002E9A
		public void OnChars(string ch)
		{
			this.current.Text = SecurityElement.Escape(ch);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void OnEndParsing(SmallXmlParser parser)
		{
		}

		// Token: 0x04000E12 RID: 3602
		private SecurityElement root;

		// Token: 0x04000E13 RID: 3603
		private SecurityElement current;

		// Token: 0x04000E14 RID: 3604
		private Stack stack;
	}
}
