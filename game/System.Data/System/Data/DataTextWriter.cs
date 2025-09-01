using System;
using System.IO;
using System.Xml;

namespace System.Data
{
	// Token: 0x0200014E RID: 334
	internal sealed class DataTextWriter : XmlWriter
	{
		// Token: 0x060011B5 RID: 4533 RVA: 0x00054E77 File Offset: 0x00053077
		internal static XmlWriter CreateWriter(XmlWriter xw)
		{
			return new DataTextWriter(xw);
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00054E7F File Offset: 0x0005307F
		private DataTextWriter(XmlWriter w)
		{
			this._xmltextWriter = w;
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00054E90 File Offset: 0x00053090
		internal Stream BaseStream
		{
			get
			{
				XmlTextWriter xmlTextWriter = this._xmltextWriter as XmlTextWriter;
				if (xmlTextWriter != null)
				{
					return xmlTextWriter.BaseStream;
				}
				return null;
			}
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x00054EB4 File Offset: 0x000530B4
		public override void WriteStartDocument()
		{
			this._xmltextWriter.WriteStartDocument();
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00054EC1 File Offset: 0x000530C1
		public override void WriteStartDocument(bool standalone)
		{
			this._xmltextWriter.WriteStartDocument(standalone);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00054ECF File Offset: 0x000530CF
		public override void WriteEndDocument()
		{
			this._xmltextWriter.WriteEndDocument();
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x00054EDC File Offset: 0x000530DC
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this._xmltextWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00054EEE File Offset: 0x000530EE
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00054EFE File Offset: 0x000530FE
		public override void WriteEndElement()
		{
			this._xmltextWriter.WriteEndElement();
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00054F0B File Offset: 0x0005310B
		public override void WriteFullEndElement()
		{
			this._xmltextWriter.WriteFullEndElement();
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00054F18 File Offset: 0x00053118
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this._xmltextWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00054F28 File Offset: 0x00053128
		public override void WriteEndAttribute()
		{
			this._xmltextWriter.WriteEndAttribute();
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00054F35 File Offset: 0x00053135
		public override void WriteCData(string text)
		{
			this._xmltextWriter.WriteCData(text);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00054F43 File Offset: 0x00053143
		public override void WriteComment(string text)
		{
			this._xmltextWriter.WriteComment(text);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00054F51 File Offset: 0x00053151
		public override void WriteProcessingInstruction(string name, string text)
		{
			this._xmltextWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00054F60 File Offset: 0x00053160
		public override void WriteEntityRef(string name)
		{
			this._xmltextWriter.WriteEntityRef(name);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00054F6E File Offset: 0x0005316E
		public override void WriteCharEntity(char ch)
		{
			this._xmltextWriter.WriteCharEntity(ch);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00054F7C File Offset: 0x0005317C
		public override void WriteWhitespace(string ws)
		{
			this._xmltextWriter.WriteWhitespace(ws);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00054F8A File Offset: 0x0005318A
		public override void WriteString(string text)
		{
			this._xmltextWriter.WriteString(text);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00054F98 File Offset: 0x00053198
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this._xmltextWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00054FA7 File Offset: 0x000531A7
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00054FB7 File Offset: 0x000531B7
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00054FC7 File Offset: 0x000531C7
		public override void WriteRaw(string data)
		{
			this._xmltextWriter.WriteRaw(data);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00054FD5 File Offset: 0x000531D5
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00054FE5 File Offset: 0x000531E5
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this._xmltextWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x00054FF5 File Offset: 0x000531F5
		public override WriteState WriteState
		{
			get
			{
				return this._xmltextWriter.WriteState;
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00055002 File Offset: 0x00053202
		public override void Close()
		{
			this._xmltextWriter.Close();
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0005500F File Offset: 0x0005320F
		public override void Flush()
		{
			this._xmltextWriter.Flush();
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x0005501C File Offset: 0x0005321C
		public override void WriteName(string name)
		{
			this._xmltextWriter.WriteName(name);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0005502A File Offset: 0x0005322A
		public override void WriteQualifiedName(string localName, string ns)
		{
			this._xmltextWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00055039 File Offset: 0x00053239
		public override string LookupPrefix(string ns)
		{
			return this._xmltextWriter.LookupPrefix(ns);
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00055047 File Offset: 0x00053247
		public override XmlSpace XmlSpace
		{
			get
			{
				return this._xmltextWriter.XmlSpace;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00055054 File Offset: 0x00053254
		public override string XmlLang
		{
			get
			{
				return this._xmltextWriter.XmlLang;
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00055061 File Offset: 0x00053261
		public override void WriteNmToken(string name)
		{
			this._xmltextWriter.WriteNmToken(name);
		}

		// Token: 0x04000B91 RID: 2961
		private XmlWriter _xmltextWriter;
	}
}
