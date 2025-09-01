using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000084 RID: 132
	[NativeAsStruct]
	[NativeConditional("ENABLE_PROFILER")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncReadManagerMetricsFilters
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00004165 File Offset: 0x00002365
		public AsyncReadManagerMetricsFilters()
		{
			this.ClearFilters();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00004176 File Offset: 0x00002376
		public AsyncReadManagerMetricsFilters(ulong typeID)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeID);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000418F File Offset: 0x0000238F
		public AsyncReadManagerMetricsFilters(ProcessingState state)
		{
			this.ClearFilters();
			this.SetStateFilter(state);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000041A8 File Offset: 0x000023A8
		public AsyncReadManagerMetricsFilters(FileReadType readType)
		{
			this.ClearFilters();
			this.SetReadTypeFilter(readType);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000041C1 File Offset: 0x000023C1
		public AsyncReadManagerMetricsFilters(Priority priorityLevel)
		{
			this.ClearFilters();
			this.SetPriorityFilter(priorityLevel);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000041DA File Offset: 0x000023DA
		public AsyncReadManagerMetricsFilters(AssetLoadingSubsystem subsystem)
		{
			this.ClearFilters();
			this.SetSubsystemFilter(subsystem);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000041F3 File Offset: 0x000023F3
		public AsyncReadManagerMetricsFilters(ulong[] typeIDs)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeIDs);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000420C File Offset: 0x0000240C
		public AsyncReadManagerMetricsFilters(ProcessingState[] states)
		{
			this.ClearFilters();
			this.SetStateFilter(states);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00004225 File Offset: 0x00002425
		public AsyncReadManagerMetricsFilters(FileReadType[] readTypes)
		{
			this.ClearFilters();
			this.SetReadTypeFilter(readTypes);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000423E File Offset: 0x0000243E
		public AsyncReadManagerMetricsFilters(Priority[] priorityLevels)
		{
			this.ClearFilters();
			this.SetPriorityFilter(priorityLevels);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00004257 File Offset: 0x00002457
		public AsyncReadManagerMetricsFilters(AssetLoadingSubsystem[] subsystems)
		{
			this.ClearFilters();
			this.SetSubsystemFilter(subsystems);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00004270 File Offset: 0x00002470
		public AsyncReadManagerMetricsFilters(ulong[] typeIDs, ProcessingState[] states, FileReadType[] readTypes, Priority[] priorityLevels, AssetLoadingSubsystem[] subsystems)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeIDs);
			this.SetStateFilter(states);
			this.SetReadTypeFilter(readTypes);
			this.SetPriorityFilter(priorityLevels);
			this.SetSubsystemFilter(subsystems);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000042AB File Offset: 0x000024AB
		public void SetTypeIDFilter(ulong[] _typeIDs)
		{
			this.TypeIDs = _typeIDs;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000042B5 File Offset: 0x000024B5
		public void SetStateFilter(ProcessingState[] _states)
		{
			this.States = _states;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000042BF File Offset: 0x000024BF
		public void SetReadTypeFilter(FileReadType[] _readTypes)
		{
			this.ReadTypes = _readTypes;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000042C9 File Offset: 0x000024C9
		public void SetPriorityFilter(Priority[] _priorityLevels)
		{
			this.PriorityLevels = _priorityLevels;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000042D3 File Offset: 0x000024D3
		public void SetSubsystemFilter(AssetLoadingSubsystem[] _subsystems)
		{
			this.Subsystems = _subsystems;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000042DD File Offset: 0x000024DD
		public void SetTypeIDFilter(ulong _typeID)
		{
			this.TypeIDs = new ulong[]
			{
				_typeID
			};
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000042F0 File Offset: 0x000024F0
		public void SetStateFilter(ProcessingState _state)
		{
			this.States = new ProcessingState[]
			{
				_state
			};
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00004303 File Offset: 0x00002503
		public void SetReadTypeFilter(FileReadType _readType)
		{
			this.ReadTypes = new FileReadType[]
			{
				_readType
			};
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00004316 File Offset: 0x00002516
		public void SetPriorityFilter(Priority _priorityLevel)
		{
			this.PriorityLevels = new Priority[]
			{
				_priorityLevel
			};
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00004329 File Offset: 0x00002529
		public void SetSubsystemFilter(AssetLoadingSubsystem _subsystem)
		{
			this.Subsystems = new AssetLoadingSubsystem[]
			{
				_subsystem
			};
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000433C File Offset: 0x0000253C
		public void RemoveTypeIDFilter()
		{
			this.TypeIDs = null;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00004346 File Offset: 0x00002546
		public void RemoveStateFilter()
		{
			this.States = null;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00004350 File Offset: 0x00002550
		public void RemoveReadTypeFilter()
		{
			this.ReadTypes = null;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000435A File Offset: 0x0000255A
		public void RemovePriorityFilter()
		{
			this.PriorityLevels = null;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00004364 File Offset: 0x00002564
		public void RemoveSubsystemFilter()
		{
			this.Subsystems = null;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000436E File Offset: 0x0000256E
		public void ClearFilters()
		{
			this.RemoveTypeIDFilter();
			this.RemoveStateFilter();
			this.RemoveReadTypeFilter();
			this.RemovePriorityFilter();
			this.RemoveSubsystemFilter();
		}

		// Token: 0x04000205 RID: 517
		[NativeName("typeIDs")]
		internal ulong[] TypeIDs;

		// Token: 0x04000206 RID: 518
		[NativeName("states")]
		internal ProcessingState[] States;

		// Token: 0x04000207 RID: 519
		[NativeName("readTypes")]
		internal FileReadType[] ReadTypes;

		// Token: 0x04000208 RID: 520
		[NativeName("priorityLevels")]
		internal Priority[] PriorityLevels;

		// Token: 0x04000209 RID: 521
		[NativeName("subsystems")]
		internal AssetLoadingSubsystem[] Subsystems;
	}
}
