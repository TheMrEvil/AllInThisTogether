using System;

namespace System.Drawing.Printing
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Printing.PrintDocument.PrintPage" /> event.</summary>
	// Token: 0x020000D2 RID: 210
	public class PrintPageEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrintPageEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the item.</param>
		/// <param name="marginBounds">The area between the margins.</param>
		/// <param name="pageBounds">The total area of the paper.</param>
		/// <param name="pageSettings">The <see cref="T:System.Drawing.Printing.PageSettings" /> for the page.</param>
		// Token: 0x06000B20 RID: 2848 RVA: 0x00019421 File Offset: 0x00017621
		public PrintPageEventArgs(Graphics graphics, Rectangle marginBounds, Rectangle pageBounds, PageSettings pageSettings)
		{
			this.graphics = graphics;
			this.marginBounds = marginBounds;
			this.pageBounds = pageBounds;
			this.pageSettings = pageSettings;
		}

		/// <summary>Gets or sets a value indicating whether the print job should be canceled.</summary>
		/// <returns>
		///   <see langword="true" /> if the print job should be canceled; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00019446 File Offset: 0x00017646
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x0001944E File Offset: 0x0001764E
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint the page.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the page.</returns>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00019457 File Offset: 0x00017657
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets or sets a value indicating whether an additional page should be printed.</summary>
		/// <returns>
		///   <see langword="true" /> if an additional page should be printed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0001945F File Offset: 0x0001765F
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x00019467 File Offset: 0x00017667
		public bool HasMorePages
		{
			get
			{
				return this.hasmorePages;
			}
			set
			{
				this.hasmorePages = value;
			}
		}

		/// <summary>Gets the rectangular area that represents the portion of the page inside the margins.</summary>
		/// <returns>The rectangular area, measured in hundredths of an inch, that represents the portion of the page inside the margins.</returns>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x00019470 File Offset: 0x00017670
		public Rectangle MarginBounds
		{
			get
			{
				return this.marginBounds;
			}
		}

		/// <summary>Gets the rectangular area that represents the total area of the page.</summary>
		/// <returns>The rectangular area that represents the total area of the page.</returns>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x00019478 File Offset: 0x00017678
		public Rectangle PageBounds
		{
			get
			{
				return this.pageBounds;
			}
		}

		/// <summary>Gets the page settings for the current page.</summary>
		/// <returns>The page settings for the current page.</returns>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x00019480 File Offset: 0x00017680
		public PageSettings PageSettings
		{
			get
			{
				return this.pageSettings;
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00019488 File Offset: 0x00017688
		internal void SetGraphics(Graphics g)
		{
			this.graphics = g;
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x00019491 File Offset: 0x00017691
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x00019499 File Offset: 0x00017699
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

		// Token: 0x04000730 RID: 1840
		private bool cancel;

		// Token: 0x04000731 RID: 1841
		private Graphics graphics;

		// Token: 0x04000732 RID: 1842
		private bool hasmorePages;

		// Token: 0x04000733 RID: 1843
		private Rectangle marginBounds;

		// Token: 0x04000734 RID: 1844
		private Rectangle pageBounds;

		// Token: 0x04000735 RID: 1845
		private PageSettings pageSettings;

		// Token: 0x04000736 RID: 1846
		private GraphicsPrinter graphics_context;
	}
}
