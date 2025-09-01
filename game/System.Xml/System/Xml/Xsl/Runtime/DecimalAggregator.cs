using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000465 RID: 1125
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DecimalAggregator
	{
		// Token: 0x06002B88 RID: 11144 RVA: 0x00104807 File Offset: 0x00102A07
		public void Create()
		{
			this.cnt = 0;
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x00104810 File Offset: 0x00102A10
		public void Sum(decimal value)
		{
			if (this.cnt == 0)
			{
				this.result = value;
				this.cnt = 1;
				return;
			}
			this.result += value;
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x0010483B File Offset: 0x00102A3B
		public void Average(decimal value)
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

		// Token: 0x06002B8B RID: 11147 RVA: 0x0010486E File Offset: 0x00102A6E
		public void Minimum(decimal value)
		{
			if (this.cnt == 0 || value < this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x00104894 File Offset: 0x00102A94
		public void Maximum(decimal value)
		{
			if (this.cnt == 0 || value > this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x001048BA File Offset: 0x00102ABA
		public decimal SumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x001048C2 File Offset: 0x00102AC2
		public decimal AverageResult
		{
			get
			{
				return this.result / this.cnt;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x001048BA File Offset: 0x00102ABA
		public decimal MinimumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x001048BA File Offset: 0x00102ABA
		public decimal MaximumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x001048DA File Offset: 0x00102ADA
		public bool IsEmpty
		{
			get
			{
				return this.cnt == 0;
			}
		}

		// Token: 0x04002291 RID: 8849
		private decimal result;

		// Token: 0x04002292 RID: 8850
		private int cnt;
	}
}
