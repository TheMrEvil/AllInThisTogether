using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Linq.Expressions
{
	// Token: 0x02000296 RID: 662
	internal sealed class StackGuard
	{
		// Token: 0x0600131A RID: 4890 RVA: 0x0003C7A6 File Offset: 0x0003A9A6
		public bool TryEnterOnCurrentStack()
		{
			if (RuntimeHelpers.TryEnsureSufficientExecutionStack())
			{
				return true;
			}
			if (this._executionStackCount < 1024)
			{
				return false;
			}
			throw new InsufficientExecutionStackException();
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0003C7C5 File Offset: 0x0003A9C5
		public void RunOnEmptyStack<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
		{
			this.RunOnEmptyStackCore<object>(delegate(object s)
			{
				Tuple<Action<T1, T2>, T1, T2> tuple = (Tuple<Action<T1, T2>, T1, T2>)s;
				tuple.Item1(tuple.Item2, tuple.Item3);
				return null;
			}, Tuple.Create<Action<T1, T2>, T1, T2>(action, arg1, arg2));
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0003C7F5 File Offset: 0x0003A9F5
		public void RunOnEmptyStack<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3)
		{
			this.RunOnEmptyStackCore<object>(delegate(object s)
			{
				Tuple<Action<T1, T2, T3>, T1, T2, T3> tuple = (Tuple<Action<T1, T2, T3>, T1, T2, T3>)s;
				tuple.Item1(tuple.Item2, tuple.Item3, tuple.Item4);
				return null;
			}, Tuple.Create<Action<T1, T2, T3>, T1, T2, T3>(action, arg1, arg2, arg3));
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0003C827 File Offset: 0x0003AA27
		public R RunOnEmptyStack<T1, T2, R>(Func<T1, T2, R> action, T1 arg1, T2 arg2)
		{
			return this.RunOnEmptyStackCore<R>(delegate(object s)
			{
				Tuple<Func<T1, T2, R>, T1, T2> tuple = (Tuple<Func<T1, T2, R>, T1, T2>)s;
				return tuple.Item1(tuple.Item2, tuple.Item3);
			}, Tuple.Create<Func<T1, T2, R>, T1, T2>(action, arg1, arg2));
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0003C856 File Offset: 0x0003AA56
		public R RunOnEmptyStack<T1, T2, T3, R>(Func<T1, T2, T3, R> action, T1 arg1, T2 arg2, T3 arg3)
		{
			return this.RunOnEmptyStackCore<R>(delegate(object s)
			{
				Tuple<Func<T1, T2, T3, R>, T1, T2, T3> tuple = (Tuple<Func<T1, T2, T3, R>, T1, T2, T3>)s;
				return tuple.Item1(tuple.Item2, tuple.Item3, tuple.Item4);
			}, Tuple.Create<Func<T1, T2, T3, R>, T1, T2, T3>(action, arg1, arg2, arg3));
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0003C888 File Offset: 0x0003AA88
		private R RunOnEmptyStackCore<R>(Func<object, R> action, object state)
		{
			this._executionStackCount++;
			R result;
			try
			{
				Task<R> task = Task.Factory.StartNew<R>(action, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
				TaskAwaiter<R> awaiter = task.GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					((IAsyncResult)task).AsyncWaitHandle.WaitOne();
				}
				result = awaiter.GetResult();
			}
			finally
			{
				this._executionStackCount--;
			}
			return result;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00002162 File Offset: 0x00000362
		public StackGuard()
		{
		}

		// Token: 0x04000A4C RID: 2636
		private const int MaxExecutionStackCount = 1024;

		// Token: 0x04000A4D RID: 2637
		private int _executionStackCount;

		// Token: 0x02000297 RID: 663
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__3<T1, T2>
		{
			// Token: 0x06001321 RID: 4897 RVA: 0x0003C904 File Offset: 0x0003AB04
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__3()
			{
			}

			// Token: 0x06001322 RID: 4898 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__3()
			{
			}

			// Token: 0x06001323 RID: 4899 RVA: 0x0003C910 File Offset: 0x0003AB10
			internal object <RunOnEmptyStack>b__3_0(object s)
			{
				Tuple<Action<T1, T2>, T1, T2> tuple = (Tuple<Action<T1, T2>, T1, T2>)s;
				tuple.Item1(tuple.Item2, tuple.Item3);
				return null;
			}

			// Token: 0x04000A4E RID: 2638
			public static readonly StackGuard.<>c__3<T1, T2> <>9 = new StackGuard.<>c__3<T1, T2>();

			// Token: 0x04000A4F RID: 2639
			public static Func<object, object> <>9__3_0;
		}

		// Token: 0x02000298 RID: 664
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__4<T1, T2, T3>
		{
			// Token: 0x06001324 RID: 4900 RVA: 0x0003C93C File Offset: 0x0003AB3C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__4()
			{
			}

			// Token: 0x06001325 RID: 4901 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__4()
			{
			}

			// Token: 0x06001326 RID: 4902 RVA: 0x0003C948 File Offset: 0x0003AB48
			internal object <RunOnEmptyStack>b__4_0(object s)
			{
				Tuple<Action<T1, T2, T3>, T1, T2, T3> tuple = (Tuple<Action<T1, T2, T3>, T1, T2, T3>)s;
				tuple.Item1(tuple.Item2, tuple.Item3, tuple.Item4);
				return null;
			}

			// Token: 0x04000A50 RID: 2640
			public static readonly StackGuard.<>c__4<T1, T2, T3> <>9 = new StackGuard.<>c__4<T1, T2, T3>();

			// Token: 0x04000A51 RID: 2641
			public static Func<object, object> <>9__4_0;
		}

		// Token: 0x02000299 RID: 665
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__5<T1, T2, R>
		{
			// Token: 0x06001327 RID: 4903 RVA: 0x0003C97A File Offset: 0x0003AB7A
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__5()
			{
			}

			// Token: 0x06001328 RID: 4904 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__5()
			{
			}

			// Token: 0x06001329 RID: 4905 RVA: 0x0003C988 File Offset: 0x0003AB88
			internal R <RunOnEmptyStack>b__5_0(object s)
			{
				Tuple<Func<T1, T2, R>, T1, T2> tuple = (Tuple<Func<T1, T2, R>, T1, T2>)s;
				return tuple.Item1(tuple.Item2, tuple.Item3);
			}

			// Token: 0x04000A52 RID: 2642
			public static readonly StackGuard.<>c__5<T1, T2, R> <>9 = new StackGuard.<>c__5<T1, T2, R>();

			// Token: 0x04000A53 RID: 2643
			public static Func<object, R> <>9__5_0;
		}

		// Token: 0x0200029A RID: 666
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__6<T1, T2, T3, R>
		{
			// Token: 0x0600132A RID: 4906 RVA: 0x0003C9B3 File Offset: 0x0003ABB3
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__6()
			{
			}

			// Token: 0x0600132B RID: 4907 RVA: 0x00002162 File Offset: 0x00000362
			public <>c__6()
			{
			}

			// Token: 0x0600132C RID: 4908 RVA: 0x0003C9C0 File Offset: 0x0003ABC0
			internal R <RunOnEmptyStack>b__6_0(object s)
			{
				Tuple<Func<T1, T2, T3, R>, T1, T2, T3> tuple = (Tuple<Func<T1, T2, T3, R>, T1, T2, T3>)s;
				return tuple.Item1(tuple.Item2, tuple.Item3, tuple.Item4);
			}

			// Token: 0x04000A54 RID: 2644
			public static readonly StackGuard.<>c__6<T1, T2, T3, R> <>9 = new StackGuard.<>c__6<T1, T2, T3, R>();

			// Token: 0x04000A55 RID: 2645
			public static Func<object, R> <>9__6_0;
		}
	}
}
