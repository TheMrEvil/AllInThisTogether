using System;

namespace System.Linq.Parallel
{
	// Token: 0x020001DC RID: 476
	internal static class Scheduling
	{
		// Token: 0x06000BD7 RID: 3031 RVA: 0x00029B54 File Offset: 0x00027D54
		internal static int GetDefaultDegreeOfParallelism()
		{
			return Scheduling.DefaultDegreeOfParallelism;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00029B5C File Offset: 0x00027D5C
		internal static int GetDefaultChunkSize<T>()
		{
			int result;
			if (default(T) != null || Nullable.GetUnderlyingType(typeof(T)) != null)
			{
				result = 128;
			}
			else
			{
				result = 512 / IntPtr.Size;
			}
			return result;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00029BA5 File Offset: 0x00027DA5
		// Note: this type is marked as 'beforefieldinit'.
		static Scheduling()
		{
		}

		// Token: 0x04000860 RID: 2144
		internal const bool DefaultPreserveOrder = false;

		// Token: 0x04000861 RID: 2145
		internal static int DefaultDegreeOfParallelism = Math.Min(Environment.ProcessorCount, 16);

		// Token: 0x04000862 RID: 2146
		internal const int DEFAULT_BOUNDED_BUFFER_CAPACITY = 512;

		// Token: 0x04000863 RID: 2147
		internal const int DEFAULT_BYTES_PER_CHUNK = 512;

		// Token: 0x04000864 RID: 2148
		internal const int ZOMBIED_PRODUCER_TIMEOUT = -1;

		// Token: 0x04000865 RID: 2149
		internal const int MAX_SUPPORTED_DOP = 512;
	}
}
