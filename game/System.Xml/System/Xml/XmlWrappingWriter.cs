using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000177 RID: 375
	internal class XmlWrappingWriter : XmlWriter
	{
		// Token: 0x06000D09 RID: 3337 RVA: 0x00057B16 File Offset: 0x00055D16
		internal XmlWrappingWriter(XmlWriter baseWriter)
		{
			this.writer = baseWriter;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x00057B25 File Offset: 0x00055D25
		public override XmlWriterSettings Settings
		{
			get
			{
				return this.writer.Settings;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00057B32 File Offset: 0x00055D32
		public override WriteState WriteState
		{
			get
			{
				return this.writer.WriteState;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00057B3F File Offset: 0x00055D3F
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.writer.XmlSpace;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00057B4C File Offset: 0x00055D4C
		public override string XmlLang
		{
			get
			{
				return this.writer.XmlLang;
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00057B59 File Offset: 0x00055D59
		public override void WriteStartDocument()
		{
			this.writer.WriteStartDocument();
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00057B66 File Offset: 0x00055D66
		public override void WriteStartDocument(bool standalone)
		{
			this.writer.WriteStartDocument(standalone);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00057B74 File Offset: 0x00055D74
		public override void WriteEndDocument()
		{
			this.writer.WriteEndDocument();
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x00057B81 File Offset: 0x00055D81
		public override void WriteDocType(string name, string pubid, string sysid, string subset)
		{
			this.writer.WriteDocType(name, pubid, sysid, subset);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00057B93 File Offset: 0x00055D93
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			this.writer.WriteStartElement(prefix, localName, ns);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00057BA3 File Offset: 0x00055DA3
		public override void WriteEndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00057BB0 File Offset: 0x00055DB0
		public override void WriteFullEndElement()
		{
			this.writer.WriteFullEndElement();
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00057BBD File Offset: 0x00055DBD
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			this.writer.WriteStartAttribute(prefix, localName, ns);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00057BCD File Offset: 0x00055DCD
		public override void WriteEndAttribute()
		{
			this.writer.WriteEndAttribute();
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00057BDA File Offset: 0x00055DDA
		public override void WriteCData(string text)
		{
			this.writer.WriteCData(text);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00057BE8 File Offset: 0x00055DE8
		public override void WriteComment(string text)
		{
			this.writer.WriteComment(text);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00057BF6 File Offset: 0x00055DF6
		public override void WriteProcessingInstruction(string name, string text)
		{
			this.writer.WriteProcessingInstruction(name, text);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00057C05 File Offset: 0x00055E05
		public override void WriteEntityRef(string name)
		{
			this.writer.WriteEntityRef(name);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00057C13 File Offset: 0x00055E13
		public override void WriteCharEntity(char ch)
		{
			this.writer.WriteCharEntity(ch);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00057C21 File Offset: 0x00055E21
		public override void WriteWhitespace(string ws)
		{
			this.writer.WriteWhitespace(ws);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00057C2F File Offset: 0x00055E2F
		public override void WriteString(string text)
		{
			this.writer.WriteString(text);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x000140CC File Offset: 0x000122CC
		public override void WriteSurrogateCharEntity(char lowChar, char highChar)
		{
			this.writer.WriteSurrogateCharEntity(lowChar, highChar);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00057C3D File Offset: 0x00055E3D
		public override void WriteChars(char[] buffer, int index, int count)
		{
			this.writer.WriteChars(buffer, index, count);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00057C4D File Offset: 0x00055E4D
		public override void WriteRaw(char[] buffer, int index, int count)
		{
			this.writer.WriteRaw(buffer, index, count);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00057C5D File Offset: 0x00055E5D
		public override void WriteRaw(string data)
		{
			this.writer.WriteRaw(data);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00057C6B File Offset: 0x00055E6B
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			this.writer.WriteBase64(buffer, index, count);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00057C7B File Offset: 0x00055E7B
		public override void Close()
		{
			this.writer.Close();
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00057C88 File Offset: 0x00055E88
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00057C95 File Offset: 0x00055E95
		public override string LookupPrefix(string ns)
		{
			return this.writer.LookupPrefix(ns);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00057CA3 File Offset: 0x00055EA3
		public override void WriteValue(object value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00057CB1 File Offset: 0x00055EB1
		public override void WriteValue(string value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00057CBF File Offset: 0x00055EBF
		public override void WriteValue(bool value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00057CCD File Offset: 0x00055ECD
		public override void WriteValue(DateTime value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00057CDB File Offset: 0x00055EDB
		public override void WriteValue(DateTimeOffset value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00057CE9 File Offset: 0x00055EE9
		public override void WriteValue(double value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00057CF7 File Offset: 0x00055EF7
		public override void WriteValue(float value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00057D05 File Offset: 0x00055F05
		public override void WriteValue(decimal value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00057D13 File Offset: 0x00055F13
		public override void WriteValue(int value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00057D21 File Offset: 0x00055F21
		public override void WriteValue(long value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00057D2F File Offset: 0x00055F2F
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				((IDisposable)this.writer).Dispose();
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00057D3F File Offset: 0x00055F3F
		public override Task WriteStartDocumentAsync()
		{
			return this.writer.WriteStartDocumentAsync();
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00057D4C File Offset: 0x00055F4C
		public override Task WriteStartDocumentAsync(bool standalone)
		{
			return this.writer.WriteStartDocumentAsync(standalone);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00057D5A File Offset: 0x00055F5A
		public override Task WriteEndDocumentAsync()
		{
			return this.writer.WriteEndDocumentAsync();
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00057D67 File Offset: 0x00055F67
		public override Task WriteDocTypeAsync(string name, string pubid, string sysid, string subset)
		{
			return this.writer.WriteDocTypeAsync(name, pubid, sysid, subset);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00057D79 File Offset: 0x00055F79
		public override Task WriteStartElementAsync(string prefix, string localName, string ns)
		{
			return this.writer.WriteStartElementAsync(prefix, localName, ns);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00057D89 File Offset: 0x00055F89
		public override Task WriteEndElementAsync()
		{
			return this.writer.WriteEndElementAsync();
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00057D96 File Offset: 0x00055F96
		public override Task WriteFullEndElementAsync()
		{
			return this.writer.WriteFullEndElementAsync();
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00057DA3 File Offset: 0x00055FA3
		protected internal override Task WriteStartAttributeAsync(string prefix, string localName, string ns)
		{
			return this.writer.WriteStartAttributeAsync(prefix, localName, ns);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00057DB3 File Offset: 0x00055FB3
		protected internal override Task WriteEndAttributeAsync()
		{
			return this.writer.WriteEndAttributeAsync();
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00057DC0 File Offset: 0x00055FC0
		public override Task WriteCDataAsync(string text)
		{
			return this.writer.WriteCDataAsync(text);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00057DCE File Offset: 0x00055FCE
		public override Task WriteCommentAsync(string text)
		{
			return this.writer.WriteCommentAsync(text);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00057DDC File Offset: 0x00055FDC
		public override Task WriteProcessingInstructionAsync(string name, string text)
		{
			return this.writer.WriteProcessingInstructionAsync(name, text);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00057DEB File Offset: 0x00055FEB
		public override Task WriteEntityRefAsync(string name)
		{
			return this.writer.WriteEntityRefAsync(name);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00057DF9 File Offset: 0x00055FF9
		public override Task WriteCharEntityAsync(char ch)
		{
			return this.writer.WriteCharEntityAsync(ch);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00057E07 File Offset: 0x00056007
		public override Task WriteWhitespaceAsync(string ws)
		{
			return this.writer.WriteWhitespaceAsync(ws);
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00057E15 File Offset: 0x00056015
		public override Task WriteStringAsync(string text)
		{
			return this.writer.WriteStringAsync(text);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00014888 File Offset: 0x00012A88
		public override Task WriteSurrogateCharEntityAsync(char lowChar, char highChar)
		{
			return this.writer.WriteSurrogateCharEntityAsync(lowChar, highChar);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00057E23 File Offset: 0x00056023
		public override Task WriteCharsAsync(char[] buffer, int index, int count)
		{
			return this.writer.WriteCharsAsync(buffer, index, count);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00057E33 File Offset: 0x00056033
		public override Task WriteRawAsync(char[] buffer, int index, int count)
		{
			return this.writer.WriteRawAsync(buffer, index, count);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00057E43 File Offset: 0x00056043
		public override Task WriteRawAsync(string data)
		{
			return this.writer.WriteRawAsync(data);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00057E51 File Offset: 0x00056051
		public override Task WriteBase64Async(byte[] buffer, int index, int count)
		{
			return this.writer.WriteBase64Async(buffer, index, count);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00057E61 File Offset: 0x00056061
		public override Task FlushAsync()
		{
			return this.writer.FlushAsync();
		}

		// Token: 0x04000EA3 RID: 3747
		protected XmlWriter writer;
	}
}
