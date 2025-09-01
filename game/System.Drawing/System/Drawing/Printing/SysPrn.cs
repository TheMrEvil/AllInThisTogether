using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000DA RID: 218
	internal class SysPrn
	{
		// Token: 0x06000B9C RID: 2972 RVA: 0x00019B2F File Offset: 0x00017D2F
		static SysPrn()
		{
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00019B3B File Offset: 0x00017D3B
		internal static PrintingServices CreatePrintingService()
		{
			if (SysPrn.is_unix)
			{
				return new PrintingServicesUnix();
			}
			return new PrintingServicesWin32();
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00019B4F File Offset: 0x00017D4F
		internal static GlobalPrintingServices GlobalService
		{
			get
			{
				if (SysPrn.global_printing_services == null)
				{
					if (SysPrn.is_unix)
					{
						SysPrn.global_printing_services = new GlobalPrintingServicesUnix();
					}
					else
					{
						SysPrn.global_printing_services = new GlobalPrintingServicesWin32();
					}
				}
				return SysPrn.global_printing_services;
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00019B7A File Offset: 0x00017D7A
		internal static void GetPrintDialogInfo(string printer, ref string port, ref string type, ref string status, ref string comment)
		{
			SysPrn.CreatePrintingService().GetPrintDialogInfo(printer, ref port, ref type, ref status, ref comment);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00002050 File Offset: 0x00000250
		public SysPrn()
		{
		}

		// Token: 0x04000751 RID: 1873
		private static GlobalPrintingServices global_printing_services;

		// Token: 0x04000752 RID: 1874
		private static bool is_unix = GDIPlus.RunningOnUnix();

		// Token: 0x020000DB RID: 219
		internal class Printer
		{
			// Token: 0x06000BA1 RID: 2977 RVA: 0x00019B8C File Offset: 0x00017D8C
			public Printer(string port, string type, string status, string comment)
			{
				this.Port = port;
				this.Type = type;
				this.Status = status;
				this.Comment = comment;
			}

			// Token: 0x04000753 RID: 1875
			public readonly string Comment;

			// Token: 0x04000754 RID: 1876
			public readonly string Port;

			// Token: 0x04000755 RID: 1877
			public readonly string Type;

			// Token: 0x04000756 RID: 1878
			public readonly string Status;

			// Token: 0x04000757 RID: 1879
			public PrinterSettings Settings;
		}
	}
}
