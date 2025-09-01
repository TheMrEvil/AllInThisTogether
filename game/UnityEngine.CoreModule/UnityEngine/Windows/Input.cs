using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	// Token: 0x02000289 RID: 649
	[NativeHeader("PlatformDependent/Win/Bindings/InputBindings.h")]
	public static class Input
	{
		// Token: 0x06001C2C RID: 7212
		[ThreadSafe]
		[NativeName("ForwardRawInput")]
		[StaticAccessor("", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ForwardRawInputImpl(uint* rawInputHeaderIndices, uint* rawInputDataIndices, uint indicesCount, byte* rawInputData, uint rawInputDataSize);

		// Token: 0x06001C2D RID: 7213 RVA: 0x0002D399 File Offset: 0x0002B599
		public unsafe static void ForwardRawInput(IntPtr rawInputHeaderIndices, IntPtr rawInputDataIndices, uint indicesCount, IntPtr rawInputData, uint rawInputDataSize)
		{
			Input.ForwardRawInput((uint*)((void*)rawInputHeaderIndices), (uint*)((void*)rawInputDataIndices), indicesCount, (byte*)((void*)rawInputData), rawInputDataSize);
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x0002D3BC File Offset: 0x0002B5BC
		public unsafe static void ForwardRawInput(uint* rawInputHeaderIndices, uint* rawInputDataIndices, uint indicesCount, byte* rawInputData, uint rawInputDataSize)
		{
			bool flag = rawInputHeaderIndices == null;
			if (flag)
			{
				throw new ArgumentNullException("rawInputHeaderIndices");
			}
			bool flag2 = rawInputDataIndices == null;
			if (flag2)
			{
				throw new ArgumentNullException("rawInputDataIndices");
			}
			bool flag3 = rawInputData == null;
			if (flag3)
			{
				throw new ArgumentNullException("rawInputData");
			}
			Input.ForwardRawInputImpl(rawInputHeaderIndices, rawInputDataIndices, indicesCount, rawInputData, rawInputDataSize);
		}
	}
}
