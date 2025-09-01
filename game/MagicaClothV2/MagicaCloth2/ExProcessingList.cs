using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace MagicaCloth2
{
	// Token: 0x020000E9 RID: 233
	public class ExProcessingList<T> : IDisposable, IValid where T : struct
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x0002160C File Offset: 0x0001F80C
		public void Dispose()
		{
			if (this.Counter.IsCreated)
			{
				this.Counter.Dispose();
			}
			if (this.Buffer.IsCreated)
			{
				this.Buffer.Dispose();
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0002163E File Offset: 0x0001F83E
		public bool IsValid()
		{
			return this.Counter.IsCreated;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0002164B File Offset: 0x0001F84B
		public ExProcessingList()
		{
			this.Counter = new NativeReference<int>(Allocator.Persistent, NativeArrayOptions.ClearMemory);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00021668 File Offset: 0x0001F868
		public void UpdateBuffer(int capacity)
		{
			if (!this.Buffer.IsCreated || this.Buffer.Length < capacity)
			{
				if (this.Buffer.IsCreated)
				{
					this.Buffer.Dispose();
				}
				this.Buffer = new NativeArray<T>(capacity, Allocator.Persistent, NativeArrayOptions.ClearMemory);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000216B6 File Offset: 0x0001F8B6
		public unsafe int* GetJobSchedulePtr()
		{
			return (int*)this.Counter.GetUnsafePtrWithoutChecks<int>();
		}

		// Token: 0x0400063F RID: 1599
		public NativeReference<int> Counter;

		// Token: 0x04000640 RID: 1600
		public NativeArray<T> Buffer;
	}
}
