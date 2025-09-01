using System;
using System.IO;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003B8 RID: 952
	internal class TextOutput : SequentialOutput
	{
		// Token: 0x060026BE RID: 9918 RVA: 0x000E8383 File Offset: 0x000E6583
		internal TextOutput(Processor processor, Stream stream) : base(processor)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.encoding = processor.Output.Encoding;
			this.writer = new StreamWriter(stream, this.encoding);
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x000E83BD File Offset: 0x000E65BD
		internal TextOutput(Processor processor, TextWriter writer) : base(processor)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.encoding = writer.Encoding;
			this.writer = writer;
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x000E83E7 File Offset: 0x000E65E7
		internal override void Write(char outputChar)
		{
			this.writer.Write(outputChar);
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x000E83F5 File Offset: 0x000E65F5
		internal override void Write(string outputText)
		{
			this.writer.Write(outputText);
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000E8403 File Offset: 0x000E6603
		internal override void Close()
		{
			this.writer.Flush();
			this.writer = null;
		}

		// Token: 0x04001E6D RID: 7789
		private TextWriter writer;
	}
}
