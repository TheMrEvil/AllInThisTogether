using System;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000066 RID: 102
	internal class XmlAsyncCheckWriter : XmlWriter
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00010E51 File Offset: 0x0000F051
		internal XmlWriter CoreWriter
		{
			get
			{
				return this.coreWriter;
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00010E59 File Offset: 0x0000F059
		public XmlAsyncCheckWriter(XmlWriter writer)
		{
			this.coreWriter = writer;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00010E73 File Offset: 0x0000F073
		private void CheckAsync()
		{
			if (!this.lastTask.IsCompleted)
			{
				throw new InvalidOperationException(Res.GetString("An asynchronous operation is already in progress."));
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00010E94 File Offset: 0x0000F094
		public override XmlWriterSettings Settings
		{
			get
			{
				XmlWriterSettings xmlWriterSettings = this.coreWriter.Settings;
				if (xmlWriterSettings != null)
				{
					xmlWriterSettings = xmlWriterSettings.Clone();
				}
				else
				{
					xmlWriterSettings = new XmlWriterSettings();
				}
				xmlWriterSettings.Async = true;
				xmlWriterSettings.ReadOnly = true;
				return xmlWriterSettings;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00010ECE File Offset: 0x0000F0CE
		public override void WriteStartDocument()
		{
			this.CheckAsync();
			this.coreWriter.WriteStartDocument();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00010EE1 File Offset: 0x0000F0E1
		public override void WriteStartDocument(bool standalone)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartDocument(standalone);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00010EF5 File Offset: 0x0000F0F5
		public override void WriteEndDocument()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndDocument();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00010F08 File Offset: 0x0000F108
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.CheckAsync();
			this.coreWriter.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00010F20 File Offset: 0x0000F120
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00010F36 File Offset: 0x0000F136
		public override void WriteEndElement()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndElement();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00010F49 File Offset: 0x0000F149
		public override void WriteFullEndElement()
		{
			this.CheckAsync();
			this.coreWriter.WriteFullEndElement();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00010F5C File Offset: 0x0000F15C
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00010F72 File Offset: 0x0000F172
		public override void WriteEndAttribute()
		{
			this.CheckAsync();
			this.coreWriter.WriteEndAttribute();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00010F85 File Offset: 0x0000F185
		public override void WriteCData(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteCData(text);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00010F99 File Offset: 0x0000F199
		public override void WriteComment(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteComment(text);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00010FAD File Offset: 0x0000F1AD
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00010FC2 File Offset: 0x0000F1C2
		public override void WriteEntityRef(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteEntityRef(name);
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00010FD6 File Offset: 0x0000F1D6
		public override void WriteCharEntity(char ch)
		{
			this.CheckAsync();
			this.coreWriter.WriteCharEntity(ch);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00010FEA File Offset: 0x0000F1EA
		public override void WriteWhitespace(string ws)
		{
			this.CheckAsync();
			this.coreWriter.WriteWhitespace(ws);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00010FFE File Offset: 0x0000F1FE
		public override void WriteString(string text)
		{
			this.CheckAsync();
			this.coreWriter.WriteString(text);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00011012 File Offset: 0x0000F212
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.CheckAsync();
			this.coreWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00011027 File Offset: 0x0000F227
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteChars(buffer, index, count);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001103D File Offset: 0x0000F23D
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteRaw(buffer, index, count);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00011053 File Offset: 0x0000F253
		public override void WriteRaw(string data)
		{
			this.CheckAsync();
			this.coreWriter.WriteRaw(data);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00011067 File Offset: 0x0000F267
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteBase64(buffer, index, count);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001107D File Offset: 0x0000F27D
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			this.coreWriter.WriteBinHex(buffer, index, count);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00011093 File Offset: 0x0000F293
		public override WriteState WriteState
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.WriteState;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000110A6 File Offset: 0x0000F2A6
		public override void Close()
		{
			this.CheckAsync();
			this.coreWriter.Close();
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000110B9 File Offset: 0x0000F2B9
		public override void Flush()
		{
			this.CheckAsync();
			this.coreWriter.Flush();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x000110CC File Offset: 0x0000F2CC
		public override string LookupPrefix(string ns)
		{
			this.CheckAsync();
			return this.coreWriter.LookupPrefix(ns);
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000393 RID: 915 RVA: 0x000110E0 File Offset: 0x0000F2E0
		public override XmlSpace XmlSpace
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.XmlSpace;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000394 RID: 916 RVA: 0x000110F3 File Offset: 0x0000F2F3
		public override string XmlLang
		{
			get
			{
				this.CheckAsync();
				return this.coreWriter.XmlLang;
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00011106 File Offset: 0x0000F306
		public override void WriteNmToken(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteNmToken(name);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001111A File Offset: 0x0000F31A
		public override void WriteName(string name)
		{
			this.CheckAsync();
			this.coreWriter.WriteName(name);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001112E File Offset: 0x0000F32E
		public override void WriteQualifiedName(string localName, string ns)
		{
			this.CheckAsync();
			this.coreWriter.WriteQualifiedName(localName, ns);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00011143 File Offset: 0x0000F343
		public override void WriteValue(object value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00011157 File Offset: 0x0000F357
		public override void WriteValue(string value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001116B File Offset: 0x0000F36B
		public override void WriteValue(bool value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001117F File Offset: 0x0000F37F
		public override void WriteValue(DateTime value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00011193 File Offset: 0x0000F393
		public override void WriteValue(DateTimeOffset value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000111A7 File Offset: 0x0000F3A7
		public override void WriteValue(double value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000111BB File Offset: 0x0000F3BB
		public override void WriteValue(float value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000111CF File Offset: 0x0000F3CF
		public override void WriteValue(decimal value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x000111E3 File Offset: 0x0000F3E3
		public override void WriteValue(int value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000111F7 File Offset: 0x0000F3F7
		public override void WriteValue(long value)
		{
			this.CheckAsync();
			this.coreWriter.WriteValue(value);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001120B File Offset: 0x0000F40B
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteAttributes(reader, defattr);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00011220 File Offset: 0x0000F420
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteNode(reader, defattr);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00011235 File Offset: 0x0000F435
		public override void WriteNode(XPathNavigator navigator, bool defattr)
		{
			this.CheckAsync();
			this.coreWriter.WriteNode(navigator, defattr);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001124A File Offset: 0x0000F44A
		protected override void Dispose(bool disposing)
		{
			this.CheckAsync();
			this.coreWriter.Dispose();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00011260 File Offset: 0x0000F460
		public override Task WriteStartDocumentAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteStartDocumentAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00011288 File Offset: 0x0000F488
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteStartDocumentAsync(standalone);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000112B0 File Offset: 0x0000F4B0
		public override Task WriteEndDocumentAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteEndDocumentAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteDocTypeAsync(name, pubid, sysid, subset);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00011304 File Offset: 0x0000F504
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteStartElementAsync(prefix, localName, ns);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00011330 File Offset: 0x0000F530
		public override Task WriteEndElementAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteEndElementAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00011358 File Offset: 0x0000F558
		public override Task WriteFullEndElementAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteFullEndElementAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00011380 File Offset: 0x0000F580
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteStartAttributeAsync(prefix, localName, ns);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000113AC File Offset: 0x0000F5AC
		protected internal override Task WriteEndAttributeAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteEndAttributeAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000113D4 File Offset: 0x0000F5D4
		public override Task WriteCDataAsync(string text)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteCDataAsync(text);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000113FC File Offset: 0x0000F5FC
		public override Task WriteCommentAsync(string text)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteCommentAsync(text);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00011424 File Offset: 0x0000F624
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteProcessingInstructionAsync(name, text);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00011450 File Offset: 0x0000F650
		public override Task WriteEntityRefAsync(string name)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteEntityRefAsync(name);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00011478 File Offset: 0x0000F678
		public override Task WriteCharEntityAsync(char ch)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteCharEntityAsync(ch);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000114A0 File Offset: 0x0000F6A0
		public override Task WriteWhitespaceAsync(string ws)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteWhitespaceAsync(ws);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000114C8 File Offset: 0x0000F6C8
		public override Task WriteStringAsync(string text)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteStringAsync(text);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000114F0 File Offset: 0x0000F6F0
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteSurrogateCharEntityAsync(lowChar, highChar);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001151C File Offset: 0x0000F71C
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteCharsAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00011548 File Offset: 0x0000F748
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteRawAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00011574 File Offset: 0x0000F774
		public override Task WriteRawAsync(string data)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteRawAsync(data);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001159C File Offset: 0x0000F79C
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteBase64Async(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x000115C8 File Offset: 0x0000F7C8
		public override Task WriteBinHexAsync(byte[] buffer, int index, int count)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteBinHexAsync(buffer, index, count);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000115F4 File Offset: 0x0000F7F4
		public override Task FlushAsync()
		{
			this.CheckAsync();
			Task result = this.coreWriter.FlushAsync();
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001161C File Offset: 0x0000F81C
		public override Task WriteNmTokenAsync(string name)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteNmTokenAsync(name);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00011644 File Offset: 0x0000F844
		public override Task WriteNameAsync(string name)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteNameAsync(name);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001166C File Offset: 0x0000F86C
		public override Task WriteQualifiedNameAsync(string localName, string ns)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteQualifiedNameAsync(localName, ns);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00011698 File Offset: 0x0000F898
		public override Task WriteAttributesAsync(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteAttributesAsync(reader, defattr);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000116C4 File Offset: 0x0000F8C4
		public override Task WriteNodeAsync(XmlReader reader, bool defattr)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteNodeAsync(reader, defattr);
			this.lastTask = result;
			return result;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000116F0 File Offset: 0x0000F8F0
		public override Task WriteNodeAsync(XPathNavigator navigator, bool defattr)
		{
			this.CheckAsync();
			Task result = this.coreWriter.WriteNodeAsync(navigator, defattr);
			this.lastTask = result;
			return result;
		}

		// Token: 0x040006B3 RID: 1715
		private readonly XmlWriter coreWriter;

		// Token: 0x040006B4 RID: 1716
		private Task lastTask = AsyncHelper.DoneTask;
	}
}
