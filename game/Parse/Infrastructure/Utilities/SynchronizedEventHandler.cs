using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x0200005A RID: 90
	public class SynchronizedEventHandler<T>
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000DA9E File Offset: 0x0000BC9E
		private LinkedList<Tuple<Delegate, TaskFactory>> Callbacks
		{
			[CompilerGenerated]
			get
			{
				return this.<Callbacks>k__BackingField;
			}
		} = new LinkedList<Tuple<Delegate, TaskFactory>>();

		// Token: 0x0600044F RID: 1103 RVA: 0x0000DAA8 File Offset: 0x0000BCA8
		public void Add(Delegate target)
		{
			LinkedList<Tuple<Delegate, TaskFactory>> callbacks = this.Callbacks;
			lock (callbacks)
			{
				TaskFactory item = (SynchronizationContext.Current != null) ? new TaskFactory(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.FromCurrentSynchronizationContext()) : Task.Factory;
				foreach (Delegate item2 in target.GetInvocationList())
				{
					this.Callbacks.AddLast(new Tuple<Delegate, TaskFactory>(item2, item));
				}
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		public void Remove(Delegate target)
		{
			LinkedList<Tuple<Delegate, TaskFactory>> callbacks = this.Callbacks;
			lock (callbacks)
			{
				if (this.Callbacks.Count != 0)
				{
					foreach (Delegate d in target.GetInvocationList())
					{
						for (LinkedListNode<Tuple<Delegate, TaskFactory>> linkedListNode = this.Callbacks.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
						{
							if (linkedListNode.Value.Item1 == d)
							{
								this.Callbacks.Remove(linkedListNode);
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		public Task Invoke(object sender, T args)
		{
			Task<int>[] toContinue = new Task<int>[]
			{
				Task.FromResult<int>(0)
			};
			LinkedList<Tuple<Delegate, TaskFactory>> callbacks = this.Callbacks;
			IEnumerable<Tuple<Delegate, TaskFactory>> source;
			lock (callbacks)
			{
				source = this.Callbacks.ToList<Tuple<Delegate, TaskFactory>>();
			}
			return Task.WhenAll<object>((from callback in source
			select callback.Item2.ContinueWhenAll<int, object>(toContinue, (Task<int>[] _) => callback.Item1.DynamicInvoke(new object[]
			{
				sender,
				args
			}))).ToList<Task<object>>());
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		public SynchronizedEventHandler()
		{
		}

		// Token: 0x040000DB RID: 219
		[CompilerGenerated]
		private readonly LinkedList<Tuple<Delegate, TaskFactory>> <Callbacks>k__BackingField;

		// Token: 0x0200012C RID: 300
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0
		{
			// Token: 0x06000797 RID: 1943 RVA: 0x0001709B File Offset: 0x0001529B
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x06000798 RID: 1944 RVA: 0x000170A4 File Offset: 0x000152A4
			internal Task<object> <Invoke>b__0(Tuple<Delegate, TaskFactory> callback)
			{
				SynchronizedEventHandler<T>.<>c__DisplayClass5_1 CS$<>8__locals1 = new SynchronizedEventHandler<T>.<>c__DisplayClass5_1();
				CS$<>8__locals1.CS$<>8__locals1 = this;
				CS$<>8__locals1.callback = callback;
				return CS$<>8__locals1.callback.Item2.ContinueWhenAll<int, object>(this.toContinue, new Func<Task<int>[], object>(CS$<>8__locals1.<Invoke>b__1));
			}

			// Token: 0x040002B9 RID: 697
			public Task<int>[] toContinue;

			// Token: 0x040002BA RID: 698
			public object sender;

			// Token: 0x040002BB RID: 699
			public T args;
		}

		// Token: 0x0200012D RID: 301
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_1
		{
			// Token: 0x06000799 RID: 1945 RVA: 0x000170E7 File Offset: 0x000152E7
			public <>c__DisplayClass5_1()
			{
			}

			// Token: 0x0600079A RID: 1946 RVA: 0x000170EF File Offset: 0x000152EF
			internal object <Invoke>b__1(Task<int>[] _)
			{
				return this.callback.Item1.DynamicInvoke(new object[]
				{
					this.CS$<>8__locals1.sender,
					this.CS$<>8__locals1.args
				});
			}

			// Token: 0x040002BC RID: 700
			public Tuple<Delegate, TaskFactory> callback;

			// Token: 0x040002BD RID: 701
			public SynchronizedEventHandler<T>.<>c__DisplayClass5_0 CS$<>8__locals1;
		}
	}
}
