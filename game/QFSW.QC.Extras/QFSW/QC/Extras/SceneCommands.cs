using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace QFSW.QC.Extras
{
	// Token: 0x0200000E RID: 14
	public static class SceneCommands
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00003638 File Offset: 0x00001838
		private static Task PollUntilAsync(int pollInterval, Func<bool> predicate)
		{
			SceneCommands.<PollUntilAsync>d__0 <PollUntilAsync>d__;
			<PollUntilAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PollUntilAsync>d__.pollInterval = pollInterval;
			<PollUntilAsync>d__.predicate = predicate;
			<PollUntilAsync>d__.<>1__state = -1;
			<PollUntilAsync>d__.<>t__builder.Start<SceneCommands.<PollUntilAsync>d__0>(ref <PollUntilAsync>d__);
			return <PollUntilAsync>d__.<>t__builder.Task;
		}

		// Token: 0x02000021 RID: 33
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <PollUntilAsync>d__0 : IAsyncStateMachine
		{
			// Token: 0x0600008F RID: 143 RVA: 0x0000429C File Offset: 0x0000249C
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num != 0)
					{
						goto IL_69;
					}
					TaskAwaiter awaiter = this.<>u__1;
					this.<>u__1 = default(TaskAwaiter);
					this.<>1__state = -1;
					IL_62:
					awaiter.GetResult();
					IL_69:
					if (!this.predicate())
					{
						awaiter = Task.Delay(this.pollInterval).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							this.<>1__state = 0;
							this.<>u__1 = awaiter;
							this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, SceneCommands.<PollUntilAsync>d__0>(ref awaiter, ref this);
							return;
						}
						goto IL_62;
					}
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

			// Token: 0x06000090 RID: 144 RVA: 0x0000435C File Offset: 0x0000255C
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000049 RID: 73
			public int <>1__state;

			// Token: 0x0400004A RID: 74
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x0400004B RID: 75
			public int pollInterval;

			// Token: 0x0400004C RID: 76
			public Func<bool> predicate;

			// Token: 0x0400004D RID: 77
			private TaskAwaiter <>u__1;
		}
	}
}
