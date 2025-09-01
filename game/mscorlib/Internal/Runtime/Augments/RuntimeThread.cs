using System;
using System.Threading;

namespace Internal.Runtime.Augments
{
	// Token: 0x020000C8 RID: 200
	internal sealed class RuntimeThread
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x00017850 File Offset: 0x00015A50
		private RuntimeThread(Thread t)
		{
			this.thread = t;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void ResetThreadPoolThread()
		{
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001785F File Offset: 0x00015A5F
		public static RuntimeThread InitializeThreadPoolThread()
		{
			return new RuntimeThread(null);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00017867 File Offset: 0x00015A67
		public static RuntimeThread Create(ParameterizedThreadStart start, int maxStackSize)
		{
			return new RuntimeThread(new Thread(start, maxStackSize));
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00017875 File Offset: 0x00015A75
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x00017882 File Offset: 0x00015A82
		public bool IsBackground
		{
			get
			{
				return this.thread.IsBackground;
			}
			set
			{
				this.thread.IsBackground = value;
			}
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00017890 File Offset: 0x00015A90
		public void Start()
		{
			this.thread.Start();
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001789D File Offset: 0x00015A9D
		public void Start(object state)
		{
			this.thread.Start(state);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000178AB File Offset: 0x00015AAB
		public static void Sleep(int millisecondsTimeout)
		{
			Thread.Sleep(millisecondsTimeout);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x000178B3 File Offset: 0x00015AB3
		public static bool Yield()
		{
			return Thread.Yield();
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x000178BA File Offset: 0x00015ABA
		public static bool SpinWait(int iterations)
		{
			Thread.SpinWait(iterations);
			return true;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000040F7 File Offset: 0x000022F7
		public static int GetCurrentProcessorId()
		{
			return 1;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000178C3 File Offset: 0x00015AC3
		// Note: this type is marked as 'beforefieldinit'.
		static RuntimeThread()
		{
		}

		// Token: 0x04000FD1 RID: 4049
		internal static readonly int OptimalMaxSpinWaitsPerSpinIteration = 64;

		// Token: 0x04000FD2 RID: 4050
		private readonly Thread thread;
	}
}
