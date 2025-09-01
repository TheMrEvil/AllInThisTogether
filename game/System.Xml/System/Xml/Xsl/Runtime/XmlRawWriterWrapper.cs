using System;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000482 RID: 1154
	internal sealed class XmlRawWriterWrapper : XmlRawWriter
	{
		// Token: 0x06002D29 RID: 11561 RVA: 0x0010928B File Offset: 0x0010748B
		public XmlRawWriterWrapper(XmlWriter writer)
		{
			this.wrapped = writer;
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002D2A RID: 11562 RVA: 0x0010929A File Offset: 0x0010749A
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.wrapped.Settings;
			}
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x001092A7 File Offset: 0x001074A7
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.wrapped.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x001092B9 File Offset: 0x001074B9
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x001092C9 File Offset: 0x001074C9
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x001092D9 File Offset: 0x001074D9
		public override void WriteEndAttribute()
		{
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x001092E6 File Offset: 0x001074E6
		public override void WriteCData(string text)
		{
			this.wrapped.WriteCData(text);
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x001092F4 File Offset: 0x001074F4
		public override void WriteComment(string text)
		{
			this.wrapped.WriteComment(text);
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x00109302 File Offset: 0x00107502
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x00109311 File Offset: 0x00107511
		public override void WriteWhitespace(string ws)
		{
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x0010931F File Offset: 0x0010751F
		public override void WriteString(string text)
		{
			this.wrapped.WriteString(text);
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x0010932D File Offset: 0x0010752D
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.wrapped.WriteChars(buffer, index, count);
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x0010933D File Offset: 0x0010753D
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.wrapped.WriteRaw(buffer, index, count);
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x0010934D File Offset: 0x0010754D
		public override void WriteRaw(string data)
		{
			this.wrapped.WriteRaw(data);
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x0010935B File Offset: 0x0010755B
		public override void WriteEntityRef(string name)
		{
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x00109369 File Offset: 0x00107569
		public override void WriteCharEntity(char ch)
		{
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x00109377 File Offset: 0x00107577
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x00109386 File Offset: 0x00107586
		public override void Close()
		{
			this.wrapped.Close();
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x00109393 File Offset: 0x00107593
		public override void Flush()
		{
			this.wrapped.Flush();
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x001093A0 File Offset: 0x001075A0
		public override void WriteValue(object value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x001093AE File Offset: 0x001075AE
		public override void WriteValue(string value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x001093BC File Offset: 0x001075BC
		public override void WriteValue(bool value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x001093CA File Offset: 0x001075CA
		public override void WriteValue(DateTime value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x001093D8 File Offset: 0x001075D8
		public override void WriteValue(float value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x001093E6 File Offset: 0x001075E6
		public override void WriteValue(decimal value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x001093F4 File Offset: 0x001075F4
		public override void WriteValue(double value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x00109402 File Offset: 0x00107602
		public override void WriteValue(int value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x00109410 File Offset: 0x00107610
		public override void WriteValue(long value)
		{
			this.wrapped.WriteValue(value);
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x00109420 File Offset: 0x00107620
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((IDisposable)this.wrapped).Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void StartElementContent()
		{
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x00109458 File Offset: 0x00107658
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteEndElement();
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x00109465 File Offset: 0x00107665
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteFullEndElement();
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x00109472 File Offset: 0x00107672
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			if (prefix.Length == 0)
			{
				this.wrapped.WriteAttributeString(string.Empty, "xmlns", "http://www.w3.org/2000/xmlns/", ns);
				return;
			}
			this.wrapped.WriteAttributeString("xmlns", prefix, "http://www.w3.org/2000/xmlns/", ns);
		}

		// Token: 0x04002320 RID: 8992
		private XmlWriter wrapped;
	}
}
