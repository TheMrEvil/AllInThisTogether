using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Profiling
{
	// Token: 0x02000279 RID: 633
	[NativeHeader("Runtime/Utilities/MemoryUtilities.h")]
	[UsedByNativeCode]
	[MovedFrom("UnityEngine")]
	[NativeHeader("Runtime/Allocator/MemoryManager.h")]
	[NativeHeader("Runtime/ScriptingBackend/ScriptingApi.h")]
	[NativeHeader("Runtime/Profiler/ScriptBindings/Profiler.bindings.h")]
	[NativeHeader("Runtime/Profiler/Profiler.h")]
	public sealed class Profiler
	{
		// Token: 0x06001B6E RID: 7022 RVA: 0x00008CBB File Offset: 0x00006EBB
		private Profiler()
		{
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001B6F RID: 7023
		public static extern bool supported { [NativeMethod(Name = "profiler_is_available", IsFreeFunction = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001B70 RID: 7024
		// (set) Token: 0x06001B71 RID: 7025
		[StaticAccessor("ProfilerBindings", StaticAccessorType.DoubleColon)]
		public static extern string logFile { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001B72 RID: 7026
		// (set) Token: 0x06001B73 RID: 7027
		public static extern bool enableBinaryLog { [NativeMethod(Name = "ProfilerBindings::IsBinaryLogEnabled", IsFreeFunction = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod(Name = "ProfilerBindings::SetBinaryLogEnabled", IsFreeFunction = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001B74 RID: 7028
		// (set) Token: 0x06001B75 RID: 7029
		public static extern int maxUsedMemory { [NativeMethod(Name = "ProfilerBindings::GetMaxUsedMemory", IsFreeFunction = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod(Name = "ProfilerBindings::SetMaxUsedMemory", IsFreeFunction = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001B76 RID: 7030
		// (set) Token: 0x06001B77 RID: 7031
		public static extern bool enabled { [NativeConditional("ENABLE_PROFILER")] [NativeMethod(Name = "profiler_is_enabled", IsFreeFunction = true, IsThreadSafe = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod(Name = "ProfilerBindings::SetProfilerEnabled", IsFreeFunction = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001B78 RID: 7032
		// (set) Token: 0x06001B79 RID: 7033
		public static extern bool enableAllocationCallstacks { [NativeMethod(Name = "ProfilerBindings::IsAllocationCallstackCaptureEnabled", IsFreeFunction = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod(Name = "ProfilerBindings::SetAllocationCallstackCaptureEnabled", IsFreeFunction = true)] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06001B7A RID: 7034
		[FreeFunction("ProfilerBindings::profiler_set_area_enabled")]
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetAreaEnabled(ProfilerArea area, bool enabled);

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x0002BFFC File Offset: 0x0002A1FC
		public static int areaCount
		{
			get
			{
				return Enum.GetNames(typeof(ProfilerArea)).Length;
			}
		}

		// Token: 0x06001B7C RID: 7036
		[FreeFunction("ProfilerBindings::profiler_is_area_enabled")]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetAreaEnabled(ProfilerArea area);

		// Token: 0x06001B7D RID: 7037 RVA: 0x0002C020 File Offset: 0x0002A220
		[Conditional("UNITY_EDITOR")]
		public static void AddFramesFromFile(string file)
		{
			bool flag = string.IsNullOrEmpty(file);
			if (flag)
			{
				Debug.LogError("AddFramesFromFile: Invalid or empty path");
			}
			else
			{
				Profiler.AddFramesFromFile_Internal(file, true);
			}
		}

		// Token: 0x06001B7E RID: 7038
		[NativeConditional("ENABLE_PROFILER && UNITY_EDITOR")]
		[NativeHeader("Modules/ProfilerEditor/Public/ProfilerSession.h")]
		[NativeMethod(Name = "LoadFromFile")]
		[StaticAccessor("profiling::GetProfilerSessionPtr()", StaticAccessorType.Arrow)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddFramesFromFile_Internal(string file, bool keepExistingFrames);

		// Token: 0x06001B7F RID: 7039 RVA: 0x0002C050 File Offset: 0x0002A250
		[Conditional("ENABLE_PROFILER")]
		public static void BeginThreadProfiling(string threadGroupName, string threadName)
		{
			bool flag = string.IsNullOrEmpty(threadGroupName);
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid string", "threadGroupName");
			}
			bool flag2 = string.IsNullOrEmpty(threadName);
			if (flag2)
			{
				throw new ArgumentException("Argument should be a valid string", "threadName");
			}
			Profiler.BeginThreadProfilingInternal(threadGroupName, threadName);
		}

		// Token: 0x06001B80 RID: 7040
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "ProfilerBindings::BeginThreadProfiling", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginThreadProfilingInternal(string threadGroupName, string threadName);

		// Token: 0x06001B81 RID: 7041 RVA: 0x00004563 File Offset: 0x00002763
		[NativeConditional("ENABLE_PROFILER")]
		public static void EndThreadProfiling()
		{
		}

		// Token: 0x06001B82 RID: 7042 RVA: 0x0002C09A File Offset: 0x0002A29A
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void BeginSample(string name)
		{
			Profiler.ValidateArguments(name);
			Profiler.BeginSampleImpl(name, null);
		}

		// Token: 0x06001B83 RID: 7043 RVA: 0x0002C0AC File Offset: 0x0002A2AC
		[Conditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void BeginSample(string name, Object targetObject)
		{
			Profiler.ValidateArguments(name);
			Profiler.BeginSampleImpl(name, targetObject);
		}

		// Token: 0x06001B84 RID: 7044 RVA: 0x0002C0C0 File Offset: 0x0002A2C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void ValidateArguments(string name)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid string.", "name");
			}
		}

		// Token: 0x06001B85 RID: 7045
		[NativeMethod(Name = "ProfilerBindings::BeginSample", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BeginSampleImpl(string name, Object targetObject);

		// Token: 0x06001B86 RID: 7046
		[Conditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "ProfilerBindings::EndSample", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndSample();

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001B87 RID: 7047 RVA: 0x0002C0EC File Offset: 0x0002A2EC
		// (set) Token: 0x06001B88 RID: 7048 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("maxNumberOfSamplesPerFrame has been depricated. Use maxUsedMemory instead")]
		public static int maxNumberOfSamplesPerFrame
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001B89 RID: 7049 RVA: 0x0002C100 File Offset: 0x0002A300
		[Obsolete("usedHeapSize has been deprecated since it is limited to 4GB. Please use usedHeapSizeLong instead.")]
		public static uint usedHeapSize
		{
			get
			{
				return (uint)Profiler.usedHeapSizeLong;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001B8A RID: 7050
		public static extern long usedHeapSizeLong { [NativeMethod(Name = "GetUsedHeapSize", IsFreeFunction = true)] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001B8B RID: 7051 RVA: 0x0002C118 File Offset: 0x0002A318
		[Obsolete("GetRuntimeMemorySize has been deprecated since it is limited to 2GB. Please use GetRuntimeMemorySizeLong() instead.")]
		public static int GetRuntimeMemorySize(Object o)
		{
			return (int)Profiler.GetRuntimeMemorySizeLong(o);
		}

		// Token: 0x06001B8C RID: 7052
		[NativeMethod(Name = "ProfilerBindings::GetRuntimeMemorySizeLong", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetRuntimeMemorySizeLong([NotNull("ArgumentNullException")] Object o);

		// Token: 0x06001B8D RID: 7053 RVA: 0x0002C134 File Offset: 0x0002A334
		[Obsolete("GetMonoHeapSize has been deprecated since it is limited to 4GB. Please use GetMonoHeapSizeLong() instead.")]
		public static uint GetMonoHeapSize()
		{
			return (uint)Profiler.GetMonoHeapSizeLong();
		}

		// Token: 0x06001B8E RID: 7054
		[NativeMethod(Name = "scripting_gc_get_heap_size", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetMonoHeapSizeLong();

		// Token: 0x06001B8F RID: 7055 RVA: 0x0002C14C File Offset: 0x0002A34C
		[Obsolete("GetMonoUsedSize has been deprecated since it is limited to 4GB. Please use GetMonoUsedSizeLong() instead.")]
		public static uint GetMonoUsedSize()
		{
			return (uint)Profiler.GetMonoUsedSizeLong();
		}

		// Token: 0x06001B90 RID: 7056
		[NativeMethod(Name = "scripting_gc_get_used_size", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetMonoUsedSizeLong();

		// Token: 0x06001B91 RID: 7057
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SetTempAllocatorRequestedSize(uint size);

		// Token: 0x06001B92 RID: 7058
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetTempAllocatorSize();

		// Token: 0x06001B93 RID: 7059 RVA: 0x0002C164 File Offset: 0x0002A364
		[Obsolete("GetTotalAllocatedMemory has been deprecated since it is limited to 4GB. Please use GetTotalAllocatedMemoryLong() instead.")]
		public static uint GetTotalAllocatedMemory()
		{
			return (uint)Profiler.GetTotalAllocatedMemoryLong();
		}

		// Token: 0x06001B94 RID: 7060
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetTotalAllocatedMemory")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTotalAllocatedMemoryLong();

		// Token: 0x06001B95 RID: 7061 RVA: 0x0002C17C File Offset: 0x0002A37C
		[Obsolete("GetTotalUnusedReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalUnusedReservedMemoryLong() instead.")]
		public static uint GetTotalUnusedReservedMemory()
		{
			return (uint)Profiler.GetTotalUnusedReservedMemoryLong();
		}

		// Token: 0x06001B96 RID: 7062
		[NativeMethod(Name = "GetTotalUnusedReservedMemory")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTotalUnusedReservedMemoryLong();

		// Token: 0x06001B97 RID: 7063 RVA: 0x0002C194 File Offset: 0x0002A394
		[Obsolete("GetTotalReservedMemory has been deprecated since it is limited to 4GB. Please use GetTotalReservedMemoryLong() instead.")]
		public static uint GetTotalReservedMemory()
		{
			return (uint)Profiler.GetTotalReservedMemoryLong();
		}

		// Token: 0x06001B98 RID: 7064
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeMethod(Name = "GetTotalReservedMemory")]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTotalReservedMemoryLong();

		// Token: 0x06001B99 RID: 7065 RVA: 0x0002C1AC File Offset: 0x0002A3AC
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		public static long GetTotalFragmentationInfo(NativeArray<int> stats)
		{
			return Profiler.InternalGetTotalFragmentationInfo((IntPtr)stats.GetUnsafePtr<int>(), stats.Length);
		}

		// Token: 0x06001B9A RID: 7066
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_MEMORY_MANAGER")]
		[NativeMethod(Name = "GetTotalFragmentationInfo")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long InternalGetTotalFragmentationInfo(IntPtr pStats, int count);

		// Token: 0x06001B9B RID: 7067
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "GetRegisteredGFXDriverMemory")]
		[StaticAccessor("GetMemoryManager()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetAllocatedMemoryForGraphicsDriver();

		// Token: 0x06001B9C RID: 7068 RVA: 0x0002C1D8 File Offset: 0x0002A3D8
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitFrameMetaData(Guid id, int tag, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type elementType = data.GetType().GetElementType();
			bool flag2 = !UnsafeUtility.IsBlittable(elementType);
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", elementType));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, data, data.Length, UnsafeUtility.SizeOf(elementType), true);
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0002C240 File Offset: 0x0002A440
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitFrameMetaData<T>(Guid id, int tag, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type typeFromHandle = typeof(T);
			bool flag2 = !UnsafeUtility.IsBlittable(typeof(T));
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", typeFromHandle));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, NoAllocHelpers.ExtractArrayFromList(data), data.Count, UnsafeUtility.SizeOf(typeFromHandle), true);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x0002C2B2 File Offset: 0x0002A4B2
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitFrameMetaData<T>(Guid id, int tag, NativeArray<T> data) where T : struct
		{
			Profiler.Internal_EmitGlobalMetaData_Native((void*)(&id), 16, tag, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), data.Length, UnsafeUtility.SizeOf<T>(), true);
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0002C2DC File Offset: 0x0002A4DC
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitSessionMetaData(Guid id, int tag, Array data)
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type elementType = data.GetType().GetElementType();
			bool flag2 = !UnsafeUtility.IsBlittable(elementType);
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", elementType));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, data, data.Length, UnsafeUtility.SizeOf(elementType), false);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0002C344 File Offset: 0x0002A544
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitSessionMetaData<T>(Guid id, int tag, List<T> data) where T : struct
		{
			bool flag = data == null;
			if (flag)
			{
				throw new ArgumentNullException("data");
			}
			Type typeFromHandle = typeof(T);
			bool flag2 = !UnsafeUtility.IsBlittable(typeof(T));
			if (flag2)
			{
				throw new ArgumentException(string.Format("{0} type must be blittable", typeFromHandle));
			}
			Profiler.Internal_EmitGlobalMetaData_Array((void*)(&id), 16, tag, NoAllocHelpers.ExtractArrayFromList(data), data.Count, UnsafeUtility.SizeOf(typeFromHandle), false);
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0002C3B6 File Offset: 0x0002A5B6
		[Conditional("ENABLE_PROFILER")]
		public unsafe static void EmitSessionMetaData<T>(Guid id, int tag, NativeArray<T> data) where T : struct
		{
			Profiler.Internal_EmitGlobalMetaData_Native((void*)(&id), 16, tag, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), data.Length, UnsafeUtility.SizeOf<T>(), false);
		}

		// Token: 0x06001BA2 RID: 7074
		[NativeMethod(Name = "ProfilerBindings::Internal_EmitGlobalMetaData_Array", IsFreeFunction = true, IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_EmitGlobalMetaData_Array(void* id, int idLen, int tag, Array data, int count, int elementSize, bool frameData);

		// Token: 0x06001BA3 RID: 7075
		[NativeMethod(Name = "ProfilerBindings::Internal_EmitGlobalMetaData_Native", IsFreeFunction = true, IsThreadSafe = true)]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void Internal_EmitGlobalMetaData_Native(void* id, int idLen, int tag, IntPtr data, int count, int elementSize, bool frameData);

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0002C3E0 File Offset: 0x0002A5E0
		[Conditional("ENABLE_PROFILER")]
		public static void SetCategoryEnabled(ProfilerCategory category, bool enabled)
		{
			bool flag = category == ProfilerCategory.Any;
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid category", "category");
			}
			Profiler.Internal_SetCategoryEnabled(category, enabled);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0002C424 File Offset: 0x0002A624
		public static bool IsCategoryEnabled(ProfilerCategory category)
		{
			bool flag = category == ProfilerCategory.Any;
			if (flag)
			{
				throw new ArgumentException("Argument should be a valid category", "category");
			}
			return Profiler.Internal_IsCategoryEnabled(category);
		}

		// Token: 0x06001BA6 RID: 7078
		[NativeHeader("Runtime/Profiler/ProfilerManager.h")]
		[StaticAccessor("profiling::GetProfilerManagerPtr()", StaticAccessorType.Arrow)]
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "GetCategoriesCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetCategoriesCount();

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0002C468 File Offset: 0x0002A668
		[Conditional("ENABLE_PROFILER")]
		public static void GetAllCategories(ProfilerCategory[] categories)
		{
			int num = 0;
			while ((long)num < Math.Min((long)((ulong)Profiler.GetCategoriesCount()), (long)categories.Length))
			{
				categories[num] = new ProfilerCategory((ushort)num);
				num++;
			}
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0002C4A4 File Offset: 0x0002A6A4
		[Conditional("ENABLE_PROFILER")]
		public static void GetAllCategories(NativeArray<ProfilerCategory> categories)
		{
			int num = 0;
			while ((long)num < Math.Min((long)((ulong)Profiler.GetCategoriesCount()), (long)categories.Length))
			{
				categories[num] = new ProfilerCategory((ushort)num);
				num++;
			}
		}

		// Token: 0x06001BA9 RID: 7081
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "profiler_set_category_enable", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetCategoryEnabled(ushort categoryId, bool enabled);

		// Token: 0x06001BAA RID: 7082
		[NativeConditional("ENABLE_PROFILER")]
		[NativeMethod(Name = "profiler_is_category_enabled", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Internal_IsCategoryEnabled(ushort categoryId);

		// Token: 0x0400090C RID: 2316
		internal const uint invalidProfilerArea = 4294967295U;
	}
}
