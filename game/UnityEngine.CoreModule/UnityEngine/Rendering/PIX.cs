using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x020003A0 RID: 928
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public class PIX
	{
		// Token: 0x06001F55 RID: 8021
		[FreeFunction("PIX::BeginGPUCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BeginGPUCapture();

		// Token: 0x06001F56 RID: 8022
		[FreeFunction("PIX::EndGPUCapture")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EndGPUCapture();

		// Token: 0x06001F57 RID: 8023
		[FreeFunction("PIX::IsAttached")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsAttached();

		// Token: 0x06001F58 RID: 8024 RVA: 0x00002072 File Offset: 0x00000272
		public PIX()
		{
		}
	}
}
