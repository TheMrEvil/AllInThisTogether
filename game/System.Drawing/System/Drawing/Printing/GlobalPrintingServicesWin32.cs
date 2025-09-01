using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000F0 RID: 240
	internal class GlobalPrintingServicesWin32 : GlobalPrintingServices
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0001BA3D File Offset: 0x00019C3D
		internal override PrinterSettings.StringCollection InstalledPrinters
		{
			get
			{
				return PrintingServicesWin32.InstalledPrinters;
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0001BA44 File Offset: 0x00019C44
		internal override IntPtr CreateGraphicsContext(PrinterSettings settings, PageSettings default_page_settings)
		{
			return PrintingServicesWin32.CreateGraphicsContext(settings, default_page_settings);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0001BA4D File Offset: 0x00019C4D
		internal override bool StartDoc(GraphicsPrinter gr, string doc_name, string output_file)
		{
			return PrintingServicesWin32.StartDoc(gr, doc_name, output_file);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0001BA57 File Offset: 0x00019C57
		internal override bool EndDoc(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.EndDoc(gr);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0001BA5F File Offset: 0x00019C5F
		internal override bool StartPage(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.StartPage(gr);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001BA67 File Offset: 0x00019C67
		internal override bool EndPage(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.EndPage(gr);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0001AF30 File Offset: 0x00019130
		public GlobalPrintingServicesWin32()
		{
		}
	}
}
