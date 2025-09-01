using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200039C RID: 924
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/AsyncGPUReadbackManaged.h")]
	internal struct AsyncRequestNativeArrayData
	{
		// Token: 0x06001F24 RID: 7972 RVA: 0x00032A94 File Offset: 0x00030C94
		public static AsyncRequestNativeArrayData CreateAndCheckAccess<T>(NativeArray<T> array) where T : struct
		{
			bool flag = array.m_AllocatorLabel == Allocator.Temp || array.m_AllocatorLabel == Allocator.TempJob;
			if (flag)
			{
				throw new ArgumentException("AsyncGPUReadback cannot use Temp memory as input since the result may only become available at an unspecified point in the future.");
			}
			return new AsyncRequestNativeArrayData
			{
				nativeArrayBuffer = array.GetUnsafePtr<T>(),
				lengthInBytes = (long)array.Length * (long)UnsafeUtility.SizeOf<T>()
			};
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00032AF8 File Offset: 0x00030CF8
		public static AsyncRequestNativeArrayData CreateAndCheckAccess<T>(NativeSlice<T> array) where T : struct
		{
			return new AsyncRequestNativeArrayData
			{
				nativeArrayBuffer = array.GetUnsafePtr<T>(),
				lengthInBytes = (long)array.Length * (long)UnsafeUtility.SizeOf<T>()
			};
		}

		// Token: 0x04000A39 RID: 2617
		public unsafe void* nativeArrayBuffer;

		// Token: 0x04000A3A RID: 2618
		public long lengthInBytes;
	}
}
