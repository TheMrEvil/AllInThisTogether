using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000E6 RID: 230
	internal class GlobalPrintingServicesUnix : GlobalPrintingServices
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0001AEFE File Offset: 0x000190FE
		internal override PrinterSettings.StringCollection InstalledPrinters
		{
			get
			{
				return PrintingServicesUnix.InstalledPrinters;
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001AF05 File Offset: 0x00019105
		internal override IntPtr CreateGraphicsContext(PrinterSettings settings, PageSettings default_page_settings)
		{
			return PrintingServicesUnix.CreateGraphicsContext(settings, default_page_settings);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0001AF0E File Offset: 0x0001910E
		internal override bool StartDoc(GraphicsPrinter gr, string doc_name, string output_file)
		{
			return PrintingServicesUnix.StartDoc(gr, doc_name, output_file);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0001AF18 File Offset: 0x00019118
		internal override bool EndDoc(GraphicsPrinter gr)
		{
			return PrintingServicesUnix.EndDoc(gr);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0001AF20 File Offset: 0x00019120
		internal override bool StartPage(GraphicsPrinter gr)
		{
			return PrintingServicesUnix.StartPage(gr);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0001AF28 File Offset: 0x00019128
		internal override bool EndPage(GraphicsPrinter gr)
		{
			return PrintingServicesUnix.EndPage(gr);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x0001AF30 File Offset: 0x00019130
		public GlobalPrintingServicesUnix()
		{
		}
	}
}
