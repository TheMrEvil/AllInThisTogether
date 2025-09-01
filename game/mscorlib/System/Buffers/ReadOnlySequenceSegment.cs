using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000AEC RID: 2796
	public abstract class ReadOnlySequenceSegment<T>
	{
		// Token: 0x17001193 RID: 4499
		// (get) Token: 0x06006366 RID: 25446 RVA: 0x0014CA04 File Offset: 0x0014AC04
		// (set) Token: 0x06006367 RID: 25447 RVA: 0x0014CA0C File Offset: 0x0014AC0C
		public ReadOnlyMemory<T> Memory
		{
			[CompilerGenerated]
			get
			{
				return this.<Memory>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Memory>k__BackingField = value;
			}
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x06006368 RID: 25448 RVA: 0x0014CA15 File Offset: 0x0014AC15
		// (set) Token: 0x06006369 RID: 25449 RVA: 0x0014CA1D File Offset: 0x0014AC1D
		public ReadOnlySequenceSegment<T> Next
		{
			[CompilerGenerated]
			get
			{
				return this.<Next>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<Next>k__BackingField = value;
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600636A RID: 25450 RVA: 0x0014CA26 File Offset: 0x0014AC26
		// (set) Token: 0x0600636B RID: 25451 RVA: 0x0014CA2E File Offset: 0x0014AC2E
		public long RunningIndex
		{
			[CompilerGenerated]
			get
			{
				return this.<RunningIndex>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<RunningIndex>k__BackingField = value;
			}
		}

		// Token: 0x0600636C RID: 25452 RVA: 0x0000259F File Offset: 0x0000079F
		protected ReadOnlySequenceSegment()
		{
		}

		// Token: 0x04003A7E RID: 14974
		[CompilerGenerated]
		private ReadOnlyMemory<T> <Memory>k__BackingField;

		// Token: 0x04003A7F RID: 14975
		[CompilerGenerated]
		private ReadOnlySequenceSegment<T> <Next>k__BackingField;

		// Token: 0x04003A80 RID: 14976
		[CompilerGenerated]
		private long <RunningIndex>k__BackingField;
	}
}
