using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020006B0 RID: 1712
	internal class ServicePointScheduler
	{
		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x000C0B8A File Offset: 0x000BED8A
		// (set) Token: 0x0600370B RID: 14091 RVA: 0x000C0B92 File Offset: 0x000BED92
		private ServicePoint ServicePoint
		{
			[CompilerGenerated]
			get
			{
				return this.<ServicePoint>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ServicePoint>k__BackingField = value;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000C0B9B File Offset: 0x000BED9B
		// (set) Token: 0x0600370D RID: 14093 RVA: 0x000C0BA3 File Offset: 0x000BEDA3
		public int MaxIdleTime
		{
			get
			{
				return this.maxIdleTime;
			}
			set
			{
				if (value < -1 || value > 2147483647)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (value == this.maxIdleTime)
				{
					return;
				}
				this.maxIdleTime = value;
				this.Run();
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x0600370E RID: 14094 RVA: 0x000C0BCE File Offset: 0x000BEDCE
		// (set) Token: 0x0600370F RID: 14095 RVA: 0x000C0BD6 File Offset: 0x000BEDD6
		public int ConnectionLimit
		{
			get
			{
				return this.connectionLimit;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (value == this.connectionLimit)
				{
					return;
				}
				this.connectionLimit = value;
				this.Run();
			}
		}

		// Token: 0x06003710 RID: 14096 RVA: 0x000C0BFC File Offset: 0x000BEDFC
		public ServicePointScheduler(ServicePoint servicePoint, int connectionLimit, int maxIdleTime)
		{
			this.ServicePoint = servicePoint;
			this.connectionLimit = connectionLimit;
			this.maxIdleTime = maxIdleTime;
			this.schedulerEvent = new ServicePointScheduler.AsyncManualResetEvent(false);
			this.defaultGroup = new ServicePointScheduler.ConnectionGroup(this, string.Empty);
			this.operations = new LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>>();
			this.idleConnections = new LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>>();
			this.idleSince = DateTime.UtcNow;
		}

		// Token: 0x06003711 RID: 14097 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_WEB_DEBUG")]
		private void Debug(string message)
		{
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06003712 RID: 14098 RVA: 0x000C0C80 File Offset: 0x000BEE80
		public int CurrentConnections
		{
			get
			{
				return this.currentConnections;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06003713 RID: 14099 RVA: 0x000C0C88 File Offset: 0x000BEE88
		public DateTime IdleSince
		{
			get
			{
				return this.idleSince;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06003714 RID: 14100 RVA: 0x000C0C90 File Offset: 0x000BEE90
		internal string ME
		{
			[CompilerGenerated]
			get
			{
				return this.<ME>k__BackingField;
			}
		}

		// Token: 0x06003715 RID: 14101 RVA: 0x000C0C98 File Offset: 0x000BEE98
		public void Run()
		{
			if (Interlocked.CompareExchange(ref this.running, 1, 0) == 0)
			{
				Task.Run(() => this.RunScheduler());
			}
			this.schedulerEvent.Set();
		}

		// Token: 0x06003716 RID: 14102 RVA: 0x000C0CC8 File Offset: 0x000BEEC8
		private Task RunScheduler()
		{
			ServicePointScheduler.<RunScheduler>d__32 <RunScheduler>d__;
			<RunScheduler>d__.<>4__this = this;
			<RunScheduler>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RunScheduler>d__.<>1__state = -1;
			<RunScheduler>d__.<>t__builder.Start<ServicePointScheduler.<RunScheduler>d__32>(ref <RunScheduler>d__);
			return <RunScheduler>d__.<>t__builder.Task;
		}

		// Token: 0x06003717 RID: 14103 RVA: 0x000C0D0C File Offset: 0x000BEF0C
		private void Cleanup()
		{
			if (this.groups != null)
			{
				string[] array = new string[this.groups.Count];
				this.groups.Keys.CopyTo(array, 0);
				foreach (string key in array)
				{
					if (this.groups.ContainsKey(key) && this.groups[key].IsEmpty())
					{
						this.groups.Remove(key);
					}
				}
				if (this.groups.Count == 0)
				{
					this.groups = null;
				}
			}
		}

		// Token: 0x06003718 RID: 14104 RVA: 0x000C0D9C File Offset: 0x000BEF9C
		private void RunSchedulerIteration()
		{
			this.schedulerEvent.Reset();
			bool flag;
			do
			{
				flag = this.SchedulerIteration(this.defaultGroup);
				if (this.groups != null)
				{
					foreach (KeyValuePair<string, ServicePointScheduler.ConnectionGroup> keyValuePair in this.groups)
					{
						flag |= this.SchedulerIteration(keyValuePair.Value);
					}
				}
			}
			while (flag);
		}

		// Token: 0x06003719 RID: 14105 RVA: 0x000C0E1C File Offset: 0x000BF01C
		private bool OperationCompleted(ServicePointScheduler.ConnectionGroup group, WebOperation operation)
		{
			WebCompletionSource<ValueTuple<bool, WebOperation>>.Result currentResult = operation.Finished.CurrentResult;
			bool flag;
			WebOperation webOperation;
			if (!currentResult.Success)
			{
				flag = false;
				webOperation = null;
			}
			else
			{
				ValueTuple<bool, WebOperation> argument = currentResult.Argument;
				flag = argument.Item1;
				webOperation = argument.Item2;
			}
			if (!flag || !operation.Connection.Continue(webOperation))
			{
				group.RemoveConnection(operation.Connection);
				if (webOperation == null)
				{
					return true;
				}
				flag = false;
			}
			if (webOperation == null)
			{
				if (flag)
				{
					Task item = Task.Delay(this.MaxIdleTime);
					this.idleConnections.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>(group, operation.Connection, item));
				}
				return true;
			}
			this.operations.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>(group, webOperation));
			if (flag)
			{
				this.RemoveIdleConnection(operation.Connection);
				return false;
			}
			group.Cleanup();
			group.CreateOrReuseConnection(webOperation, true);
			return false;
		}

		// Token: 0x0600371A RID: 14106 RVA: 0x000C0EDF File Offset: 0x000BF0DF
		private void CloseIdleConnection(ServicePointScheduler.ConnectionGroup group, WebConnection connection)
		{
			group.RemoveConnection(connection);
			this.RemoveIdleConnection(connection);
		}

		// Token: 0x0600371B RID: 14107 RVA: 0x000C0EF0 File Offset: 0x000BF0F0
		private bool SchedulerIteration(ServicePointScheduler.ConnectionGroup group)
		{
			group.Cleanup();
			WebOperation nextOperation = group.GetNextOperation();
			if (nextOperation == null)
			{
				return false;
			}
			WebConnection item = group.CreateOrReuseConnection(nextOperation, false).Item1;
			if (item == null)
			{
				return false;
			}
			this.operations.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>(group, nextOperation));
			this.RemoveIdleConnection(item);
			return true;
		}

		// Token: 0x0600371C RID: 14108 RVA: 0x000C0F40 File Offset: 0x000BF140
		private void RemoveOperation(WebOperation operation)
		{
			LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> linkedListNode = this.operations.First;
			while (linkedListNode != null)
			{
				LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> linkedListNode2 = linkedListNode;
				linkedListNode = linkedListNode.Next;
				if (linkedListNode2.Value.Item2 == operation)
				{
					this.operations.Remove(linkedListNode2);
				}
			}
		}

		// Token: 0x0600371D RID: 14109 RVA: 0x000C0F84 File Offset: 0x000BF184
		private void RemoveIdleConnection(WebConnection connection)
		{
			LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> linkedListNode = this.idleConnections.First;
			while (linkedListNode != null)
			{
				LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> linkedListNode2 = linkedListNode;
				linkedListNode = linkedListNode.Next;
				if (linkedListNode2.Value.Item2 == connection)
				{
					this.idleConnections.Remove(linkedListNode2);
				}
			}
		}

		// Token: 0x0600371E RID: 14110 RVA: 0x000C0FC5 File Offset: 0x000BF1C5
		private void FinalCleanup()
		{
			this.groups = null;
			this.operations = null;
			this.idleConnections = null;
			this.defaultGroup = null;
			this.ServicePoint.FreeServicePoint();
			ServicePointManager.RemoveServicePoint(this.ServicePoint);
			this.ServicePoint = null;
		}

		// Token: 0x0600371F RID: 14111 RVA: 0x000C1000 File Offset: 0x000BF200
		public void SendRequest(WebOperation operation, string groupName)
		{
			ServicePoint servicePoint = this.ServicePoint;
			lock (servicePoint)
			{
				this.GetConnectionGroup(groupName).EnqueueOperation(operation);
				this.Run();
			}
		}

		// Token: 0x06003720 RID: 14112 RVA: 0x000C1050 File Offset: 0x000BF250
		public bool CloseConnectionGroup(string groupName)
		{
			ServicePointScheduler.ConnectionGroup connectionGroup;
			if (string.IsNullOrEmpty(groupName))
			{
				connectionGroup = this.defaultGroup;
			}
			else if (this.groups == null || !this.groups.TryGetValue(groupName, out connectionGroup))
			{
				return false;
			}
			if (connectionGroup != this.defaultGroup)
			{
				this.groups.Remove(groupName);
				if (this.groups.Count == 0)
				{
					this.groups = null;
				}
			}
			connectionGroup.Close();
			this.Run();
			return true;
		}

		// Token: 0x06003721 RID: 14113 RVA: 0x000C10C0 File Offset: 0x000BF2C0
		private ServicePointScheduler.ConnectionGroup GetConnectionGroup(string name)
		{
			ServicePoint servicePoint = this.ServicePoint;
			ServicePointScheduler.ConnectionGroup result;
			lock (servicePoint)
			{
				if (string.IsNullOrEmpty(name))
				{
					result = this.defaultGroup;
				}
				else
				{
					if (this.groups == null)
					{
						this.groups = new Dictionary<string, ServicePointScheduler.ConnectionGroup>();
					}
					ServicePointScheduler.ConnectionGroup connectionGroup;
					if (this.groups.TryGetValue(name, out connectionGroup))
					{
						result = connectionGroup;
					}
					else
					{
						connectionGroup = new ServicePointScheduler.ConnectionGroup(this, name);
						this.groups.Add(name, connectionGroup);
						result = connectionGroup;
					}
				}
			}
			return result;
		}

		// Token: 0x06003722 RID: 14114 RVA: 0x000C114C File Offset: 0x000BF34C
		private void OnConnectionCreated(WebConnection connection)
		{
			Interlocked.Increment(ref this.currentConnections);
		}

		// Token: 0x06003723 RID: 14115 RVA: 0x000C115A File Offset: 0x000BF35A
		private void OnConnectionClosed(WebConnection connection)
		{
			this.RemoveIdleConnection(connection);
			Interlocked.Decrement(ref this.currentConnections);
		}

		// Token: 0x06003724 RID: 14116 RVA: 0x000C1170 File Offset: 0x000BF370
		public static Task<bool> WaitAsync(Task workerTask, int millisecondTimeout)
		{
			ServicePointScheduler.<WaitAsync>d__46 <WaitAsync>d__;
			<WaitAsync>d__.workerTask = workerTask;
			<WaitAsync>d__.millisecondTimeout = millisecondTimeout;
			<WaitAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<WaitAsync>d__.<>1__state = -1;
			<WaitAsync>d__.<>t__builder.Start<ServicePointScheduler.<WaitAsync>d__46>(ref <WaitAsync>d__);
			return <WaitAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06003725 RID: 14117 RVA: 0x000C11BB File Offset: 0x000BF3BB
		[CompilerGenerated]
		private Task <Run>b__31_0()
		{
			return this.RunScheduler();
		}

		// Token: 0x0400200E RID: 8206
		[CompilerGenerated]
		private ServicePoint <ServicePoint>k__BackingField;

		// Token: 0x0400200F RID: 8207
		private int running;

		// Token: 0x04002010 RID: 8208
		private int maxIdleTime = 100000;

		// Token: 0x04002011 RID: 8209
		private ServicePointScheduler.AsyncManualResetEvent schedulerEvent;

		// Token: 0x04002012 RID: 8210
		private ServicePointScheduler.ConnectionGroup defaultGroup;

		// Token: 0x04002013 RID: 8211
		private Dictionary<string, ServicePointScheduler.ConnectionGroup> groups;

		// Token: 0x04002014 RID: 8212
		private LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> operations;

		// Token: 0x04002015 RID: 8213
		private LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> idleConnections;

		// Token: 0x04002016 RID: 8214
		private int currentConnections;

		// Token: 0x04002017 RID: 8215
		private int connectionLimit;

		// Token: 0x04002018 RID: 8216
		private DateTime idleSince;

		// Token: 0x04002019 RID: 8217
		private static int nextId;

		// Token: 0x0400201A RID: 8218
		public readonly int ID = ++ServicePointScheduler.nextId;

		// Token: 0x0400201B RID: 8219
		[CompilerGenerated]
		private readonly string <ME>k__BackingField;

		// Token: 0x020006B1 RID: 1713
		private class ConnectionGroup
		{
			// Token: 0x17000B82 RID: 2946
			// (get) Token: 0x06003726 RID: 14118 RVA: 0x000C11C3 File Offset: 0x000BF3C3
			public ServicePointScheduler Scheduler
			{
				[CompilerGenerated]
				get
				{
					return this.<Scheduler>k__BackingField;
				}
			}

			// Token: 0x17000B83 RID: 2947
			// (get) Token: 0x06003727 RID: 14119 RVA: 0x000C11CB File Offset: 0x000BF3CB
			public string Name
			{
				[CompilerGenerated]
				get
				{
					return this.<Name>k__BackingField;
				}
			}

			// Token: 0x17000B84 RID: 2948
			// (get) Token: 0x06003728 RID: 14120 RVA: 0x000C11D3 File Offset: 0x000BF3D3
			public bool IsDefault
			{
				get
				{
					return string.IsNullOrEmpty(this.Name);
				}
			}

			// Token: 0x06003729 RID: 14121 RVA: 0x000C11E0 File Offset: 0x000BF3E0
			public ConnectionGroup(ServicePointScheduler scheduler, string name)
			{
				this.Scheduler = scheduler;
				this.Name = name;
				this.connections = new LinkedList<WebConnection>();
				this.queue = new LinkedList<WebOperation>();
			}

			// Token: 0x0600372A RID: 14122 RVA: 0x000C121F File Offset: 0x000BF41F
			public bool IsEmpty()
			{
				return this.connections.Count == 0 && this.queue.Count == 0;
			}

			// Token: 0x0600372B RID: 14123 RVA: 0x000C123E File Offset: 0x000BF43E
			public void RemoveConnection(WebConnection connection)
			{
				this.connections.Remove(connection);
				connection.Dispose();
				this.Scheduler.OnConnectionClosed(connection);
			}

			// Token: 0x0600372C RID: 14124 RVA: 0x000C1260 File Offset: 0x000BF460
			public void Cleanup()
			{
				LinkedListNode<WebConnection> linkedListNode = this.connections.First;
				while (linkedListNode != null)
				{
					WebConnection value = linkedListNode.Value;
					LinkedListNode<WebConnection> node = linkedListNode;
					linkedListNode = linkedListNode.Next;
					if (value.Closed)
					{
						this.connections.Remove(node);
						this.Scheduler.OnConnectionClosed(value);
					}
				}
			}

			// Token: 0x0600372D RID: 14125 RVA: 0x000C12B0 File Offset: 0x000BF4B0
			public void Close()
			{
				foreach (WebOperation webOperation in this.queue)
				{
					webOperation.Abort();
					this.Scheduler.RemoveOperation(webOperation);
				}
				this.queue.Clear();
				foreach (WebConnection webConnection in this.connections)
				{
					webConnection.Dispose();
					this.Scheduler.OnConnectionClosed(webConnection);
				}
				this.connections.Clear();
			}

			// Token: 0x0600372E RID: 14126 RVA: 0x000C1374 File Offset: 0x000BF574
			public void EnqueueOperation(WebOperation operation)
			{
				this.queue.AddLast(operation);
			}

			// Token: 0x0600372F RID: 14127 RVA: 0x000C1384 File Offset: 0x000BF584
			public WebOperation GetNextOperation()
			{
				LinkedListNode<WebOperation> linkedListNode = this.queue.First;
				while (linkedListNode != null)
				{
					WebOperation value = linkedListNode.Value;
					LinkedListNode<WebOperation> node = linkedListNode;
					linkedListNode = linkedListNode.Next;
					if (!value.Aborted)
					{
						return value;
					}
					this.queue.Remove(node);
					this.Scheduler.RemoveOperation(value);
				}
				return null;
			}

			// Token: 0x06003730 RID: 14128 RVA: 0x000C13D8 File Offset: 0x000BF5D8
			public WebConnection FindIdleConnection(WebOperation operation)
			{
				WebConnection webConnection = null;
				foreach (WebConnection webConnection2 in this.connections)
				{
					if (webConnection2.CanReuseConnection(operation) && (webConnection == null || webConnection2.IdleSince > webConnection.IdleSince))
					{
						webConnection = webConnection2;
					}
				}
				if (webConnection != null && webConnection.StartOperation(operation, true))
				{
					this.queue.Remove(operation);
					return webConnection;
				}
				foreach (WebConnection webConnection3 in this.connections)
				{
					if (webConnection3.StartOperation(operation, true))
					{
						this.queue.Remove(operation);
						return webConnection3;
					}
				}
				return null;
			}

			// Token: 0x06003731 RID: 14129 RVA: 0x000C14C0 File Offset: 0x000BF6C0
			[return: TupleElementNames(new string[]
			{
				"connection",
				"created"
			})]
			public ValueTuple<WebConnection, bool> CreateOrReuseConnection(WebOperation operation, bool force)
			{
				WebConnection webConnection = this.FindIdleConnection(operation);
				if (webConnection != null)
				{
					return new ValueTuple<WebConnection, bool>(webConnection, false);
				}
				if (force || this.Scheduler.ServicePoint.ConnectionLimit > this.connections.Count || this.connections.Count == 0)
				{
					webConnection = new WebConnection(this.Scheduler.ServicePoint);
					webConnection.StartOperation(operation, false);
					this.connections.AddFirst(webConnection);
					this.Scheduler.OnConnectionCreated(webConnection);
					this.queue.Remove(operation);
					return new ValueTuple<WebConnection, bool>(webConnection, true);
				}
				return new ValueTuple<WebConnection, bool>(null, false);
			}

			// Token: 0x0400201C RID: 8220
			[CompilerGenerated]
			private readonly ServicePointScheduler <Scheduler>k__BackingField;

			// Token: 0x0400201D RID: 8221
			[CompilerGenerated]
			private readonly string <Name>k__BackingField;

			// Token: 0x0400201E RID: 8222
			private static int nextId;

			// Token: 0x0400201F RID: 8223
			public readonly int ID = ++ServicePointScheduler.ConnectionGroup.nextId;

			// Token: 0x04002020 RID: 8224
			private LinkedList<WebConnection> connections;

			// Token: 0x04002021 RID: 8225
			private LinkedList<WebOperation> queue;
		}

		// Token: 0x020006B2 RID: 1714
		private class AsyncManualResetEvent
		{
			// Token: 0x06003732 RID: 14130 RVA: 0x000C155C File Offset: 0x000BF75C
			public Task WaitAsync()
			{
				return this.m_tcs.Task;
			}

			// Token: 0x06003733 RID: 14131 RVA: 0x000C156B File Offset: 0x000BF76B
			public bool WaitOne(int millisecondTimeout)
			{
				return this.m_tcs.Task.Wait(millisecondTimeout);
			}

			// Token: 0x06003734 RID: 14132 RVA: 0x000C1580 File Offset: 0x000BF780
			public Task<bool> WaitAsync(int millisecondTimeout)
			{
				return ServicePointScheduler.WaitAsync(this.m_tcs.Task, millisecondTimeout);
			}

			// Token: 0x06003735 RID: 14133 RVA: 0x000C1598 File Offset: 0x000BF798
			public void Set()
			{
				TaskCompletionSource<bool> tcs = this.m_tcs;
				Task.Factory.StartNew<bool>((object s) => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
				tcs.Task.Wait();
			}

			// Token: 0x06003736 RID: 14134 RVA: 0x000C15F0 File Offset: 0x000BF7F0
			public void Reset()
			{
				TaskCompletionSource<bool> tcs;
				do
				{
					tcs = this.m_tcs;
				}
				while (tcs.Task.IsCompleted && Interlocked.CompareExchange<TaskCompletionSource<bool>>(ref this.m_tcs, new TaskCompletionSource<bool>(), tcs) != tcs);
			}

			// Token: 0x06003737 RID: 14135 RVA: 0x000C1627 File Offset: 0x000BF827
			public AsyncManualResetEvent(bool state)
			{
				if (state)
				{
					this.Set();
				}
			}

			// Token: 0x04002022 RID: 8226
			private volatile TaskCompletionSource<bool> m_tcs = new TaskCompletionSource<bool>();

			// Token: 0x020006B3 RID: 1715
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06003738 RID: 14136 RVA: 0x000C1645 File Offset: 0x000BF845
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06003739 RID: 14137 RVA: 0x0000219B File Offset: 0x0000039B
				public <>c()
				{
				}

				// Token: 0x0600373A RID: 14138 RVA: 0x000C1651 File Offset: 0x000BF851
				internal bool <Set>b__4_0(object s)
				{
					return ((TaskCompletionSource<bool>)s).TrySetResult(true);
				}

				// Token: 0x04002023 RID: 8227
				public static readonly ServicePointScheduler.AsyncManualResetEvent.<>c <>9 = new ServicePointScheduler.AsyncManualResetEvent.<>c();

				// Token: 0x04002024 RID: 8228
				public static Func<object, bool> <>9__4_0;
			}
		}

		// Token: 0x020006B4 RID: 1716
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <RunScheduler>d__32 : IAsyncStateMachine
		{
			// Token: 0x0600373B RID: 14139 RVA: 0x000C1660 File Offset: 0x000BF860
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				ServicePointScheduler servicePointScheduler = this.<>4__this;
				try
				{
					ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
						num = (this.<>1__state = -1);
						goto IL_208;
					}
					servicePointScheduler.idleSince = DateTime.UtcNow + TimeSpan.FromDays(3650.0);
					IL_32:
					this.<taskList>5__4 = new List<Task>();
					this.<finalCleanup>5__6 = false;
					ServicePoint servicePoint = servicePointScheduler.ServicePoint;
					bool flag = false;
					try
					{
						Monitor.Enter(servicePoint, ref flag);
						servicePointScheduler.Cleanup();
						this.<operationArray>5__2 = new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>[servicePointScheduler.operations.Count];
						servicePointScheduler.operations.CopyTo(this.<operationArray>5__2, 0);
						this.<idleArray>5__3 = new ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>[servicePointScheduler.idleConnections.Count];
						servicePointScheduler.idleConnections.CopyTo(this.<idleArray>5__3, 0);
						this.<schedulerTask>5__5 = servicePointScheduler.schedulerEvent.WaitAsync(servicePointScheduler.maxIdleTime);
						this.<taskList>5__4.Add(this.<schedulerTask>5__5);
						if (servicePointScheduler.groups == null && servicePointScheduler.defaultGroup.IsEmpty() && servicePointScheduler.operations.Count == 0 && servicePointScheduler.idleConnections.Count == 0)
						{
							servicePointScheduler.idleSince = DateTime.UtcNow;
							this.<finalCleanup>5__6 = true;
						}
						else
						{
							foreach (ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation> valueTuple in this.<operationArray>5__2)
							{
								this.<taskList>5__4.Add(valueTuple.Item2.Finished.Task);
							}
							foreach (ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task> valueTuple2 in this.<idleArray>5__3)
							{
								this.<taskList>5__4.Add(valueTuple2.Item3);
							}
						}
					}
					finally
					{
						if (num < 0 && flag)
						{
							Monitor.Exit(servicePoint);
						}
					}
					awaiter = Task.WhenAny(this.<taskList>5__4).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (this.<>1__state = 0);
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter, ServicePointScheduler.<RunScheduler>d__32>(ref awaiter, ref this);
						return;
					}
					IL_208:
					Task result = awaiter.GetResult();
					servicePoint = servicePointScheduler.ServicePoint;
					flag = false;
					try
					{
						Monitor.Enter(servicePoint, ref flag);
						bool flag2 = false;
						if (this.<finalCleanup>5__6)
						{
							if (!this.<schedulerTask>5__5.Result)
							{
								servicePointScheduler.FinalCleanup();
								goto IL_386;
							}
							flag2 = true;
						}
						else if (result == this.<taskList>5__4[0])
						{
							flag2 = true;
						}
						for (int j = 0; j < this.<operationArray>5__2.Length; j++)
						{
							ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation> valueTuple3 = this.<operationArray>5__2[j];
							if (valueTuple3.Item2.Finished.CurrentResult != null)
							{
								servicePointScheduler.operations.Remove(valueTuple3);
								bool flag3 = servicePointScheduler.OperationCompleted(valueTuple3.Item1, valueTuple3.Item2);
								flag2 = (flag2 || flag3);
							}
						}
						if (flag2)
						{
							servicePointScheduler.RunSchedulerIteration();
						}
						int num2 = -1;
						for (int k = 0; k < this.<idleArray>5__3.Length; k++)
						{
							if (result == this.<taskList>5__4[k + 1 + this.<operationArray>5__2.Length])
							{
								num2 = k;
								break;
							}
						}
						if (num2 >= 0)
						{
							ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task> valueTuple4 = this.<idleArray>5__3[num2];
							servicePointScheduler.idleConnections.Remove(valueTuple4);
							servicePointScheduler.CloseIdleConnection(valueTuple4.Item1, valueTuple4.Item2);
						}
					}
					finally
					{
						if (num < 0 && flag)
						{
							Monitor.Exit(servicePoint);
						}
					}
					this.<operationArray>5__2 = null;
					this.<idleArray>5__3 = null;
					this.<taskList>5__4 = null;
					this.<schedulerTask>5__5 = null;
					goto IL_32;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_386:
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600373C RID: 14140 RVA: 0x000C1A54 File Offset: 0x000BFC54
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04002025 RID: 8229
			public int <>1__state;

			// Token: 0x04002026 RID: 8230
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04002027 RID: 8231
			public ServicePointScheduler <>4__this;

			// Token: 0x04002028 RID: 8232
			private ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>[] <operationArray>5__2;

			// Token: 0x04002029 RID: 8233
			private ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>[] <idleArray>5__3;

			// Token: 0x0400202A RID: 8234
			private List<Task> <taskList>5__4;

			// Token: 0x0400202B RID: 8235
			private Task<bool> <schedulerTask>5__5;

			// Token: 0x0400202C RID: 8236
			private bool <finalCleanup>5__6;

			// Token: 0x0400202D RID: 8237
			private ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x020006B5 RID: 1717
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WaitAsync>d__46 : IAsyncStateMachine
		{
			// Token: 0x0600373D RID: 14141 RVA: 0x000C1A64 File Offset: 0x000BFC64
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					if (num != 0)
					{
						this.<cts>5__2 = new CancellationTokenSource();
					}
					try
					{
						ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter awaiter;
						if (num != 0)
						{
							this.<timeoutTask>5__3 = Task.Delay(this.millisecondTimeout, this.<cts>5__2.Token);
							awaiter = Task.WhenAny(new Task[]
							{
								this.workerTask,
								this.<timeoutTask>5__3
							}).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter, ServicePointScheduler.<WaitAsync>d__46>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
							num = (this.<>1__state = -1);
						}
						result = (awaiter.GetResult() != this.<timeoutTask>5__3);
					}
					finally
					{
						if (num < 0)
						{
							this.<cts>5__2.Cancel();
							this.<cts>5__2.Dispose();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<cts>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<cts>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600373E RID: 14142 RVA: 0x000C1BA4 File Offset: 0x000BFDA4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400202E RID: 8238
			public int <>1__state;

			// Token: 0x0400202F RID: 8239
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x04002030 RID: 8240
			public int millisecondTimeout;

			// Token: 0x04002031 RID: 8241
			public Task workerTask;

			// Token: 0x04002032 RID: 8242
			private CancellationTokenSource <cts>5__2;

			// Token: 0x04002033 RID: 8243
			private Task <timeoutTask>5__3;

			// Token: 0x04002034 RID: 8244
			private ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
