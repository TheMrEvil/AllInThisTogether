using System;
using System.Diagnostics;

namespace System.Data.SqlClient
{
	// Token: 0x020001E2 RID: 482
	internal class SqlConnectionTimeoutPhaseDuration
	{
		// Token: 0x06001779 RID: 6009 RVA: 0x0006A208 File Offset: 0x00068408
		internal void StartCapture()
		{
			this._swDuration.Start();
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0006A215 File Offset: 0x00068415
		internal void StopCapture()
		{
			if (this._swDuration.IsRunning)
			{
				this._swDuration.Stop();
			}
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0006A22F File Offset: 0x0006842F
		internal long GetMilliSecondDuration()
		{
			return this._swDuration.ElapsedMilliseconds;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0006A23C File Offset: 0x0006843C
		public SqlConnectionTimeoutPhaseDuration()
		{
		}

		// Token: 0x04000F26 RID: 3878
		private Stopwatch _swDuration = new Stopwatch();
	}
}
