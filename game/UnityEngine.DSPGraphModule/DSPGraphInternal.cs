using System;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	// Token: 0x02000008 RID: 8
	[NativeType(Header = "Modules/DSPGraph/Public/DSPGraph.bindings.h")]
	internal struct DSPGraphInternal
	{
		// Token: 0x06000024 RID: 36
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_CreateDSPGraph(out Handle graph, int outputFormat, uint outputChannels, uint dspBufferSize, uint sampleRate);

		// Token: 0x06000025 RID: 37
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_DisposeDSPGraph(ref Handle graph);

		// Token: 0x06000026 RID: 38
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_CreateDSPCommandBlock(ref Handle graph, ref Handle block);

		// Token: 0x06000027 RID: 39
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint Internal_AddNodeEventHandler(ref Handle graph, long eventTypeHashCode, object handler);

		// Token: 0x06000028 RID: 40
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Internal_RemoveNodeEventHandler(ref Handle graph, uint handlerId);

		// Token: 0x06000029 RID: 41
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_GetRootDSP(ref Handle graph, ref Handle root);

		// Token: 0x0600002A RID: 42
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong Internal_GetDSPClock(ref Handle graph);

		// Token: 0x0600002B RID: 43
		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_BeginMix(ref Handle graph, int frameCount, int executionMode);

		// Token: 0x0600002C RID: 44
		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_ReadMix(ref Handle graph, void* buffer, int frameCount);

		// Token: 0x0600002D RID: 45
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Update(ref Handle graph);

		// Token: 0x0600002E RID: 46
		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Internal_AssertMixerThread(ref Handle graph);

		// Token: 0x0600002F RID: 47
		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool Internal_AssertMainThread(ref Handle graph);

		// Token: 0x06000030 RID: 48 RVA: 0x000022DC File Offset: 0x000004DC
		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		public static Handle Internal_AllocateHandle(ref Handle graph)
		{
			Handle result;
			DSPGraphInternal.Internal_AllocateHandle_Injected(ref graph, out result);
			return result;
		}

		// Token: 0x06000031 RID: 49
		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_InitializeJob(void* jobStructData, void* jobReflectionData, void* resourceContext);

		// Token: 0x06000032 RID: 50
		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_ExecuteJob(void* jobStructData, void* jobReflectionData, void* jobData, void* resourceContext);

		// Token: 0x06000033 RID: 51
		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_ExecuteUpdateJob(void* updateStructMemory, void* updateReflectionData, void* jobStructMemory, void* jobReflectionData, void* resourceContext, ref Handle requestHandle, ref JobHandle fence);

		// Token: 0x06000034 RID: 52
		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_DisposeJob(void* jobStructData, void* jobReflectionData, void* resourceContext);

		// Token: 0x06000035 RID: 53 RVA: 0x000022F2 File Offset: 0x000004F2
		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		public unsafe static void Internal_ScheduleGraph(JobHandle inputDeps, void* nodes, int nodeCount, int* childTable, void* dependencies)
		{
			DSPGraphInternal.Internal_ScheduleGraph_Injected(ref inputDeps, nodes, nodeCount, childTable, dependencies);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002300 File Offset: 0x00000500
		[NativeMethod(IsFreeFunction = true, ThrowsException = true, IsThreadSafe = true)]
		public static void Internal_SyncFenceNoWorkSteal(JobHandle handle)
		{
			DSPGraphInternal.Internal_SyncFenceNoWorkSteal_Injected(ref handle);
		}

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_AllocateHandle_Injected(ref Handle graph, out Handle ret);

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_ScheduleGraph_Injected(ref JobHandle inputDeps, void* nodes, int nodeCount, int* childTable, void* dependencies);

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SyncFenceNoWorkSteal_Injected(ref JobHandle handle);
	}
}
