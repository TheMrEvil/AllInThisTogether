using System;
using UnityEngine;

namespace MagicaCloth2
{
	// Token: 0x020000F8 RID: 248
	public class UnityTimeSpan
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00022A19 File Offset: 0x00020C19
		public UnityTimeSpan(string name)
		{
			this.name = name;
			this.stime = Time.realtimeSinceStartup;
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00022A3E File Offset: 0x00020C3E
		public void Finish()
		{
			if (!this.isFinish)
			{
				this.etime = Time.realtimeSinceStartup;
				this.isFinish = true;
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00022A5A File Offset: 0x00020C5A
		public float TotalSeconds()
		{
			this.Finish();
			return this.etime - this.stime;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00022A6F File Offset: 0x00020C6F
		public float TotalMilliSeconds()
		{
			this.Finish();
			return (this.etime - this.stime) * 1000f;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00022A8A File Offset: 0x00020C8A
		public override string ToString()
		{
			return string.Format("UnityTimeSpan [{0}] : {1}(ms)", this.name, this.TotalMilliSeconds());
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00022AA7 File Offset: 0x00020CA7
		public void DebugLog()
		{
			Debug.Log(this);
		}

		// Token: 0x0400064E RID: 1614
		private string name = string.Empty;

		// Token: 0x0400064F RID: 1615
		private float stime;

		// Token: 0x04000650 RID: 1616
		private float etime;

		// Token: 0x04000651 RID: 1617
		private bool isFinish;
	}
}
