using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200005E RID: 94
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	public static class AnimationSceneHandleUtility
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x00006FF0 File Offset: 0x000051F0
		public static void ReadInts(AnimationStream stream, NativeArray<PropertySceneHandle> handles, NativeArray<int> buffer)
		{
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertySceneHandle, int>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationSceneHandleUtility.ReadSceneIntsInternal(ref stream, handles.GetUnsafePtr<PropertySceneHandle>(), buffer.GetUnsafePtr<int>(), num);
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00007028 File Offset: 0x00005228
		public static void ReadFloats(AnimationStream stream, NativeArray<PropertySceneHandle> handles, NativeArray<float> buffer)
		{
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertySceneHandle, float>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationSceneHandleUtility.ReadSceneFloatsInternal(ref stream, handles.GetUnsafePtr<PropertySceneHandle>(), buffer.GetUnsafePtr<float>(), num);
			}
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00007060 File Offset: 0x00005260
		internal static int ValidateAndGetArrayCount<T0, T1>(ref AnimationStream stream, NativeArray<T0> handles, NativeArray<T1> buffer) where T0 : struct where T1 : struct
		{
			stream.CheckIsValid();
			bool flag = !handles.IsCreated;
			if (flag)
			{
				throw new NullReferenceException("Handle array is invalid.");
			}
			bool flag2 = !buffer.IsCreated;
			if (flag2)
			{
				throw new NullReferenceException("Data buffer is invalid.");
			}
			bool flag3 = buffer.Length < handles.Length;
			if (flag3)
			{
				throw new InvalidOperationException("Data buffer array is smaller than handles array.");
			}
			return handles.Length;
		}

		// Token: 0x060004DB RID: 1243
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadSceneIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadSceneIntsInternal(ref AnimationStream stream, void* propertySceneHandles, void* intBuffer, int count);

		// Token: 0x060004DC RID: 1244
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadSceneFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadSceneFloatsInternal(ref AnimationStream stream, void* propertySceneHandles, void* floatBuffer, int count);
	}
}
