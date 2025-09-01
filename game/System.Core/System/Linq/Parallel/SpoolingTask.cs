using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Linq.Parallel
{
	// Token: 0x020001DD RID: 477
	internal static class SpoolingTask
	{
		// Token: 0x06000BDA RID: 3034 RVA: 0x00029BB8 File Offset: 0x00027DB8
		internal static void SpoolStopAndGo<TInputOutput, TIgnoreKey>(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TIgnoreKey> partitions, SynchronousChannel<TInputOutput>[] channels, TaskScheduler taskScheduler)
		{
			Task task = new Task(delegate()
			{
				int num = partitions.PartitionCount - 1;
				for (int i = 0; i < num; i++)
				{
					new StopAndGoSpoolingTask<TInputOutput, TIgnoreKey>(i, groupState, partitions[i], channels[i]).RunAsynchronously(taskScheduler);
				}
				new StopAndGoSpoolingTask<TInputOutput, TIgnoreKey>(num, groupState, partitions[num], channels[num]).RunSynchronously(taskScheduler);
			});
			groupState.QueryBegin(task);
			task.RunSynchronously(taskScheduler);
			groupState.QueryEnd(false);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00029C20 File Offset: 0x00027E20
		internal static void SpoolPipeline<TInputOutput, TIgnoreKey>(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TIgnoreKey> partitions, AsynchronousChannel<TInputOutput>[] channels, TaskScheduler taskScheduler)
		{
			Task task = new Task(delegate()
			{
				for (int i = 0; i < partitions.PartitionCount; i++)
				{
					new PipelineSpoolingTask<TInputOutput, TIgnoreKey>(i, groupState, partitions[i], channels[i]).RunAsynchronously(taskScheduler);
				}
			});
			groupState.QueryBegin(task);
			task.Start(taskScheduler);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00029C7C File Offset: 0x00027E7C
		internal static void SpoolForAll<TInputOutput, TIgnoreKey>(QueryTaskGroupState groupState, PartitionedStream<TInputOutput, TIgnoreKey> partitions, TaskScheduler taskScheduler)
		{
			Task task = new Task(delegate()
			{
				int num = partitions.PartitionCount - 1;
				for (int i = 0; i < num; i++)
				{
					new ForAllSpoolingTask<TInputOutput, TIgnoreKey>(i, groupState, partitions[i]).RunAsynchronously(taskScheduler);
				}
				new ForAllSpoolingTask<TInputOutput, TIgnoreKey>(num, groupState, partitions[num]).RunSynchronously(taskScheduler);
			});
			groupState.QueryBegin(task);
			task.RunSynchronously(taskScheduler);
			groupState.QueryEnd(false);
		}

		// Token: 0x020001DE RID: 478
		[CompilerGenerated]
		private sealed class <>c__DisplayClass0_0<TInputOutput, TIgnoreKey>
		{
			// Token: 0x06000BDD RID: 3037 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass0_0()
			{
			}

			// Token: 0x06000BDE RID: 3038 RVA: 0x00029CDC File Offset: 0x00027EDC
			internal void <SpoolStopAndGo>b__0()
			{
				int num = this.partitions.PartitionCount - 1;
				for (int i = 0; i < num; i++)
				{
					new StopAndGoSpoolingTask<TInputOutput, TIgnoreKey>(i, this.groupState, this.partitions[i], this.channels[i]).RunAsynchronously(this.taskScheduler);
				}
				new StopAndGoSpoolingTask<TInputOutput, TIgnoreKey>(num, this.groupState, this.partitions[num], this.channels[num]).RunSynchronously(this.taskScheduler);
			}

			// Token: 0x04000866 RID: 2150
			public PartitionedStream<TInputOutput, TIgnoreKey> partitions;

			// Token: 0x04000867 RID: 2151
			public QueryTaskGroupState groupState;

			// Token: 0x04000868 RID: 2152
			public SynchronousChannel<TInputOutput>[] channels;

			// Token: 0x04000869 RID: 2153
			public TaskScheduler taskScheduler;
		}

		// Token: 0x020001DF RID: 479
		[CompilerGenerated]
		private sealed class <>c__DisplayClass1_0<TInputOutput, TIgnoreKey>
		{
			// Token: 0x06000BDF RID: 3039 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass1_0()
			{
			}

			// Token: 0x06000BE0 RID: 3040 RVA: 0x00029D5C File Offset: 0x00027F5C
			internal void <SpoolPipeline>b__0()
			{
				for (int i = 0; i < this.partitions.PartitionCount; i++)
				{
					new PipelineSpoolingTask<TInputOutput, TIgnoreKey>(i, this.groupState, this.partitions[i], this.channels[i]).RunAsynchronously(this.taskScheduler);
				}
			}

			// Token: 0x0400086A RID: 2154
			public QueryTaskGroupState groupState;

			// Token: 0x0400086B RID: 2155
			public PartitionedStream<TInputOutput, TIgnoreKey> partitions;

			// Token: 0x0400086C RID: 2156
			public AsynchronousChannel<TInputOutput>[] channels;

			// Token: 0x0400086D RID: 2157
			public TaskScheduler taskScheduler;
		}

		// Token: 0x020001E0 RID: 480
		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0<TInputOutput, TIgnoreKey>
		{
			// Token: 0x06000BE1 RID: 3041 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__DisplayClass2_0()
			{
			}

			// Token: 0x06000BE2 RID: 3042 RVA: 0x00029DAC File Offset: 0x00027FAC
			internal void <SpoolForAll>b__0()
			{
				int num = this.partitions.PartitionCount - 1;
				for (int i = 0; i < num; i++)
				{
					new ForAllSpoolingTask<TInputOutput, TIgnoreKey>(i, this.groupState, this.partitions[i]).RunAsynchronously(this.taskScheduler);
				}
				new ForAllSpoolingTask<TInputOutput, TIgnoreKey>(num, this.groupState, this.partitions[num]).RunSynchronously(this.taskScheduler);
			}

			// Token: 0x0400086E RID: 2158
			public PartitionedStream<TInputOutput, TIgnoreKey> partitions;

			// Token: 0x0400086F RID: 2159
			public QueryTaskGroupState groupState;

			// Token: 0x04000870 RID: 2160
			public TaskScheduler taskScheduler;
		}
	}
}
