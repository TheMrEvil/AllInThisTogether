using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200005C RID: 92
	internal class TextEncodedRawTextWriter : XmlEncodedRawTextWriter
	{
		// Token: 0x06000299 RID: 665 RVA: 0x0000FEEF File Offset: 0x0000E0EF
		public TextEncodedRawTextWriter(TextWriter writer, XmlWriterSettings settings) : base(writer, settings)
		{
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000FEF9 File Offset: 0x0000E0F9
		public TextEncodedRawTextWriter(Stream stream, XmlWriterSettings settings) : base(stream, settings)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteXmlDeclaration(string xmldecl)
		{
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void StartElementContent()
		{
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000FF03 File Offset: 0x0000E103
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.inAttributeValue = true;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000FF0C File Offset: 0x0000E10C
		public override void WriteEndAttribute()
		{
			this.inAttributeValue = false;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000B528 File Offset: 0x00009728
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000FF15 File Offset: 0x0000E115
		public override void WriteCData(string text)
		{
			base.WriteRaw(text);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteComment(string text)
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteEntityRef(string name)
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteCharEntity(char ch)
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000B528 File Offset: 0x00009728
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000FF1E File Offset: 0x0000E11E
		public override void WriteWhitespace(string ws)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(ws);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000FF1E File Offset: 0x0000E11E
		public override void WriteString(string textBlock)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(textBlock);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000FF2F File Offset: 0x0000E12F
		public override void WriteChars(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000FF2F File Offset: 0x0000E12F
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(buffer, index, count);
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000FF1E File Offset: 0x0000E11E
		public override void WriteRaw(string data)
		{
			if (!this.inAttributeValue)
			{
				base.WriteRaw(data);
			}
		}
	}
}
