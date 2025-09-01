using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Animations
{
	// Token: 0x0200005F RID: 95
	[MovedFrom("UnityEngine.Experimental.Animations")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimationStreamHandles.bindings.h")]
	public static class AnimationStreamHandleUtility
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x000070D4 File Offset: 0x000052D4
		public static void WriteInts(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<int> buffer, bool useMask)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, int>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.WriteStreamIntsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<int>(), num, useMask);
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00007114 File Offset: 0x00005314
		public static void WriteFloats(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<float> buffer, bool useMask)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, float>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.WriteStreamFloatsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<float>(), num, useMask);
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00007154 File Offset: 0x00005354
		public static void ReadInts(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<int> buffer)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, int>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.ReadStreamIntsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<int>(), num);
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00007194 File Offset: 0x00005394
		public static void ReadFloats(AnimationStream stream, NativeArray<PropertyStreamHandle> handles, NativeArray<float> buffer)
		{
			stream.CheckIsValid();
			int num = AnimationSceneHandleUtility.ValidateAndGetArrayCount<PropertyStreamHandle, float>(ref stream, handles, buffer);
			bool flag = num == 0;
			if (!flag)
			{
				AnimationStreamHandleUtility.ReadStreamFloatsInternal(ref stream, handles.GetUnsafePtr<PropertyStreamHandle>(), buffer.GetUnsafePtr<float>(), num);
			}
		}

		// Token: 0x060004E1 RID: 1249
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadStreamIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadStreamIntsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* intBuffer, int count);

		// Token: 0x060004E2 RID: 1250
		[NativeMethod(Name = "AnimationHandleUtilityBindings::ReadStreamFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ReadStreamFloatsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* floatBuffer, int count);

		// Token: 0x060004E3 RID: 1251
		[NativeMethod(Name = "AnimationHandleUtilityBindings::WriteStreamIntsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void WriteStreamIntsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* intBuffer, int count, bool useMask);

		// Token: 0x060004E4 RID: 1252
		[NativeMethod(Name = "AnimationHandleUtilityBindings::WriteStreamFloatsInternal", IsFreeFunction = true, HasExplicitThis = false, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void WriteStreamFloatsInternal(ref AnimationStream stream, void* propertyStreamHandles, void* floatBuffer, int count, bool useMask);
	}
}
