using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000229 RID: 553
	internal sealed class UnitySynchronizationContext : SynchronizationContext
	{
		// Token: 0x060017E6 RID: 6118 RVA: 0x00026BA5 File Offset: 0x00024DA5
		private UnitySynchronizationContext(int mainThreadID)
		{
			this.m_AsyncWorkQueue = new List<UnitySynchronizationContext.WorkRequest>(20);
			this.m_MainThreadID = mainThreadID;
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00026BD7 File Offset: 0x00024DD7
		private UnitySynchronizationContext(List<UnitySynchronizationContext.WorkRequest> queue, int mainThreadID)
		{
			this.m_AsyncWorkQueue = queue;
			this.m_MainThreadID = mainThreadID;
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00026C04 File Offset: 0x00024E04
		public override void Send(SendOrPostCallback callback, object state)
		{
			bool flag = this.m_MainThreadID == Thread.CurrentThread.ManagedThreadId;
			if (flag)
			{
				callback(state);
			}
			else
			{
				using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
				{
					List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
					lock (asyncWorkQueue)
					{
						this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, manualResetEvent));
					}
					manualResetEvent.WaitOne();
				}
			}
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00026CA0 File Offset: 0x00024EA0
		public override void OperationStarted()
		{
			Interlocked.Increment(ref this.m_TrackedCount);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00026CAF File Offset: 0x00024EAF
		public override void OperationCompleted()
		{
			Interlocked.Decrement(ref this.m_TrackedCount);
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00026CC0 File Offset: 0x00024EC0
		public override void Post(SendOrPostCallback callback, object state)
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_AsyncWorkQueue.Add(new UnitySynchronizationContext.WorkRequest(callback, state, null));
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00026D0C File Offset: 0x00024F0C
		public override SynchronizationContext CreateCopy()
		{
			return new UnitySynchronizationContext(this.m_AsyncWorkQueue, this.m_MainThreadID);
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00026D30 File Offset: 0x00024F30
		private void Exec()
		{
			List<UnitySynchronizationContext.WorkRequest> asyncWorkQueue = this.m_AsyncWorkQueue;
			lock (asyncWorkQueue)
			{
				this.m_CurrentFrameWork.AddRange(this.m_AsyncWorkQueue);
				this.m_AsyncWorkQueue.Clear();
			}
			while (this.m_CurrentFrameWork.Count > 0)
			{
				UnitySynchronizationContext.WorkRequest workRequest = this.m_CurrentFrameWork[0];
				this.m_CurrentFrameWork.RemoveAt(0);
				workRequest.Invoke();
			}
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x00026DC0 File Offset: 0x00024FC0
		private bool HasPendingTasks()
		{
			return this.m_AsyncWorkQueue.Count != 0 || this.m_TrackedCount != 0;
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00026DEB File Offset: 0x00024FEB
		[RequiredByNativeCode]
		private static void InitializeSynchronizationContext()
		{
			SynchronizationContext.SetSynchronizationContext(new UnitySynchronizationContext(Thread.CurrentThread.ManagedThreadId));
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x00026E04 File Offset: 0x00025004
		[RequiredByNativeCode]
		private static void ExecuteTasks()
		{
			UnitySynchronizationContext unitySynchronizationContext = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = unitySynchronizationContext != null;
			if (flag)
			{
				unitySynchronizationContext.Exec();
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00026E2C File Offset: 0x0002502C
		[RequiredByNativeCode]
		private static bool ExecutePendingTasks(long millisecondsTimeout)
		{
			UnitySynchronizationContext unitySynchronizationContext = SynchronizationContext.Current as UnitySynchronizationContext;
			bool flag = unitySynchronizationContext == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				while (unitySynchronizationContext.HasPendingTasks())
				{
					bool flag2 = stopwatch.ElapsedMilliseconds > millisecondsTimeout;
					if (flag2)
					{
						break;
					}
					unitySynchronizationContext.Exec();
					Thread.Sleep(1);
				}
				result = !unitySynchronizationContext.HasPendingTasks();
			}
			return result;
		}

		// Token: 0x0400082D RID: 2093
		private const int kAwqInitialCapacity = 20;

		// Token: 0x0400082E RID: 2094
		private readonly List<UnitySynchronizationContext.WorkRequest> m_AsyncWorkQueue;

		// Token: 0x0400082F RID: 2095
		private readonly List<UnitySynchronizationContext.WorkRequest> m_CurrentFrameWork = new List<UnitySynchronizationContext.WorkRequest>(20);

		// Token: 0x04000830 RID: 2096
		private readonly int m_MainThreadID;

		// Token: 0x04000831 RID: 2097
		private int m_TrackedCount = 0;

		// Token: 0x0200022A RID: 554
		private struct WorkRequest
		{
			// Token: 0x060017F2 RID: 6130 RVA: 0x00026E9C File Offset: 0x0002509C
			public WorkRequest(SendOrPostCallback callback, object state, ManualResetEvent waitHandle = null)
			{
				this.m_DelagateCallback = callback;
				this.m_DelagateState = state;
				this.m_WaitHandle = waitHandle;
			}

			// Token: 0x060017F3 RID: 6131 RVA: 0x00026EB4 File Offset: 0x000250B4
			public void Invoke()
			{
				try
				{
					this.m_DelagateCallback(this.m_DelagateState);
				}
				finally
				{
					ManualResetEvent waitHandle = this.m_WaitHandle;
					if (waitHandle != null)
					{
						waitHandle.Set();
					}
				}
			}

			// Token: 0x04000832 RID: 2098
			private readonly SendOrPostCallback m_DelagateCallback;

			// Token: 0x04000833 RID: 2099
			private readonly object m_DelagateState;

			// Token: 0x04000834 RID: 2100
			private readonly ManualResetEvent m_WaitHandle;
		}
	}
}
