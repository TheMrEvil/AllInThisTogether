using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200009D RID: 157
	internal class XmlUTF8TextWriter : XmlBaseWriter, IXmlTextWriterInitializer
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x000066E8 File Offset: 0x000048E8
		internal override bool FastAsync
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00022448 File Offset: 0x00020648
		public void SetOutput(Stream stream, Encoding encoding, bool ownsStream)
		{
			if (stream == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("stream");
			}
			if (encoding == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encoding");
			}
			if (encoding.WebName != Encoding.UTF8.WebName)
			{
				stream = new EncodingStreamWrapper(stream, encoding, true);
			}
			if (this.writer == null)
			{
				this.writer = new XmlUTF8NodeWriter();
			}
			this.writer.SetOutput(stream, ownsStream, encoding);
			base.SetOutput(this.writer);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x000224BF File Offset: 0x000206BF
		public override bool CanFragment
		{
			get
			{
				return this.writer.Encoding == null;
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0002238C File Offset: 0x0002058C
		protected override XmlSigningNodeWriter CreateSigningNodeWriter()
		{
			return new XmlSigningNodeWriter(true);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public XmlUTF8TextWriter()
		{
		}

		// Token: 0x040003AC RID: 940
		private XmlUTF8NodeWriter writer;
	}
}
