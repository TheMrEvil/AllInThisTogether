using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies settings that apply to a single, printed page.</summary>
	// Token: 0x020000CD RID: 205
	[Serializable]
	public class PageSettings : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PageSettings" /> class using the default printer.</summary>
		// Token: 0x06000ADA RID: 2778 RVA: 0x00018AB0 File Offset: 0x00016CB0
		public PageSettings() : this(new PrinterSettings())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PageSettings" /> class using a specified printer.</summary>
		/// <param name="printerSettings">The <see cref="T:System.Drawing.Printing.PrinterSettings" /> that describes the printer to use.</param>
		// Token: 0x06000ADB RID: 2779 RVA: 0x00018AC0 File Offset: 0x00016CC0
		public PageSettings(PrinterSettings printerSettings)
		{
			this.margins = new Margins();
			base..ctor();
			this.PrinterSettings = printerSettings;
			this.color = printerSettings.DefaultPageSettings.color;
			this.landscape = printerSettings.DefaultPageSettings.landscape;
			this.paperSize = printerSettings.DefaultPageSettings.paperSize;
			this.paperSource = printerSettings.DefaultPageSettings.paperSource;
			this.printerResolution = printerSettings.DefaultPageSettings.printerResolution;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00018B3A File Offset: 0x00016D3A
		internal PageSettings(PrinterSettings printerSettings, bool color, bool landscape, PaperSize paperSize, PaperSource paperSource, PrinterResolution printerResolution)
		{
			this.margins = new Margins();
			base..ctor();
			this.PrinterSettings = printerSettings;
			this.color = color;
			this.landscape = landscape;
			this.paperSize = paperSize;
			this.paperSource = paperSource;
			this.printerResolution = printerResolution;
		}

		/// <summary>Gets the size of the page, taking into account the page orientation specified by the <see cref="P:System.Drawing.Printing.PageSettings.Landscape" /> property.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the length and width, in hundredths of an inch, of the page.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00018B7C File Offset: 0x00016D7C
		public Rectangle Bounds
		{
			get
			{
				int num = this.paperSize.Width;
				int num2 = this.paperSize.Height;
				num -= this.margins.Left + this.margins.Right;
				num2 -= this.margins.Top + this.margins.Bottom;
				if (this.landscape)
				{
					int num3 = num;
					num = num2;
					num2 = num3;
				}
				return new Rectangle(this.margins.Left, this.margins.Top, num, num2);
			}
		}

		/// <summary>Gets or sets a value indicating whether the page should be printed in color.</summary>
		/// <returns>
		///   <see langword="true" /> if the page should be printed in color; otherwise, <see langword="false" />. The default is determined by the printer.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00018BFE File Offset: 0x00016DFE
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00018C1F File Offset: 0x00016E1F
		public bool Color
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the page is printed in landscape or portrait orientation.</summary>
		/// <returns>
		///   <see langword="true" /> if the page should be printed in landscape orientation; otherwise, <see langword="false" />. The default is determined by the printer.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00018C28 File Offset: 0x00016E28
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x00018C49 File Offset: 0x00016E49
		public bool Landscape
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.landscape;
			}
			set
			{
				this.landscape = value;
			}
		}

		/// <summary>Gets or sets the margins for this page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.Margins" /> that represents the margins, in hundredths of an inch, for the page. The default is 1-inch margins on all sides.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist.</exception>
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00018C52 File Offset: 0x00016E52
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x00018C73 File Offset: 0x00016E73
		public Margins Margins
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.margins;
			}
			set
			{
				this.margins = value;
			}
		}

		/// <summary>Gets or sets the paper size for the page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PaperSize" /> that represents the size of the paper. The default is the printer's default paper size.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00018C7C File Offset: 0x00016E7C
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x00018C9D File Offset: 0x00016E9D
		public PaperSize PaperSize
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.paperSize;
			}
			set
			{
				if (value != null)
				{
					this.paperSize = value;
				}
			}
		}

		/// <summary>Gets or sets the page's paper source; for example, the printer's upper tray.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PaperSource" /> that specifies the source of the paper. The default is the printer's default paper source.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00018CA9 File Offset: 0x00016EA9
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x00018CCA File Offset: 0x00016ECA
		public PaperSource PaperSource
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.paperSource;
			}
			set
			{
				if (value != null)
				{
					this.paperSource = value;
				}
			}
		}

		/// <summary>Gets or sets the printer resolution for the page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterResolution" /> that specifies the printer resolution for the page. The default is the printer's default resolution.</returns>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00018CD6 File Offset: 0x00016ED6
		// (set) Token: 0x06000AE9 RID: 2793 RVA: 0x00018CF7 File Offset: 0x00016EF7
		public PrinterResolution PrinterResolution
		{
			get
			{
				if (!this.printerSettings.IsValid)
				{
					throw new InvalidPrinterException(this.printerSettings);
				}
				return this.printerResolution;
			}
			set
			{
				if (value != null)
				{
					this.printerResolution = value;
				}
			}
		}

		/// <summary>Gets or sets the printer settings associated with the page.</summary>
		/// <returns>A <see cref="T:System.Drawing.Printing.PrinterSettings" /> that represents the printer settings associated with the page.</returns>
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00018D03 File Offset: 0x00016F03
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00018D0B File Offset: 0x00016F0B
		public PrinterSettings PrinterSettings
		{
			get
			{
				return this.printerSettings;
			}
			set
			{
				this.printerSettings = value;
			}
		}

		/// <summary>Gets the x-coordinate, in hundredths of an inch, of the hard margin at the left of the page.</summary>
		/// <returns>The x-coordinate, in hundredths of an inch, of the left-hand hard margin.</returns>
		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00018D14 File Offset: 0x00016F14
		public float HardMarginX
		{
			get
			{
				return this.hardMarginX;
			}
		}

		/// <summary>Gets the y-coordinate, in hundredths of an inch, of the hard margin at the top of the page.</summary>
		/// <returns>The y-coordinate, in hundredths of an inch, of the hard margin at the top of the page.</returns>
		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00018D1C File Offset: 0x00016F1C
		public float HardMarginY
		{
			get
			{
				return this.hardMarginY;
			}
		}

		/// <summary>Gets the bounds of the printable area of the page for the printer.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> representing the length and width, in hundredths of an inch, of the area the printer is capable of printing in.</returns>
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00018D24 File Offset: 0x00016F24
		public RectangleF PrintableArea
		{
			get
			{
				return this.printableArea;
			}
		}

		/// <summary>Creates a copy of this <see cref="T:System.Drawing.Printing.PageSettings" />.</summary>
		/// <returns>A copy of this object.</returns>
		// Token: 0x06000AEF RID: 2799 RVA: 0x00018D2C File Offset: 0x00016F2C
		public object Clone()
		{
			PrinterResolution printerResolution = new PrinterResolution(this.printerResolution.Kind, this.printerResolution.X, this.printerResolution.Y);
			PaperSource paperSource = new PaperSource(this.paperSource.Kind, this.paperSource.SourceName);
			PaperSize paperSize = new PaperSize(this.paperSize.PaperName, this.paperSize.Width, this.paperSize.Height);
			paperSize.RawKind = (int)this.paperSize.Kind;
			return new PageSettings(this.printerSettings, this.color, this.landscape, paperSize, paperSource, printerResolution)
			{
				Margins = (Margins)this.margins.Clone()
			};
		}

		/// <summary>Copies the relevant information from the <see cref="T:System.Drawing.Printing.PageSettings" /> to the specified <see langword="DEVMODE" /> structure.</summary>
		/// <param name="hdevmode">The handle to a Win32 <see langword="DEVMODE" /> structure.</param>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
		// Token: 0x06000AF0 RID: 2800 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("PageSettings.CopyToHdevmode")]
		public void CopyToHdevmode(IntPtr hdevmode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Copies relevant information to the <see cref="T:System.Drawing.Printing.PageSettings" /> from the specified <see langword="DEVMODE" /> structure.</summary>
		/// <param name="hdevmode">The handle to a Win32 <see langword="DEVMODE" /> structure.</param>
		/// <exception cref="T:System.ArgumentException">The printer handle is not valid.</exception>
		/// <exception cref="T:System.Drawing.Printing.InvalidPrinterException">The printer named in the <see cref="P:System.Drawing.Printing.PrinterSettings.PrinterName" /> property does not exist or there is no default printer installed.</exception>
		// Token: 0x06000AF1 RID: 2801 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("PageSettings.SetHdevmode")]
		public void SetHdevmode(IntPtr hdevmode)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the <see cref="T:System.Drawing.Printing.PageSettings" /> to string form.</summary>
		/// <returns>A string showing the various property settings for the <see cref="T:System.Drawing.Printing.PageSettings" />.</returns>
		// Token: 0x06000AF2 RID: 2802 RVA: 0x00018DE4 File Offset: 0x00016FE4
		public override string ToString()
		{
			return string.Format("[PageSettings: Color={0}" + ", Landscape={1}" + ", Margins={2}" + ", PaperSize={3}" + ", PaperSource={4}" + ", PrinterResolution={5}" + "]", new object[]
			{
				this.color,
				this.landscape,
				this.margins,
				this.paperSize,
				this.paperSource,
				this.printerResolution
			});
		}

		// Token: 0x04000719 RID: 1817
		internal bool color;

		// Token: 0x0400071A RID: 1818
		internal bool landscape;

		// Token: 0x0400071B RID: 1819
		internal PaperSize paperSize;

		// Token: 0x0400071C RID: 1820
		internal PaperSource paperSource;

		// Token: 0x0400071D RID: 1821
		internal PrinterResolution printerResolution;

		// Token: 0x0400071E RID: 1822
		private Margins margins;

		// Token: 0x0400071F RID: 1823
		private float hardMarginX;

		// Token: 0x04000720 RID: 1824
		private float hardMarginY;

		// Token: 0x04000721 RID: 1825
		private RectangleF printableArea;

		// Token: 0x04000722 RID: 1826
		private PrinterSettings printerSettings;
	}
}
