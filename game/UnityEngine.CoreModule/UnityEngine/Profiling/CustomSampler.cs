using System;
using System.Diagnostics;
using Unity.Profiling.LowLevel;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Profiling
{
	// Token: 0x0200027C RID: 636
	[UsedByNativeCode]
	[NativeHeader("Runtime/Profiler/Marker.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Sampler.bindings.h")]
	public sealed class CustomSampler : Sampler
	{
		// Token: 0x06001BC2 RID: 7106 RVA: 0x0002C9A1 File Offset: 0x0002ABA1
		internal CustomSampler()
		{
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0002C9AB File Offset: 0x0002ABAB
		internal CustomSampler(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x0002C9BC File Offset: 0x0002ABBC
		public static CustomSampler Create(string name, bool collectGpuData = false)
		{
			IntPtr intPtr = ProfilerUnsafeUtility.CreateMarker(name, 1, MarkerFlags.AvailabilityNonDevelopment | (collectGpuData ? MarkerFlags.SampleGPU : MarkerFlags.Default), 0);
			bool flag = intPtr == IntPtr.Zero;
			CustomSampler result;
			if (flag)
			{
				result = CustomSampler.s_InvalidCustomSampler;
			}
			else
			{
				result = new CustomSampler(intPtr);
			}
			return result;
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x0002CA01 File Offset: 0x0002AC01
		[Conditional("ENABLE_PROFILER")]
		public void Begin()
		{
			ProfilerUnsafeUtility.BeginSample(this.m_Ptr);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x0002CA10 File Offset: 0x0002AC10
		[Conditional("ENABLE_PROFILER")]
		public void Begin(Object targetObject)
		{
			ProfilerUnsafeUtility.Internal_BeginWithObject(this.m_Ptr, targetObject);
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x0002CA20 File Offset: 0x0002AC20
		[Conditional("ENABLE_PROFILER")]
		public void End()
		{
			ProfilerUnsafeUtility.EndSample(this.m_Ptr);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x0002CA2F File Offset: 0x0002AC2F
		// Note: this type is marked as 'beforefieldinit'.
		static CustomSampler()
		{
		}

		// Token: 0x04000913 RID: 2323
		internal static CustomSampler s_InvalidCustomSampler = new CustomSampler();
	}
}
