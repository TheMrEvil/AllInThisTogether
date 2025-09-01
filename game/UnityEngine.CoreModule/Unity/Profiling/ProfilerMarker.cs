using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Scripting;

namespace Unity.Profiling
{
	// Token: 0x02000042 RID: 66
	[UsedByNativeCode]
	public struct ProfilerMarker
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x000027C4 File Offset: 0x000009C4
		public IntPtr Handle
		{
			get
			{
				return this.m_Ptr;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000027CC File Offset: 0x000009CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(string name)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, 1, MarkerFlags.Default, 0);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000027DE File Offset: 0x000009DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ProfilerMarker(char* name, int nameLen)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, nameLen, 1, MarkerFlags.Default, 0);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000027F1 File Offset: 0x000009F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(ProfilerCategory category, string name)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, category, MarkerFlags.Default, 0);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002808 File Offset: 0x00000A08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ProfilerMarker(ProfilerCategory category, char* name, int nameLen)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, nameLen, category, MarkerFlags.Default, 0);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002820 File Offset: 0x00000A20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker(ProfilerCategory category, string name, MarkerFlags flags)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, category, flags, 0);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00002837 File Offset: 0x00000A37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe ProfilerMarker(ProfilerCategory category, char* name, int nameLen, MarkerFlags flags)
		{
			this.m_Ptr = ProfilerUnsafeUtility.CreateMarker(name, nameLen, category, flags, 0);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00002850 File Offset: 0x00000A50
		[Conditional("ENABLE_PROFILER")]
		[Pure]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Begin()
		{
			ProfilerUnsafeUtility.BeginSample(this.m_Ptr);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000285F File Offset: 0x00000A5F
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Begin(UnityEngine.Object contextUnityObject)
		{
			ProfilerUnsafeUtility.Internal_BeginWithObject(this.m_Ptr, contextUnityObject);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000286F File Offset: 0x00000A6F
		[Conditional("ENABLE_PROFILER")]
		[Pure]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void End()
		{
			ProfilerUnsafeUtility.EndSample(this.m_Ptr);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000287E File Offset: 0x00000A7E
		[Conditional("ENABLE_PROFILER")]
		internal void GetName(ref string name)
		{
			name = ProfilerUnsafeUtility.Internal_GetName(this.m_Ptr);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002890 File Offset: 0x00000A90
		[Pure]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ProfilerMarker.AutoScope Auto()
		{
			return new ProfilerMarker.AutoScope(this.m_Ptr);
		}

		// Token: 0x0400010D RID: 269
		[NativeDisableUnsafePtrRestriction]
		[NonSerialized]
		internal readonly IntPtr m_Ptr;

		// Token: 0x02000043 RID: 67
		[UsedByNativeCode]
		public struct AutoScope : IDisposable
		{
			// Token: 0x060000D4 RID: 212 RVA: 0x000028B0 File Offset: 0x00000AB0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			internal AutoScope(IntPtr markerPtr)
			{
				this.m_Ptr = markerPtr;
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					ProfilerUnsafeUtility.BeginSample(markerPtr);
				}
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x000028E0 File Offset: 0x00000AE0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void Dispose()
			{
				bool flag = this.m_Ptr != IntPtr.Zero;
				if (flag)
				{
					ProfilerUnsafeUtility.EndSample(this.m_Ptr);
				}
			}

			// Token: 0x0400010E RID: 270
			[NativeDisableUnsafePtrRestriction]
			internal readonly IntPtr m_Ptr;
		}
	}
}
