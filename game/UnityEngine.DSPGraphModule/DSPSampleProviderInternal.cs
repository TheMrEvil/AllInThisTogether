using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Audio
{
	// Token: 0x0200000A RID: 10
	[NativeType(Header = "Modules/DSPGraph/Public/DSPSampleProvider.bindings.h")]
	internal struct DSPSampleProviderInternal
	{
		// Token: 0x0600003F RID: 63
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadUInt8FromSampleProvider(void* provider, int format, void* buffer, int length);

		// Token: 0x06000040 RID: 64
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadSInt16FromSampleProvider(void* provider, int format, void* buffer, int length);

		// Token: 0x06000041 RID: 65
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadFloatFromSampleProvider(void* provider, void* buffer, int length);

		// Token: 0x06000042 RID: 66
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern ushort Internal_GetChannelCount(void* provider);

		// Token: 0x06000043 RID: 67
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern uint Internal_GetSampleRate(void* provider);

		// Token: 0x06000044 RID: 68
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadUInt8FromSampleProviderById(uint providerId, int format, void* buffer, int length);

		// Token: 0x06000045 RID: 69
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadSInt16FromSampleProviderById(uint providerId, int format, void* buffer, int length);

		// Token: 0x06000046 RID: 70
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int Internal_ReadFloatFromSampleProviderById(uint providerId, void* buffer, int length);

		// Token: 0x06000047 RID: 71
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort Internal_GetChannelCountById(uint providerId);

		// Token: 0x06000048 RID: 72
		[NativeMethod(IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint Internal_GetSampleRateById(uint providerId);
	}
}
