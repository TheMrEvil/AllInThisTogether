using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001FB RID: 507
	[UsedByNativeCode]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	public class Behaviour : Component
	{
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001675 RID: 5749
		// (set) Token: 0x06001676 RID: 5750
		[NativeProperty]
		[RequiredByNativeCode]
		public extern bool enabled { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001677 RID: 5751
		[NativeProperty]
		public extern bool isActiveAndEnabled { [NativeMethod("IsAddedToManager")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06001678 RID: 5752 RVA: 0x00010727 File Offset: 0x0000E927
		public Behaviour()
		{
		}
	}
}
