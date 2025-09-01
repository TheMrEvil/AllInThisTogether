using System;
using System.Collections.Generic;

namespace System.Xml.Linq
{
	// Token: 0x02000056 RID: 86
	internal class XNodeBuilder : XmlWriter
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x0000DEBB File Offset: 0x0000C0BB
		public XNodeBuilder(XContainer container)
		{
			this._root = container;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000DECA File Offset: 0x0000C0CA
		public override XmlWriterSettings Settings
		{
			get
			{
				return new XmlWriterSettings
				{
					ConformanceLevel = ConformanceLevel.Auto
				};
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x000032E5 File Offset: 0x000014E5
		public override WriteState WriteState
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000DED8 File Offset: 0x0000C0D8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000DEE3 File Offset: 0x0000C0E3
		public override void Close()
		{
			this._root.Add(this._content);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000043E9 File Offset: 0x000025E9
		public override void Flush()
		{
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000032E5 File Offset: 0x000014E5
		public override string LookupPrefix(string namespaceName)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000DEF6 File Offset: 0x0000C0F6
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException("This XmlWriter does not support base64 encoded data.");
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000DF02 File Offset: 0x0000C102
		public override void WriteCData(string text)
		{
			this.AddNode(new XCData(text));
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000DF10 File Offset: 0x0000C110
		public override void WriteCharEntity(char ch)
		{
			this.AddString(new string(ch, 1));
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000DF1F File Offset: 0x0000C11F
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.AddString(new string(buffer, index, count));
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000DF2F File Offset: 0x0000C12F
		public override void WriteComment(string text)
		{
			this.AddNode(new XComment(text));
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000DF3D File Offset: 0x0000C13D
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.AddNode(new XDocumentType(name, pubid, sysid, subset));
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000DF50 File Offset: 0x0000C150
		public override void WriteEndAttribute()
		{
			XAttribute xattribute = new XAttribute(this._attrName, this._attrValue);
			this._attrName = null;
			this._attrValue = null;
			if (this._parent != null)
			{
				this._parent.Add(xattribute);
				return;
			}
			this.Add(xattribute);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000043E9 File Offset: 0x000025E9
		public override void WriteEndDocument()
		{
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000DF99 File Offset: 0x0000C199
		public override void WriteEndElement()
		{
			this._parent = ((XElement)this._parent).parent;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000DFB4 File Offset: 0x0000C1B4
		public override void WriteEntityRef(string name)
		{
			if (name == "amp")
			{
				this.AddString("&");
				return;
			}
			if (name == "apos")
			{
				this.AddString("'");
				return;
			}
			if (name == "gt")
			{
				this.AddString(">");
				return;
			}
			if (name == "lt")
			{
				this.AddString("<");
				return;
			}
			if (!(name == "quot"))
			{
				throw new NotSupportedException("This XmlWriter does not support entity references.");
			}
			this.AddString("\"");
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000E04C File Offset: 0x0000C24C
		public override void WriteFullEndElement()
		{
			XElement xelement = (XElement)this._parent;
			if (xelement.IsEmpty)
			{
				xelement.Add(string.Empty);
			}
			this._parent = xelement.parent;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000E084 File Offset: 0x0000C284
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (name == "xml")
			{
				return;
			}
			this.AddNode(new XProcessingInstruction(name, text));
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000DF1F File Offset: 0x0000C11F
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.AddString(new string(buffer, index, count));
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000E0A1 File Offset: 0x0000C2A1
		public override void WriteRaw(string data)
		{
			this.AddString(data);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000E0AA File Offset: 0x0000C2AA
		public override void WriteStartAttribute(string prefix, string localName, string namespaceName)
		{
			if (prefix == null)
			{
				throw new ArgumentNullException("prefix");
			}
			this._attrName = XNamespace.Get((prefix.Length == 0) ? string.Empty : namespaceName).GetName(localName);
			this._attrValue = string.Empty;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000043E9 File Offset: 0x000025E9
		public override void WriteStartDocument()
		{
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000043E9 File Offset: 0x000025E9
		public override void WriteStartDocument(bool standalone)
		{
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000E0E6 File Offset: 0x0000C2E6
		public override void WriteStartElement(string prefix, string localName, string namespaceName)
		{
			this.AddNode(new XElement(XNamespace.Get(namespaceName).GetName(localName)));
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E0A1 File Offset: 0x0000C2A1
		public override void WriteString(string text)
		{
			this.AddString(text);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000E0FF File Offset: 0x0000C2FF
		public override void WriteSurrogateCharEntity(char lowCh, char highCh)
		{
			this.AddString(new string(new char[]
			{
				highCh,
				lowCh
			}));
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E11A File Offset: 0x0000C31A
		public override void WriteValue(DateTimeOffset value)
		{
			this.WriteString(XmlConvert.ToString(value));
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E0A1 File Offset: 0x0000C2A1
		public override void WriteWhitespace(string ws)
		{
			this.AddString(ws);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000E128 File Offset: 0x0000C328
		private void Add(object o)
		{
			if (this._content == null)
			{
				this._content = new List<object>();
			}
			this._content.Add(o);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E14C File Offset: 0x0000C34C
		private void AddNode(XNode n)
		{
			if (this._parent != null)
			{
				this._parent.Add(n);
			}
			else
			{
				this.Add(n);
			}
			XContainer xcontainer = n as XContainer;
			if (xcontainer != null)
			{
				this._parent = xcontainer;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E187 File Offset: 0x0000C387
		private void AddString(string s)
		{
			if (s == null)
			{
				return;
			}
			if (this._attrValue != null)
			{
				this._attrValue += s;
				return;
			}
			if (this._parent != null)
			{
				this._parent.Add(s);
				return;
			}
			this.Add(s);
		}

		// Token: 0x040001BB RID: 443
		private List<object> _content;

		// Token: 0x040001BC RID: 444
		private XContainer _parent;

		// Token: 0x040001BD RID: 445
		private XName _attrName;

		// Token: 0x040001BE RID: 446
		private string _attrValue;

		// Token: 0x040001BF RID: 447
		private XContainer _root;
	}
}
