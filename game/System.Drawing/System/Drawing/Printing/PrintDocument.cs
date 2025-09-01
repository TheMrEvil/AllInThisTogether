using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Drawing.Printing
{
	/// <summary>Defines a reusable object that sends output to a printer, when printing from a Windows Forms application.</summary>
	// Token: 0x020000D0 RID: 208
	[DefaultEvent("PrintPage")]
	[DefaultProperty("DocumentName")]
	[ToolboxItemFilter("System.Drawing.Printing", ToolboxItemFilterType.Allow)]
	public class PrintDocument : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintDocument" /> class.</summary>
		// Token: 0x06000B02 RID: 2818 RVA: 0x00018FF4 File Offset: 0x000171F4
		public PrintDocument()
		{
			this.documentname = "document";
			this.printersettings = new PrinterSettings();
			this.defaultpagesettings = (PageSettings)this.printersettings.DefaultPageSettings.Clone();
			this.printcontroller = new StandardPrintController();
		}

		/// <summary>Gets or sets page settings that are used as defaults for all pages to be printed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PageSettings" /> that specifies the default page settings for the document.</returns>
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x00019043 File Offset: 0x00017243
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x0001904B File Offset: 0x0001724B
		[SRDescription("The settings for the current page.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public PageSettings DefaultPageSettings
		{
			get
			{
				return this.defaultpagesettings;
			}
			set
			{
				this.defaultpagesettings = value;
			}
		}

		/// <summary>Gets or sets the document name to display (for example, in a print status dialog box or printer queue) while printing the document.</summary>
		/// <returns>The document name to display while printing the document. The default is "document".</returns>
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x00019054 File Offset: 0x00017254
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x0001905C File Offset: 0x0001725C
		[DefaultValue("document")]
		[SRDescription("The name of the document.")]
		public string DocumentName
		{
			get
			{
				return this.documentname;
			}
			set
			{
				this.documentname = value;
			}
		}

		/// <summary>Gets or sets the print controller that guides the printing process.</summary>
		/// <returns>The <see cref="T:System.Drawing.Printing.PrintController" /> that guides the printing process. The default is a new instance of the <see cref="T:System.Windows.Forms.PrintControllerWithStatusDialog" /> class.</returns>
		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00019065 File Offset: 0x00017265
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x0001906D File Offset: 0x0001726D
		[Browsable(false)]
		[SRDescription("The print controller object.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PrintController PrintController
		{
			get
			{
				return this.printcontroller;
			}
			set
			{
				this.printcontroller = value;
			}
		}

		/// <summary>Gets or sets the printer that prints the document.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings" /> that specifies where and how the document is printed. The default is a <see cref="T:System.Drawing.Printing.PrinterSettings" /> with its properties set to their default values.</returns>
		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00019076 File Offset: 0x00017276
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x0001907E File Offset: 0x0001727E
		[SRDescription("The current settings for the active printer.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public PrinterSettings PrinterSettings
		{
			get
			{
				return this.printersettings;
			}
			set
			{
				this.printersettings = ((value == null) ? new PrinterSettings() : value);
			}
		}

		/// <summary>Gets or sets a value indicating whether the position of a graphics object associated with a page is located just inside the user-specified margins or at the top-left corner of the printable area of the page.</summary>
		/// <returns>
		///   <see langword="true" /> if the graphics origin starts at the page margins; <see langword="false" /> if the graphics origin is at the top-left corner of the printable page. The default is <see langword="false" />.</returns>
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x00019091 File Offset: 0x00017291
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x00019099 File Offset: 0x00017299
		[SRDescription("Determines if the origin is set at the specified margins.")]
		[DefaultValue(false)]
		public bool OriginAtMargins
		{
			get
			{
				return this.originAtMargins;
			}
			set
			{
				this.originAtMargins = value;
			}
		}

		/// <summary>Starts the document's printing process.</summary>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		// Token: 0x06000B0D RID: 2829 RVA: 0x000190A4 File Offset: 0x000172A4
		public void Print()
		{
			PrintEventArgs printEventArgs = new PrintEventArgs();
			this.OnBeginPrint(printEventArgs);
			if (printEventArgs.Cancel)
			{
				return;
			}
			this.PrintController.OnStartPrint(this, printEventArgs);
			if (printEventArgs.Cancel)
			{
				return;
			}
			Graphics graphics = null;
			if (printEventArgs.GraphicsContext != null)
			{
				graphics = Graphics.FromHdc(printEventArgs.GraphicsContext.Hdc);
				printEventArgs.GraphicsContext.Graphics = graphics;
			}
			PrintPageEventArgs printPageEventArgs;
			do
			{
				QueryPageSettingsEventArgs queryPageSettingsEventArgs = new QueryPageSettingsEventArgs(this.DefaultPageSettings.Clone() as PageSettings);
				this.OnQueryPageSettings(queryPageSettingsEventArgs);
				PageSettings pageSettings = queryPageSettingsEventArgs.PageSettings;
				printPageEventArgs = new PrintPageEventArgs(graphics, pageSettings.Bounds, new Rectangle(0, 0, pageSettings.PaperSize.Width, pageSettings.PaperSize.Height), pageSettings);
				printPageEventArgs.GraphicsContext = printEventArgs.GraphicsContext;
				Graphics graphics2 = this.PrintController.OnStartPage(this, printPageEventArgs);
				printPageEventArgs.SetGraphics(graphics2);
				if (!printPageEventArgs.Cancel)
				{
					this.OnPrintPage(printPageEventArgs);
				}
				this.PrintController.OnEndPage(this, printPageEventArgs);
			}
			while (!printPageEventArgs.Cancel && printPageEventArgs.HasMorePages);
			this.OnEndPrint(printEventArgs);
			this.PrintController.OnEndPrint(this, printEventArgs);
		}

		/// <summary>Provides information about the print document, in string form.</summary>
		/// <returns>A string.</returns>
		// Token: 0x06000B0E RID: 2830 RVA: 0x000191BE File Offset: 0x000173BE
		public override string ToString()
		{
			return "[PrintDocument " + this.DocumentName + "]";
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.BeginPrint" /> event. It is called after the <see cref="M:System.Drawing.Printing.PrintDocument.Print" /> method is called and before the first page of the document prints.</summary>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
		// Token: 0x06000B0F RID: 2831 RVA: 0x000191D5 File Offset: 0x000173D5
		protected virtual void OnBeginPrint(PrintEventArgs e)
		{
			if (this.BeginPrint != null)
			{
				this.BeginPrint(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.EndPrint" /> event. It is called when the last page of the document has printed.</summary>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
		// Token: 0x06000B10 RID: 2832 RVA: 0x000191EC File Offset: 0x000173EC
		protected virtual void OnEndPrint(PrintEventArgs e)
		{
			if (this.EndPrint != null)
			{
				this.EndPrint(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event. It is called before a page prints.</summary>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
		// Token: 0x06000B11 RID: 2833 RVA: 0x00019203 File Offset: 0x00017403
		protected virtual void OnPrintPage(PrintPageEventArgs e)
		{
			if (this.PrintPage != null)
			{
				this.PrintPage(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Drawing.Printing.PrintDocument.QueryPageSettings" /> event. It is called immediately before each <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.QueryPageSettingsEventArgs" /> that contains the event data.</param>
		// Token: 0x06000B12 RID: 2834 RVA: 0x0001921A File Offset: 0x0001741A
		protected virtual void OnQueryPageSettings(QueryPageSettingsEventArgs e)
		{
			if (this.QueryPageSettings != null)
			{
				this.QueryPageSettings(this, e);
			}
		}

		/// <summary>Occurs when the <see cref="M:System.Drawing.Printing.PrintDocument.Print" /> method is called and before the first page of the document prints.</summary>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000B13 RID: 2835 RVA: 0x00019234 File Offset: 0x00017434
		// (remove) Token: 0x06000B14 RID: 2836 RVA: 0x0001926C File Offset: 0x0001746C
		[SRDescription("Raised when printing begins")]
		public event PrintEventHandler BeginPrint
		{
			[CompilerGenerated]
			add
			{
				PrintEventHandler printEventHandler = this.BeginPrint;
				PrintEventHandler printEventHandler2;
				do
				{
					printEventHandler2 = printEventHandler;
					PrintEventHandler value2 = (PrintEventHandler)Delegate.Combine(printEventHandler2, value);
					printEventHandler = Interlocked.CompareExchange<PrintEventHandler>(ref this.BeginPrint, value2, printEventHandler2);
				}
				while (printEventHandler != printEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PrintEventHandler printEventHandler = this.BeginPrint;
				PrintEventHandler printEventHandler2;
				do
				{
					printEventHandler2 = printEventHandler;
					PrintEventHandler value2 = (PrintEventHandler)Delegate.Remove(printEventHandler2, value);
					printEventHandler = Interlocked.CompareExchange<PrintEventHandler>(ref this.BeginPrint, value2, printEventHandler2);
				}
				while (printEventHandler != printEventHandler2);
			}
		}

		/// <summary>Occurs when the last page of the document has printed.</summary>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000B15 RID: 2837 RVA: 0x000192A4 File Offset: 0x000174A4
		// (remove) Token: 0x06000B16 RID: 2838 RVA: 0x000192DC File Offset: 0x000174DC
		[SRDescription("Raised when printing ends")]
		public event PrintEventHandler EndPrint
		{
			[CompilerGenerated]
			add
			{
				PrintEventHandler printEventHandler = this.EndPrint;
				PrintEventHandler printEventHandler2;
				do
				{
					printEventHandler2 = printEventHandler;
					PrintEventHandler value2 = (PrintEventHandler)Delegate.Combine(printEventHandler2, value);
					printEventHandler = Interlocked.CompareExchange<PrintEventHandler>(ref this.EndPrint, value2, printEventHandler2);
				}
				while (printEventHandler != printEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PrintEventHandler printEventHandler = this.EndPrint;
				PrintEventHandler printEventHandler2;
				do
				{
					printEventHandler2 = printEventHandler;
					PrintEventHandler value2 = (PrintEventHandler)Delegate.Remove(printEventHandler2, value);
					printEventHandler = Interlocked.CompareExchange<PrintEventHandler>(ref this.EndPrint, value2, printEventHandler2);
				}
				while (printEventHandler != printEventHandler2);
			}
		}

		/// <summary>Occurs when the output to print for the current page is needed.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000B17 RID: 2839 RVA: 0x00019314 File Offset: 0x00017514
		// (remove) Token: 0x06000B18 RID: 2840 RVA: 0x0001934C File Offset: 0x0001754C
		[SRDescription("Raised when printing of a new page begins")]
		public event PrintPageEventHandler PrintPage
		{
			[CompilerGenerated]
			add
			{
				PrintPageEventHandler printPageEventHandler = this.PrintPage;
				PrintPageEventHandler printPageEventHandler2;
				do
				{
					printPageEventHandler2 = printPageEventHandler;
					PrintPageEventHandler value2 = (PrintPageEventHandler)Delegate.Combine(printPageEventHandler2, value);
					printPageEventHandler = Interlocked.CompareExchange<PrintPageEventHandler>(ref this.PrintPage, value2, printPageEventHandler2);
				}
				while (printPageEventHandler != printPageEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				PrintPageEventHandler printPageEventHandler = this.PrintPage;
				PrintPageEventHandler printPageEventHandler2;
				do
				{
					printPageEventHandler2 = printPageEventHandler;
					PrintPageEventHandler value2 = (PrintPageEventHandler)Delegate.Remove(printPageEventHandler2, value);
					printPageEventHandler = Interlocked.CompareExchange<PrintPageEventHandler>(ref this.PrintPage, value2, printPageEventHandler2);
				}
				while (printPageEventHandler != printPageEventHandler2);
			}
		}

		/// <summary>Occurs immediately before each <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000B19 RID: 2841 RVA: 0x00019384 File Offset: 0x00017584
		// (remove) Token: 0x06000B1A RID: 2842 RVA: 0x000193BC File Offset: 0x000175BC
		[SRDescription("Raised before printing of a new page begins")]
		public event QueryPageSettingsEventHandler QueryPageSettings
		{
			[CompilerGenerated]
			add
			{
				QueryPageSettingsEventHandler queryPageSettingsEventHandler = this.QueryPageSettings;
				QueryPageSettingsEventHandler queryPageSettingsEventHandler2;
				do
				{
					queryPageSettingsEventHandler2 = queryPageSettingsEventHandler;
					QueryPageSettingsEventHandler value2 = (QueryPageSettingsEventHandler)Delegate.Combine(queryPageSettingsEventHandler2, value);
					queryPageSettingsEventHandler = Interlocked.CompareExchange<QueryPageSettingsEventHandler>(ref this.QueryPageSettings, value2, queryPageSettingsEventHandler2);
				}
				while (queryPageSettingsEventHandler != queryPageSettingsEventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				QueryPageSettingsEventHandler queryPageSettingsEventHandler = this.QueryPageSettings;
				QueryPageSettingsEventHandler queryPageSettingsEventHandler2;
				do
				{
					queryPageSettingsEventHandler2 = queryPageSettingsEventHandler;
					QueryPageSettingsEventHandler value2 = (QueryPageSettingsEventHandler)Delegate.Remove(queryPageSettingsEventHandler2, value);
					queryPageSettingsEventHandler = Interlocked.CompareExchange<QueryPageSettingsEventHandler>(ref this.QueryPageSettings, value2, queryPageSettingsEventHandler2);
				}
				while (queryPageSettingsEventHandler != queryPageSettingsEventHandler2);
			}
		}

		// Token: 0x04000725 RID: 1829
		private PageSettings defaultpagesettings;

		// Token: 0x04000726 RID: 1830
		private PrinterSettings printersettings;

		// Token: 0x04000727 RID: 1831
		private PrintController printcontroller;

		// Token: 0x04000728 RID: 1832
		private string documentname;

		// Token: 0x04000729 RID: 1833
		private bool originAtMargins;

		// Token: 0x0400072A RID: 1834
		[CompilerGenerated]
		private PrintEventHandler BeginPrint;

		// Token: 0x0400072B RID: 1835
		[CompilerGenerated]
		private PrintEventHandler EndPrint;

		// Token: 0x0400072C RID: 1836
		[CompilerGenerated]
		private PrintPageEventHandler PrintPage;

		// Token: 0x0400072D RID: 1837
		[CompilerGenerated]
		private QueryPageSettingsEventHandler QueryPageSettings;
	}
}
