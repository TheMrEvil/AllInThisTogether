using System;

namespace System.Diagnostics
{
	// Token: 0x02000243 RID: 579
	internal class ProcessThreadTimes
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0004DD0A File Offset: 0x0004BF0A
		public DateTime StartTime
		{
			get
			{
				return DateTime.FromFileTime(this.create);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x0004DD17 File Offset: 0x0004BF17
		public DateTime ExitTime
		{
			get
			{
				return DateTime.FromFileTime(this.exit);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0004DD24 File Offset: 0x0004BF24
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				return new TimeSpan(this.kernel);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0004DD31 File Offset: 0x0004BF31
		public TimeSpan UserProcessorTime
		{
			get
			{
				return new TimeSpan(this.user);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0004DD3E File Offset: 0x0004BF3E
		public TimeSpan TotalProcessorTime
		{
			get
			{
				return new TimeSpan(this.user + this.kernel);
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0000219B File Offset: 0x0000039B
		public ProcessThreadTimes()
		{
		}

		// Token: 0x04000A6E RID: 2670
		internal long create;

		// Token: 0x04000A6F RID: 2671
		internal long exit;

		// Token: 0x04000A70 RID: 2672
		internal long kernel;

		// Token: 0x04000A71 RID: 2673
		internal long user;
	}
}
