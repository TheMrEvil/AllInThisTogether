using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200005D RID: 93
	internal class TextUtf8RawTextWriter : XmlUtf8RawTextWriter
	{
		// Token: 0x060002B1 RID: 689 RVA: 0x0000FF42 File Offset: 0x0000E142
		public TextUtf8RawTextWriter(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void StartElementContent()
		{
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000FF4C File Offset: 0x0000E14C
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttributeValue = true;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000FF55 File Offset: 0x0000E155
		public override void WriteEndAttribute()
		{
			this.inAttributeValue = false;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000FF5E File Offset: 0x0000E15E
		public override void WriteCData(string text)
		{
			base.WriteRaw(text);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteComment(string text)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteCharEntity(char ch)
		{
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000FF67 File Offset: 0x0000E167
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(ws);
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000FF67 File Offset: 0x0000E167
		public override void WriteString(string textBlock)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(textBlock);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000FF78 File Offset: 0x0000E178
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000FF78 File Offset: 0x0000E178
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000FF67 File Offset: 0x0000E167
		public override void WriteRaw(string data)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(data);
			}
		}
	}
}
