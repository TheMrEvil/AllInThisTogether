using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x02000055 RID: 85
	public static class InternalExtensions
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x0000D14C File Offset: 0x0000B34C
		public static Task<T> Safe<T>(this Task<T> task)
		{
			return task ?? Task.FromResult<T>(default(T));
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000D16C File Offset: 0x0000B36C
		public static Task Safe(this Task task)
		{
			return task ?? Task.FromResult<object>(null);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000D17C File Offset: 0x0000B37C
		public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, TValue defaultValue)
		{
			TValue result;
			if (self.TryGetValue(key, out result))
			{
				return result;
			}
			return defaultValue;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000D197 File Offset: 0x0000B397
		public static bool CollectionsEqual<T>(this IEnumerable<T> a, IEnumerable<T> b)
		{
			return object.Equals(a, b) || (a != null && b != null && a.SequenceEqual(b));
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000D1B4 File Offset: 0x0000B3B4
		public static Task<TResult> OnSuccess<TIn, TResult>(this Task<TIn> task, Func<Task<TIn>, TResult> continuation)
		{
			return task.OnSuccess((Task t) => continuation((Task<TIn>)t));
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		public static Task OnSuccess<TIn>(this Task<TIn> task, Action<Task<TIn>> continuation)
		{
			return task.OnSuccess(delegate(Task<TIn> t)
			{
				continuation(t);
				return null;
			});
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000D20C File Offset: 0x0000B40C
		public static Task<TResult> OnSuccess<TResult>(this Task task, Func<Task, TResult> continuation)
		{
			return task.ContinueWith<Task<TResult>>(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					AggregateException ex = t.Exception.Flatten();
					if (ex.InnerExceptions.Count == 1)
					{
						ExceptionDispatchInfo.Capture(ex.InnerExceptions[0]).Throw();
					}
					else
					{
						ExceptionDispatchInfo.Capture(ex).Throw();
					}
					return Task.FromResult<TResult>(default(TResult));
				}
				if (t.IsCanceled)
				{
					TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
					taskCompletionSource.SetCanceled();
					return taskCompletionSource.Task;
				}
				return Task.FromResult<TResult>(continuation(t));
			}).Unwrap<TResult>();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000D240 File Offset: 0x0000B440
		public static Task OnSuccess(this Task task, Action<Task> continuation)
		{
			return task.OnSuccess(delegate(Task t)
			{
				continuation(t);
				return null;
			});
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000D26C File Offset: 0x0000B46C
		public static Task WhileAsync(Func<Task<bool>> predicate, Func<Task> body)
		{
			Func<Task> iterate = null;
			Func<Task, Task> <>9__2;
			Func<Task<bool>, Task> <>9__1;
			iterate = delegate()
			{
				Task<bool> task = predicate();
				Func<Task<bool>, Task> continuation;
				if ((continuation = <>9__1) == null)
				{
					continuation = (<>9__1 = delegate(Task<bool> t)
					{
						if (!t.Result)
						{
							return Task.FromResult<int>(0);
						}
						Task task2 = body();
						Func<Task, Task> continuation2;
						if ((continuation2 = <>9__2) == null)
						{
							continuation2 = (<>9__2 = ((Task _) => iterate()));
						}
						return task2.OnSuccess(continuation2).Unwrap();
					});
				}
				return task.OnSuccess(continuation).Unwrap();
			};
			return iterate();
		}

		// Token: 0x02000120 RID: 288
		// (Invoke) Token: 0x06000768 RID: 1896
		public delegate void PartialAccessor<T>(ref T arg);

		// Token: 0x02000121 RID: 289
		[CompilerGenerated]
		private sealed class <>c__DisplayClass5_0<TIn, TResult>
		{
			// Token: 0x0600076B RID: 1899 RVA: 0x0001680D File Offset: 0x00014A0D
			public <>c__DisplayClass5_0()
			{
			}

			// Token: 0x0600076C RID: 1900 RVA: 0x00016815 File Offset: 0x00014A15
			internal TResult <OnSuccess>b__0(Task t)
			{
				return this.continuation((Task<TIn>)t);
			}

			// Token: 0x040002A3 RID: 675
			public Func<Task<TIn>, TResult> continuation;
		}

		// Token: 0x02000122 RID: 290
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0<TIn>
		{
			// Token: 0x0600076D RID: 1901 RVA: 0x00016828 File Offset: 0x00014A28
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x0600076E RID: 1902 RVA: 0x00016830 File Offset: 0x00014A30
			internal object <OnSuccess>b__0(Task<TIn> t)
			{
				this.continuation(t);
				return null;
			}

			// Token: 0x040002A4 RID: 676
			public Action<Task<TIn>> continuation;
		}

		// Token: 0x02000123 RID: 291
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0<TResult>
		{
			// Token: 0x0600076F RID: 1903 RVA: 0x0001683F File Offset: 0x00014A3F
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06000770 RID: 1904 RVA: 0x00016848 File Offset: 0x00014A48
			internal Task<TResult> <OnSuccess>b__0(Task t)
			{
				if (t.IsFaulted)
				{
					AggregateException ex = t.Exception.Flatten();
					if (ex.InnerExceptions.Count == 1)
					{
						ExceptionDispatchInfo.Capture(ex.InnerExceptions[0]).Throw();
					}
					else
					{
						ExceptionDispatchInfo.Capture(ex).Throw();
					}
					return Task.FromResult<TResult>(default(TResult));
				}
				if (t.IsCanceled)
				{
					TaskCompletionSource<TResult> taskCompletionSource = new TaskCompletionSource<TResult>();
					taskCompletionSource.SetCanceled();
					return taskCompletionSource.Task;
				}
				return Task.FromResult<TResult>(this.continuation(t));
			}

			// Token: 0x040002A5 RID: 677
			public Func<Task, TResult> continuation;
		}

		// Token: 0x02000124 RID: 292
		[CompilerGenerated]
		private sealed class <>c__DisplayClass8_0
		{
			// Token: 0x06000771 RID: 1905 RVA: 0x000168D3 File Offset: 0x00014AD3
			public <>c__DisplayClass8_0()
			{
			}

			// Token: 0x06000772 RID: 1906 RVA: 0x000168DB File Offset: 0x00014ADB
			internal object <OnSuccess>b__0(Task t)
			{
				this.continuation(t);
				return null;
			}

			// Token: 0x040002A6 RID: 678
			public Action<Task> continuation;
		}

		// Token: 0x02000125 RID: 293
		[CompilerGenerated]
		private sealed class <>c__DisplayClass9_0
		{
			// Token: 0x06000773 RID: 1907 RVA: 0x000168EA File Offset: 0x00014AEA
			public <>c__DisplayClass9_0()
			{
			}

			// Token: 0x06000774 RID: 1908 RVA: 0x000168F4 File Offset: 0x00014AF4
			internal Task <WhileAsync>b__0()
			{
				Task<bool> task = this.predicate();
				Func<Task<bool>, Task> continuation;
				if ((continuation = this.<>9__1) == null)
				{
					continuation = (this.<>9__1 = delegate(Task<bool> t)
					{
						if (!t.Result)
						{
							return Task.FromResult<int>(0);
						}
						Task task2 = this.body();
						Func<Task, Task> continuation2;
						if ((continuation2 = this.<>9__2) == null)
						{
							continuation2 = (this.<>9__2 = ((Task _) => this.iterate()));
						}
						return task2.OnSuccess(continuation2).Unwrap();
					});
				}
				return task.OnSuccess(continuation).Unwrap();
			}

			// Token: 0x06000775 RID: 1909 RVA: 0x00016938 File Offset: 0x00014B38
			internal Task <WhileAsync>b__1(Task<bool> t)
			{
				if (!t.Result)
				{
					return Task.FromResult<int>(0);
				}
				Task task = this.body();
				Func<Task, Task> continuation;
				if ((continuation = this.<>9__2) == null)
				{
					continuation = (this.<>9__2 = ((Task _) => this.iterate()));
				}
				return task.OnSuccess(continuation).Unwrap();
			}

			// Token: 0x06000776 RID: 1910 RVA: 0x00016988 File Offset: 0x00014B88
			internal Task <WhileAsync>b__2(Task _)
			{
				return this.iterate();
			}

			// Token: 0x040002A7 RID: 679
			public Func<Task<bool>> predicate;

			// Token: 0x040002A8 RID: 680
			public Func<Task> body;

			// Token: 0x040002A9 RID: 681
			public Func<Task> iterate;

			// Token: 0x040002AA RID: 682
			public Func<Task, Task> <>9__2;

			// Token: 0x040002AB RID: 683
			public Func<Task<bool>, Task> <>9__1;
		}
	}
}
