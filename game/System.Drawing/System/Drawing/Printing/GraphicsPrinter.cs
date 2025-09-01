using System;

namespace System.Drawing.Printing
{
	// Token: 0x020000DC RID: 220
	internal class GraphicsPrinter
	{
		// Token: 0x06000BA2 RID: 2978 RVA: 0x00019BB1 File Offset: 0x00017DB1
		internal GraphicsPrinter(Graphics gr, IntPtr dc)
		{
			this.graphics = gr;
			this.hDC = dc;
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00019BC7 File Offset: 0x00017DC7
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x00019BCF File Offset: 0x00017DCF
		internal Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
			set
			{
				this.graphics = value;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x00019BD8 File Offset: 0x00017DD8
		internal IntPtr Hdc
		{
			get
			{
				return this.hDC;
			}
		}

		// Token: 0x04000758 RID: 1880
		private Graphics graphics;

		// Token: 0x04000759 RID: 1881
		private IntPtr hDC;
	}
}
