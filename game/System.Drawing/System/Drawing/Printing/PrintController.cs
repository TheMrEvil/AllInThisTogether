using System;

namespace System.Drawing.Printing
{
	/// <summary>Controls how a document is printed, when printing from a Windows Forms application.</summary>
	// Token: 0x020000CF RID: 207
	public abstract class PrintController
	{
		/// <summary>Gets a value indicating whether the <see cref="T:System.Drawing.Printing.PrintController" /> is used for print preview.</summary>
		/// <returns>
		///   <see langword="false" /> in all cases.</returns>
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0000C228 File Offset: 0x0000A428
		public virtual bool IsPreview
		{
			get
			{
				return false;
			}
		}

		/// <summary>When overridden in a derived class, completes the control sequence that determines when and how to print a page of a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AFD RID: 2813 RVA: 0x000049FE File Offset: 0x00002BFE
		public virtual void OnEndPage(PrintDocument document, PrintPageEventArgs e)
		{
		}

		/// <summary>When overridden in a derived class, begins the control sequence that determines when and how to print a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AFE RID: 2814 RVA: 0x000049FE File Offset: 0x00002BFE
		public virtual void OnStartPrint(PrintDocument document, PrintEventArgs e)
		{
		}

		/// <summary>When overridden in a derived class, completes the control sequence that determines when and how to print a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintEventArgs" /> that contains the event data.</param>
		// Token: 0x06000AFF RID: 2815 RVA: 0x000049FE File Offset: 0x00002BFE
		public virtual void OnEndPrint(PrintDocument document, PrintEventArgs e)
		{
		}

		/// <summary>When overridden in a derived class, begins the control sequence that determines when and how to print a page of a document.</summary>
		/// <param name="document">A <see cref="T:System.Drawing.Printing.PrintDocument" /> that represents the document currently being printed.</param>
		/// <param name="e">A <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> that contains the event data.</param>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> that represents a page from a <see cref="T:System.Drawing.Printing.PrintDocument" />.</returns>
		// Token: 0x06000B00 RID: 2816 RVA: 0x00018785 File Offset: 0x00016985
		public virtual Graphics OnStartPage(PrintDocument document, PrintPageEventArgs e)
		{
			return null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintController" /> class.</summary>
		// Token: 0x06000B01 RID: 2817 RVA: 0x00002050 File Offset: 0x00000250
		protected PrintController()
		{
		}
	}
}
