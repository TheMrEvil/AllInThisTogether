using System;

namespace Unity.Profiling
{
	// Token: 0x0200004E RID: 78
	internal sealed class ProfilerRecorderDebugView
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00002FAB File Offset: 0x000011AB
		public ProfilerRecorderDebugView(ProfilerRecorder r)
		{
			this.m_Recorder = r;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00002FBC File Offset: 0x000011BC
		public ProfilerRecorderSample[] Items
		{
			get
			{
				return this.m_Recorder.ToArray();
			}
		}

		// Token: 0x0400013A RID: 314
		private ProfilerRecorder m_Recorder;
	}
}
