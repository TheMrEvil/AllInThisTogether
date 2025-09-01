using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x02000067 RID: 103
	internal class XmlAutoDetectWriter : XmlRawWriter, IRemovableWriter
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x00011719 File Offset: 0x0000F919
		private XmlAutoDetectWriter(XmlWriterSettings writerSettings)
		{
			this.writerSettings = writerSettings.Clone();
			this.writerSettings.ReadOnly = true;
			this.eventCache = new XmlEventCache(string.Empty, true);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001174A File Offset: 0x0000F94A
		public XmlAutoDetectWriter(TextWriter textWriter, XmlWriterSettings writerSettings) : this(writerSettings)
		{
			this.textWriter = textWriter;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001175A File Offset: 0x0000F95A
		public XmlAutoDetectWriter(Stream strm, XmlWriterSettings writerSettings) : this(writerSettings)
		{
			this.strm = strm;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0001176A File Offset: 0x0000F96A
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00011772 File Offset: 0x0000F972
		public OnRemoveWriter OnRemoveWriterEvent
		{
			get
			{
				return this.onRemove;
			}
			set
			{
				this.onRemove = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001177B File Offset: 0x0000F97B
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.writerSettings;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00011783 File Offset: 0x0000F983
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001179C File Offset: 0x0000F99C
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (this.wrapped == null)
			{
				if (ns.Length == 0 && XmlAutoDetectWriter.IsHtmlTag(localName))
				{
					this.CreateWrappedWriter(XmlOutputMethod.Html);
				}
				else
				{
					this.CreateWrappedWriter(XmlOutputMethod.Xml);
				}
			}
			this.wrapped.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000117D4 File Offset: 0x0000F9D4
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000117EB File Offset: 0x0000F9EB
		public override void WriteEndAttribute()
		{
			this.wrapped.WriteEndAttribute();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000117F8 File Offset: 0x0000F9F8
		public override void WriteCData(string text)
		{
			if (this.TextBlockCreatesWriter(text))
			{
				this.wrapped.WriteCData(text);
				return;
			}
			this.eventCache.WriteCData(text);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001181C File Offset: 0x0000FA1C
		public override void WriteComment(string text)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteComment(text);
				return;
			}
			this.wrapped.WriteComment(text);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001183F File Offset: 0x0000FA3F
		public override void WriteProcessingInstruction(string name, string text)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteProcessingInstruction(name, text);
				return;
			}
			this.wrapped.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00011864 File Offset: 0x0000FA64
		public override void WriteWhitespace(string ws)
		{
			if (this.wrapped == null)
			{
				this.eventCache.WriteWhitespace(ws);
				return;
			}
			this.wrapped.WriteWhitespace(ws);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00011887 File Offset: 0x0000FA87
		public override void WriteString(string text)
		{
			if (this.TextBlockCreatesWriter(text))
			{
				this.wrapped.WriteString(text);
				return;
			}
			this.eventCache.WriteString(text);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000118AB File Offset: 0x0000FAAB
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.WriteString(new string(buffer, index, count));
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000118BB File Offset: 0x0000FABB
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.WriteRaw(new string(buffer, index, count));
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000118CB File Offset: 0x0000FACB
		public override void WriteRaw(string data)
		{
			if (this.TextBlockCreatesWriter(data))
			{
				this.wrapped.WriteRaw(data);
				return;
			}
			this.eventCache.WriteRaw(data);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000118EF File Offset: 0x0000FAEF
		public override void WriteEntityRef(string name)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteEntityRef(name);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00011904 File Offset: 0x0000FB04
		public override void WriteCharEntity(char ch)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteCharEntity(ch);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00011919 File Offset: 0x0000FB19
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001192F File Offset: 0x0000FB2F
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteBase64(buffer, index, count);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00011946 File Offset: 0x0000FB46
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteBinHex(buffer, index, count);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001195D File Offset: 0x0000FB5D
		public override void Close()
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.Close();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00011971 File Offset: 0x0000FB71
		public override void Flush()
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.Flush();
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00011985 File Offset: 0x0000FB85
		public override void WriteValue(object value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001199A File Offset: 0x0000FB9A
		public override void WriteValue(string value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000119AF File Offset: 0x0000FBAF
		public override void WriteValue(bool value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000119C4 File Offset: 0x0000FBC4
		public override void WriteValue(DateTime value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000119D9 File Offset: 0x0000FBD9
		public override void WriteValue(DateTimeOffset value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000119EE File Offset: 0x0000FBEE
		public override void WriteValue(double value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00011A03 File Offset: 0x0000FC03
		public override void WriteValue(float value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00011A18 File Offset: 0x0000FC18
		public override void WriteValue(decimal value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00011A2D File Offset: 0x0000FC2D
		public override void WriteValue(int value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00011A42 File Offset: 0x0000FC42
		public override void WriteValue(long value)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteValue(value);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000D3BC File Offset: 0x0000B5BC
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00011A57 File Offset: 0x0000FC57
		internal override IXmlNamespaceResolver NamespaceResolver
		{
			get
			{
				return this.resolver;
			}
			set
			{
				this.resolver = value;
				if (this.wrapped == null)
				{
					this.eventCache.NamespaceResolver = value;
					return;
				}
				this.wrapped.NamespaceResolver = value;
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00011A81 File Offset: 0x0000FC81
		internal override void WriteXmlDeclaration(XmlStandalone standalone)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteXmlDeclaration(standalone);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00011A96 File Offset: 0x0000FC96
		internal override void WriteXmlDeclaration(string xmldecl)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteXmlDeclaration(xmldecl);
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00011AAB File Offset: 0x0000FCAB
		internal override void StartElementContent()
		{
			this.wrapped.StartElementContent();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00011AB8 File Offset: 0x0000FCB8
		internal override void WriteEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteEndElement(prefix, localName, ns);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		internal override void WriteFullEndElement(string prefix, string localName, string ns)
		{
			this.wrapped.WriteFullEndElement(prefix, localName, ns);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		internal override void WriteNamespaceDeclaration(string prefix, string ns)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteNamespaceDeclaration(prefix, ns);
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00011AEE File Offset: 0x0000FCEE
		internal override bool SupportsNamespaceDeclarationInChunks
		{
			get
			{
				return this.wrapped.SupportsNamespaceDeclarationInChunks;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00011AFB File Offset: 0x0000FCFB
		internal override void WriteStartNamespaceDeclaration(string prefix)
		{
			this.EnsureWrappedWriter(XmlOutputMethod.Xml);
			this.wrapped.WriteStartNamespaceDeclaration(prefix);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00011B10 File Offset: 0x0000FD10
		internal override void WriteEndNamespaceDeclaration()
		{
			this.wrapped.WriteEndNamespaceDeclaration();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00011B20 File Offset: 0x0000FD20
		private static bool IsHtmlTag(string tagName)
		{
			return tagName.Length == 4 && (tagName[0] == 'H' || tagName[0] == 'h') && (tagName[1] == 'T' || tagName[1] == 't') && (tagName[2] == 'M' || tagName[2] == 'm') && (tagName[3] == 'L' || tagName[3] == 'l');
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00011B99 File Offset: 0x0000FD99
		private void EnsureWrappedWriter(XmlOutputMethod outMethod)
		{
			if (this.wrapped == null)
			{
				this.CreateWrappedWriter(outMethod);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00011BAC File Offset: 0x0000FDAC
		private bool TextBlockCreatesWriter(string textBlock)
		{
			if (this.wrapped == null)
			{
				if (XmlCharType.Instance.IsOnlyWhitespace(textBlock))
				{
					return false;
				}
				this.CreateWrappedWriter(XmlOutputMethod.Xml);
			}
			return true;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00011BDC File Offset: 0x0000FDDC
		private void CreateWrappedWriter(XmlOutputMethod outMethod)
		{
			this.writerSettings.ReadOnly = false;
			this.writerSettings.OutputMethod = outMethod;
			if (outMethod == XmlOutputMethod.Html && this.writerSettings.IndentInternal == TriState.Unknown)
			{
				this.writerSettings.Indent = true;
			}
			this.writerSettings.ReadOnly = true;
			if (this.textWriter != null)
			{
				this.wrapped = ((XmlWellFormedWriter)XmlWriter.Create(this.textWriter, this.writerSettings)).RawWriter;
			}
			else
			{
				this.wrapped = ((XmlWellFormedWriter)XmlWriter.Create(this.strm, this.writerSettings)).RawWriter;
			}
			this.eventCache.EndEvents();
			this.eventCache.EventsToWriter(this.wrapped);
			if (this.onRemove != null)
			{
				this.onRemove(this.wrapped);
			}
		}

		// Token: 0x040006B5 RID: 1717
		private XmlRawWriter wrapped;

		// Token: 0x040006B6 RID: 1718
		private OnRemoveWriter onRemove;

		// Token: 0x040006B7 RID: 1719
		private XmlWriterSettings writerSettings;

		// Token: 0x040006B8 RID: 1720
		private XmlEventCache eventCache;

		// Token: 0x040006B9 RID: 1721
		private TextWriter textWriter;

		// Token: 0x040006BA RID: 1722
		private Stream strm;
	}
}
