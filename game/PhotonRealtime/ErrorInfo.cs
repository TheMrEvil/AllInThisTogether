using System;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x0200001C RID: 28
	public class ErrorInfo
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00007068 File Offset: 0x00005268
		public ErrorInfo(EventData eventData)
		{
			this.Info = (eventData[218] as string);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007086 File Offset: 0x00005286
		public override string ToString()
		{
			return string.Format("ErrorInfo: {0}", this.Info);
		}

		// Token: 0x040000B0 RID: 176
		public readonly string Info;
	}
}
