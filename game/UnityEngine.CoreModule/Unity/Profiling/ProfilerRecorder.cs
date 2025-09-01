using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x0200004B RID: 75
	[UsedByNativeCode]
	[DebuggerTypeProxy(typeof(ProfilerRecorderDebugView))]
	[NativeHeader("Runtime/Profiler/ScriptBindings/ProfilerRecorder.bindings.h")]
	[DebuggerDisplay("Count = {Count}")]
	public struct ProfilerRecorder : IDisposable
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00002A50 File Offset: 0x00000C50
		internal ProfilerRecorder(ProfilerRecorderOptions options)
		{
			this = ProfilerRecorder.Create(default(ProfilerRecorderHandle), 0, options);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00002A74 File Offset: 0x00000C74
		public ProfilerRecorder(string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = new ProfilerRecorder(ProfilerCategory.Any, statName, capacity, options);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002A86 File Offset: 0x00000C86
		public ProfilerRecorder(string categoryName, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = new ProfilerRecorder(new ProfilerCategory(categoryName), statName, capacity, options);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00002A9C File Offset: 0x00000C9C
		public ProfilerRecorder(ProfilerCategory category, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			ProfilerRecorderHandle byName = ProfilerRecorderHandle.GetByName(category, statName);
			this = ProfilerRecorder.Create(byName, capacity, options);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public unsafe ProfilerRecorder(ProfilerCategory category, char* statName, int statNameLen, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			ProfilerRecorderHandle byName = ProfilerRecorderHandle.GetByName(category, statName, statNameLen);
			this = ProfilerRecorder.Create(byName, capacity, options);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00002AEB File Offset: 0x00000CEB
		public ProfilerRecorder(ProfilerMarker marker, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = ProfilerRecorder.Create(ProfilerRecorderHandle.Get(marker), capacity, options);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002B01 File Offset: 0x00000D01
		public ProfilerRecorder(ProfilerRecorderHandle statHandle, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			this = ProfilerRecorder.Create(statHandle, capacity, options);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002B14 File Offset: 0x00000D14
		public unsafe static ProfilerRecorder StartNew(ProfilerCategory category, string statName, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			char* ptr = statName;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return new ProfilerRecorder(category, ptr, statName.Length, capacity, options | ProfilerRecorderOptions.StartImmediately);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00002B48 File Offset: 0x00000D48
		public static ProfilerRecorder StartNew(ProfilerMarker marker, int capacity = 1, ProfilerRecorderOptions options = ProfilerRecorderOptions.Default)
		{
			return new ProfilerRecorder(marker, capacity, options | ProfilerRecorderOptions.StartImmediately);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00002B64 File Offset: 0x00000D64
		internal static ProfilerRecorder StartNew()
		{
			return ProfilerRecorder.Create(default(ProfilerRecorderHandle), 0, ProfilerRecorderOptions.StartImmediately);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002B86 File Offset: 0x00000D86
		public bool Valid
		{
			get
			{
				return this.handle != 0UL && ProfilerRecorder.GetValid(this);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public ProfilerMarkerDataType DataType
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetValueDataType(this);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public ProfilerMarkerDataUnit UnitType
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetValueUnitType(this);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public void Start()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Start);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002BFF File Offset: 0x00000DFF
		public void Stop()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Stop);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00002C16 File Offset: 0x00000E16
		public void Reset()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Reset);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00002C30 File Offset: 0x00000E30
		public long CurrentValue
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCurrentValue(this);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00002C54 File Offset: 0x00000E54
		public double CurrentValueAsDouble
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCurrentValueAsDouble(this);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00002C78 File Offset: 0x00000E78
		public long LastValue
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetLastValue(this);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002C9C File Offset: 0x00000E9C
		public double LastValueAsDouble
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetLastValueAsDouble(this);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public int Capacity
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCount(this, ProfilerRecorder.CountOptions.MaxCount);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public int Count
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetCount(this, ProfilerRecorder.CountOptions.Count);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002D10 File Offset: 0x00000F10
		public bool IsRunning
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetRunning(this);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002D34 File Offset: 0x00000F34
		public bool WrappedAround
		{
			get
			{
				this.CheckInitializedAndThrow();
				return ProfilerRecorder.GetWrapped(this);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00002D58 File Offset: 0x00000F58
		public ProfilerRecorderSample GetSample(int index)
		{
			this.CheckInitializedAndThrow();
			return ProfilerRecorder.GetSampleInternal(this, index);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00002D80 File Offset: 0x00000F80
		public void CopyTo(List<ProfilerRecorderSample> outSamples, bool reset = false)
		{
			bool flag = outSamples == null;
			if (flag)
			{
				throw new ArgumentNullException("outSamples");
			}
			this.CheckInitializedAndThrow();
			ProfilerRecorder.CopyTo_List(this, outSamples, reset);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public unsafe int CopyTo(ProfilerRecorderSample* dest, int destSize, bool reset = false)
		{
			this.CheckInitializedWithParamsAndThrow(dest);
			return ProfilerRecorder.CopyTo_Pointer(this, dest, destSize, reset);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public unsafe ProfilerRecorderSample[] ToArray()
		{
			this.CheckInitializedAndThrow();
			int count = this.Count;
			ProfilerRecorderSample[] array = new ProfilerRecorderSample[count];
			ProfilerRecorderSample[] array2;
			ProfilerRecorderSample* outSamples;
			if ((array2 = array) == null || array2.Length == 0)
			{
				outSamples = null;
			}
			else
			{
				outSamples = &array2[0];
			}
			ProfilerRecorder.CopyTo_Pointer(this, outSamples, count, false);
			array2 = null;
			return array;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002E35 File Offset: 0x00001035
		internal void FilterToCurrentThread()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.SetFilterToCurrentThread);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00002E4C File Offset: 0x0000104C
		internal void CollectFromAllThreads()
		{
			this.CheckInitializedAndThrow();
			ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.SetToCollectFromAllThreads);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002E64 File Offset: 0x00001064
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static ProfilerRecorder Create(ProfilerRecorderHandle statHandle, int maxSampleCount, ProfilerRecorderOptions options)
		{
			ProfilerRecorder result;
			ProfilerRecorder.Create_Injected(ref statHandle, maxSampleCount, options, out result);
			return result;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00002E7D File Offset: 0x0000107D
		[NativeMethod(IsThreadSafe = true)]
		private static void Control(ProfilerRecorder handle, ProfilerRecorder.ControlOptions options)
		{
			ProfilerRecorder.Control_Injected(ref handle, options);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00002E87 File Offset: 0x00001087
		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerMarkerDataUnit GetValueUnitType(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValueUnitType_Injected(ref handle);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00002E90 File Offset: 0x00001090
		[NativeMethod(IsThreadSafe = true)]
		private static ProfilerMarkerDataType GetValueDataType(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValueDataType_Injected(ref handle);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00002E99 File Offset: 0x00001099
		[NativeMethod(IsThreadSafe = true)]
		private static long GetCurrentValue(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetCurrentValue_Injected(ref handle);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00002EA2 File Offset: 0x000010A2
		[NativeMethod(IsThreadSafe = true)]
		private static double GetCurrentValueAsDouble(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetCurrentValueAsDouble_Injected(ref handle);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00002EAB File Offset: 0x000010AB
		[NativeMethod(IsThreadSafe = true)]
		private static long GetLastValue(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetLastValue_Injected(ref handle);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00002EB4 File Offset: 0x000010B4
		[NativeMethod(IsThreadSafe = true)]
		private static double GetLastValueAsDouble(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetLastValueAsDouble_Injected(ref handle);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00002EBD File Offset: 0x000010BD
		[NativeMethod(IsThreadSafe = true)]
		private static int GetCount(ProfilerRecorder handle, ProfilerRecorder.CountOptions countOptions)
		{
			return ProfilerRecorder.GetCount_Injected(ref handle, countOptions);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00002EC7 File Offset: 0x000010C7
		[NativeMethod(IsThreadSafe = true)]
		private static bool GetValid(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetValid_Injected(ref handle);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00002ED0 File Offset: 0x000010D0
		[NativeMethod(IsThreadSafe = true)]
		private static bool GetWrapped(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetWrapped_Injected(ref handle);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00002ED9 File Offset: 0x000010D9
		[NativeMethod(IsThreadSafe = true)]
		private static bool GetRunning(ProfilerRecorder handle)
		{
			return ProfilerRecorder.GetRunning_Injected(ref handle);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002EE4 File Offset: 0x000010E4
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		private static ProfilerRecorderSample GetSampleInternal(ProfilerRecorder handle, int index)
		{
			ProfilerRecorderSample result;
			ProfilerRecorder.GetSampleInternal_Injected(ref handle, index, out result);
			return result;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00002EFC File Offset: 0x000010FC
		[NativeMethod(IsThreadSafe = true)]
		private static void CopyTo_List(ProfilerRecorder handle, List<ProfilerRecorderSample> outSamples, bool reset)
		{
			ProfilerRecorder.CopyTo_List_Injected(ref handle, outSamples, reset);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00002F07 File Offset: 0x00001107
		[NativeMethod(IsThreadSafe = true)]
		private unsafe static int CopyTo_Pointer(ProfilerRecorder handle, ProfilerRecorderSample* outSamples, int outSamplesSize, bool reset)
		{
			return ProfilerRecorder.CopyTo_Pointer_Injected(ref handle, outSamples, outSamplesSize, reset);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00002F14 File Offset: 0x00001114
		public void Dispose()
		{
			bool flag = this.handle == 0UL;
			if (!flag)
			{
				ProfilerRecorder.Control(this, ProfilerRecorder.ControlOptions.Release);
				this.handle = 0UL;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00002F48 File Offset: 0x00001148
		[BurstDiscard]
		private unsafe void CheckInitializedWithParamsAndThrow(ProfilerRecorderSample* dest)
		{
			bool flag = this.handle == 0UL;
			if (flag)
			{
				throw new InvalidOperationException("ProfilerRecorder object is not initialized or has been disposed.");
			}
			bool flag2 = dest == null;
			if (flag2)
			{
				throw new ArgumentNullException("dest");
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00002F84 File Offset: 0x00001184
		[BurstDiscard]
		private void CheckInitializedAndThrow()
		{
			bool flag = this.handle == 0UL;
			if (flag)
			{
				throw new InvalidOperationException("ProfilerRecorder object is not initialized or has been disposed.");
			}
		}

		// Token: 0x0600010D RID: 269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Create_Injected(ref ProfilerRecorderHandle statHandle, int maxSampleCount, ProfilerRecorderOptions options, out ProfilerRecorder ret);

		// Token: 0x0600010E RID: 270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Control_Injected(ref ProfilerRecorder handle, ProfilerRecorder.ControlOptions options);

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ProfilerMarkerDataUnit GetValueUnitType_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000110 RID: 272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ProfilerMarkerDataType GetValueDataType_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000111 RID: 273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetCurrentValue_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000112 RID: 274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetCurrentValueAsDouble_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000113 RID: 275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetLastValue_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000114 RID: 276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double GetLastValueAsDouble_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000115 RID: 277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCount_Injected(ref ProfilerRecorder handle, ProfilerRecorder.CountOptions countOptions);

		// Token: 0x06000116 RID: 278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetValid_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000117 RID: 279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetWrapped_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000118 RID: 280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetRunning_Injected(ref ProfilerRecorder handle);

		// Token: 0x06000119 RID: 281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSampleInternal_Injected(ref ProfilerRecorder handle, int index, out ProfilerRecorderSample ret);

		// Token: 0x0600011A RID: 282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyTo_List_Injected(ref ProfilerRecorder handle, List<ProfilerRecorderSample> outSamples, bool reset);

		// Token: 0x0600011B RID: 283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int CopyTo_Pointer_Injected(ref ProfilerRecorder handle, ProfilerRecorderSample* outSamples, int outSamplesSize, bool reset);

		// Token: 0x0400012E RID: 302
		internal ulong handle;

		// Token: 0x0400012F RID: 303
		internal const ProfilerRecorderOptions SharedRecorder = (ProfilerRecorderOptions)128;

		// Token: 0x0200004C RID: 76
		internal enum ControlOptions
		{
			// Token: 0x04000131 RID: 305
			Start,
			// Token: 0x04000132 RID: 306
			Stop,
			// Token: 0x04000133 RID: 307
			Reset,
			// Token: 0x04000134 RID: 308
			Release = 4,
			// Token: 0x04000135 RID: 309
			SetFilterToCurrentThread,
			// Token: 0x04000136 RID: 310
			SetToCollectFromAllThreads
		}

		// Token: 0x0200004D RID: 77
		internal enum CountOptions
		{
			// Token: 0x04000138 RID: 312
			Count,
			// Token: 0x04000139 RID: 313
			MaxCount
		}
	}
}
