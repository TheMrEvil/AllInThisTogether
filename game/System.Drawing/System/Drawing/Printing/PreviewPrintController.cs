using System;
using System.Collections;

namespace System.Drawing.Printing
{
	/// <summary>Specifies a print controller that displays a document on a screen as a series of images.</summary>
	// Token: 0x020000CE RID: 206
	public class PreviewPrintController : PrintController
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PreviewPrintController" /> class.</summary>
		// Token: 0x06000AF3 RID: 2803 RVA: 0x00018E7D File Offset: 0x0001707D
		public PreviewPrintController()
		{
			this.pageInfoList = new ArrayList();
		}

		/// <summary>Gets a value indicating whether this controller is used for print preview.</summary>
		/// <returns>
		///   <see langword="true" /> in all cases.</returns>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00003610 File Offset: 0x00001810
		public override bool IsPreview
		{
			get
			{
				return true;
			}
		}

		/// <summary>Completes the control sequence that determines when and how to preview a page in a print document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains data about how to preview a page in the print document.</param>
		// Token: 0x06000AF5 RID: 2805 RVA: 0x000049FE File Offset: 0x00002BFE
		[MonoTODO]
		public override void OnEndPage(PrintDocument document, PrintPageEventArgs e)
		{
		}

		/// <summary>Begins the control sequence that determines when and how to preview a print document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains data about how to print the document.</param>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		// Token: 0x06000AF6 RID: 2806 RVA: 0x00018E90 File Offset: 0x00017090
		[MonoTODO]
		public override void OnStartPrint(PrintDocument document, PrintEventArgs e)
		{
			if (!document.PrinterSettings.IsValid)
			{
				throw new InvalidPrinterException(document.PrinterSettings);
			}
			foreach (object obj in this.pageInfoList)
			{
				((PreviewPageInfo)obj).Image.Dispose();
			}
			this.pageInfoList.Clear();
		}

		/// <summary>Completes the control sequence that determines when and how to preview a print document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains data about how to preview the print document.</param>
		// Token: 0x06000AF7 RID: 2807 RVA: 0x000049FE File Offset: 0x00002BFE
		[MonoTODO]
		public override void OnEndPrint(PrintDocument document, PrintEventArgs e)
		{
		}

		/// <summary>Begins the control sequence that determines when and how to preview a page in a print document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document being previewed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains data about how to preview a page in the print document. Initially, the <see cref="P:System.Drawing.Printing.PrintPageEventArgs.Graphics" /> property of this parameter will be <see langword="null" />. The value returned from this method will be used to set this property.</param>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that represents a page from a <see cref="T:System.Drawing.Printing.PrintDocument" />.</returns>
		// Token: 0x06000AF8 RID: 2808 RVA: 0x00018F10 File Offset: 0x00017110
		[MonoTODO]
		public override Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
		{
			Image image = new Bitmap(e.PageSettings.PaperSize.Width, e.PageSettings.PaperSize.Height);
			PreviewPageInfo previewPageInfo = new PreviewPageInfo(image, new Size(e.PageSettings.PaperSize.Width, e.PageSettings.PaperSize.Height));
			this.pageInfoList.Add(previewPageInfo);
			Graphics graphics = Graphics.FromImage(previewPageInfo.Image);
			graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(new Point(0, 0), new Size(image.Width, image.Height)));
			return graphics;
		}

		/// <summary>Gets or sets a value indicating whether to use anti-aliasing when displaying the print preview.</summary>
		/// <returns>
		///   <see langword="true" /> if the print preview uses anti-aliasing; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00018FB4 File Offset: 0x000171B4
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x00018FBC File Offset: 0x000171BC
		public virtual bool UseAntiAlias
		{
			get
			{
				return this.useantialias;
			}
			set
			{
				this.useantialias = value;
			}
		}

		/// <summary>Captures the pages of a document as a series of images.</summary>
		/// <returns>An array of type <see cref="T:System.Drawing.Printing.PreviewPageInfo" /> that contains the pages of a <see cref="T:System.Drawing.Printing.PrintDocument" /> as a series of images.</returns>
		// Token: 0x06000AFB RID: 2811 RVA: 0x00018FC8 File Offset: 0x000171C8
		public PreviewPageInfo[] GetPreviewPageInfo()
		{
			PreviewPageInfo[] array = new PreviewPageInfo[this.pageInfoList.Count];
			this.pageInfoList.CopyTo(array);
			return array;
		}

		// Token: 0x04000723 RID: 1827
		private bool useantialias;

		// Token: 0x04000724 RID: 1828
		private ArrayList pageInfoList;
	}
}
