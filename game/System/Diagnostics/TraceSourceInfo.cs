using System;

namespace System.Diagnostics
{
	// Token: 0x02000285 RID: 645
	internal class TraceSourceInfo
	{
		// Token: 0x06001467 RID: 5223 RVA: 0x000532A8 File Offset: 0x000514A8
		public TraceSourceInfo(string name, SourceLevels levels)
		{
			this.name = name;
			this.levels = levels;
			this.listeners = new TraceListenerCollection();
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x000532C9 File Offset: 0x000514C9
		internal TraceSourceInfo(string name, SourceLevels levels, TraceImplSettings settings)
		{
			this.name = name;
			this.levels = levels;
			this.listeners = new TraceListenerCollection();
			this.listeners.Add(new DefaultTraceListener
			{
				IndentSize = settings.IndentSize
			});
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x00053307 File Offset: 0x00051507
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0005330F File Offset: 0x0005150F
		public SourceLevels Levels
		{
			get
			{
				return this.levels;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x00053317 File Offset: 0x00051517
		public TraceListenerCollection Listeners
		{
			get
			{
				return this.listeners;
			}
		}

		// Token: 0x04000B8B RID: 2955
		private string name;

		// Token: 0x04000B8C RID: 2956
		private SourceLevels levels;

		// Token: 0x04000B8D RID: 2957
		private TraceListenerCollection listeners;
	}
}
