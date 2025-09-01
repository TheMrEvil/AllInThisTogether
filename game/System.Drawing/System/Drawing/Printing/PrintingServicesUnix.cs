using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Drawing.Printing
{
	// Token: 0x020000DD RID: 221
	internal class PrintingServicesUnix : PrintingServices
	{
		// Token: 0x06000BA6 RID: 2982 RVA: 0x00019BE0 File Offset: 0x00017DE0
		internal PrintingServicesUnix()
		{
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00019BE8 File Offset: 0x00017DE8
		static PrintingServicesUnix()
		{
			PrintingServicesUnix.installed_printers = new Hashtable();
			PrintingServicesUnix.CheckCupsInstalled();
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00019C10 File Offset: 0x00017E10
		internal static PrinterSettings.StringCollection InstalledPrinters
		{
			get
			{
				PrintingServicesUnix.LoadPrinters();
				PrinterSettings.StringCollection stringCollection = new PrinterSettings.StringCollection(new string[0]);
				foreach (object obj in PrintingServicesUnix.installed_printers.Keys)
				{
					stringCollection.Add(obj.ToString());
				}
				return stringCollection;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x00019C80 File Offset: 0x00017E80
		internal override string DefaultPrinter
		{
			get
			{
				if (PrintingServicesUnix.installed_printers.Count == 0)
				{
					PrintingServicesUnix.LoadPrinters();
				}
				return PrintingServicesUnix.default_printer;
			}
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00019C98 File Offset: 0x00017E98
		private static void CheckCupsInstalled()
		{
			try
			{
				PrintingServicesUnix.cupsGetDefault();
			}
			catch (DllNotFoundException)
			{
				Console.WriteLine("libcups not found. To have printing support, you need cups installed");
				PrintingServicesUnix.cups_installed = false;
				return;
			}
			PrintingServicesUnix.cups_installed = true;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00019CD8 File Offset: 0x00017ED8
		private IntPtr OpenPrinter(string printer)
		{
			try
			{
				return PrintingServicesUnix.ppdOpenFile(Marshal.PtrToStringAnsi(PrintingServicesUnix.cupsGetPPD(printer)));
			}
			catch (Exception)
			{
				Console.WriteLine("There was an error opening the printer {0}. Please check your cups installation.");
			}
			return IntPtr.Zero;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00019D1C File Offset: 0x00017F1C
		private void ClosePrinter(ref IntPtr handle)
		{
			try
			{
				if (handle != IntPtr.Zero)
				{
					PrintingServicesUnix.ppdClose(handle);
				}
			}
			finally
			{
				handle = IntPtr.Zero;
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00019D58 File Offset: 0x00017F58
		private static int OpenDests(ref IntPtr ptr)
		{
			try
			{
				return PrintingServicesUnix.cupsGetDests(ref ptr);
			}
			catch
			{
				ptr = IntPtr.Zero;
			}
			return 0;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00019D8C File Offset: 0x00017F8C
		private static void CloseDests(ref IntPtr ptr, int count)
		{
			try
			{
				if (ptr != IntPtr.Zero)
				{
					PrintingServicesUnix.cupsFreeDests(count, ptr);
				}
			}
			finally
			{
				ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x00019DCC File Offset: 0x00017FCC
		internal override bool IsPrinterValid(string printer)
		{
			return PrintingServicesUnix.cups_installed && !(printer == null | printer == string.Empty) && PrintingServicesUnix.installed_printers.Contains(printer);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00019DF4 File Offset: 0x00017FF4
		internal override void LoadPrinterSettings(string printer, PrinterSettings settings)
		{
			if (!PrintingServicesUnix.cups_installed || printer == null || printer == string.Empty)
			{
				return;
			}
			if (PrintingServicesUnix.installed_printers.Count == 0)
			{
				PrintingServicesUnix.LoadPrinters();
			}
			if (((SysPrn.Printer)PrintingServicesUnix.installed_printers[printer]).Settings != null)
			{
				SysPrn.Printer printer2 = (SysPrn.Printer)PrintingServicesUnix.installed_printers[printer];
				settings.can_duplex = printer2.Settings.can_duplex;
				settings.is_plotter = printer2.Settings.is_plotter;
				settings.landscape_angle = printer2.Settings.landscape_angle;
				settings.maximum_copies = printer2.Settings.maximum_copies;
				settings.paper_sizes = printer2.Settings.paper_sizes;
				settings.paper_sources = printer2.Settings.paper_sources;
				settings.printer_capabilities = printer2.Settings.printer_capabilities;
				settings.printer_resolutions = printer2.Settings.printer_resolutions;
				settings.supports_color = printer2.Settings.supports_color;
				return;
			}
			settings.PrinterCapabilities.Clear();
			IntPtr zero = IntPtr.Zero;
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			string text = string.Empty;
			int num = 0;
			try
			{
				num = PrintingServicesUnix.OpenDests(ref zero);
				if (num != 0)
				{
					int num2 = Marshal.SizeOf(typeof(PrintingServicesUnix.CUPS_DESTS));
					intPtr = zero;
					for (int i = 0; i < num; i++)
					{
						if (Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(intPtr)).Equals(printer))
						{
							text = printer;
							break;
						}
						intPtr = (IntPtr)((long)intPtr + (long)num2);
					}
					if (text.Equals(printer))
					{
						intPtr2 = this.OpenPrinter(printer);
						if (!(intPtr2 == IntPtr.Zero))
						{
							PrintingServicesUnix.CUPS_DESTS cups_DESTS = (PrintingServicesUnix.CUPS_DESTS)Marshal.PtrToStructure(intPtr, typeof(PrintingServicesUnix.CUPS_DESTS));
							NameValueCollection nameValueCollection = new NameValueCollection();
							NameValueCollection paper_names = new NameValueCollection();
							NameValueCollection paper_sources = new NameValueCollection();
							string def_size;
							string def_source;
							PrintingServicesUnix.LoadPrinterOptions(cups_DESTS.options, cups_DESTS.num_options, intPtr2, nameValueCollection, paper_names, out def_size, paper_sources, out def_source);
							if (settings.paper_sizes == null)
							{
								settings.paper_sizes = new PrinterSettings.PaperSizeCollection(new PaperSize[0]);
							}
							else
							{
								settings.paper_sizes.Clear();
							}
							if (settings.paper_sources == null)
							{
								settings.paper_sources = new PrinterSettings.PaperSourceCollection(new PaperSource[0]);
							}
							else
							{
								settings.paper_sources.Clear();
							}
							settings.DefaultPageSettings.PaperSource = this.LoadPrinterPaperSources(settings, def_source, paper_sources);
							settings.DefaultPageSettings.PaperSize = this.LoadPrinterPaperSizes(intPtr2, settings, def_size, paper_names);
							this.LoadPrinterResolutionsAndDefault(printer, settings, intPtr2);
							PrintingServicesUnix.PPD_FILE ppd_FILE = (PrintingServicesUnix.PPD_FILE)Marshal.PtrToStructure(intPtr2, typeof(PrintingServicesUnix.PPD_FILE));
							settings.landscape_angle = ppd_FILE.landscape;
							settings.supports_color = (ppd_FILE.color_device != 0);
							settings.can_duplex = (nameValueCollection["Duplex"] != null);
							this.ClosePrinter(ref intPtr2);
							((SysPrn.Printer)PrintingServicesUnix.installed_printers[printer]).Settings = settings;
						}
					}
				}
			}
			finally
			{
				PrintingServicesUnix.CloseDests(ref zero, num);
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0001A104 File Offset: 0x00018304
		private static void LoadPrinterOptions(IntPtr options, int numOptions, IntPtr ppd, NameValueCollection list, NameValueCollection paper_names, out string defsize, NameValueCollection paper_sources, out string defsource)
		{
			int num = Marshal.SizeOf(typeof(PrintingServicesUnix.CUPS_OPTIONS));
			PrintingServicesUnix.LoadOptionList(ppd, "PageSize", paper_names, out defsize);
			PrintingServicesUnix.LoadOptionList(ppd, "InputSlot", paper_sources, out defsource);
			for (int i = 0; i < numOptions; i++)
			{
				PrintingServicesUnix.CUPS_OPTIONS cups_OPTIONS = (PrintingServicesUnix.CUPS_OPTIONS)Marshal.PtrToStructure(options, typeof(PrintingServicesUnix.CUPS_OPTIONS));
				string text = Marshal.PtrToStringAnsi(cups_OPTIONS.name);
				string text2 = Marshal.PtrToStringAnsi(cups_OPTIONS.val);
				if (text == "PageSize")
				{
					defsize = text2;
				}
				else if (text == "InputSlot")
				{
					defsource = text2;
				}
				list.Add(text, text2);
				options = (IntPtr)((long)options + (long)num);
			}
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001A1B4 File Offset: 0x000183B4
		private static NameValueCollection LoadPrinterOptions(IntPtr options, int numOptions)
		{
			int num = Marshal.SizeOf(typeof(PrintingServicesUnix.CUPS_OPTIONS));
			NameValueCollection nameValueCollection = new NameValueCollection();
			for (int i = 0; i < numOptions; i++)
			{
				PrintingServicesUnix.CUPS_OPTIONS cups_OPTIONS = (PrintingServicesUnix.CUPS_OPTIONS)Marshal.PtrToStructure(options, typeof(PrintingServicesUnix.CUPS_OPTIONS));
				string name = Marshal.PtrToStringAnsi(cups_OPTIONS.name);
				string value = Marshal.PtrToStringAnsi(cups_OPTIONS.val);
				nameValueCollection.Add(name, value);
				options = (IntPtr)((long)options + (long)num);
			}
			return nameValueCollection;
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001A22C File Offset: 0x0001842C
		private static void LoadOptionList(IntPtr ppd, string option_name, NameValueCollection list, out string defoption)
		{
			IntPtr intPtr = IntPtr.Zero;
			int num = Marshal.SizeOf(typeof(PrintingServicesUnix.PPD_CHOICE));
			defoption = null;
			intPtr = PrintingServicesUnix.ppdFindOption(ppd, option_name);
			if (intPtr != IntPtr.Zero)
			{
				PrintingServicesUnix.PPD_OPTION ppd_OPTION = (PrintingServicesUnix.PPD_OPTION)Marshal.PtrToStructure(intPtr, typeof(PrintingServicesUnix.PPD_OPTION));
				defoption = ppd_OPTION.defchoice;
				intPtr = ppd_OPTION.choices;
				for (int i = 0; i < ppd_OPTION.num_choices; i++)
				{
					PrintingServicesUnix.PPD_CHOICE ppd_CHOICE = (PrintingServicesUnix.PPD_CHOICE)Marshal.PtrToStructure(intPtr, typeof(PrintingServicesUnix.PPD_CHOICE));
					list.Add(ppd_CHOICE.choice, ppd_CHOICE.text);
					intPtr = (IntPtr)((long)intPtr + (long)num);
				}
			}
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0001A2D8 File Offset: 0x000184D8
		internal override void LoadPrinterResolutions(string printer, PrinterSettings settings)
		{
			IntPtr intPtr = this.OpenPrinter(printer);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			this.LoadPrinterResolutionsAndDefault(printer, settings, intPtr);
			this.ClosePrinter(ref intPtr);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001A30C File Offset: 0x0001850C
		private PrinterResolution ParseResolution(string resolution)
		{
			if (string.IsNullOrEmpty(resolution))
			{
				return null;
			}
			int num = resolution.IndexOf("dpi");
			if (num == -1)
			{
				return null;
			}
			resolution = resolution.Substring(0, num);
			int num2;
			int y;
			try
			{
				if (resolution.Contains("x"))
				{
					string[] array = resolution.Split(new char[]
					{
						'x'
					});
					num2 = Convert.ToInt32(array[0]);
					y = Convert.ToInt32(array[1]);
				}
				else
				{
					num2 = Convert.ToInt32(resolution);
					y = num2;
				}
			}
			catch (Exception)
			{
				return null;
			}
			return new PrinterResolution(PrinterResolutionKind.Custom, num2, y);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001A39C File Offset: 0x0001859C
		private PaperSize LoadPrinterPaperSizes(IntPtr ppd_handle, PrinterSettings settings, string def_size, NameValueCollection paper_names)
		{
			PaperSize result = new PaperSize(this.GetPaperKind(827, 1169), "A4", 827, 1169);
			PrintingServicesUnix.PPD_FILE ppd_FILE = (PrintingServicesUnix.PPD_FILE)Marshal.PtrToStructure(ppd_handle, typeof(PrintingServicesUnix.PPD_FILE));
			IntPtr intPtr = ppd_FILE.sizes;
			for (int i = 0; i < ppd_FILE.num_sizes; i++)
			{
				PrintingServicesUnix.PPD_SIZE ppd_SIZE = (PrintingServicesUnix.PPD_SIZE)Marshal.PtrToStructure(intPtr, typeof(PrintingServicesUnix.PPD_SIZE));
				string name = paper_names[ppd_SIZE.name];
				float num = ppd_SIZE.width * 100f / 72f;
				float num2 = ppd_SIZE.length * 100f / 72f;
				PaperKind paperKind = this.GetPaperKind((int)num, (int)num2);
				PaperSize paperSize = new PaperSize(paperKind, name, (int)num, (int)num2);
				paperSize.RawKind = (int)paperKind;
				if (def_size == paperSize.Kind.ToString())
				{
					result = paperSize;
				}
				settings.paper_sizes.Add(paperSize);
				intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf<PrintingServicesUnix.PPD_SIZE>(ppd_SIZE));
			}
			return result;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0001A4C0 File Offset: 0x000186C0
		private PaperSource LoadPrinterPaperSources(PrinterSettings settings, string def_source, NameValueCollection paper_sources)
		{
			PaperSource paperSource = null;
			foreach (object obj in paper_sources)
			{
				string text = (string)obj;
				PaperSourceKind kind;
				if (!(text == "Auto"))
				{
					if (!(text == "Standard"))
					{
						if (!(text == "Tray"))
						{
							if (!(text == "Envelope"))
							{
								if (!(text == "Manual"))
								{
									kind = PaperSourceKind.Custom;
								}
								else
								{
									kind = PaperSourceKind.Manual;
								}
							}
							else
							{
								kind = PaperSourceKind.Envelope;
							}
						}
						else
						{
							kind = PaperSourceKind.AutomaticFeed;
						}
					}
					else
					{
						kind = PaperSourceKind.AutomaticFeed;
					}
				}
				else
				{
					kind = PaperSourceKind.AutomaticFeed;
				}
				settings.paper_sources.Add(new PaperSource(kind, paper_sources[text]));
				if (def_source == text)
				{
					paperSource = settings.paper_sources[settings.paper_sources.Count - 1];
				}
			}
			if (paperSource == null && settings.paper_sources.Count > 0)
			{
				return settings.paper_sources[0];
			}
			return paperSource;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0001A5D0 File Offset: 0x000187D0
		private void LoadPrinterResolutionsAndDefault(string printer, PrinterSettings settings, IntPtr ppd_handle)
		{
			if (settings.printer_resolutions == null)
			{
				settings.printer_resolutions = new PrinterSettings.PrinterResolutionCollection(new PrinterResolution[0]);
			}
			else
			{
				settings.printer_resolutions.Clear();
			}
			NameValueCollection nameValueCollection = new NameValueCollection();
			string resolution;
			PrintingServicesUnix.LoadOptionList(ppd_handle, "Resolution", nameValueCollection, out resolution);
			foreach (object obj in nameValueCollection.Keys)
			{
				PrinterResolution printerResolution = this.ParseResolution(obj.ToString());
				settings.PrinterResolutions.Add(printerResolution);
			}
			PrinterResolution printerResolution2 = this.ParseResolution(resolution);
			if (printerResolution2 == null)
			{
				printerResolution2 = this.ParseResolution("300dpi");
			}
			if (nameValueCollection.Count == 0)
			{
				settings.PrinterResolutions.Add(printerResolution2);
			}
			settings.DefaultPageSettings.PrinterResolution = printerResolution2;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001A6B0 File Offset: 0x000188B0
		private static void LoadPrinters()
		{
			PrintingServicesUnix.installed_printers.Clear();
			if (!PrintingServicesUnix.cups_installed)
			{
				return;
			}
			IntPtr zero = IntPtr.Zero;
			int num = 0;
			int num2 = Marshal.SizeOf(typeof(PrintingServicesUnix.CUPS_DESTS));
			string comment;
			string text;
			string type = text = (comment = string.Empty);
			int num3 = 0;
			try
			{
				num = PrintingServicesUnix.OpenDests(ref zero);
				IntPtr intPtr = zero;
				for (int i = 0; i < num; i++)
				{
					PrintingServicesUnix.CUPS_DESTS cups_DESTS = (PrintingServicesUnix.CUPS_DESTS)Marshal.PtrToStructure(intPtr, typeof(PrintingServicesUnix.CUPS_DESTS));
					string text2 = Marshal.PtrToStringAnsi(cups_DESTS.name);
					if (cups_DESTS.is_default == 1)
					{
						PrintingServicesUnix.default_printer = text2;
					}
					if (text.Equals(string.Empty))
					{
						text = text2;
					}
					NameValueCollection nameValueCollection = PrintingServicesUnix.LoadPrinterOptions(cups_DESTS.options, cups_DESTS.num_options);
					if (nameValueCollection["printer-state"] != null)
					{
						num3 = int.Parse(nameValueCollection["printer-state"]);
					}
					if (nameValueCollection["printer-comment"] != null)
					{
						comment = nameValueCollection["printer-state"];
					}
					string status;
					if (num3 != 4)
					{
						if (num3 != 5)
						{
							status = "Ready";
						}
						else
						{
							status = "Stopped";
						}
					}
					else
					{
						status = "Printing";
					}
					PrintingServicesUnix.installed_printers.Add(text2, new SysPrn.Printer(string.Empty, type, status, comment));
					intPtr = (IntPtr)((long)intPtr + (long)num2);
				}
			}
			finally
			{
				PrintingServicesUnix.CloseDests(ref zero, num);
			}
			if (PrintingServicesUnix.default_printer.Equals(string.Empty))
			{
				PrintingServicesUnix.default_printer = text;
			}
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0001A840 File Offset: 0x00018A40
		internal override void GetPrintDialogInfo(string printer, ref string port, ref string type, ref string status, ref string comment)
		{
			int num = 0;
			int num2 = -1;
			bool flag = false;
			IntPtr zero = IntPtr.Zero;
			int num3 = Marshal.SizeOf(typeof(PrintingServicesUnix.CUPS_DESTS));
			if (!PrintingServicesUnix.cups_installed)
			{
				return;
			}
			try
			{
				num = PrintingServicesUnix.OpenDests(ref zero);
				if (num != 0)
				{
					IntPtr intPtr = zero;
					for (int i = 0; i < num; i++)
					{
						if (Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(intPtr)).Equals(printer))
						{
							flag = true;
							break;
						}
						intPtr = (IntPtr)((long)intPtr + (long)num3);
					}
					if (flag)
					{
						PrintingServicesUnix.CUPS_DESTS cups_DESTS = (PrintingServicesUnix.CUPS_DESTS)Marshal.PtrToStructure(intPtr, typeof(PrintingServicesUnix.CUPS_DESTS));
						NameValueCollection nameValueCollection = PrintingServicesUnix.LoadPrinterOptions(cups_DESTS.options, cups_DESTS.num_options);
						if (nameValueCollection["printer-state"] != null)
						{
							num2 = int.Parse(nameValueCollection["printer-state"]);
						}
						if (nameValueCollection["printer-comment"] != null)
						{
							comment = nameValueCollection["printer-state"];
						}
						if (num2 != 4)
						{
							if (num2 != 5)
							{
								status = "Ready";
							}
							else
							{
								status = "Stopped";
							}
						}
						else
						{
							status = "Printing";
						}
					}
				}
			}
			finally
			{
				PrintingServicesUnix.CloseDests(ref zero, num);
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0001A974 File Offset: 0x00018B74
		private PaperKind GetPaperKind(int width, int height)
		{
			if (width == 827 && height == 1169)
			{
				return PaperKind.A4;
			}
			if (width == 583 && height == 827)
			{
				return PaperKind.A5;
			}
			if (width == 717 && height == 1012)
			{
				return PaperKind.B5;
			}
			if (width == 693 && height == 984)
			{
				return PaperKind.B5Envelope;
			}
			if (width == 638 && height == 902)
			{
				return PaperKind.C5Envelope;
			}
			if (width == 449 && height == 638)
			{
				return PaperKind.C6Envelope;
			}
			if (width == 1700 && height == 2200)
			{
				return PaperKind.CSheet;
			}
			if (width == 433 && height == 866)
			{
				return PaperKind.DLEnvelope;
			}
			if (width == 2200 && height == 3400)
			{
				return PaperKind.DSheet;
			}
			if (width == 3400 && height == 4400)
			{
				return PaperKind.ESheet;
			}
			if (width == 725 && height == 1050)
			{
				return PaperKind.Executive;
			}
			if (width == 850 && height == 1300)
			{
				return PaperKind.Folio;
			}
			if (width == 850 && height == 1200)
			{
				return PaperKind.GermanStandardFanfold;
			}
			if (width == 1700 && height == 1100)
			{
				return PaperKind.Ledger;
			}
			if (width == 850 && height == 1400)
			{
				return PaperKind.Legal;
			}
			if (width == 927 && height == 1500)
			{
				return PaperKind.LegalExtra;
			}
			if (width == 850 && height == 1100)
			{
				return PaperKind.Letter;
			}
			if (width == 927 && height == 1200)
			{
				return PaperKind.LetterExtra;
			}
			if (width == 850 && height == 1269)
			{
				return PaperKind.LetterPlus;
			}
			if (width == 387 && height == 750)
			{
				return PaperKind.MonarchEnvelope;
			}
			if (width == 387 && height == 887)
			{
				return PaperKind.Number9Envelope;
			}
			if (width == 413 && height == 950)
			{
				return PaperKind.Number10Envelope;
			}
			if (width == 450 && height == 1037)
			{
				return PaperKind.Number11Envelope;
			}
			if (width == 475 && height == 1100)
			{
				return PaperKind.Number12Envelope;
			}
			if (width == 500 && height == 1150)
			{
				return PaperKind.Number14Envelope;
			}
			if (width == 363 && height == 650)
			{
				return PaperKind.PersonalEnvelope;
			}
			if (width == 1000 && height == 1100)
			{
				return PaperKind.Standard10x11;
			}
			if (width == 1000 && height == 1400)
			{
				return PaperKind.Standard10x14;
			}
			if (width == 1100 && height == 1700)
			{
				return PaperKind.Standard11x17;
			}
			if (width == 1200 && height == 1100)
			{
				return PaperKind.Standard12x11;
			}
			if (width == 1500 && height == 1100)
			{
				return PaperKind.Standard15x11;
			}
			if (width == 900 && height == 1100)
			{
				return PaperKind.Standard9x11;
			}
			if (width == 550 && height == 850)
			{
				return PaperKind.Statement;
			}
			if (width == 1100 && height == 1700)
			{
				return PaperKind.Tabloid;
			}
			if (width == 1487 && height == 1100)
			{
				return PaperKind.USStandardFanfold;
			}
			return PaperKind.Custom;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0001AC18 File Offset: 0x00018E18
		internal static int GetCupsOptions(PrinterSettings printer_settings, PageSettings page_settings, out IntPtr options)
		{
			options = IntPtr.Zero;
			PaperSize paperSize = page_settings.PaperSize;
			int num = paperSize.Width * 72 / 100;
			int num2 = paperSize.Height * 72 / 100;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(string.Concat(new string[]
			{
				"copies=",
				printer_settings.Copies.ToString(),
				" Collate=",
				printer_settings.Collate.ToString(),
				" ColorModel=",
				page_settings.Color ? "Color" : "Black",
				" PageSize=",
				string.Format("Custom.{0}x{1}", num, num2),
				" landscape=",
				page_settings.Landscape.ToString()
			}));
			if (printer_settings.CanDuplex)
			{
				if (printer_settings.Duplex == Duplex.Simplex)
				{
					stringBuilder.Append(" Duplex=None");
				}
				else
				{
					stringBuilder.Append(" Duplex=DuplexNoTumble");
				}
			}
			return PrintingServicesUnix.cupsParseOptions(stringBuilder.ToString(), 0, ref options);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0001AD2C File Offset: 0x00018F2C
		internal static bool StartDoc(GraphicsPrinter gr, string doc_name, string output_file)
		{
			((PrintingServicesUnix.DOCINFO)PrintingServicesUnix.doc_info[gr.Hdc]).title = doc_name;
			return true;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0001AD60 File Offset: 0x00018F60
		internal static bool EndDoc(GraphicsPrinter gr)
		{
			PrintingServicesUnix.DOCINFO docinfo = (PrintingServicesUnix.DOCINFO)PrintingServicesUnix.doc_info[gr.Hdc];
			gr.Graphics.Dispose();
			IntPtr options;
			int cupsOptions = PrintingServicesUnix.GetCupsOptions(docinfo.settings, docinfo.default_page_settings, out options);
			PrintingServicesUnix.cupsPrintFile(docinfo.settings.PrinterName, docinfo.filename, docinfo.title, cupsOptions, options);
			PrintingServicesUnix.cupsFreeOptions(cupsOptions, options);
			PrintingServicesUnix.doc_info.Remove(gr.Hdc);
			if (PrintingServicesUnix.tmpfile != null)
			{
				try
				{
					File.Delete(PrintingServicesUnix.tmpfile);
				}
				catch
				{
				}
			}
			return true;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00003610 File Offset: 0x00001810
		internal static bool StartPage(GraphicsPrinter gr)
		{
			return true;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0001AE0C File Offset: 0x0001900C
		internal static bool EndPage(GraphicsPrinter gr)
		{
			PrintingServicesUnix.GdipGetPostScriptSavePage(gr.Hdc);
			return true;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001AE1C File Offset: 0x0001901C
		internal static IntPtr CreateGraphicsContext(PrinterSettings settings, PageSettings default_page_settings)
		{
			IntPtr zero = IntPtr.Zero;
			string filename;
			if (!settings.PrintToFile)
			{
				StringBuilder stringBuilder = new StringBuilder(1024);
				int capacity = stringBuilder.Capacity;
				PrintingServicesUnix.cupsTempFd(stringBuilder, capacity);
				filename = stringBuilder.ToString();
				PrintingServicesUnix.tmpfile = filename;
			}
			else
			{
				filename = settings.PrintFileName;
			}
			PaperSize paperSize = default_page_settings.PaperSize;
			int num;
			int num2;
			if (default_page_settings.Landscape)
			{
				num = paperSize.Height;
				num2 = paperSize.Width;
			}
			else
			{
				num = paperSize.Width;
				num2 = paperSize.Height;
			}
			PrintingServicesUnix.GdipGetPostScriptGraphicsContext(filename, num * 72 / 100, num2 * 72 / 100, (double)default_page_settings.PrinterResolution.X, (double)default_page_settings.PrinterResolution.Y, ref zero);
			PrintingServicesUnix.DOCINFO docinfo = default(PrintingServicesUnix.DOCINFO);
			docinfo.filename = filename;
			docinfo.settings = settings;
			docinfo.default_page_settings = default_page_settings;
			PrintingServicesUnix.doc_info.Add(zero, docinfo);
			return zero;
		}

		// Token: 0x06000BC2 RID: 3010
		[DllImport("libcups", CharSet = CharSet.Ansi)]
		private static extern int cupsGetDests(ref IntPtr dests);

		// Token: 0x06000BC3 RID: 3011
		[DllImport("libcups")]
		private static extern void cupsFreeDests(int num_dests, IntPtr dests);

		// Token: 0x06000BC4 RID: 3012
		[DllImport("libcups", CharSet = CharSet.Ansi)]
		private static extern IntPtr cupsTempFd(StringBuilder sb, int len);

		// Token: 0x06000BC5 RID: 3013
		[DllImport("libcups", CharSet = CharSet.Ansi)]
		private static extern IntPtr cupsGetDefault();

		// Token: 0x06000BC6 RID: 3014
		[DllImport("libcups", CharSet = CharSet.Ansi)]
		private static extern int cupsPrintFile(string printer, string filename, string title, int num_options, IntPtr options);

		// Token: 0x06000BC7 RID: 3015
		[DllImport("libcups", CharSet = CharSet.Ansi)]
		private static extern IntPtr cupsGetPPD(string printer);

		// Token: 0x06000BC8 RID: 3016
		[DllImport("libcups", CharSet = CharSet.Ansi)]
		private static extern IntPtr ppdOpenFile(string filename);

		// Token: 0x06000BC9 RID: 3017
		[DllImport("libcups", CharSet = CharSet.Ansi)]
		private static extern IntPtr ppdFindOption(IntPtr ppd_file, string keyword);

		// Token: 0x06000BCA RID: 3018
		[DllImport("libcups")]
		private static extern void ppdClose(IntPtr ppd);

		// Token: 0x06000BCB RID: 3019
		[DllImport("libcups", CharSet = CharSet.Ansi)]
		private static extern int cupsParseOptions(string arg, int number_of_options, ref IntPtr options);

		// Token: 0x06000BCC RID: 3020
		[DllImport("libcups")]
		private static extern void cupsFreeOptions(int number_options, IntPtr options);

		// Token: 0x06000BCD RID: 3021
		[DllImport("gdiplus.dll", CharSet = CharSet.Ansi)]
		private static extern int GdipGetPostScriptGraphicsContext(string filename, int with, int height, double dpix, double dpiy, ref IntPtr graphics);

		// Token: 0x06000BCE RID: 3022
		[DllImport("gdiplus.dll")]
		private static extern int GdipGetPostScriptSavePage(IntPtr graphics);

		// Token: 0x0400075A RID: 1882
		private static Hashtable doc_info = new Hashtable();

		// Token: 0x0400075B RID: 1883
		private static bool cups_installed;

		// Token: 0x0400075C RID: 1884
		private static Hashtable installed_printers;

		// Token: 0x0400075D RID: 1885
		private static string default_printer = string.Empty;

		// Token: 0x0400075E RID: 1886
		private static string tmpfile;

		// Token: 0x020000DE RID: 222
		public struct DOCINFO
		{
			// Token: 0x0400075F RID: 1887
			public PrinterSettings settings;

			// Token: 0x04000760 RID: 1888
			public PageSettings default_page_settings;

			// Token: 0x04000761 RID: 1889
			public string title;

			// Token: 0x04000762 RID: 1890
			public string filename;
		}

		// Token: 0x020000DF RID: 223
		public struct PPD_SIZE
		{
			// Token: 0x04000763 RID: 1891
			public int marked;

			// Token: 0x04000764 RID: 1892
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 42)]
			public string name;

			// Token: 0x04000765 RID: 1893
			public float width;

			// Token: 0x04000766 RID: 1894
			public float length;

			// Token: 0x04000767 RID: 1895
			public float left;

			// Token: 0x04000768 RID: 1896
			public float bottom;

			// Token: 0x04000769 RID: 1897
			public float right;

			// Token: 0x0400076A RID: 1898
			public float top;
		}

		// Token: 0x020000E0 RID: 224
		public struct PPD_GROUP
		{
			// Token: 0x0400076B RID: 1899
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 40)]
			public string text;

			// Token: 0x0400076C RID: 1900
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 42)]
			public string name;

			// Token: 0x0400076D RID: 1901
			public int num_options;

			// Token: 0x0400076E RID: 1902
			public IntPtr options;

			// Token: 0x0400076F RID: 1903
			public int num_subgroups;

			// Token: 0x04000770 RID: 1904
			public IntPtr subgrups;
		}

		// Token: 0x020000E1 RID: 225
		public struct PPD_OPTION
		{
			// Token: 0x04000771 RID: 1905
			public byte conflicted;

			// Token: 0x04000772 RID: 1906
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
			public string keyword;

			// Token: 0x04000773 RID: 1907
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
			public string defchoice;

			// Token: 0x04000774 RID: 1908
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
			public string text;

			// Token: 0x04000775 RID: 1909
			public int ui;

			// Token: 0x04000776 RID: 1910
			public int section;

			// Token: 0x04000777 RID: 1911
			public float order;

			// Token: 0x04000778 RID: 1912
			public int num_choices;

			// Token: 0x04000779 RID: 1913
			public IntPtr choices;
		}

		// Token: 0x020000E2 RID: 226
		public struct PPD_CHOICE
		{
			// Token: 0x0400077A RID: 1914
			public byte marked;

			// Token: 0x0400077B RID: 1915
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 41)]
			public string choice;

			// Token: 0x0400077C RID: 1916
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 81)]
			public string text;

			// Token: 0x0400077D RID: 1917
			public IntPtr code;

			// Token: 0x0400077E RID: 1918
			public IntPtr option;
		}

		// Token: 0x020000E3 RID: 227
		public struct PPD_FILE
		{
			// Token: 0x0400077F RID: 1919
			public int language_level;

			// Token: 0x04000780 RID: 1920
			public int color_device;

			// Token: 0x04000781 RID: 1921
			public int variable_sizes;

			// Token: 0x04000782 RID: 1922
			public int accurate_screens;

			// Token: 0x04000783 RID: 1923
			public int contone_only;

			// Token: 0x04000784 RID: 1924
			public int landscape;

			// Token: 0x04000785 RID: 1925
			public int model_number;

			// Token: 0x04000786 RID: 1926
			public int manual_copies;

			// Token: 0x04000787 RID: 1927
			public int throughput;

			// Token: 0x04000788 RID: 1928
			public int colorspace;

			// Token: 0x04000789 RID: 1929
			public IntPtr patches;

			// Token: 0x0400078A RID: 1930
			public int num_emulations;

			// Token: 0x0400078B RID: 1931
			public IntPtr emulations;

			// Token: 0x0400078C RID: 1932
			public IntPtr jcl_begin;

			// Token: 0x0400078D RID: 1933
			public IntPtr jcl_ps;

			// Token: 0x0400078E RID: 1934
			public IntPtr jcl_end;

			// Token: 0x0400078F RID: 1935
			public IntPtr lang_encoding;

			// Token: 0x04000790 RID: 1936
			public IntPtr lang_version;

			// Token: 0x04000791 RID: 1937
			public IntPtr modelname;

			// Token: 0x04000792 RID: 1938
			public IntPtr ttrasterizer;

			// Token: 0x04000793 RID: 1939
			public IntPtr manufacturer;

			// Token: 0x04000794 RID: 1940
			public IntPtr product;

			// Token: 0x04000795 RID: 1941
			public IntPtr nickname;

			// Token: 0x04000796 RID: 1942
			public IntPtr shortnickname;

			// Token: 0x04000797 RID: 1943
			public int num_groups;

			// Token: 0x04000798 RID: 1944
			public IntPtr groups;

			// Token: 0x04000799 RID: 1945
			public int num_sizes;

			// Token: 0x0400079A RID: 1946
			public IntPtr sizes;
		}

		// Token: 0x020000E4 RID: 228
		public struct CUPS_OPTIONS
		{
			// Token: 0x0400079B RID: 1947
			public IntPtr name;

			// Token: 0x0400079C RID: 1948
			public IntPtr val;
		}

		// Token: 0x020000E5 RID: 229
		public struct CUPS_DESTS
		{
			// Token: 0x0400079D RID: 1949
			public IntPtr name;

			// Token: 0x0400079E RID: 1950
			public IntPtr instance;

			// Token: 0x0400079F RID: 1951
			public int is_default;

			// Token: 0x040007A0 RID: 1952
			public int num_options;

			// Token: 0x040007A1 RID: 1953
			public IntPtr options;
		}
	}
}
