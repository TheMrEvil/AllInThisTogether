using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x020001BF RID: 447
	internal class XmlDOMTextWriter : XmlTextWriter
	{
		// Token: 0x06001102 RID: 4354 RVA: 0x00069AB0 File Offset: 0x00067CB0
		public XmlDOMTextWriter(Stream w, Encoding encoding) : base(w, encoding)
		{
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x00069ABA File Offset: 0x00067CBA
		public XmlDOMTextWriter(string filename, Encoding encoding) : base(filename, encoding)
		{
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x00069AC4 File Offset: 0x00067CC4
		public XmlDOMTextWriter(TextWriter w) : base(w)
		{
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00069ACD File Offset: 0x00067CCD
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (ns.Length == 0 && prefix.Length != 0)
			{
				prefix = "";
			}
			base.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00069AEF File Offset: 0x00067CEF
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (ns.Length == 0 && prefix.Length != 0)
			{
				prefix = "";
			}
			base.WriteStartAttribute(prefix, localName, ns);
		}
	}
}
