using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

// Token: 0x02000002 RID: 2
public class DeferredSynchronizeInvoke : ISynchronizeInvoke
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	public bool InvokeRequired
	{
		get
		{
			return this.mainThread.ManagedThreadId != Thread.CurrentThread.ManagedThreadId;
		}
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000206C File Offset: 0x0000026C
	public DeferredSynchronizeInvoke()
	{
		this.mainThread = Thread.CurrentThread;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000208C File Offset: 0x0000028C
	public IAsyncResult BeginInvoke(Delegate method, object[] args)
	{
		DeferredSynchronizeInvoke.UnityAsyncResult unityAsyncResult = new DeferredSynchronizeInvoke.UnityAsyncResult
		{
			method = method,
			args = args,
			IsCompleted = false,
			AsyncWaitHandle = new ManualResetEvent(false)
		};
		Queue<DeferredSynchronizeInvoke.UnityAsyncResult> obj = this.fifoToExecute;
		lock (obj)
		{
			this.fifoToExecute.Enqueue(unityAsyncResult);
		}
		return unityAsyncResult;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
	public object EndInvoke(IAsyncResult result)
	{
		if (!result.IsCompleted)
		{
			result.AsyncWaitHandle.WaitOne();
		}
		return result.AsyncState;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002118 File Offset: 0x00000318
	public object Invoke(Delegate method, object[] args)
	{
		if (this.InvokeRequired)
		{
			IAsyncResult result = this.BeginInvoke(method, args);
			return this.EndInvoke(result);
		}
		return method.DynamicInvoke(args);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002148 File Offset: 0x00000348
	public void ProcessQueue()
	{
		if (Thread.CurrentThread != this.mainThread)
		{
			string[] array = new string[7];
			int num = 0;
			Type type = base.GetType();
			array[num] = ((type != null) ? type.ToString() : null);
			array[1] = ".";
			array[2] = MethodBase.GetCurrentMethod().Name;
			array[3] = "() must be called from the same thread it was created on (created on thread id: ";
			array[4] = this.mainThread.ManagedThreadId.ToString();
			array[5] = ", called from thread id: ";
			array[6] = Thread.CurrentThread.ManagedThreadId.ToString();
			throw new TargetException(string.Concat(array));
		}
		bool flag = true;
		DeferredSynchronizeInvoke.UnityAsyncResult unityAsyncResult = null;
		while (flag)
		{
			Queue<DeferredSynchronizeInvoke.UnityAsyncResult> obj = this.fifoToExecute;
			lock (obj)
			{
				flag = (this.fifoToExecute.Count > 0);
				if (!flag)
				{
					break;
				}
				unityAsyncResult = this.fifoToExecute.Dequeue();
			}
			unityAsyncResult.AsyncState = this.Invoke(unityAsyncResult.method, unityAsyncResult.args);
			unityAsyncResult.IsCompleted = true;
		}
	}

	// Token: 0x04000001 RID: 1
	private Queue<DeferredSynchronizeInvoke.UnityAsyncResult> fifoToExecute = new Queue<DeferredSynchronizeInvoke.UnityAsyncResult>();

	// Token: 0x04000002 RID: 2
	private Thread mainThread;

	// Token: 0x02000013 RID: 19
	private class UnityAsyncResult : IAsyncResult
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000372F File Offset: 0x0000192F
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00003737 File Offset: 0x00001937
		public bool IsCompleted
		{
			[CompilerGenerated]
			get
			{
				return this.<IsCompleted>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsCompleted>k__BackingField = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003740 File Offset: 0x00001940
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00003748 File Offset: 0x00001948
		public WaitHandle AsyncWaitHandle
		{
			[CompilerGenerated]
			get
			{
				return this.<AsyncWaitHandle>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<AsyncWaitHandle>k__BackingField = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003751 File Offset: 0x00001951
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003759 File Offset: 0x00001959
		public object AsyncState
		{
			[CompilerGenerated]
			get
			{
				return this.<AsyncState>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AsyncState>k__BackingField = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003762 File Offset: 0x00001962
		public bool CompletedSynchronously
		{
			get
			{
				return this.IsCompleted;
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000376A File Offset: 0x0000196A
		public UnityAsyncResult()
		{
		}

		// Token: 0x04000014 RID: 20
		public Delegate method;

		// Token: 0x04000015 RID: 21
		public object[] args;

		// Token: 0x04000016 RID: 22
		[CompilerGenerated]
		private bool <IsCompleted>k__BackingField;

		// Token: 0x04000017 RID: 23
		[CompilerGenerated]
		private WaitHandle <AsyncWaitHandle>k__BackingField;

		// Token: 0x04000018 RID: 24
		[CompilerGenerated]
		private object <AsyncState>k__BackingField;
	}
}
