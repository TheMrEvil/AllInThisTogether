using System;

namespace MagicaCloth2
{
	// Token: 0x020000F7 RID: 247
	public class TimeSpan
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x00022952 File Offset: 0x00020B52
		public TimeSpan()
		{
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00022965 File Offset: 0x00020B65
		public TimeSpan(string name)
		{
			this.name = name;
			this.stime = DateTime.Now;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0002298A File Offset: 0x00020B8A
		public void Start()
		{
			this.stime = DateTime.Now;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00022997 File Offset: 0x00020B97
		public void Finish()
		{
			this.etime = DateTime.Now;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000229A4 File Offset: 0x00020BA4
		public double TotalSeconds()
		{
			this.Finish();
			return (this.etime - this.stime).TotalSeconds;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000229D0 File Offset: 0x00020BD0
		public double TotalMilliSeconds()
		{
			this.Finish();
			return (this.etime - this.stime).TotalMilliseconds;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000229FC File Offset: 0x00020BFC
		public override string ToString()
		{
			return string.Format("TimeSpan [{0}] : {1}(ms)", this.name, this.TotalMilliSeconds());
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00005305 File Offset: 0x00003505
		public void DebugLog()
		{
		}

		// Token: 0x0400064B RID: 1611
		private string name = string.Empty;

		// Token: 0x0400064C RID: 1612
		private DateTime stime;

		// Token: 0x0400064D RID: 1613
		private DateTime etime;
	}
}
