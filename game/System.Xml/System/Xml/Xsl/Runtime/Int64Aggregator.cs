using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000464 RID: 1124
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct Int64Aggregator
	{
		// Token: 0x06002B7E RID: 11134 RVA: 0x00104743 File Offset: 0x00102943
		public void Create()
		{
			this.cnt = 0;
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x0010474C File Offset: 0x0010294C
		public void Sum(long value)
		{
			if (this.cnt == 0)
			{
				this.result = value;
				this.cnt = 1;
				return;
			}
			this.result += value;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x00104773 File Offset: 0x00102973
		public void Average(long value)
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

		// Token: 0x06002B81 RID: 11137 RVA: 0x001047A2 File Offset: 0x001029A2
		public void Minimum(long value)
		{
			if (this.cnt == 0 || value < this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x001047C3 File Offset: 0x001029C3
		public void Maximum(long value)
		{
			if (this.cnt == 0 || value > this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x001047E4 File Offset: 0x001029E4
		public long SumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06002B84 RID: 11140 RVA: 0x001047EC File Offset: 0x001029EC
		public long AverageResult
		{
			get
			{
				return this.result / (long)this.cnt;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x001047E4 File Offset: 0x001029E4
		public long MinimumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x001047E4 File Offset: 0x001029E4
		public long MaximumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x001047FC File Offset: 0x001029FC
		public bool IsEmpty
		{
			get
			{
				return this.cnt == 0;
			}
		}

		// Token: 0x0400228F RID: 8847
		private long result;

		// Token: 0x04002290 RID: 8848
		private int cnt;
	}
}
