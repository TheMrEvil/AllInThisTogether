using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x0200005B RID: 91
	public class TaskQueue
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000DC7F File Offset: 0x0000BE7F
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0000DC87 File Offset: 0x0000BE87
		private Task Tail
		{
			[CompilerGenerated]
			get
			{
				return this.<Tail>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tail>k__BackingField = value;
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000DC90 File Offset: 0x0000BE90
		private Task GetTaskToAwait(CancellationToken cancellationToken)
		{
			object mutex = this.Mutex;
			Task result;
			lock (mutex)
			{
				result = (this.Tail ?? Task.FromResult<bool>(true)).ContinueWith(delegate(Task task)
				{
				}, cancellationToken);
			}
			return result;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000DD04 File Offset: 0x0000BF04
		public T Enqueue<T>(Func<Task, T> taskStart, CancellationToken cancellationToken = default(CancellationToken)) where T : Task
		{
			object mutex = this.Mutex;
			T t;
			lock (mutex)
			{
				Task task = this.Tail ?? Task.FromResult<bool>(true);
				t = taskStart(this.GetTaskToAwait(cancellationToken));
				this.Tail = Task.WhenAll(new Task[]
				{
					task,
					t
				});
			}
			return t;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		public object Mutex
		{
			[CompilerGenerated]
			get
			{
				return this.<Mutex>k__BackingField;
			}
		} = new object();

		// Token: 0x06000458 RID: 1112 RVA: 0x0000DD84 File Offset: 0x0000BF84
		public TaskQueue()
		{
		}

		// Token: 0x040000DC RID: 220
		[CompilerGenerated]
		private Task <Tail>k__BackingField;

		// Token: 0x040000DD RID: 221
		[CompilerGenerated]
		private readonly object <Mutex>k__BackingField;

		// Token: 0x0200012E RID: 302
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600079B RID: 1947 RVA: 0x00017128 File Offset: 0x00015328
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600079C RID: 1948 RVA: 0x00017134 File Offset: 0x00015334
			public <>c()
			{
			}

			// Token: 0x0600079D RID: 1949 RVA: 0x0001713C File Offset: 0x0001533C
			internal void <GetTaskToAwait>b__4_0(Task task)
			{
			}

			// Token: 0x040002BE RID: 702
			public static readonly TaskQueue.<>c <>9 = new TaskQueue.<>c();

			// Token: 0x040002BF RID: 703
			public static Action<Task> <>9__4_0;
		}
	}
}
