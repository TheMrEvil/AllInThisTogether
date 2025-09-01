using System;
using System.IO;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003B7 RID: 951
	internal class TextOnlyOutput : RecordOutput
	{
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x000E82C8 File Offset: 0x000E64C8
		internal XsltOutput Output
		{
			get
			{
				return this.processor.Output;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060026B9 RID: 9913 RVA: 0x000E82D5 File Offset: 0x000E64D5
		public TextWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x000E82DD File Offset: 0x000E64DD
		internal TextOnlyOutput(Processor processor, Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.processor = processor;
			this.writer = new StreamWriter(stream, this.Output.Encoding);
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x000E8311 File Offset: 0x000E6511
		internal TextOnlyOutput(Processor processor, TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.processor = processor;
			this.writer = writer;
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000E8338 File Offset: 0x000E6538
		public Processor.OutputResult RecordDone(RecordBuilder record)
		{
			BuilderInfo mainNode = record.MainNode;
			XmlNodeType nodeType = mainNode.NodeType;
			if (nodeType == XmlNodeType.Text || nodeType - XmlNodeType.Whitespace <= 1)
			{
				this.writer.Write(mainNode.Value);
			}
			record.Reset();
			return Processor.OutputResult.Continue;
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x000E8376 File Offset: 0x000E6576
		public void TheEnd()
		{
			this.writer.Flush();
		}

		// Token: 0x04001E6B RID: 7787
		private Processor processor;

		// Token: 0x04001E6C RID: 7788
		private TextWriter writer;
	}
}
