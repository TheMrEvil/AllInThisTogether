using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/ClusterRenderer/ClusterSerialization.h")]
	[ExcludeFromDocs]
	public static class ClusterSerialization
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000205C File Offset: 0x0000025C
		public static int SaveTimeManagerState(NativeArray<byte> buffer)
		{
			return ClusterSerialization.SaveTimeManagerStateInternal(buffer.GetUnsafePtr<byte>(), buffer.Length);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002084 File Offset: 0x00000284
		public static bool RestoreTimeManagerState(NativeArray<byte> buffer)
		{
			return ClusterSerialization.RestoreTimeManagerStateInternal(buffer.GetUnsafePtr<byte>(), buffer.Length);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020AC File Offset: 0x000002AC
		public static int SaveInputManagerState(NativeArray<byte> buffer)
		{
			return ClusterSerialization.SaveInputManagerStateInternal(buffer.GetUnsafePtr<byte>(), buffer.Length);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020D4 File Offset: 0x000002D4
		public static bool RestoreInputManagerState(NativeArray<byte> buffer)
		{
			return ClusterSerialization.RestoreInputManagerStateInternal(buffer.GetUnsafePtr<byte>(), buffer.Length);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020FC File Offset: 0x000002FC
		public static int SaveClusterInputState(NativeArray<byte> buffer)
		{
			return ClusterSerialization.SaveClusterInputStateInternal(buffer.GetUnsafePtr<byte>(), buffer.Length);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002124 File Offset: 0x00000324
		public static bool RestoreClusterInputState(NativeArray<byte> buffer)
		{
			return ClusterSerialization.RestoreClusterInputStateInternal(buffer.GetUnsafePtr<byte>(), buffer.Length);
		}

		// Token: 0x0600000C RID: 12
		[FreeFunction("ClusterSerialization::SaveTimeManagerState", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int SaveTimeManagerStateInternal(void* intBuffer, int bufferSize);

		// Token: 0x0600000D RID: 13
		[FreeFunction("ClusterSerialization::RestoreTimeManagerState", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool RestoreTimeManagerStateInternal(void* buffer, int bufferSize);

		// Token: 0x0600000E RID: 14
		[FreeFunction("ClusterSerialization::SaveInputManagerState", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int SaveInputManagerStateInternal(void* intBuffer, int bufferSize);

		// Token: 0x0600000F RID: 15
		[FreeFunction("ClusterSerialization::RestoreInputManagerState", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool RestoreInputManagerStateInternal(void* buffer, int bufferSize);

		// Token: 0x06000010 RID: 16
		[FreeFunction("ClusterSerialization::SaveClusterInputState", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int SaveClusterInputStateInternal(void* intBuffer, int bufferSize);

		// Token: 0x06000011 RID: 17
		[FreeFunction("ClusterSerialization::RestoreClusterInputState", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool RestoreClusterInputStateInternal(void* buffer, int bufferSize);
	}
}
