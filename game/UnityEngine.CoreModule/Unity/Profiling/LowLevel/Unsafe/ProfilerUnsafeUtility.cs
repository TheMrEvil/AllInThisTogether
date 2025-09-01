using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling.LowLevel.Unsafe
{
	// Token: 0x02000055 RID: 85
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerUnsafeUtility.bindings.h")]
	public static class ProfilerUnsafeUtility
	{
		// Token: 0x06000135 RID: 309
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ushort CreateCategory(string name, ProfilerCategoryColor colorIndex);

		// Token: 0x06000136 RID: 310
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern ushort CreateCategory__Unmanaged(byte* name, int nameLen, ProfilerCategoryColor colorIndex);

		// Token: 0x06000137 RID: 311 RVA: 0x0000315C File Offset: 0x0000135C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort CreateCategory(char* name, int nameLen, ProfilerCategoryColor colorIndex)
		{
			return ProfilerUnsafeUtility.CreateCategory_Unsafe(name, nameLen, colorIndex);
		}

		// Token: 0x06000138 RID: 312
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern ushort CreateCategory_Unsafe(char* name, int nameLen, ProfilerCategoryColor colorIndex);

		// Token: 0x06000139 RID: 313 RVA: 0x00003178 File Offset: 0x00001378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort GetCategoryByName(char* name, int nameLen)
		{
			return ProfilerUnsafeUtility.GetCategoryByName_Unsafe(name, nameLen);
		}

		// Token: 0x0600013A RID: 314
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern ushort GetCategoryByName_Unsafe(char* name, int nameLen);

		// Token: 0x0600013B RID: 315 RVA: 0x00003194 File Offset: 0x00001394
		[ThreadSafe]
		public static ProfilerCategoryDescription GetCategoryDescription(ushort categoryId)
		{
			ProfilerCategoryDescription result;
			ProfilerUnsafeUtility.GetCategoryDescription_Injected(categoryId, out result);
			return result;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000031AC File Offset: 0x000013AC
		[ThreadSafe]
		internal static Color32 GetCategoryColor(ProfilerCategoryColor colorIndex)
		{
			Color32 result;
			ProfilerUnsafeUtility.GetCategoryColor_Injected(colorIndex, out result);
			return result;
		}

		// Token: 0x0600013D RID: 317
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CreateMarker(string name, ushort categoryId, MarkerFlags flags, int metadataCount);

		// Token: 0x0600013E RID: 318
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr CreateMarker__Unmanaged(byte* name, int nameLen, ushort categoryId, MarkerFlags flags, int metadataCount);

		// Token: 0x0600013F RID: 319 RVA: 0x000031C4 File Offset: 0x000013C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static IntPtr CreateMarker(char* name, int nameLen, ushort categoryId, MarkerFlags flags, int metadataCount)
		{
			return ProfilerUnsafeUtility.CreateMarker_Unsafe(name, nameLen, categoryId, flags, metadataCount);
		}

		// Token: 0x06000140 RID: 320
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr CreateMarker_Unsafe(char* name, int nameLen, ushort categoryId, MarkerFlags flags, int metadataCount);

		// Token: 0x06000141 RID: 321
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetMarker(string name);

		// Token: 0x06000142 RID: 322
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetMarkerMetadata(IntPtr markerPtr, int index, string name, byte type, byte unit);

		// Token: 0x06000143 RID: 323
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void SetMarkerMetadata__Unmanaged(IntPtr markerPtr, int index, byte* name, int nameLen, byte type, byte unit);

		// Token: 0x06000144 RID: 324 RVA: 0x000031E1 File Offset: 0x000013E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void SetMarkerMetadata(IntPtr markerPtr, int index, char* name, int nameLen, byte type, byte unit)
		{
			ProfilerUnsafeUtility.SetMarkerMetadata_Unsafe(markerPtr, index, name, nameLen, type, unit);
		}

		// Token: 0x06000145 RID: 325
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetMarkerMetadata_Unsafe(IntPtr markerPtr, int index, char* name, int nameLen, byte type, byte unit);

		// Token: 0x06000146 RID: 326
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginSample(IntPtr markerPtr);

		// Token: 0x06000147 RID: 327
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void BeginSampleWithMetadata(IntPtr markerPtr, int metadataCount, void* metadata);

		// Token: 0x06000148 RID: 328
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample(IntPtr markerPtr);

		// Token: 0x06000149 RID: 329
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void SingleSampleWithMetadata(IntPtr markerPtr, int metadataCount, void* metadata);

		// Token: 0x0600014A RID: 330
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* CreateCounterValue(out IntPtr counterPtr, string name, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions);

		// Token: 0x0600014B RID: 331
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void* CreateCounterValue__Unmanaged(out IntPtr counterPtr, byte* name, int nameLen, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions);

		// Token: 0x0600014C RID: 332 RVA: 0x000031F4 File Offset: 0x000013F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* CreateCounterValue(out IntPtr counterPtr, char* name, int nameLen, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions)
		{
			return ProfilerUnsafeUtility.CreateCounterValue_Unsafe(out counterPtr, name, nameLen, categoryId, flags, dataType, dataUnit, dataSize, counterOptions);
		}

		// Token: 0x0600014D RID: 333
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* CreateCounterValue_Unsafe(out IntPtr counterPtr, char* name, int nameLen, ushort categoryId, MarkerFlags flags, byte dataType, byte dataUnit, int dataSize, ProfilerCounterOptions counterOptions);

		// Token: 0x0600014E RID: 334
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void FlushCounterValue(void* counterValuePtr);

		// Token: 0x0600014F RID: 335 RVA: 0x0000321C File Offset: 0x0000141C
		internal unsafe static string Utf8ToString(byte* chars, int charsLen)
		{
			bool flag = chars == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				byte[] array = new byte[charsLen];
				Marshal.Copy((IntPtr)((void*)chars), array, 0, charsLen);
				result = Encoding.UTF8.GetString(array, 0, charsLen);
			}
			return result;
		}

		// Token: 0x06000150 RID: 336
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint CreateFlow(ushort categoryId);

		// Token: 0x06000151 RID: 337
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FlowEvent(uint flowId, ProfilerFlowEventType flowEventType);

		// Token: 0x06000152 RID: 338
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_BeginWithObject(IntPtr markerPtr, UnityEngine.Object contextUnityObject);

		// Token: 0x06000153 RID: 339
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Internal_GetName(IntPtr markerPtr);

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000154 RID: 340
		public static extern long Timestamp { [ThreadSafe] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00003260 File Offset: 0x00001460
		public static ProfilerUnsafeUtility.TimestampConversionRatio TimestampToNanosecondsConversionRatio
		{
			[ThreadSafe]
			get
			{
				ProfilerUnsafeUtility.TimestampConversionRatio result;
				ProfilerUnsafeUtility.get_TimestampToNanosecondsConversionRatio_Injected(out result);
				return result;
			}
		}

		// Token: 0x06000156 RID: 342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCategoryDescription_Injected(ushort categoryId, out ProfilerCategoryDescription ret);

		// Token: 0x06000157 RID: 343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCategoryColor_Injected(ProfilerCategoryColor colorIndex, out Color32 ret);

		// Token: 0x06000158 RID: 344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_TimestampToNanosecondsConversionRatio_Injected(out ProfilerUnsafeUtility.TimestampConversionRatio ret);

		// Token: 0x04000162 RID: 354
		public const ushort CategoryRender = 0;

		// Token: 0x04000163 RID: 355
		public const ushort CategoryScripts = 1;

		// Token: 0x04000164 RID: 356
		public const ushort CategoryGUI = 4;

		// Token: 0x04000165 RID: 357
		public const ushort CategoryPhysics = 5;

		// Token: 0x04000166 RID: 358
		public const ushort CategoryAnimation = 6;

		// Token: 0x04000167 RID: 359
		public const ushort CategoryAi = 7;

		// Token: 0x04000168 RID: 360
		public const ushort CategoryAudio = 8;

		// Token: 0x04000169 RID: 361
		public const ushort CategoryVideo = 11;

		// Token: 0x0400016A RID: 362
		public const ushort CategoryParticles = 12;

		// Token: 0x0400016B RID: 363
		public const ushort CategoryLighting = 13;

		// Token: 0x0400016C RID: 364
		[Obsolete("CategoryLightning has been renamed. Use CategoryLighting instead (UnityUpgradable) -> CategoryLighting", false)]
		public const ushort CategoryLightning = 13;

		// Token: 0x0400016D RID: 365
		public const ushort CategoryNetwork = 14;

		// Token: 0x0400016E RID: 366
		public const ushort CategoryLoading = 15;

		// Token: 0x0400016F RID: 367
		public const ushort CategoryOther = 16;

		// Token: 0x04000170 RID: 368
		public const ushort CategoryVr = 22;

		// Token: 0x04000171 RID: 369
		public const ushort CategoryAllocation = 23;

		// Token: 0x04000172 RID: 370
		public const ushort CategoryInternal = 24;

		// Token: 0x04000173 RID: 371
		public const ushort CategoryFileIO = 25;

		// Token: 0x04000174 RID: 372
		public const ushort CategoryInput = 30;

		// Token: 0x04000175 RID: 373
		public const ushort CategoryVirtualTexturing = 31;

		// Token: 0x04000176 RID: 374
		internal const ushort CategoryGPU = 32;

		// Token: 0x04000177 RID: 375
		internal const ushort CategoryPhysics2D = 33;

		// Token: 0x04000178 RID: 376
		internal const ushort CategoryAny = 65535;

		// Token: 0x02000056 RID: 86
		public struct TimestampConversionRatio
		{
			// Token: 0x04000179 RID: 377
			public long Numerator;

			// Token: 0x0400017A RID: 378
			public long Denominator;
		}
	}
}
