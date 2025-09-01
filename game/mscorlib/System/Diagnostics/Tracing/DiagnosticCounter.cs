using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x020009FE RID: 2558
	public abstract class DiagnosticCounter : IDisposable
	{
		// Token: 0x06005B3C RID: 23356 RVA: 0x0000259F File Offset: 0x0000079F
		internal DiagnosticCounter(string name, EventSource eventSource)
		{
		}

		// Token: 0x06005B3D RID: 23357 RVA: 0x0000259F File Offset: 0x0000079F
		internal DiagnosticCounter()
		{
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06005B3E RID: 23358 RVA: 0x001348D1 File Offset: 0x00132AD1
		// (set) Token: 0x06005B3F RID: 23359 RVA: 0x001348D9 File Offset: 0x00132AD9
		public string DisplayName
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisplayName>k__BackingField = value;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06005B40 RID: 23360 RVA: 0x001348E2 File Offset: 0x00132AE2
		// (set) Token: 0x06005B41 RID: 23361 RVA: 0x001348EA File Offset: 0x00132AEA
		public string DisplayUnits
		{
			[CompilerGenerated]
			get
			{
				return this.<DisplayUnits>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisplayUnits>k__BackingField = value;
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06005B42 RID: 23362 RVA: 0x001348F3 File Offset: 0x00132AF3
		public EventSource EventSource
		{
			[CompilerGenerated]
			get
			{
				return this.<EventSource>k__BackingField;
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06005B43 RID: 23363 RVA: 0x001348FB File Offset: 0x00132AFB
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
		}

		// Token: 0x06005B44 RID: 23364 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void AddMetadata(string key, string value)
		{
		}

		// Token: 0x06005B45 RID: 23365 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dispose()
		{
		}

		// Token: 0x04003846 RID: 14406
		[CompilerGenerated]
		private string <DisplayName>k__BackingField;

		// Token: 0x04003847 RID: 14407
		[CompilerGenerated]
		private string <DisplayUnits>k__BackingField;

		// Token: 0x04003848 RID: 14408
		[CompilerGenerated]
		private readonly EventSource <EventSource>k__BackingField;

		// Token: 0x04003849 RID: 14409
		[CompilerGenerated]
		private readonly string <Name>k__BackingField;
	}
}
