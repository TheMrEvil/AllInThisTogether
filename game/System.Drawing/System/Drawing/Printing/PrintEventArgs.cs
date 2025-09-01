using System;
using System.ComponentModel;

namespace System.Drawing.Printing
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Printing.PrintDocument.BeginPrint" /> and <see cref="E:System.Drawing.Printing.PrintDocument.EndPrint" /> events.</summary>
	// Token: 0x020000D1 RID: 209
	public class PrintEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintEventArgs" /> class.</summary>
		// Token: 0x06000B1B RID: 2843 RVA: 0x000193F1 File Offset: 0x000175F1
		public PrintEventArgs()
		{
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000193F9 File Offset: 0x000175F9
		internal PrintEventArgs(PrintAction action)
		{
			this.action = action;
		}

		/// <summary>Returns <see cref="F:System.Drawing.Printing.PrintAction.PrintToFile" /> in all cases.</summary>
		/// <returns>
		///   <see cref="F:System.Drawing.Printing.PrintAction.PrintToFile" /> in all cases.</returns>
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00019408 File Offset: 0x00017608
		public PrintAction PrintAction
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x00019410 File Offset: 0x00017610
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x00019418 File Offset: 0x00017618
		internal GraphicsPrinter GraphicsContext
		{
			get
			{
				return this.graphics_context;
			}
			set
			{
				this.graphics_context = value;
			}
		}

		// Token: 0x0400072E RID: 1838
		private GraphicsPrinter graphics_context;

		// Token: 0x0400072F RID: 1839
		private PrintAction action;
	}
}
