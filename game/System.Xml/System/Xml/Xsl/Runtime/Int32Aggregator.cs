using System;
using System.ComponentModel;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x02000463 RID: 1123
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct Int32Aggregator
	{
		// Token: 0x06002B74 RID: 11124 RVA: 0x00104680 File Offset: 0x00102880
		public void Create()
		{
			this.cnt = 0;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x00104689 File Offset: 0x00102889
		public void Sum(int value)
		{
			if (this.cnt == 0)
			{
				this.result = value;
				this.cnt = 1;
				return;
			}
			this.result += value;
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x001046B0 File Offset: 0x001028B0
		public void Average(int value)
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

		// Token: 0x06002B77 RID: 11127 RVA: 0x001046DF File Offset: 0x001028DF
		public void Minimum(int value)
		{
			if (this.cnt == 0 || value < this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x00104700 File Offset: 0x00102900
		public void Maximum(int value)
		{
			if (this.cnt == 0 || value > this.result)
			{
				this.result = value;
			}
			this.cnt = 1;
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002B79 RID: 11129 RVA: 0x00104721 File Offset: 0x00102921
		public int SumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002B7A RID: 11130 RVA: 0x00104729 File Offset: 0x00102929
		public int AverageResult
		{
			get
			{
				return this.result / this.cnt;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x00104721 File Offset: 0x00102921
		public int MinimumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002B7C RID: 11132 RVA: 0x00104721 File Offset: 0x00102921
		public int MaximumResult
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x00104738 File Offset: 0x00102938
		public bool IsEmpty
		{
			get
			{
				return this.cnt == 0;
			}
		}

		// Token: 0x0400228D RID: 8845
		private int result;

		// Token: 0x0400228E RID: 8846
		private int cnt;
	}
}
