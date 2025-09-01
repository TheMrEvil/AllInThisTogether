using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000052 RID: 82
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public readonly struct ProfilerRecorderHandle
	{
		// Token: 0x06000125 RID: 293 RVA: 0x0000300C File Offset: 0x0000120C
		internal ProfilerRecorderHandle(ulong handle)
		{
			this.handle = handle;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00003016 File Offset: 0x00001216
		public bool Valid
		{
			get
			{
				return this.handle != 0UL && this.handle != ulong.MaxValue;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00003030 File Offset: 0x00001230
		internal static ProfilerRecorderHandle Get(ProfilerMarker marker)
		{
			return new ProfilerRecorderHandle((ulong)marker.Handle.ToInt64());
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00003058 File Offset: 0x00001258
		internal static ProfilerRecorderHandle Get(ProfilerCategory category, string statName)
		{
			bool flag = string.IsNullOrEmpty(statName);
			if (flag)
			{
				throw new ArgumentException("String must be not null or empty", "statName");
			}
			return ProfilerRecorderHandle.GetByName(category, statName);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000308C File Offset: 0x0000128C
		public static ProfilerRecorderDescription GetDescription(ProfilerRecorderHandle handle)
		{
			bool flag = !handle.Valid;
			if (flag)
			{
				throw new ArgumentException("ProfilerRecorderHandle is not initialized or is not available", "handle");
			}
			return ProfilerRecorderHandle.GetDescriptionInternal(handle);
		}

		// Token: 0x0600012A RID: 298
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GetAvailable(List<ProfilerRecorderHandle> outRecorderHandleList);

		// Token: 0x0600012B RID: 299 RVA: 0x000030C4 File Offset: 0x000012C4
		[NativeMethod(IsThreadSafe = true)]
		internal static ProfilerRecorderHandle GetByName(ProfilerCategory category, string name)
		{
			ProfilerRecorderHandle result;
			ProfilerRecorderHandle.GetByName_Injected(ref category, name, out result);
			return result;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000030DC File Offset: 0x000012DC
		[NativeMethod(IsThreadSafe = true)]
		internal unsafe static ProfilerRecorderHandle GetByName__Unmanaged(ProfilerCategory category, byte* name, int nameLen)
		{
			ProfilerRecorderHandle result;
			ProfilerRecorderHandle.GetByName__Unmanaged_Injected(ref category, name, nameLen, out result);
			return result;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000030F8 File Offset: 0x000012F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static ProfilerRecorderHandle GetByName(ProfilerCategory category, char* name, int nameLen)
		{
			return ProfilerRecorderHandle.GetByName_Unsafe(category, name, nameLen);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00003114 File Offset: 0x00001314
		[NativeMethod(IsThreadSafe = true)]
		private unsafe static ProfilerRecorderHandle GetByName_Unsafe(ProfilerCategory category, char* name, int nameLen)
		{
			ProfilerRecorderHandle result;
			ProfilerRecorderHandle.GetByName_Unsafe_Injected(ref category, name, nameLen, out result);
			return result;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00003130 File Offset: 0x00001330
		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerRecorderDescription GetDescriptionInternal(ProfilerRecorderHandle handle)
		{
			ProfilerRecorderDescription result;
			ProfilerRecorderHandle.GetDescriptionInternal_Injected(ref handle, out result);
			return result;
		}

		// Token: 0x06000130 RID: 304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetByName_Injected(ref ProfilerCategory category, string name, out ProfilerRecorderHandle ret);

		// Token: 0x06000131 RID: 305
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetByName__Unmanaged_Injected(ref ProfilerCategory category, byte* name, int nameLen, out ProfilerRecorderHandle ret);

		// Token: 0x06000132 RID: 306
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GetByName_Unsafe_Injected(ref ProfilerCategory category, char* name, int nameLen, out ProfilerRecorderHandle ret);

		// Token: 0x06000133 RID: 307
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDescriptionInternal_Injected(ref ProfilerRecorderHandle handle, out ProfilerRecorderDescription ret);

		// Token: 0x04000155 RID: 341
		private const ulong k_InvalidHandle = 18446744073709551615UL;

		// Token: 0x04000156 RID: 342
		[FieldOffset(0)]
		internal readonly ulong handle;
	}
}
