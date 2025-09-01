using System;
using System.IO;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000091 RID: 145
	internal class XmlSigningNodeWriter : XmlNodeWriter
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x0001FAF4 File Offset: 0x0001DCF4
		public XmlSigningNodeWriter(bool text)
		{
			this.text = text;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001FB03 File Offset: 0x0001DD03
		public void SetOutput(XmlNodeWriter writer, Stream stream, bool includeComments, string[] inclusivePrefixes)
		{
			this.writer = writer;
			if (this.signingWriter == null)
			{
				this.signingWriter = new XmlCanonicalWriter();
			}
			this.signingWriter.SetOutput(stream, includeComments, inclusivePrefixes);
			this.chars = new byte[64];
			this.base64Chars = null;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x0001FB42 File Offset: 0x0001DD42
		// (set) Token: 0x060007BA RID: 1978 RVA: 0x0001FB4A File Offset: 0x0001DD4A
		public XmlNodeWriter NodeWriter
		{
			get
			{
				return this.writer;
			}
			set
			{
				this.writer = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0001FB53 File Offset: 0x0001DD53
		public XmlCanonicalWriter CanonicalWriter
		{
			get
			{
				return this.signingWriter;
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001FB5B File Offset: 0x0001DD5B
		public override void Flush()
		{
			this.writer.Flush();
			this.signingWriter.Flush();
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001FB73 File Offset: 0x0001DD73
		public override void Close()
		{
			this.writer.Close();
			this.signingWriter.Close();
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001FB8B File Offset: 0x0001DD8B
		public override void WriteDeclaration()
		{
			this.writer.WriteDeclaration();
			this.signingWriter.WriteDeclaration();
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001FBA3 File Offset: 0x0001DDA3
		public override void WriteComment(string text)
		{
			this.writer.WriteComment(text);
			this.signingWriter.WriteComment(text);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0001FBBD File Offset: 0x0001DDBD
		public override void WriteCData(string text)
		{
			this.writer.WriteCData(text);
			this.signingWriter.WriteEscapedText(text);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001FBD7 File Offset: 0x0001DDD7
		public override void WriteStartElement(string prefix, string localName)
		{
			this.writer.WriteStartElement(prefix, localName);
			this.signingWriter.WriteStartElement(prefix, localName);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001FBF3 File Offset: 0x0001DDF3
		public override void WriteStartElement(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
		{
			this.writer.WriteStartElement(prefixBuffer, prefixOffset, prefixLength, localNameBuffer, localNameOffset, localNameLength);
			this.signingWriter.WriteStartElement(prefixBuffer, prefixOffset, prefixLength, localNameBuffer, localNameOffset, localNameLength);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001FC1D File Offset: 0x0001DE1D
		public override void WriteStartElement(string prefix, XmlDictionaryString localName)
		{
			this.writer.WriteStartElement(prefix, localName);
			this.signingWriter.WriteStartElement(prefix, localName.Value);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0001FC3E File Offset: 0x0001DE3E
		public override void WriteEndStartElement(bool isEmpty)
		{
			this.writer.WriteEndStartElement(isEmpty);
			this.signingWriter.WriteEndStartElement(isEmpty);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0001FC58 File Offset: 0x0001DE58
		public override void WriteEndElement(string prefix, string localName)
		{
			this.writer.WriteEndElement(prefix, localName);
			this.signingWriter.WriteEndElement(prefix, localName);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0001FC74 File Offset: 0x0001DE74
		public override void WriteXmlnsAttribute(string prefix, string ns)
		{
			this.writer.WriteXmlnsAttribute(prefix, ns);
			this.signingWriter.WriteXmlnsAttribute(prefix, ns);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001FC90 File Offset: 0x0001DE90
		public override void WriteXmlnsAttribute(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] nsBuffer, int nsOffset, int nsLength)
		{
			this.writer.WriteXmlnsAttribute(prefixBuffer, prefixOffset, prefixLength, nsBuffer, nsOffset, nsLength);
			this.signingWriter.WriteXmlnsAttribute(prefixBuffer, prefixOffset, prefixLength, nsBuffer, nsOffset, nsLength);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0001FCBA File Offset: 0x0001DEBA
		public override void WriteXmlnsAttribute(string prefix, XmlDictionaryString ns)
		{
			this.writer.WriteXmlnsAttribute(prefix, ns);
			this.signingWriter.WriteXmlnsAttribute(prefix, ns.Value);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0001FCDB File Offset: 0x0001DEDB
		public override void WriteStartAttribute(string prefix, string localName)
		{
			this.writer.WriteStartAttribute(prefix, localName);
			this.signingWriter.WriteStartAttribute(prefix, localName);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0001FCF7 File Offset: 0x0001DEF7
		public override void WriteStartAttribute(byte[] prefixBuffer, int prefixOffset, int prefixLength, byte[] localNameBuffer, int localNameOffset, int localNameLength)
		{
			this.writer.WriteStartAttribute(prefixBuffer, prefixOffset, prefixLength, localNameBuffer, localNameOffset, localNameLength);
			this.signingWriter.WriteStartAttribute(prefixBuffer, prefixOffset, prefixLength, localNameBuffer, localNameOffset, localNameLength);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0001FD21 File Offset: 0x0001DF21
		public override void WriteStartAttribute(string prefix, XmlDictionaryString localName)
		{
			this.writer.WriteStartAttribute(prefix, localName);
			this.signingWriter.WriteStartAttribute(prefix, localName.Value);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001FD42 File Offset: 0x0001DF42
		public override void WriteEndAttribute()
		{
			this.writer.WriteEndAttribute();
			this.signingWriter.WriteEndAttribute();
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0001FD5A File Offset: 0x0001DF5A
		public override void WriteCharEntity(int ch)
		{
			this.writer.WriteCharEntity(ch);
			this.signingWriter.WriteCharEntity(ch);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0001FD74 File Offset: 0x0001DF74
		public override void WriteEscapedText(string value)
		{
			this.writer.WriteEscapedText(value);
			this.signingWriter.WriteEscapedText(value);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001FD8E File Offset: 0x0001DF8E
		public override void WriteEscapedText(char[] chars, int offset, int count)
		{
			this.writer.WriteEscapedText(chars, offset, count);
			this.signingWriter.WriteEscapedText(chars, offset, count);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001FDAC File Offset: 0x0001DFAC
		public override void WriteEscapedText(XmlDictionaryString value)
		{
			this.writer.WriteEscapedText(value);
			this.signingWriter.WriteEscapedText(value.Value);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001FDCB File Offset: 0x0001DFCB
		public override void WriteEscapedText(byte[] chars, int offset, int count)
		{
			this.writer.WriteEscapedText(chars, offset, count);
			this.signingWriter.WriteEscapedText(chars, offset, count);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001FDE9 File Offset: 0x0001DFE9
		public override void WriteText(string value)
		{
			this.writer.WriteText(value);
			this.signingWriter.WriteText(value);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001FE03 File Offset: 0x0001E003
		public override void WriteText(char[] chars, int offset, int count)
		{
			this.writer.WriteText(chars, offset, count);
			this.signingWriter.WriteText(chars, offset, count);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001FE21 File Offset: 0x0001E021
		public override void WriteText(byte[] chars, int offset, int count)
		{
			this.writer.WriteText(chars, offset, count);
			this.signingWriter.WriteText(chars, offset, count);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0001FE3F File Offset: 0x0001E03F
		public override void WriteText(XmlDictionaryString value)
		{
			this.writer.WriteText(value);
			this.signingWriter.WriteText(value.Value);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0001FE60 File Offset: 0x0001E060
		public override void WriteInt32Text(int value)
		{
			int count = XmlConverter.ToChars(value, this.chars, 0);
			if (this.text)
			{
				this.writer.WriteText(this.chars, 0, count);
			}
			else
			{
				this.writer.WriteInt32Text(value);
			}
			this.signingWriter.WriteText(this.chars, 0, count);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
		public override void WriteInt64Text(long value)
		{
			int count = XmlConverter.ToChars(value, this.chars, 0);
			if (this.text)
			{
				this.writer.WriteText(this.chars, 0, count);
			}
			else
			{
				this.writer.WriteInt64Text(value);
			}
			this.signingWriter.WriteText(this.chars, 0, count);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0001FF10 File Offset: 0x0001E110
		public override void WriteBoolText(bool value)
		{
			int count = XmlConverter.ToChars(value, this.chars, 0);
			if (this.text)
			{
				this.writer.WriteText(this.chars, 0, count);
			}
			else
			{
				this.writer.WriteBoolText(value);
			}
			this.signingWriter.WriteText(this.chars, 0, count);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x0001FF68 File Offset: 0x0001E168
		public override void WriteUInt64Text(ulong value)
		{
			int count = XmlConverter.ToChars(value, this.chars, 0);
			if (this.text)
			{
				this.writer.WriteText(this.chars, 0, count);
			}
			else
			{
				this.writer.WriteUInt64Text(value);
			}
			this.signingWriter.WriteText(this.chars, 0, count);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		public override void WriteFloatText(float value)
		{
			int count = XmlConverter.ToChars(value, this.chars, 0);
			if (this.text)
			{
				this.writer.WriteText(this.chars, 0, count);
			}
			else
			{
				this.writer.WriteFloatText(value);
			}
			this.signingWriter.WriteText(this.chars, 0, count);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00020018 File Offset: 0x0001E218
		public override void WriteDoubleText(double value)
		{
			int count = XmlConverter.ToChars(value, this.chars, 0);
			if (this.text)
			{
				this.writer.WriteText(this.chars, 0, count);
			}
			else
			{
				this.writer.WriteDoubleText(value);
			}
			this.signingWriter.WriteText(this.chars, 0, count);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00020070 File Offset: 0x0001E270
		public override void WriteDecimalText(decimal value)
		{
			int count = XmlConverter.ToChars(value, this.chars, 0);
			if (this.text)
			{
				this.writer.WriteText(this.chars, 0, count);
			}
			else
			{
				this.writer.WriteDecimalText(value);
			}
			this.signingWriter.WriteText(this.chars, 0, count);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000200C8 File Offset: 0x0001E2C8
		public override void WriteDateTimeText(DateTime value)
		{
			int count = XmlConverter.ToChars(value, this.chars, 0);
			if (this.text)
			{
				this.writer.WriteText(this.chars, 0, count);
			}
			else
			{
				this.writer.WriteDateTimeText(value);
			}
			this.signingWriter.WriteText(this.chars, 0, count);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00020120 File Offset: 0x0001E320
		public override void WriteUniqueIdText(UniqueId value)
		{
			string value2 = XmlConverter.ToString(value);
			if (this.text)
			{
				this.writer.WriteText(value2);
			}
			else
			{
				this.writer.WriteUniqueIdText(value);
			}
			this.signingWriter.WriteText(value2);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00020164 File Offset: 0x0001E364
		public override void WriteTimeSpanText(TimeSpan value)
		{
			string value2 = XmlConverter.ToString(value);
			if (this.text)
			{
				this.writer.WriteText(value2);
			}
			else
			{
				this.writer.WriteTimeSpanText(value);
			}
			this.signingWriter.WriteText(value2);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000201A8 File Offset: 0x0001E3A8
		public override void WriteGuidText(Guid value)
		{
			string value2 = XmlConverter.ToString(value);
			if (this.text)
			{
				this.writer.WriteText(value2);
			}
			else
			{
				this.writer.WriteGuidText(value);
			}
			this.signingWriter.WriteText(value2);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000201EA File Offset: 0x0001E3EA
		public override void WriteStartListText()
		{
			this.writer.WriteStartListText();
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x000201F7 File Offset: 0x0001E3F7
		public override void WriteListSeparator()
		{
			this.writer.WriteListSeparator();
			this.signingWriter.WriteText(32);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00020211 File Offset: 0x0001E411
		public override void WriteEndListText()
		{
			this.writer.WriteEndListText();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002021E File Offset: 0x0001E41E
		public override void WriteBase64Text(byte[] trailBytes, int trailByteCount, byte[] buffer, int offset, int count)
		{
			if (trailByteCount > 0)
			{
				this.WriteBase64Text(trailBytes, 0, trailByteCount);
			}
			this.WriteBase64Text(buffer, offset, count);
			if (!this.text)
			{
				this.writer.WriteBase64Text(trailBytes, trailByteCount, buffer, offset, count);
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00020254 File Offset: 0x0001E454
		private void WriteBase64Text(byte[] buffer, int offset, int count)
		{
			if (this.base64Chars == null)
			{
				this.base64Chars = new byte[512];
			}
			Base64Encoding base64Encoding = XmlConverter.Base64Encoding;
			while (count >= 3)
			{
				int num = Math.Min(this.base64Chars.Length / 4 * 3, count - count % 3);
				int count2 = num / 3 * 4;
				base64Encoding.GetChars(buffer, offset, num, this.base64Chars, 0);
				this.signingWriter.WriteText(this.base64Chars, 0, count2);
				if (this.text)
				{
					this.writer.WriteText(this.base64Chars, 0, count2);
				}
				offset += num;
				count -= num;
			}
			if (count > 0)
			{
				base64Encoding.GetChars(buffer, offset, count, this.base64Chars, 0);
				this.signingWriter.WriteText(this.base64Chars, 0, 4);
				if (this.text)
				{
					this.writer.WriteText(this.base64Chars, 0, 4);
				}
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00020330 File Offset: 0x0001E530
		public override void WriteQualifiedName(string prefix, XmlDictionaryString localName)
		{
			this.writer.WriteQualifiedName(prefix, localName);
			if (prefix.Length != 0)
			{
				this.signingWriter.WriteText(prefix);
				this.signingWriter.WriteText(":");
			}
			this.signingWriter.WriteText(localName.Value);
		}

		// Token: 0x0400037A RID: 890
		private XmlNodeWriter writer;

		// Token: 0x0400037B RID: 891
		private XmlCanonicalWriter signingWriter;

		// Token: 0x0400037C RID: 892
		private byte[] chars;

		// Token: 0x0400037D RID: 893
		private byte[] base64Chars;

		// Token: 0x0400037E RID: 894
		private bool text;
	}
}
