using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000080 RID: 128
	[NativeConditional("ENABLE_PROFILER")]
	[RequiredByNativeCode]
	public struct AsyncReadManagerRequestMetric
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00003EF4 File Offset: 0x000020F4
		[NativeName("assetName")]
		public readonly string AssetName
		{
			[CompilerGenerated]
			get
			{
				return this.<AssetName>k__BackingField;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00003EFC File Offset: 0x000020FC
		[NativeName("fileName")]
		public readonly string FileName
		{
			[CompilerGenerated]
			get
			{
				return this.<FileName>k__BackingField;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00003F04 File Offset: 0x00002104
		[NativeName("offsetBytes")]
		public readonly ulong OffsetBytes
		{
			[CompilerGenerated]
			get
			{
				return this.<OffsetBytes>k__BackingField;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x00003F0C File Offset: 0x0000210C
		[NativeName("sizeBytes")]
		public readonly ulong SizeBytes
		{
			[CompilerGenerated]
			get
			{
				return this.<SizeBytes>k__BackingField;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00003F14 File Offset: 0x00002114
		[NativeName("assetTypeId")]
		public readonly ulong AssetTypeId
		{
			[CompilerGenerated]
			get
			{
				return this.<AssetTypeId>k__BackingField;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x00003F1C File Offset: 0x0000211C
		[NativeName("currentBytesRead")]
		public readonly ulong CurrentBytesRead
		{
			[CompilerGenerated]
			get
			{
				return this.<CurrentBytesRead>k__BackingField;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00003F24 File Offset: 0x00002124
		[NativeName("batchReadCount")]
		public readonly uint BatchReadCount
		{
			[CompilerGenerated]
			get
			{
				return this.<BatchReadCount>k__BackingField;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00003F2C File Offset: 0x0000212C
		[NativeName("isBatchRead")]
		public readonly bool IsBatchRead
		{
			[CompilerGenerated]
			get
			{
				return this.<IsBatchRead>k__BackingField;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00003F34 File Offset: 0x00002134
		[NativeName("state")]
		public readonly ProcessingState State
		{
			[CompilerGenerated]
			get
			{
				return this.<State>k__BackingField;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00003F3C File Offset: 0x0000213C
		[NativeName("readType")]
		public readonly FileReadType ReadType
		{
			[CompilerGenerated]
			get
			{
				return this.<ReadType>k__BackingField;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00003F44 File Offset: 0x00002144
		[NativeName("priorityLevel")]
		public readonly Priority PriorityLevel
		{
			[CompilerGenerated]
			get
			{
				return this.<PriorityLevel>k__BackingField;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00003F4C File Offset: 0x0000214C
		[NativeName("subsystem")]
		public readonly AssetLoadingSubsystem Subsystem
		{
			[CompilerGenerated]
			get
			{
				return this.<Subsystem>k__BackingField;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00003F54 File Offset: 0x00002154
		[NativeName("requestTimeMicroseconds")]
		public readonly double RequestTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<RequestTimeMicroseconds>k__BackingField;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00003F5C File Offset: 0x0000215C
		[NativeName("timeInQueueMicroseconds")]
		public readonly double TimeInQueueMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<TimeInQueueMicroseconds>k__BackingField;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00003F64 File Offset: 0x00002164
		[NativeName("totalTimeMicroseconds")]
		public readonly double TotalTimeMicroseconds
		{
			[CompilerGenerated]
			get
			{
				return this.<TotalTimeMicroseconds>k__BackingField;
			}
		}

		// Token: 0x040001DD RID: 477
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly string <AssetName>k__BackingField;

		// Token: 0x040001DE RID: 478
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly string <FileName>k__BackingField;

		// Token: 0x040001DF RID: 479
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly ulong <OffsetBytes>k__BackingField;

		// Token: 0x040001E0 RID: 480
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly ulong <SizeBytes>k__BackingField;

		// Token: 0x040001E1 RID: 481
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly ulong <AssetTypeId>k__BackingField;

		// Token: 0x040001E2 RID: 482
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly ulong <CurrentBytesRead>k__BackingField;

		// Token: 0x040001E3 RID: 483
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly uint <BatchReadCount>k__BackingField;

		// Token: 0x040001E4 RID: 484
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly bool <IsBatchRead>k__BackingField;

		// Token: 0x040001E5 RID: 485
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly ProcessingState <State>k__BackingField;

		// Token: 0x040001E6 RID: 486
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly FileReadType <ReadType>k__BackingField;

		// Token: 0x040001E7 RID: 487
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly Priority <PriorityLevel>k__BackingField;

		// Token: 0x040001E8 RID: 488
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly AssetLoadingSubsystem <Subsystem>k__BackingField;

		// Token: 0x040001E9 RID: 489
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly double <RequestTimeMicroseconds>k__BackingField;

		// Token: 0x040001EA RID: 490
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly double <TimeInQueueMicroseconds>k__BackingField;

		// Token: 0x040001EB RID: 491
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly double <TotalTimeMicroseconds>k__BackingField;
	}
}
