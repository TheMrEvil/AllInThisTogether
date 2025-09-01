using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.Rendering.HybridV2
{
	// Token: 0x02000071 RID: 113
	public class HybridV2ShaderReflection
	{
		// Token: 0x060001B5 RID: 437
		[FreeFunction("ShaderScripting::GetDOTSInstancingCbuffersPointer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDOTSInstancingCbuffersPointer([NotNull("ArgumentNullException")] Shader shader, ref int cbufferCount);

		// Token: 0x060001B6 RID: 438
		[FreeFunction("ShaderScripting::GetDOTSInstancingPropertiesPointer")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDOTSInstancingPropertiesPointer([NotNull("ArgumentNullException")] Shader shader, ref int propertyCount);

		// Token: 0x060001B7 RID: 439
		[FreeFunction("Shader::GetDOTSReflectionVersionNumber")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetDOTSReflectionVersionNumber();

		// Token: 0x060001B8 RID: 440 RVA: 0x00003950 File Offset: 0x00001B50
		public unsafe static NativeArray<DOTSInstancingCbuffer> GetDOTSInstancingCbuffers(Shader shader)
		{
			bool flag = shader == null;
			NativeArray<DOTSInstancingCbuffer> result;
			if (flag)
			{
				result = default(NativeArray<DOTSInstancingCbuffer>);
			}
			else
			{
				int length = 0;
				IntPtr dotsinstancingCbuffersPointer = HybridV2ShaderReflection.GetDOTSInstancingCbuffersPointer(shader, ref length);
				bool flag2 = dotsinstancingCbuffersPointer == IntPtr.Zero;
				if (flag2)
				{
					result = default(NativeArray<DOTSInstancingCbuffer>);
				}
				else
				{
					NativeArray<DOTSInstancingCbuffer> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<DOTSInstancingCbuffer>((void*)dotsinstancingCbuffersPointer, length, Allocator.Temp);
					result = nativeArray;
				}
			}
			return result;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000039B8 File Offset: 0x00001BB8
		public unsafe static NativeArray<DOTSInstancingProperty> GetDOTSInstancingProperties(Shader shader)
		{
			bool flag = shader == null;
			NativeArray<DOTSInstancingProperty> result;
			if (flag)
			{
				result = default(NativeArray<DOTSInstancingProperty>);
			}
			else
			{
				int length = 0;
				IntPtr dotsinstancingPropertiesPointer = HybridV2ShaderReflection.GetDOTSInstancingPropertiesPointer(shader, ref length);
				bool flag2 = dotsinstancingPropertiesPointer == IntPtr.Zero;
				if (flag2)
				{
					result = default(NativeArray<DOTSInstancingProperty>);
				}
				else
				{
					NativeArray<DOTSInstancingProperty> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<DOTSInstancingProperty>((void*)dotsinstancingPropertiesPointer, length, Allocator.Temp);
					result = nativeArray;
				}
			}
			return result;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00002072 File Offset: 0x00000272
		public HybridV2ShaderReflection()
		{
		}
	}
}
