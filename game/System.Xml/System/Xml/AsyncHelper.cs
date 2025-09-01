using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200000E RID: 14
	internal static class AsyncHelper
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000022CC File Offset: 0x000004CC
		public static bool IsSuccess(this Task task)
		{
			return task.IsCompleted && task.Exception == null;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022E1 File Offset: 0x000004E1
		public static Task CallVoidFuncWhenFinish(this Task task, Action func)
		{
			if (task.IsSuccess())
			{
				func();
				return AsyncHelper.DoneTask;
			}
			return task._CallVoidFuncWhenFinish(func);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002300 File Offset: 0x00000500
		private static Task _CallVoidFuncWhenFinish(this Task task, Action func)
		{
			AsyncHelper.<_CallVoidFuncWhenFinish>d__6 <_CallVoidFuncWhenFinish>d__;
			<_CallVoidFuncWhenFinish>d__.task = task;
			<_CallVoidFuncWhenFinish>d__.func = func;
			<_CallVoidFuncWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_CallVoidFuncWhenFinish>d__.<>1__state = -1;
			<_CallVoidFuncWhenFinish>d__.<>t__builder.Start<AsyncHelper.<_CallVoidFuncWhenFinish>d__6>(ref <_CallVoidFuncWhenFinish>d__);
			return <_CallVoidFuncWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000234B File Offset: 0x0000054B
		public static Task<bool> ReturnTaskBoolWhenFinish(this Task task, bool ret)
		{
			if (!task.IsSuccess())
			{
				return task._ReturnTaskBoolWhenFinish(ret);
			}
			if (ret)
			{
				return AsyncHelper.DoneTaskTrue;
			}
			return AsyncHelper.DoneTaskFalse;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000236C File Offset: 0x0000056C
		public static Task<bool> _ReturnTaskBoolWhenFinish(this Task task, bool ret)
		{
			AsyncHelper.<_ReturnTaskBoolWhenFinish>d__8 <_ReturnTaskBoolWhenFinish>d__;
			<_ReturnTaskBoolWhenFinish>d__.task = task;
			<_ReturnTaskBoolWhenFinish>d__.ret = ret;
			<_ReturnTaskBoolWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ReturnTaskBoolWhenFinish>d__.<>1__state = -1;
			<_ReturnTaskBoolWhenFinish>d__.<>t__builder.Start<AsyncHelper.<_ReturnTaskBoolWhenFinish>d__8>(ref <_ReturnTaskBoolWhenFinish>d__);
			return <_ReturnTaskBoolWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000023B7 File Offset: 0x000005B7
		public static Task CallTaskFuncWhenFinish(this Task task, Func<Task> func)
		{
			if (task.IsSuccess())
			{
				return func();
			}
			return AsyncHelper._CallTaskFuncWhenFinish(task, func);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023D0 File Offset: 0x000005D0
		private static Task _CallTaskFuncWhenFinish(Task task, Func<Task> func)
		{
			AsyncHelper.<_CallTaskFuncWhenFinish>d__10 <_CallTaskFuncWhenFinish>d__;
			<_CallTaskFuncWhenFinish>d__.task = task;
			<_CallTaskFuncWhenFinish>d__.func = func;
			<_CallTaskFuncWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<_CallTaskFuncWhenFinish>d__.<>1__state = -1;
			<_CallTaskFuncWhenFinish>d__.<>t__builder.Start<AsyncHelper.<_CallTaskFuncWhenFinish>d__10>(ref <_CallTaskFuncWhenFinish>d__);
			return <_CallTaskFuncWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000241B File Offset: 0x0000061B
		public static Task<bool> CallBoolTaskFuncWhenFinish(this Task task, Func<Task<bool>> func)
		{
			if (task.IsSuccess())
			{
				return func();
			}
			return task._CallBoolTaskFuncWhenFinish(func);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002434 File Offset: 0x00000634
		private static Task<bool> _CallBoolTaskFuncWhenFinish(this Task task, Func<Task<bool>> func)
		{
			AsyncHelper.<_CallBoolTaskFuncWhenFinish>d__12 <_CallBoolTaskFuncWhenFinish>d__;
			<_CallBoolTaskFuncWhenFinish>d__.task = task;
			<_CallBoolTaskFuncWhenFinish>d__.func = func;
			<_CallBoolTaskFuncWhenFinish>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_CallBoolTaskFuncWhenFinish>d__.<>1__state = -1;
			<_CallBoolTaskFuncWhenFinish>d__.<>t__builder.Start<AsyncHelper.<_CallBoolTaskFuncWhenFinish>d__12>(ref <_CallBoolTaskFuncWhenFinish>d__);
			return <_CallBoolTaskFuncWhenFinish>d__.<>t__builder.Task;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000247F File Offset: 0x0000067F
		public static Task<bool> ContinueBoolTaskFuncWhenFalse(this Task<bool> task, Func<Task<bool>> func)
		{
			if (!task.IsSuccess())
			{
				return AsyncHelper._ContinueBoolTaskFuncWhenFalse(task, func);
			}
			if (task.Result)
			{
				return AsyncHelper.DoneTaskTrue;
			}
			return func();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024A8 File Offset: 0x000006A8
		private static Task<bool> _ContinueBoolTaskFuncWhenFalse(Task<bool> task, Func<Task<bool>> func)
		{
			AsyncHelper.<_ContinueBoolTaskFuncWhenFalse>d__14 <_ContinueBoolTaskFuncWhenFalse>d__;
			<_ContinueBoolTaskFuncWhenFalse>d__.task = task;
			<_ContinueBoolTaskFuncWhenFalse>d__.func = func;
			<_ContinueBoolTaskFuncWhenFalse>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<_ContinueBoolTaskFuncWhenFalse>d__.<>1__state = -1;
			<_ContinueBoolTaskFuncWhenFalse>d__.<>t__builder.Start<AsyncHelper.<_ContinueBoolTaskFuncWhenFalse>d__14>(ref <_ContinueBoolTaskFuncWhenFalse>d__);
			return <_ContinueBoolTaskFuncWhenFalse>d__.<>t__builder.Task;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000024F3 File Offset: 0x000006F3
		// Note: this type is marked as 'beforefieldinit'.
		static AsyncHelper()
		{
		}

		// Token: 0x040004C3 RID: 1219
		public static readonly Task DoneTask = Task.FromResult<bool>(true);

		// Token: 0x040004C4 RID: 1220
		public static readonly Task<bool> DoneTaskTrue = Task.FromResult<bool>(true);

		// Token: 0x040004C5 RID: 1221
		public static readonly Task<bool> DoneTaskFalse = Task.FromResult<bool>(false);

		// Token: 0x040004C6 RID: 1222
		public static readonly Task<int> DoneTaskZero = Task.FromResult<int>(0);

		// Token: 0x0200000F RID: 15
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_CallVoidFuncWhenFinish>d__6 : IAsyncStateMachine
		{
			// Token: 0x06000029 RID: 41 RVA: 0x00002524 File Offset: 0x00000724
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, AsyncHelper.<_CallVoidFuncWhenFinish>d__6>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					this.func();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600002A RID: 42 RVA: 0x000025E4 File Offset: 0x000007E4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004C7 RID: 1223
			public int <>1__state;

			// Token: 0x040004C8 RID: 1224
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040004C9 RID: 1225
			public Task task;

			// Token: 0x040004CA RID: 1226
			public Action func;

			// Token: 0x040004CB RID: 1227
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000010 RID: 16
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_ReturnTaskBoolWhenFinish>d__8 : IAsyncStateMachine
		{
			// Token: 0x0600002B RID: 43 RVA: 0x000025F4 File Offset: 0x000007F4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, AsyncHelper.<_ReturnTaskBoolWhenFinish>d__8>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					result = this.ret;
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600002C RID: 44 RVA: 0x000026B4 File Offset: 0x000008B4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004CC RID: 1228
			public int <>1__state;

			// Token: 0x040004CD RID: 1229
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x040004CE RID: 1230
			public Task task;

			// Token: 0x040004CF RID: 1231
			public bool ret;

			// Token: 0x040004D0 RID: 1232
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000011 RID: 17
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_CallTaskFuncWhenFinish>d__10 : IAsyncStateMachine
		{
			// Token: 0x0600002D RID: 45 RVA: 0x000026C4 File Offset: 0x000008C4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_D4;
						}
						awaiter = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, AsyncHelper.<_CallTaskFuncWhenFinish>d__10>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter.GetResult();
					awaiter = this.func().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, AsyncHelper.<_CallTaskFuncWhenFinish>d__10>(ref awaiter, ref this);
						return;
					}
					IL_D4:
					awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult();
			}

			// Token: 0x0600002E RID: 46 RVA: 0x000027E8 File Offset: 0x000009E8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004D1 RID: 1233
			public int <>1__state;

			// Token: 0x040004D2 RID: 1234
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x040004D3 RID: 1235
			public Task task;

			// Token: 0x040004D4 RID: 1236
			public Func<Task> func;

			// Token: 0x040004D5 RID: 1237
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;
		}

		// Token: 0x02000012 RID: 18
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_CallBoolTaskFuncWhenFinish>d__12 : IAsyncStateMachine
		{
			// Token: 0x0600002F RID: 47 RVA: 0x000027F8 File Offset: 0x000009F8
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__2;
							this.<>u__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_D8;
						}
						awaiter2 = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter2;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable.ConfiguredTaskAwaiter, AsyncHelper.<_CallBoolTaskFuncWhenFinish>d__12>(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					awaiter2.GetResult();
					awaiter = this.func().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__2 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, AsyncHelper.<_CallBoolTaskFuncWhenFinish>d__12>(ref awaiter, ref this);
						return;
					}
					IL_D8:
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000030 RID: 48 RVA: 0x00002924 File Offset: 0x00000B24
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004D6 RID: 1238
			public int <>1__state;

			// Token: 0x040004D7 RID: 1239
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x040004D8 RID: 1240
			public Task task;

			// Token: 0x040004D9 RID: 1241
			public Func<Task<bool>> func;

			// Token: 0x040004DA RID: 1242
			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;

			// Token: 0x040004DB RID: 1243
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__2;
		}

		// Token: 0x02000013 RID: 19
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <_ContinueBoolTaskFuncWhenFalse>d__14 : IAsyncStateMachine
		{
			// Token: 0x06000031 RID: 49 RVA: 0x00002934 File Offset: 0x00000B34
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							this.<>1__state = -1;
							goto IL_DD;
						}
						awaiter = this.task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, AsyncHelper.<_ContinueBoolTaskFuncWhenFalse>d__14>(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = this.<>u__1;
						this.<>u__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						this.<>1__state = -1;
					}
					if (awaiter.GetResult())
					{
						result = true;
						goto IL_100;
					}
					awaiter = this.func().ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						this.<>1__state = 1;
						this.<>u__1 = awaiter;
						this.<>t__builder.AwaitUnsafeOnCompleted<ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter, AsyncHelper.<_ContinueBoolTaskFuncWhenFalse>d__14>(ref awaiter, ref this);
						return;
					}
					IL_DD:
					result = awaiter.GetResult();
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<>t__builder.SetException(exception);
					return;
				}
				IL_100:
				this.<>1__state = -2;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x06000032 RID: 50 RVA: 0x00002A68 File Offset: 0x00000C68
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x040004DC RID: 1244
			public int <>1__state;

			// Token: 0x040004DD RID: 1245
			public AsyncTaskMethodBuilder<bool> <>t__builder;

			// Token: 0x040004DE RID: 1246
			public Task<bool> task;

			// Token: 0x040004DF RID: 1247
			public Func<Task<bool>> func;

			// Token: 0x040004E0 RID: 1248
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter <>u__1;
		}
	}
}
