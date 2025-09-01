using System;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	internal abstract class Response : IResponse
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x0000576A File Offset: 0x0000396A
		public void SetSuccess()
		{
			this.success = true;
			this.extendedInfo = "";
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000577F File Offset: 0x0000397F
		public void SetFailure(string info)
		{
			this.success = false;
			this.extendedInfo += info;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000579C File Offset: 0x0000399C
		public override string ToString()
		{
			return UnityString.Format("[{0}]-success:{1}-extendedInfo:{2}", new object[]
			{
				base.ToString(),
				this.success,
				this.extendedInfo
			});
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00005759 File Offset: 0x00003959
		protected Response()
		{
		}

		// Token: 0x0400009E RID: 158
		public bool success;

		// Token: 0x0400009F RID: 159
		public string extendedInfo;
	}
}
