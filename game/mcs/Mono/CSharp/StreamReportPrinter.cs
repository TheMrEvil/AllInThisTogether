using System;
using System.IO;

namespace Mono.CSharp
{
	// Token: 0x0200028B RID: 651
	public class StreamReportPrinter : ReportPrinter
	{
		// Token: 0x06001FB0 RID: 8112 RVA: 0x0009B848 File Offset: 0x00099A48
		public StreamReportPrinter(TextWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0009B857 File Offset: 0x00099A57
		public override void Print(AbstractMessage msg, bool showFullPath)
		{
			base.Print(msg, this.writer, showFullPath);
			base.Print(msg, showFullPath);
		}

		// Token: 0x04000B9A RID: 2970
		private readonly TextWriter writer;
	}
}
