using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x02000051 RID: 81
	internal static class FileUtilities
	{
		// Token: 0x06000408 RID: 1032 RVA: 0x0000CD38 File Offset: 0x0000AF38
		public static Task<string> ReadAllTextAsync(this FileInfo file)
		{
			FileUtilities.<ReadAllTextAsync>d__0 <ReadAllTextAsync>d__;
			<ReadAllTextAsync>d__.file = file;
			<ReadAllTextAsync>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<ReadAllTextAsync>d__.<>1__state = -1;
			<ReadAllTextAsync>d__.<>t__builder.Start<FileUtilities.<ReadAllTextAsync>d__0>(ref <ReadAllTextAsync>d__);
			return <ReadAllTextAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000CD7C File Offset: 0x0000AF7C
		public static Task WriteContentAsync(this FileInfo file, string content)
		{
			FileUtilities.<WriteContentAsync>d__1 <WriteContentAsync>d__;
			<WriteContentAsync>d__.file = file;
			<WriteContentAsync>d__.content = content;
			<WriteContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<WriteContentAsync>d__.<>1__state = -1;
			<WriteContentAsync>d__.<>t__builder.Start<FileUtilities.<WriteContentAsync>d__1>(ref <WriteContentAsync>d__);
			return <WriteContentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0200011A RID: 282
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <ReadAllTextAsync>d__0 : IAsyncStateMachine
		{
			// Token: 0x0600074E RID: 1870 RVA: 0x000162C0 File Offset: 0x000144C0
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				string result;
				try
				{
					if (num != 0)
					{
						this.<reader>5__2 = new StreamReader(this.file.OpenRead(), Encoding.Unicode);
					}
					try
					{
						TaskAwaiter<string> awaiter;
						if (num != 0)
						{
							awaiter = this.<reader>5__2.ReadToEndAsync().GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, FileUtilities.<ReadAllTextAsync>d__0>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter<string>);
							num = (this.<>1__state = -1);
						}
						result = awaiter.GetResult();
					}
					finally
					{
						if (num < 0 && this.<reader>5__2 != null)
						{
							((IDisposable)this.<reader>5__2).Dispose();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<reader>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<reader>5__2 = null;
				this.<>t__builder.SetResult(result);
			}

			// Token: 0x0600074F RID: 1871 RVA: 0x000163C4 File Offset: 0x000145C4
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x0400028B RID: 651
			public int <>1__state;

			// Token: 0x0400028C RID: 652
			public AsyncTaskMethodBuilder<string> <>t__builder;

			// Token: 0x0400028D RID: 653
			public FileInfo file;

			// Token: 0x0400028E RID: 654
			private StreamReader <reader>5__2;

			// Token: 0x0400028F RID: 655
			private TaskAwaiter<string> <>u__1;
		}

		// Token: 0x0200011B RID: 283
		[CompilerGenerated]
		[StructLayout(LayoutKind.Auto)]
		private struct <WriteContentAsync>d__1 : IAsyncStateMachine
		{
			// Token: 0x06000750 RID: 1872 RVA: 0x000163D4 File Offset: 0x000145D4
			void IAsyncStateMachine.MoveNext()
			{
				int num = this.<>1__state;
				try
				{
					if (num != 0)
					{
						this.<stream>5__2 = new FileStream(Path.GetFullPath(this.file.FullName), FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
					}
					try
					{
						TaskAwaiter awaiter;
						if (num != 0)
						{
							byte[] bytes = Encoding.Unicode.GetBytes(this.content);
							awaiter = this.<stream>5__2.WriteAsync(bytes, 0, bytes.Length).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (this.<>1__state = 0);
								this.<>u__1 = awaiter;
								this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter, FileUtilities.<WriteContentAsync>d__1>(ref awaiter, ref this);
								return;
							}
						}
						else
						{
							awaiter = this.<>u__1;
							this.<>u__1 = default(TaskAwaiter);
							num = (this.<>1__state = -1);
						}
						awaiter.GetResult();
					}
					finally
					{
						if (num < 0 && this.<stream>5__2 != null)
						{
							((IDisposable)this.<stream>5__2).Dispose();
						}
					}
				}
				catch (Exception exception)
				{
					this.<>1__state = -2;
					this.<stream>5__2 = null;
					this.<>t__builder.SetException(exception);
					return;
				}
				this.<>1__state = -2;
				this.<stream>5__2 = null;
				this.<>t__builder.SetResult();
			}

			// Token: 0x06000751 RID: 1873 RVA: 0x000164F8 File Offset: 0x000146F8
			[DebuggerHidden]
			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				this.<>t__builder.SetStateMachine(stateMachine);
			}

			// Token: 0x04000290 RID: 656
			public int <>1__state;

			// Token: 0x04000291 RID: 657
			public AsyncTaskMethodBuilder <>t__builder;

			// Token: 0x04000292 RID: 658
			public FileInfo file;

			// Token: 0x04000293 RID: 659
			public string content;

			// Token: 0x04000294 RID: 660
			private FileStream <stream>5__2;

			// Token: 0x04000295 RID: 661
			private TaskAwaiter <>u__1;
		}
	}
}
