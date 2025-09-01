using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Drawing.Printing
{
	/// <summary>Represents the resolution supported by a printer.</summary>
	// Token: 0x020000C3 RID: 195
	[Serializable]
	public class PrinterResolution
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PrinterResolution" /> class.</summary>
		// Token: 0x06000AAC RID: 2732 RVA: 0x000184B5 File Offset: 0x000166B5
		public PrinterResolution()
		{
			this._kind = PrinterResolutionKind.Custom;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000184C4 File Offset: 0x000166C4
		internal PrinterResolution(PrinterResolutionKind kind, int x, int y)
		{
			this._kind = kind;
			this._x = x;
			this._y = y;
		}

		/// <summary>Gets or sets the printer resolution.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Printing.PrinterResolutionKind" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not a member of the <see cref="T:System.Drawing.Printing.PrinterResolutionKind" /> enumeration.</exception>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x000184E1 File Offset: 0x000166E1
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x000184E9 File Offset: 0x000166E9
		public PrinterResolutionKind Kind
		{
			get
			{
				return this._kind;
			}
			set
			{
				if (value < PrinterResolutionKind.High || value > PrinterResolutionKind.Custom)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PrinterResolutionKind));
				}
				this._kind = value;
			}
		}

		/// <summary>Gets the horizontal printer resolution, in dots per inch.</summary>
		/// <returns>The horizontal printer resolution, in dots per inch, if <see cref="P:System.Drawing.Printing.PrinterResolution.Kind" /> is set to <see cref="F:System.Drawing.Printing.PrinterResolutionKind.Custom" />; otherwise, a <see langword="dmPrintQuality" /> value.</returns>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00018511 File Offset: 0x00016711
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x00018519 File Offset: 0x00016719
		public int X
		{
			get
			{
				return this._x;
			}
			set
			{
				this._x = value;
			}
		}

		/// <summary>Gets the vertical printer resolution, in dots per inch.</summary>
		/// <returns>The vertical printer resolution, in dots per inch.</returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00018522 File Offset: 0x00016722
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x0001852A File Offset: 0x0001672A
		public int Y
		{
			get
			{
				return this._y;
			}
			set
			{
				this._y = value;
			}
		}

		/// <summary>This member overrides the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains information about the <see cref="T:System.Drawing.Printing.PrinterResolution" />.</returns>
		// Token: 0x06000AB4 RID: 2740 RVA: 0x00018534 File Offset: 0x00016734
		public override string ToString()
		{
			if (this._kind != PrinterResolutionKind.Custom)
			{
				return "[PrinterResolution " + this.Kind.ToString() + "]";
			}
			return string.Concat(new string[]
			{
				"[PrinterResolution X=",
				this.X.ToString(CultureInfo.InvariantCulture),
				" Y=",
				this.Y.ToString(CultureInfo.InvariantCulture),
				"]"
			});
		}

		// Token: 0x04000702 RID: 1794
		private int _x;

		// Token: 0x04000703 RID: 1795
		private int _y;

		// Token: 0x04000704 RID: 1796
		private PrinterResolutionKind _kind;
	}
}
