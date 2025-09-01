using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	// Token: 0x02000006 RID: 6
	[NativeHeader("Modules/DSPGraph/Public/DSPSampleProvider.bindings.h")]
	[NativeType(Header = "Modules/DSPGraph/Public/DSPCommandBlock.bindings.h")]
	internal struct DSPCommandBlockInternal
	{
		// Token: 0x06000010 RID: 16
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_CreateDSPNode(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, void* jobMemory, void* parameterDescriptionArray, int parameterCount, void* sampleProviderDescriptionArray, int sampleProviderCount);

		// Token: 0x06000011 RID: 17
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_SetFloat(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, uint pIndex, float value, uint interpolationLength);

		// Token: 0x06000012 RID: 18
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_AddFloatKey(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, uint pIndex, ulong dspClock, float value);

		// Token: 0x06000013 RID: 19
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_SustainFloat(ref Handle graph, ref Handle block, ref Handle node, void* jobReflectionData, uint pIndex, ulong dspClock);

		// Token: 0x06000014 RID: 20
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_UpdateAudioJob(ref Handle graph, ref Handle block, ref Handle node, void* updateJobMem, void* updateJobReflectionData, void* nodeReflectionData);

		// Token: 0x06000015 RID: 21
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_CreateUpdateRequest(ref Handle graph, ref Handle block, ref Handle node, ref Handle request, object callback, void* updateJobMem, void* updateJobReflectionData, void* nodeReflectionData);

		// Token: 0x06000016 RID: 22
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_ReleaseDSPNode(ref Handle graph, ref Handle block, ref Handle node);

		// Token: 0x06000017 RID: 23
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Connect(ref Handle graph, ref Handle block, ref Handle output, int outputPort, ref Handle input, int inputPort, ref Handle connection);

		// Token: 0x06000018 RID: 24
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Disconnect(ref Handle graph, ref Handle block, ref Handle output, int outputPort, ref Handle input, int inputPort);

		// Token: 0x06000019 RID: 25
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_DisconnectByHandle(ref Handle graph, ref Handle block, ref Handle connection);

		// Token: 0x0600001A RID: 26
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_SetAttenuation(ref Handle graph, ref Handle block, ref Handle connection, void* value, byte dimension, uint interpolationLength);

		// Token: 0x0600001B RID: 27
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Internal_AddAttenuationKey(ref Handle graph, ref Handle block, ref Handle connection, ulong dspClock, void* value, byte dimension);

		// Token: 0x0600001C RID: 28
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_SustainAttenuation(ref Handle graph, ref Handle block, ref Handle connection, ulong dspClock);

		// Token: 0x0600001D RID: 29
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_AddInletPort(ref Handle graph, ref Handle block, ref Handle node, int channelCount, int format);

		// Token: 0x0600001E RID: 30
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_AddOutletPort(ref Handle graph, ref Handle block, ref Handle node, int channelCount, int format);

		// Token: 0x0600001F RID: 31
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_SetSampleProvider(ref Handle graph, ref Handle block, ref Handle node, int item, int index, uint audioSampleProviderId, bool destroyOnRemove);

		// Token: 0x06000020 RID: 32
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_InsertSampleProvider(ref Handle graph, ref Handle block, ref Handle node, int item, int index, uint audioSampleProviderId, bool destroyOnRemove);

		// Token: 0x06000021 RID: 33
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_RemoveSampleProvider(ref Handle graph, ref Handle block, ref Handle node, int item, int index);

		// Token: 0x06000022 RID: 34
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Complete(ref Handle graph, ref Handle block);

		// Token: 0x06000023 RID: 35
		[NativeMethod(IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Internal_Cancel(ref Handle graph, ref Handle block);
	}
}
