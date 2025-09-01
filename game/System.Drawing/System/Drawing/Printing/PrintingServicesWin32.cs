using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Drawing.Printing
{
	// Token: 0x020000E7 RID: 231
	internal class PrintingServicesWin32 : PrintingServices
	{
		// Token: 0x06000BD6 RID: 3030 RVA: 0x00019BE0 File Offset: 0x00017DE0
		internal PrintingServicesWin32()
		{
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0001AF38 File Offset: 0x00019138
		internal override bool IsPrinterValid(string printer)
		{
			if (printer == null | printer == string.Empty)
			{
				return false;
			}
			int num = PrintingServicesWin32.Win32DocumentProperties(IntPtr.Zero, IntPtr.Zero, printer, IntPtr.Zero, IntPtr.Zero, 0);
			this.is_printer_valid = (num > 0);
			return this.is_printer_valid;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0001AF88 File Offset: 0x00019188
		internal override void LoadPrinterSettings(string printer, PrinterSettings settings)
		{
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			settings.maximum_copies = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_COPIES, IntPtr.Zero, IntPtr.Zero);
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_DUPLEX, IntPtr.Zero, IntPtr.Zero);
			settings.can_duplex = (num == 1);
			num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_COLORDEVICE, IntPtr.Zero, IntPtr.Zero);
			settings.supports_color = (num == 1);
			num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_ORIENTATION, IntPtr.Zero, IntPtr.Zero);
			if (num != -1)
			{
				settings.landscape_angle = num;
			}
			IntPtr zero2 = IntPtr.Zero;
			IntPtr hDc = PrintingServicesWin32.Win32CreateIC(null, printer, null, IntPtr.Zero);
			num = PrintingServicesWin32.Win32GetDeviceCaps(hDc, 2);
			settings.is_plotter = (num == 0);
			PrintingServicesWin32.Win32DeleteDC(hDc);
			try
			{
				PrintingServicesWin32.Win32OpenPrinter(printer, out zero, IntPtr.Zero);
				num = PrintingServicesWin32.Win32DocumentProperties(IntPtr.Zero, zero, null, IntPtr.Zero, IntPtr.Zero, 0);
				if (num >= 0)
				{
					intPtr = Marshal.AllocHGlobal(num);
					num = PrintingServicesWin32.Win32DocumentProperties(IntPtr.Zero, zero, null, intPtr, IntPtr.Zero, 2);
					PrintingServicesWin32.DEVMODE devmode = (PrintingServicesWin32.DEVMODE)Marshal.PtrToStructure(intPtr, typeof(PrintingServicesWin32.DEVMODE));
					this.LoadPrinterPaperSizes(printer, settings);
					foreach (object obj in settings.PaperSizes)
					{
						PaperSize paperSize = (PaperSize)obj;
						if (paperSize.Kind == (PaperKind)devmode.dmPaperSize)
						{
							settings.DefaultPageSettings.PaperSize = paperSize;
							break;
						}
					}
					this.LoadPrinterPaperSources(printer, settings);
					foreach (object obj2 in settings.PaperSources)
					{
						PaperSource paperSource = (PaperSource)obj2;
						if (paperSource.Kind == (PaperSourceKind)devmode.dmDefaultSource)
						{
							settings.DefaultPageSettings.PaperSource = paperSource;
							break;
						}
					}
				}
			}
			finally
			{
				PrintingServicesWin32.Win32ClosePrinter(zero);
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0001B1D4 File Offset: 0x000193D4
		internal override void LoadPrinterResolutions(string printer, PrinterSettings settings)
		{
			IntPtr intPtr = IntPtr.Zero;
			settings.PrinterResolutions.Clear();
			base.LoadDefaultResolutions(settings.PrinterResolutions);
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_ENUMRESOLUTIONS, IntPtr.Zero, IntPtr.Zero);
			if (num == -1)
			{
				return;
			}
			IntPtr ptr;
			intPtr = (ptr = Marshal.AllocHGlobal(num * 2 * Marshal.SizeOf<IntPtr>(intPtr)));
			num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_ENUMRESOLUTIONS, intPtr, IntPtr.Zero);
			if (num != -1)
			{
				for (int i = 0; i < num; i++)
				{
					int num2 = Marshal.ReadInt32(ptr);
					ptr = new IntPtr(ptr.ToInt64() + (long)Marshal.SizeOf<int>(num2));
					int num3 = Marshal.ReadInt32(ptr);
					ptr = new IntPtr(ptr.ToInt64() + (long)Marshal.SizeOf<int>(num3));
					settings.PrinterResolutions.Add(new PrinterResolution(PrinterResolutionKind.Custom, num2, num3));
				}
			}
			Marshal.FreeHGlobal(intPtr);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0001B2A4 File Offset: 0x000194A4
		private void LoadPrinterPaperSizes(string printer, PrinterSettings settings)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			IntPtr intPtr3 = IntPtr.Zero;
			if (settings.PaperSizes == null)
			{
				settings.paper_sizes = new PrinterSettings.PaperSizeCollection(new PaperSize[0]);
			}
			else
			{
				settings.PaperSizes.Clear();
			}
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERSIZE, IntPtr.Zero, IntPtr.Zero);
			if (num == -1)
			{
				return;
			}
			try
			{
				IntPtr ptr;
				intPtr2 = (ptr = Marshal.AllocHGlobal(num * 2 * 4));
				IntPtr ptr2;
				intPtr = (ptr2 = Marshal.AllocHGlobal(num * 64 * 2));
				IntPtr ptr3;
				intPtr3 = (ptr3 = Marshal.AllocHGlobal(num * 2));
				int num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERSIZE, intPtr2, IntPtr.Zero);
				if (num2 != -1)
				{
					num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERS, intPtr3, IntPtr.Zero);
					num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_PAPERNAMES, intPtr, IntPtr.Zero);
					for (int i = 0; i < num2; i++)
					{
						int num3 = Marshal.ReadInt32(ptr, i * 8);
						int num4 = Marshal.ReadInt32(ptr, i * 8 + 4);
						num3 = PrinterUnitConvert.Convert(num3, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
						num4 = PrinterUnitConvert.Convert(num4, PrinterUnit.TenthsOfAMillimeter, PrinterUnit.Display);
						string name = Marshal.PtrToStringUni(ptr2);
						ptr2 = new IntPtr(ptr2.ToInt64() + 128L);
						PaperKind rawKind = (PaperKind)Marshal.ReadInt16(ptr3);
						ptr3 = new IntPtr(ptr3.ToInt64() + 2L);
						PaperSize paperSize = new PaperSize(name, num3, num4);
						paperSize.RawKind = (int)rawKind;
						settings.PaperSizes.Add(paperSize);
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
				if (intPtr3 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr3);
				}
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0001B464 File Offset: 0x00019664
		internal static bool StartDoc(GraphicsPrinter gr, string doc_name, string output_file)
		{
			PrintingServicesWin32.DOCINFO docinfo = default(PrintingServicesWin32.DOCINFO);
			docinfo.cbSize = Marshal.SizeOf<PrintingServicesWin32.DOCINFO>(docinfo);
			docinfo.lpszDocName = Marshal.StringToHGlobalUni(doc_name);
			docinfo.lpszOutput = IntPtr.Zero;
			docinfo.lpszDatatype = IntPtr.Zero;
			docinfo.fwType = 0;
			int num = PrintingServicesWin32.Win32StartDoc(gr.Hdc, ref docinfo);
			Marshal.FreeHGlobal(docinfo.lpszDocName);
			return num > 0;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0001B4D4 File Offset: 0x000196D4
		private void LoadPrinterPaperSources(string printer, PrinterSettings settings)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			if (settings.PaperSources == null)
			{
				settings.paper_sources = new PrinterSettings.PaperSourceCollection(new PaperSource[0]);
			}
			else
			{
				settings.PaperSources.Clear();
			}
			int num = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_BINNAMES, IntPtr.Zero, IntPtr.Zero);
			if (num == -1)
			{
				return;
			}
			try
			{
				IntPtr ptr;
				intPtr = (ptr = Marshal.AllocHGlobal(num * 2 * 24));
				IntPtr ptr2;
				intPtr2 = (ptr2 = Marshal.AllocHGlobal(num * 2));
				int num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_BINNAMES, intPtr, IntPtr.Zero);
				if (num2 != -1)
				{
					num2 = PrintingServicesWin32.Win32DeviceCapabilities(printer, null, PrintingServicesWin32.DCCapabilities.DC_BINS, intPtr2, IntPtr.Zero);
					for (int i = 0; i < num2; i++)
					{
						string name = Marshal.PtrToStringUni(ptr);
						PaperSourceKind kind = (PaperSourceKind)Marshal.ReadInt16(ptr2);
						settings.PaperSources.Add(new PaperSource(kind, name));
						ptr = new IntPtr(ptr.ToInt64() + 48L);
						ptr2 = new IntPtr(ptr2.ToInt64() + 2L);
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x0001B608 File Offset: 0x00019808
		internal static bool StartPage(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.Win32StartPage(gr.Hdc) > 0;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0001B61B File Offset: 0x0001981B
		internal static bool EndPage(GraphicsPrinter gr)
		{
			return PrintingServicesWin32.Win32EndPage(gr.Hdc) > 0;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x0001B62E File Offset: 0x0001982E
		internal static bool EndDoc(GraphicsPrinter gr)
		{
			int num = PrintingServicesWin32.Win32EndDoc(gr.Hdc);
			PrintingServicesWin32.Win32DeleteDC(gr.Hdc);
			gr.Graphics.Dispose();
			return num > 0;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001B658 File Offset: 0x00019858
		internal static IntPtr CreateGraphicsContext(PrinterSettings settings, PageSettings default_page_settings)
		{
			IntPtr zero = IntPtr.Zero;
			return PrintingServicesWin32.Win32CreateDC(null, settings.PrinterName, null, IntPtr.Zero);
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0001B674 File Offset: 0x00019874
		internal override string DefaultPrinter
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				int capacity = stringBuilder.Capacity;
				if (PrintingServicesWin32.Win32GetDefaultPrinter(stringBuilder, ref capacity) > 0 && this.IsPrinterValid(stringBuilder.ToString()))
				{
					return stringBuilder.ToString();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0001B6B8 File Offset: 0x000198B8
		internal static PrinterSettings.StringCollection InstalledPrinters
		{
			get
			{
				PrinterSettings.StringCollection stringCollection = new PrinterSettings.StringCollection(new string[0]);
				uint num = 0U;
				uint num2 = 0U;
				PrintingServicesWin32.Win32EnumPrinters(6, null, 2U, IntPtr.Zero, 0U, ref num, ref num2);
				if (num <= 0U)
				{
					return stringCollection;
				}
				IntPtr ptr;
				IntPtr intPtr = ptr = Marshal.AllocHGlobal((int)num);
				try
				{
					PrintingServicesWin32.Win32EnumPrinters(6, null, 2U, intPtr, num, ref num, ref num2);
					int num3 = 0;
					while ((long)num3 < (long)((ulong)num2))
					{
						PrintingServicesWin32.PRINTER_INFO printer_INFO = (PrintingServicesWin32.PRINTER_INFO)Marshal.PtrToStructure(ptr, typeof(PrintingServicesWin32.PRINTER_INFO));
						string value = Marshal.PtrToStringUni(printer_INFO.pPrinterName);
						stringCollection.Add(value);
						ptr = new IntPtr(ptr.ToInt64() + (long)Marshal.SizeOf<PrintingServicesWin32.PRINTER_INFO>(printer_INFO));
						num3++;
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return stringCollection;
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0001B77C File Offset: 0x0001997C
		internal override void GetPrintDialogInfo(string printer, ref string port, ref string type, ref string status, ref string comment)
		{
			PrintingServicesWin32.PRINTER_INFO printer_INFO = default(PrintingServicesWin32.PRINTER_INFO);
			int num = 0;
			IntPtr intPtr;
			PrintingServicesWin32.Win32OpenPrinter(printer, out intPtr, IntPtr.Zero);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			PrintingServicesWin32.Win32GetPrinter(intPtr, 2, IntPtr.Zero, 0, ref num);
			IntPtr intPtr2 = Marshal.AllocHGlobal(num);
			PrintingServicesWin32.Win32GetPrinter(intPtr, 2, intPtr2, num, ref num);
			printer_INFO = (PrintingServicesWin32.PRINTER_INFO)Marshal.PtrToStructure(intPtr2, typeof(PrintingServicesWin32.PRINTER_INFO));
			Marshal.FreeHGlobal(intPtr2);
			port = Marshal.PtrToStringUni(printer_INFO.pPortName);
			comment = Marshal.PtrToStringUni(printer_INFO.pComment);
			type = Marshal.PtrToStringUni(printer_INFO.pDriverName);
			status = this.GetPrinterStatusMsg(printer_INFO.Status);
			PrintingServicesWin32.Win32ClosePrinter(intPtr);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0001B82C File Offset: 0x00019A2C
		private string GetPrinterStatusMsg(uint status)
		{
			string text = string.Empty;
			if (status == 0U)
			{
				return "Ready";
			}
			if ((status & 1U) != 0U)
			{
				text += "Paused; ";
			}
			if ((status & 2U) != 0U)
			{
				text += "Error; ";
			}
			if ((status & 4U) != 0U)
			{
				text += "Pending deletion; ";
			}
			if ((status & 8U) != 0U)
			{
				text += "Paper jam; ";
			}
			if ((status & 16U) != 0U)
			{
				text += "Paper out; ";
			}
			if ((status & 32U) != 0U)
			{
				text += "Manual feed; ";
			}
			if ((status & 64U) != 0U)
			{
				text += "Paper problem; ";
			}
			if ((status & 128U) != 0U)
			{
				text += "Offline; ";
			}
			if ((status & 256U) != 0U)
			{
				text += "I/O active; ";
			}
			if ((status & 512U) != 0U)
			{
				text += "Busy; ";
			}
			if ((status & 1024U) != 0U)
			{
				text += "Printing; ";
			}
			if ((status & 2048U) != 0U)
			{
				text += "Output bin full; ";
			}
			if ((status & 4096U) != 0U)
			{
				text += "Not available; ";
			}
			if ((status & 8192U) != 0U)
			{
				text += "Waiting; ";
			}
			if ((status & 16384U) != 0U)
			{
				text += "Processing; ";
			}
			if ((status & 32768U) != 0U)
			{
				text += "Initializing; ";
			}
			if ((status & 65536U) != 0U)
			{
				text += "Warming up; ";
			}
			if ((status & 131072U) != 0U)
			{
				text += "Toner low; ";
			}
			if ((status & 262144U) != 0U)
			{
				text += "No toner; ";
			}
			if ((status & 524288U) != 0U)
			{
				text += "Page punt; ";
			}
			if ((status & 1048576U) != 0U)
			{
				text += "User intervention; ";
			}
			if ((status & 2097152U) != 0U)
			{
				text += "Out of memory; ";
			}
			if ((status & 4194304U) != 0U)
			{
				text += "Door open; ";
			}
			if ((status & 8388608U) != 0U)
			{
				text += "Server unkown; ";
			}
			if ((status & 16777216U) != 0U)
			{
				text += "Power save; ";
			}
			return text;
		}

		// Token: 0x06000BE5 RID: 3045
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "OpenPrinter", SetLastError = true)]
		private static extern int Win32OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

		// Token: 0x06000BE6 RID: 3046
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "GetPrinter", SetLastError = true)]
		private static extern int Win32GetPrinter(IntPtr hPrinter, int level, IntPtr dwBuf, int size, ref int dwNeeded);

		// Token: 0x06000BE7 RID: 3047
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "ClosePrinter", SetLastError = true)]
		private static extern int Win32ClosePrinter(IntPtr hPrinter);

		// Token: 0x06000BE8 RID: 3048
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "DeviceCapabilities", SetLastError = true)]
		private static extern int Win32DeviceCapabilities(string device, string port, PrintingServicesWin32.DCCapabilities cap, IntPtr outputBuffer, IntPtr deviceMode);

		// Token: 0x06000BE9 RID: 3049
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "EnumPrinters", SetLastError = true)]
		private static extern int Win32EnumPrinters(int Flags, string Name, uint Level, IntPtr pPrinterEnum, uint cbBuf, ref uint pcbNeeded, ref uint pcReturned);

		// Token: 0x06000BEA RID: 3050
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "GetDefaultPrinter", SetLastError = true)]
		private static extern int Win32GetDefaultPrinter(StringBuilder buffer, ref int bufferSize);

		// Token: 0x06000BEB RID: 3051
		[DllImport("winspool.drv", CharSet = CharSet.Unicode, EntryPoint = "DocumentProperties", SetLastError = true)]
		private static extern int Win32DocumentProperties(IntPtr hwnd, IntPtr hPrinter, string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

		// Token: 0x06000BEC RID: 3052
		[DllImport("gdi32.dll", EntryPoint = "CreateDC")]
		private static extern IntPtr Win32CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

		// Token: 0x06000BED RID: 3053
		[DllImport("gdi32.dll", EntryPoint = "CreateIC")]
		private static extern IntPtr Win32CreateIC(string lpszDriver, string lpszDevice, string lpszOutput, IntPtr lpInitData);

		// Token: 0x06000BEE RID: 3054
		[DllImport("gdi32.dll", CharSet = CharSet.Unicode, EntryPoint = "StartDoc")]
		private static extern int Win32StartDoc(IntPtr hdc, [In] ref PrintingServicesWin32.DOCINFO lpdi);

		// Token: 0x06000BEF RID: 3055
		[DllImport("gdi32.dll", EntryPoint = "StartPage")]
		private static extern int Win32StartPage(IntPtr hDC);

		// Token: 0x06000BF0 RID: 3056
		[DllImport("gdi32.dll", EntryPoint = "EndPage")]
		private static extern int Win32EndPage(IntPtr hdc);

		// Token: 0x06000BF1 RID: 3057
		[DllImport("gdi32.dll", EntryPoint = "EndDoc")]
		private static extern int Win32EndDoc(IntPtr hdc);

		// Token: 0x06000BF2 RID: 3058
		[DllImport("gdi32.dll", EntryPoint = "DeleteDC")]
		public static extern IntPtr Win32DeleteDC(IntPtr hDc);

		// Token: 0x06000BF3 RID: 3059
		[DllImport("gdi32.dll", EntryPoint = "GetDeviceCaps")]
		public static extern int Win32GetDeviceCaps(IntPtr hDc, int index);

		// Token: 0x040007A2 RID: 1954
		private bool is_printer_valid;

		// Token: 0x020000E8 RID: 232
		internal struct PRINTER_INFO
		{
			// Token: 0x040007A3 RID: 1955
			public IntPtr pServerName;

			// Token: 0x040007A4 RID: 1956
			public IntPtr pPrinterName;

			// Token: 0x040007A5 RID: 1957
			public IntPtr pShareName;

			// Token: 0x040007A6 RID: 1958
			public IntPtr pPortName;

			// Token: 0x040007A7 RID: 1959
			public IntPtr pDriverName;

			// Token: 0x040007A8 RID: 1960
			public IntPtr pComment;

			// Token: 0x040007A9 RID: 1961
			public IntPtr pLocation;

			// Token: 0x040007AA RID: 1962
			public IntPtr pDevMode;

			// Token: 0x040007AB RID: 1963
			public IntPtr pSepFile;

			// Token: 0x040007AC RID: 1964
			public IntPtr pPrintProcessor;

			// Token: 0x040007AD RID: 1965
			public IntPtr pDatatype;

			// Token: 0x040007AE RID: 1966
			public IntPtr pParameters;

			// Token: 0x040007AF RID: 1967
			public IntPtr pSecurityDescriptor;

			// Token: 0x040007B0 RID: 1968
			public uint Attributes;

			// Token: 0x040007B1 RID: 1969
			public uint Priority;

			// Token: 0x040007B2 RID: 1970
			public uint DefaultPriority;

			// Token: 0x040007B3 RID: 1971
			public uint StartTime;

			// Token: 0x040007B4 RID: 1972
			public uint UntilTime;

			// Token: 0x040007B5 RID: 1973
			public uint Status;

			// Token: 0x040007B6 RID: 1974
			public uint cJobs;

			// Token: 0x040007B7 RID: 1975
			public uint AveragePPM;
		}

		// Token: 0x020000E9 RID: 233
		internal struct DOCINFO
		{
			// Token: 0x040007B8 RID: 1976
			public int cbSize;

			// Token: 0x040007B9 RID: 1977
			public IntPtr lpszDocName;

			// Token: 0x040007BA RID: 1978
			public IntPtr lpszOutput;

			// Token: 0x040007BB RID: 1979
			public IntPtr lpszDatatype;

			// Token: 0x040007BC RID: 1980
			public int fwType;
		}

		// Token: 0x020000EA RID: 234
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DEVMODE
		{
			// Token: 0x040007BD RID: 1981
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmDeviceName;

			// Token: 0x040007BE RID: 1982
			public short dmSpecVersion;

			// Token: 0x040007BF RID: 1983
			public short dmDriverVersion;

			// Token: 0x040007C0 RID: 1984
			public short dmSize;

			// Token: 0x040007C1 RID: 1985
			public short dmDriverExtra;

			// Token: 0x040007C2 RID: 1986
			public int dmFields;

			// Token: 0x040007C3 RID: 1987
			public short dmOrientation;

			// Token: 0x040007C4 RID: 1988
			public short dmPaperSize;

			// Token: 0x040007C5 RID: 1989
			public short dmPaperLength;

			// Token: 0x040007C6 RID: 1990
			public short dmPaperWidth;

			// Token: 0x040007C7 RID: 1991
			public short dmScale;

			// Token: 0x040007C8 RID: 1992
			public short dmCopies;

			// Token: 0x040007C9 RID: 1993
			public short dmDefaultSource;

			// Token: 0x040007CA RID: 1994
			public short dmPrintQuality;

			// Token: 0x040007CB RID: 1995
			public short dmColor;

			// Token: 0x040007CC RID: 1996
			public short dmDuplex;

			// Token: 0x040007CD RID: 1997
			public short dmYResolution;

			// Token: 0x040007CE RID: 1998
			public short dmTTOption;

			// Token: 0x040007CF RID: 1999
			public short dmCollate;

			// Token: 0x040007D0 RID: 2000
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string dmFormName;

			// Token: 0x040007D1 RID: 2001
			public short dmLogPixels;

			// Token: 0x040007D2 RID: 2002
			public short dmBitsPerPel;

			// Token: 0x040007D3 RID: 2003
			public int dmPelsWidth;

			// Token: 0x040007D4 RID: 2004
			public int dmPelsHeight;

			// Token: 0x040007D5 RID: 2005
			public int dmDisplayFlags;

			// Token: 0x040007D6 RID: 2006
			public int dmDisplayFrequency;

			// Token: 0x040007D7 RID: 2007
			public int dmICMMethod;

			// Token: 0x040007D8 RID: 2008
			public int dmICMIntent;

			// Token: 0x040007D9 RID: 2009
			public int dmMediaType;

			// Token: 0x040007DA RID: 2010
			public int dmDitherType;

			// Token: 0x040007DB RID: 2011
			public int dmReserved1;

			// Token: 0x040007DC RID: 2012
			public int dmReserved2;

			// Token: 0x040007DD RID: 2013
			public int dmPanningWidth;

			// Token: 0x040007DE RID: 2014
			public int dmPanningHeight;
		}

		// Token: 0x020000EB RID: 235
		internal enum DCCapabilities : short
		{
			// Token: 0x040007E0 RID: 2016
			DC_FIELDS = 1,
			// Token: 0x040007E1 RID: 2017
			DC_PAPERS,
			// Token: 0x040007E2 RID: 2018
			DC_PAPERSIZE,
			// Token: 0x040007E3 RID: 2019
			DC_MINEXTENT,
			// Token: 0x040007E4 RID: 2020
			DC_MAXEXTENT,
			// Token: 0x040007E5 RID: 2021
			DC_BINS,
			// Token: 0x040007E6 RID: 2022
			DC_DUPLEX,
			// Token: 0x040007E7 RID: 2023
			DC_SIZE,
			// Token: 0x040007E8 RID: 2024
			DC_EXTRA,
			// Token: 0x040007E9 RID: 2025
			DC_VERSION,
			// Token: 0x040007EA RID: 2026
			DC_DRIVER,
			// Token: 0x040007EB RID: 2027
			DC_BINNAMES,
			// Token: 0x040007EC RID: 2028
			DC_ENUMRESOLUTIONS,
			// Token: 0x040007ED RID: 2029
			DC_FILEDEPENDENCIES,
			// Token: 0x040007EE RID: 2030
			DC_TRUETYPE,
			// Token: 0x040007EF RID: 2031
			DC_PAPERNAMES,
			// Token: 0x040007F0 RID: 2032
			DC_ORIENTATION,
			// Token: 0x040007F1 RID: 2033
			DC_COPIES,
			// Token: 0x040007F2 RID: 2034
			DC_BINADJUST,
			// Token: 0x040007F3 RID: 2035
			DC_EMF_COMPLIANT,
			// Token: 0x040007F4 RID: 2036
			DC_DATATYPE_PRODUCED,
			// Token: 0x040007F5 RID: 2037
			DC_COLLATE,
			// Token: 0x040007F6 RID: 2038
			DC_MANUFACTURER,
			// Token: 0x040007F7 RID: 2039
			DC_MODEL,
			// Token: 0x040007F8 RID: 2040
			DC_PERSONALITY,
			// Token: 0x040007F9 RID: 2041
			DC_PRINTRATE,
			// Token: 0x040007FA RID: 2042
			DC_PRINTRATEUNIT,
			// Token: 0x040007FB RID: 2043
			DC_PRINTERMEM,
			// Token: 0x040007FC RID: 2044
			DC_MEDIAREADY,
			// Token: 0x040007FD RID: 2045
			DC_STAPLE,
			// Token: 0x040007FE RID: 2046
			DC_PRINTRATEPPM,
			// Token: 0x040007FF RID: 2047
			DC_COLORDEVICE,
			// Token: 0x04000800 RID: 2048
			DC_NUP
		}

		// Token: 0x020000EC RID: 236
		[Flags]
		internal enum PrinterStatus : uint
		{
			// Token: 0x04000802 RID: 2050
			PS_PAUSED = 1U,
			// Token: 0x04000803 RID: 2051
			PS_ERROR = 2U,
			// Token: 0x04000804 RID: 2052
			PS_PENDING_DELETION = 4U,
			// Token: 0x04000805 RID: 2053
			PS_PAPER_JAM = 8U,
			// Token: 0x04000806 RID: 2054
			PS_PAPER_OUT = 16U,
			// Token: 0x04000807 RID: 2055
			PS_MANUAL_FEED = 32U,
			// Token: 0x04000808 RID: 2056
			PS_PAPER_PROBLEM = 64U,
			// Token: 0x04000809 RID: 2057
			PS_OFFLINE = 128U,
			// Token: 0x0400080A RID: 2058
			PS_IO_ACTIVE = 256U,
			// Token: 0x0400080B RID: 2059
			PS_BUSY = 512U,
			// Token: 0x0400080C RID: 2060
			PS_PRINTING = 1024U,
			// Token: 0x0400080D RID: 2061
			PS_OUTPUT_BIN_FULL = 2048U,
			// Token: 0x0400080E RID: 2062
			PS_NOT_AVAILABLE = 4096U,
			// Token: 0x0400080F RID: 2063
			PS_WAITING = 8192U,
			// Token: 0x04000810 RID: 2064
			PS_PROCESSING = 16384U,
			// Token: 0x04000811 RID: 2065
			PS_INITIALIZING = 32768U,
			// Token: 0x04000812 RID: 2066
			PS_WARMING_UP = 65536U,
			// Token: 0x04000813 RID: 2067
			PS_TONER_LOW = 131072U,
			// Token: 0x04000814 RID: 2068
			PS_NO_TONER = 262144U,
			// Token: 0x04000815 RID: 2069
			PS_PAGE_PUNT = 524288U,
			// Token: 0x04000816 RID: 2070
			PS_USER_INTERVENTION = 1048576U,
			// Token: 0x04000817 RID: 2071
			PS_OUT_OF_MEMORY = 2097152U,
			// Token: 0x04000818 RID: 2072
			PS_DOOR_OPEN = 4194304U,
			// Token: 0x04000819 RID: 2073
			PS_SERVER_UNKNOWN = 8388608U,
			// Token: 0x0400081A RID: 2074
			PS_POWER_SAVE = 16777216U
		}

		// Token: 0x020000ED RID: 237
		internal enum DevCapabilities
		{
			// Token: 0x0400081C RID: 2076
			TECHNOLOGY = 2
		}

		// Token: 0x020000EE RID: 238
		internal enum PrinterType
		{
			// Token: 0x0400081E RID: 2078
			DT_PLOTTER,
			// Token: 0x0400081F RID: 2079
			DT_RASDIPLAY,
			// Token: 0x04000820 RID: 2080
			DT_RASPRINTER,
			// Token: 0x04000821 RID: 2081
			DT_RASCAMERA,
			// Token: 0x04000822 RID: 2082
			DT_CHARSTREAM,
			// Token: 0x04000823 RID: 2083
			DT_METAFILE,
			// Token: 0x04000824 RID: 2084
			DT_DISPFILE
		}

		// Token: 0x020000EF RID: 239
		[Flags]
		internal enum EnumPrinters : uint
		{
			// Token: 0x04000826 RID: 2086
			PRINTER_ENUM_DEFAULT = 1U,
			// Token: 0x04000827 RID: 2087
			PRINTER_ENUM_LOCAL = 2U,
			// Token: 0x04000828 RID: 2088
			PRINTER_ENUM_CONNECTIONS = 4U,
			// Token: 0x04000829 RID: 2089
			PRINTER_ENUM_FAVORITE = 4U,
			// Token: 0x0400082A RID: 2090
			PRINTER_ENUM_NAME = 8U,
			// Token: 0x0400082B RID: 2091
			PRINTER_ENUM_REMOTE = 16U,
			// Token: 0x0400082C RID: 2092
			PRINTER_ENUM_SHARED = 32U,
			// Token: 0x0400082D RID: 2093
			PRINTER_ENUM_NETWORK = 64U
		}
	}
}
