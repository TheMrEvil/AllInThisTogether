using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001FF RID: 511
	[NativeHeader("Runtime/Mono/Coroutine.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Coroutine : YieldInstruction
	{
		// Token: 0x060016AA RID: 5802 RVA: 0x000243F0 File Offset: 0x000225F0
		private Coroutine()
		{
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000243FC File Offset: 0x000225FC
		~Coroutine()
		{
			Coroutine.ReleaseCoroutine(this.m_Ptr);
		}

		// Token: 0x060016AC RID: 5804
		[FreeFunction("Coroutine::CleanupCoroutineGC", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseCoroutine(IntPtr ptr);

		// Token: 0x040007E0 RID: 2016
		internal IntPtr m_Ptr;
	}
}
