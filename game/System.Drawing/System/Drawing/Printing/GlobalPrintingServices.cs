using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000D9 RID: 217
	internal abstract class GlobalPrintingServices
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000B95 RID: 2965
		internal abstract PrinterSettings.StringCollection InstalledPrinters { get; }

		// Token: 0x06000B96 RID: 2966
		internal abstract IntPtr CreateGraphicsContext(PrinterSettings settings, PageSettings page_settings);

		// Token: 0x06000B97 RID: 2967
		internal abstract bool StartDoc(GraphicsPrinter gr, string doc_name, string output_file);

		// Token: 0x06000B98 RID: 2968
		internal abstract bool StartPage(GraphicsPrinter gr);

		// Token: 0x06000B99 RID: 2969
		internal abstract bool EndPage(GraphicsPrinter gr);

		// Token: 0x06000B9A RID: 2970
		internal abstract bool EndDoc(GraphicsPrinter gr);

		// Token: 0x06000B9B RID: 2971 RVA: 0x00002050 File Offset: 0x00000250
		protected GlobalPrintingServices()
		{
		}
	}
}
