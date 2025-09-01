using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000D8 RID: 216
	internal abstract class PrintingServices
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000B8E RID: 2958
		internal abstract string DefaultPrinter { get; }

		// Token: 0x06000B8F RID: 2959
		internal abstract bool IsPrinterValid(string printer);

		// Token: 0x06000B90 RID: 2960
		internal abstract void LoadPrinterSettings(string printer, PrinterSettings settings);

		// Token: 0x06000B91 RID: 2961
		internal abstract void LoadPrinterResolutions(string printer, PrinterSettings settings);

		// Token: 0x06000B92 RID: 2962
		internal abstract void GetPrintDialogInfo(string printer, ref string port, ref string type, ref string status, ref string comment);

		// Token: 0x06000B93 RID: 2963 RVA: 0x00019AE0 File Offset: 0x00017CE0
		internal void LoadDefaultResolutions(PrinterSettings.PrinterResolutionCollection col)
		{
			col.Add(new PrinterResolution(PrinterResolutionKind.High, -4, -1));
			col.Add(new PrinterResolution(PrinterResolutionKind.Medium, -3, -1));
			col.Add(new PrinterResolution(PrinterResolutionKind.Low, -2, -1));
			col.Add(new PrinterResolution(PrinterResolutionKind.Draft, -1, -1));
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x00002050 File Offset: 0x00000250
		protected PrintingServices()
		{
		}
	}
}
