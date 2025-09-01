using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Sockets
{
	// Token: 0x020007C0 RID: 1984
	internal sealed class SafeSocketHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06003F56 RID: 16214 RVA: 0x000D8707 File Offset: 0x000D6907
		public SafeSocketHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
			if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
			{
				this.threads_stacktraces = new Dictionary<Thread, StackTrace>();
			}
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x00013B6C File Offset: 0x00011D6C
		internal SafeSocketHandle() : base(true)
		{
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x000D872C File Offset: 0x000D692C
		protected override bool ReleaseHandle()
		{
			int num = 0;
			Socket.Blocking_icall(this.handle, false, out num);
			if (this.blocking_threads != null)
			{
				List<Thread> obj = this.blocking_threads;
				lock (obj)
				{
					int num2 = 0;
					while (this.blocking_threads.Count > 0)
					{
						if (num2++ >= 10)
						{
							if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
							{
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.AppendLine("Could not abort registered blocking threads before closing socket.");
								foreach (Thread key in this.blocking_threads)
								{
									stringBuilder.AppendLine("Thread StackTrace:");
									stringBuilder.AppendLine(this.threads_stacktraces[key].ToString());
								}
								stringBuilder.AppendLine();
								throw new Exception(stringBuilder.ToString());
							}
							break;
						}
						else
						{
							if (this.blocking_threads.Count == 1 && this.blocking_threads[0] == Thread.CurrentThread)
							{
								break;
							}
							foreach (Thread thread in this.blocking_threads)
							{
								Socket.cancel_blocking_socket_operation(thread);
							}
							this.in_cleanup = true;
							Monitor.Wait(this.blocking_threads, 100);
						}
					}
				}
			}
			Socket.Close_icall(this.handle, out num);
			return num == 0;
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x000D88EC File Offset: 0x000D6AEC
		public void RegisterForBlockingSyscall()
		{
			if (this.blocking_threads == null)
			{
				Interlocked.CompareExchange<List<Thread>>(ref this.blocking_threads, new List<Thread>(), null);
			}
			bool flag = false;
			try
			{
				base.DangerousAddRef(ref flag);
			}
			finally
			{
				List<Thread> obj = this.blocking_threads;
				lock (obj)
				{
					this.blocking_threads.Add(Thread.CurrentThread);
					if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
					{
						this.threads_stacktraces.Add(Thread.CurrentThread, new StackTrace(true));
					}
				}
				if (flag)
				{
					base.DangerousRelease();
				}
				if (base.IsClosed)
				{
					throw new SocketException(10004);
				}
			}
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x000D89A4 File Offset: 0x000D6BA4
		public void UnRegisterForBlockingSyscall()
		{
			List<Thread> obj = this.blocking_threads;
			lock (obj)
			{
				Thread currentThread = Thread.CurrentThread;
				this.blocking_threads.Remove(currentThread);
				if (SafeSocketHandle.THROW_ON_ABORT_RETRIES && this.blocking_threads.IndexOf(currentThread) == -1)
				{
					this.threads_stacktraces.Remove(currentThread);
				}
				if (this.in_cleanup && this.blocking_threads.Count == 0)
				{
					Monitor.Pulse(this.blocking_threads);
				}
			}
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x000D8A34 File Offset: 0x000D6C34
		// Note: this type is marked as 'beforefieldinit'.
		static SafeSocketHandle()
		{
		}

		// Token: 0x040025D4 RID: 9684
		private List<Thread> blocking_threads;

		// Token: 0x040025D5 RID: 9685
		private Dictionary<Thread, StackTrace> threads_stacktraces;

		// Token: 0x040025D6 RID: 9686
		private bool in_cleanup;

		// Token: 0x040025D7 RID: 9687
		private const int SOCKET_CLOSED = 10004;

		// Token: 0x040025D8 RID: 9688
		private const int ABORT_RETRIES = 10;

		// Token: 0x040025D9 RID: 9689
		private static bool THROW_ON_ABORT_RETRIES = Environment.GetEnvironmentVariable("MONO_TESTS_IN_PROGRESS") == "yes";
	}
}
