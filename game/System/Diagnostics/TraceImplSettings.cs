using System;

namespace System.Diagnostics
{
	// Token: 0x02000284 RID: 644
	internal class TraceImplSettings
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x00053271 File Offset: 0x00051471
		public TraceImplSettings()
		{
			this.Listeners.Add(new DefaultTraceListener
			{
				IndentSize = this.IndentSize
			});
		}

		// Token: 0x04000B87 RID: 2951
		public const string Key = ".__TraceInfoSettingsKey__.";

		// Token: 0x04000B88 RID: 2952
		public bool AutoFlush;

		// Token: 0x04000B89 RID: 2953
		public int IndentSize = 4;

		// Token: 0x04000B8A RID: 2954
		public TraceListenerCollection Listeners = new TraceListenerCollection();
	}
}
