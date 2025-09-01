using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000466 RID: 1126
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DoubleAggregator
	{
		// Token: 0x06002B92 RID: 11154 RVA: 0x001048E5 File Offset: 0x00102AE5
		public void Create()
		{
			this.cnt = 0;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x001048EE File Offset: 0x00102AEE
		public void Sum(double value)
		{
			if (this.cnt == 0)
			{
				this.result = value;
				this.cnt = 1;
				return;
			}
			this.result += value;
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x00104915 File Offset: 0x00102B15
		public void Average(double value)
		{
			if (this.cnt == 0)
			{
				this.result = value;
			}
			else
			{
				this.result += value;
			}
			this.cnt++;
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x00104944 File Offset: 0x00102B44
		public void Minimum(double value)
		{
			if (this.cnt == 0 || value < this.result || double.IsNaN(value))
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x0010496D File Offset: 0x00102B6D
		public void Maximum(double value)
		{
			if (this.cnt == 0 || value > this.result || double.IsNaN(value))
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x00104996 File Offset: 0x00102B96
		public double SumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x0010499E File Offset: 0x00102B9E
		public double AverageResult
		{
			get
			{
				return this.result / (double)this.cnt;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002B99 RID: 11161 RVA: 0x00104996 File Offset: 0x00102B96
		public double MinimumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06002B9A RID: 11162 RVA: 0x00104996 File Offset: 0x00102B96
		public double MaximumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06002B9B RID: 11163 RVA: 0x001049AE File Offset: 0x00102BAE
		public bool IsEmpty
		{
			get
			{
				return this.cnt == 0;
			}
		}

		// Token: 0x04002293 RID: 8851
		private double result;

		// Token: 0x04002294 RID: 8852
		private int cnt;
	}
}
